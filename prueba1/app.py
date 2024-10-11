from flask import Flask, request, render_template, redirect, url_for, session, flash
from apscheduler.schedulers.background import BackgroundScheduler
import requests
from bs4 import BeautifulSoup
import os
import pyodbc
import bcrypt
import spacy

app = Flask(__name__)
app.secret_key = 'your_secret_key'  # Necesario para manejar sesiones

# Configuración de la conexión a la base de datos
conn = pyodbc.connect(
    'DRIVER={ODBC Driver 17 for SQL Server};'
    'SERVER=MATASNIEVES01\\SQLEXPRESS01;'
    'DATABASE=SitioWeb;'
    'Trusted_Connection=yes;'
)

# Cargar modelo de spaCy para español
nlp = spacy.load("es_core_news_sm")

# Palabras clave relacionadas con eventos
palabras_clave = ['evento', 'concierto', 'exposición', 'feria', 'congreso', 'taller', 'charla', 'conferencia', 'fiesta', 'musical']

# Función para registrar usuarios
def create_user(username, password, correoElect):
    try:
        # Hashear la contraseña del usuario
        hashed_password = bcrypt.hashpw(password.encode('utf-8'), bcrypt.gensalt())

        cursor = conn.cursor()

        # Insertar el nuevo usuario en la base de datos
        cursor.execute('INSERT INTO Usuarios (username, password, correoElect) VALUES (?, ?, ?)', (username, hashed_password.decode('utf-8'), correoElect))
        conn.commit()
        print('Usuario registrado exitosamente.')

    except pyodbc.Error as ex:
        sqlstate = ex.args[1]
        print(f"Error al conectar a la base de datos: {sqlstate}")

# Función para autenticar usuarios
def authenticate_user(email, password):
    try:
        cursor = conn.cursor()
        cursor.execute('SELECT password FROM Usuarios WHERE correoElect = ?', (email,))
        row = cursor.fetchone()
        if row and bcrypt.checkpw(password.encode('utf-8'), row[0].encode('utf-8')):
            return True
        return False
    except pyodbc.Error as ex:
        sqlstate = ex.args[1]
        print(f"Error al conectar a la base de datos: {sqlstate}")
        return False

# Función para hacer scraping de noticias
news_cache = []
lugares_interes = ['Chiriquí', 'Veraguas']

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

    print(f"Scraping completado. Noticias encontradas: {len(news)}")
    global news_cache
    news_cache = news

# Función para hacer scraping de eventos
event_cache = []

def scrape_events():
    url = 'https://nextpanama.com.pa/'
    page = requests.get(url)
    soup = BeautifulSoup(page.content, 'html.parser')

    event_items = soup.find_all('a', class_='td-image-wrap')

    events = []

    for event in event_items:
        titulo = event.get('title', '')  # Obtener el título del evento desde el atributo title

        # Verificar si el título contiene palabras clave usando spaCy
        doc = nlp(titulo.lower())
        keywords_found = any(keyword in doc.text for keyword in palabras_clave)

        if keywords_found:
            imagen_style = event.find('span', class_='entry-thumb').get('style', '')
            imagen_url = extract_image_url(imagen_style)  # Extraer la URL de la imagen del estilo CSS

            referencia = event.get('href', '')  # Obtener la referencia (URL del evento)

            events.append({
                'titulo': titulo,
                'imagen': imagen_url,
                'referencia': referencia
            })

    print(f"Scraping de eventos completado. Eventos encontrados: {len(events)}")
    global event_cache
    event_cache = events

def extract_image_url(style):
    # Extraer la URL de la imagen del estilo CSS
    start_index = style.index("url('") + len("url('")
    end_index = style.index("')")
    return style[start_index:end_index]

@app.route('/')
def inicio():
    if 'user' in session:
        return render_template('snapnews.html', user=session['user'])
    return render_template('snapnews.html')

@app.route('/noticias')
def noticias():
    return render_template('noticias_destacadas.html', noticias=news_cache)

@app.route('/eventos')
def eventos():
    return render_template('eventos_destacados.html', eventos=event_cache)

@app.route('/login', methods=['GET', 'POST'])
def login():
    if request.method == 'POST':
        email = request.form['email']
        password = request.form['password']
        if authenticate_user(email, password):
            session['user'] = email
            print(f"User {email} logged in successfully")
            return redirect(url_for('inicio'))
        else:
            print("Invalid login attempt")
            flash('Invalid credentials. Please try again.')
    return render_template('login.html')

@app.route('/register', methods=['GET', 'POST'])
def register():
    if request.method == 'POST':
        username = request.form['username']
        password = request.form['password']
        correoElect = request.form['correoElect']

        # Crear el usuario en la base de datos
        create_user(username, password, correoElect)
        return redirect(url_for('inicio'))

    return render_template('register.html')

@app.route('/logout')
def logout():
    session.pop('user', None)
    return redirect(url_for('inicio'))

if __name__ == '__main__':
    # Iniciar el scheduler para el scraping de noticias y eventos
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