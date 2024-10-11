from flask import Flask, render_template, request
from apscheduler.schedulers.background import BackgroundScheduler
import requests
from bs4 import BeautifulSoup
import pyodbc
from sqlalchemy import create_engine, Column, Integer, String, MetaData
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
import spacy

app = Flask(__name__)

event_cache = []
news_cache = []
lugares_interes = ['Chiriquí', 'Veraguas']

# Definir la conexión a SQL Server
DATABASE_CONFIG = {
    'driver': 'ODBC Driver 17 for SQL Server',
    'server': 'DANIELTROETSCH\SQLEXPRESS',
    'database': 'SitioWeb',
    'trusted_connection': 'yes'
} 

# Crear la cadena de conexión para SQLAlchemy
connection_string = (
    f"mssql+pyodbc:///?odbc_connect="
    f"DRIVER={DATABASE_CONFIG['driver']};"
    f"SERVER={DATABASE_CONFIG['server']};"
    f"DATABASE={DATABASE_CONFIG['database']};"
    f"Trusted_Connection={ DATABASE_CONFIG['trusted_connection']}"
)

# Crear el motor de SQLAlchemy
engine = create_engine(connection_string)
Session = sessionmaker(bind=engine)
session = Session()
Base = declarative_base()

# Definir la estructura de la tabla Noticias
class Noticias(Base):
    __tablename__ = 'Noticias'
    id = Column(Integer, primary_key=True, autoincrement=True)
    titulo = Column(String, nullable=False)
    imagen = Column(String)
    referencia = Column(String)
    visitas = Column(Integer, default=0)

# Crear la tabla en la base de datos si no existe
Base.metadata.create_all(engine)

# Cargar el modelo de lenguaje en español de spaCy
nlp = spacy.load('es_core_news_sm')

# Definir palabras clave relacionadas con eventos
palabras_clave = ['evento', 'concierto', 'exposición', 'feria', 'congreso', 'taller', 'charla', 'conferencia', 'fiesta', 'musical']

def contains_keywords(text, keywords):
    doc = nlp(text.lower())
    return any(token.lemma_ in keywords for token in doc)

def scrape_news():
    url = 'https://www.tvn-2.com/nacionales/'
    page = requests.get(url)
    soup = BeautifulSoup(page.text, 'html.parser')

    article_items = soup.find_all('article')
    news = []

    for article in article_items:
        title_tag = article.find('a', class_='title')
        titulo = title_tag.get('title') if title_tag else ''

        picture_tag = article.find('picture')
        imagen = ''
        if picture_tag:
            source_tags = picture_tag.find_all('source')
            for source in source_tags:
                if 'image/webp' in source.get('type', ''):
                    imagen = source.get('data-srcset', '')
                    break
            if not imagen:
                for source in source_tags:
                    if 'image/jpeg' in source.get('type', ''):
                        imagen = source.get('data-srcset', '')
                        break
            if not imagen:
                img_tag = picture_tag.find('img')
                imagen = img_tag.get('data-srcset', img_tag.get('src')) if img_tag else ''

        referencia = title_tag.get('href') if title_tag else ''
        if referencia and not referencia.startswith('http'):
            referencia = requests.compat.urljoin(url, referencia)
        
        if imagen and not imagen.startswith('http'):
            imagen = requests.compat.urljoin(url, imagen)

        if any(lugar in titulo for lugar in lugares_interes):
            news.append({
                'titulo': titulo,
                'imagen': imagen,
                'referencia': referencia
            })

    # Guardar las noticias en la base de datos
    for new in news:
        # Verificar si la noticia ya existe en la base de datos
        existing_noticia = session.query(Noticias).filter_by(
            titulo=new['titulo'],
            imagen=new['imagen'],
            referencia=new['referencia']
        ).first()
        if existing_noticia:
            continue  # Si ya existe, pasar a la siguiente noticia
        
        noticia = Noticias(
            titulo=new['titulo'],
            imagen=new['imagen'],
            referencia=new['referencia']
        )
        session.add(noticia)
    session.commit()

    print(f"Scraping de noticias completado. Noticias encontradas: {len(news)}")
    global news_cache
    news_cache = news

def scrape_events():
    url = 'https://nextpanama.com.pa/'
    page = requests.get(url)
    soup = BeautifulSoup(page.content, 'html.parser')

    event_items = soup.find_all('a', class_='td-image-wrap')

    events = []

    for event in event_items:
        titulo = event.get('title', '')  # Obtener el título del evento desde el atributo title

        # Verificar si el título contiene alguna palabra clave
        if not contains_keywords(titulo, palabras_clave):
            continue

        imagen_style = event.find('span', class_='entry-thumb').get('style', '')
        imagen_url = extract_image_url(imagen_style)  # Extraer la URL de la imagen del estilo CSS

        referencia = event.get('href', '')  # Obtener la referencia (URL del evento)

        events.append({
            'titulo': titulo,
            'imagen': imagen_url,
            'referencia': referencia
        })

    # Guardar los eventos en la base de datos
    for event in events:
        # Verificar si la noticia ya existe en la base de datos
        existing_noticia = session.query(Noticias).filter_by(
            titulo=event['titulo'],
            imagen=event['imagen'],
            referencia=event['referencia']
        ).first()
        if existing_noticia:
            continue  # Si ya existe, pasar a la siguiente noticia
        
        noticia = Noticias(
            titulo=event['titulo'],
            imagen=event['imagen'],
            referencia=event['referencia']
        )
        session.add(noticia)
    session.commit()

    print(f"Scraping de eventos completado. Eventos encontrados: {len(events)}")
    global event_cache
    event_cache = events

def extract_image_url(style):
    # Extraer la URL de la imagen del estilo CSS
    start_index = style.index("url('") + len("url('")
    end_index = style.index("')")
    return style[start_index:end_index]

@app.route('/')
def index():
    print("Ruta '/' accesada.")
    return render_template('index.html', noticias=news_cache, eventos=event_cache)

@app.route('/noticia/<int:noticia_id>')
def view_noticia(noticia_id):
    noticia = session.query(Noticias).get(noticia_id)
    if noticia:
        noticia.visitas += 1  # Incrementar el contador de visitas
        session.commit()
    return render_template('noticia.html', noticia=noticia)

if __name__ == '__main__':
    scheduler = BackgroundScheduler()
    scheduler.add_job(scrape_news, 'interval', minutes=30)  # Ajusta el intervalo según tus necesidades
    scheduler.add_job(scrape_events, 'interval', minutes=30)  # Ajusta el intervalo según tus necesidades
    scheduler.start()
    
    # Realizar una primera ejecución del scraping al inicio
    scrape_news()
    scrape_events()

    try:
        app.run(debug=True)
    except (KeyboardInterrupt, SystemExit):
        scheduler.shutdown()
