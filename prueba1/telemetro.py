from flask import Flask, render_template
from apscheduler.schedulers.background import BackgroundScheduler
import requests
from bs4 import BeautifulSoup

app = Flask(__name__)

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

@app.route('/')
def index():
    print("Ruta '/' accesada.")
    return render_template('index.html', noticias=news_cache)

if __name__ == '__main__':
    scheduler = BackgroundScheduler()
    scheduler.add_job(scrape_news, 'interval', minutes=30)  # Ajusta el intervalo según tus necesidades
    scheduler.start()
    
    # Realizar una primera ejecución del scraping al inicio
    scrape_news()

    try:
        app.run(debug=True)
    except (KeyboardInterrupt, SystemExit):
        scheduler.shutdown()
