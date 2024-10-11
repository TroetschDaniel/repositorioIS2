import requests
from bs4 import BeautifulSoup

# Realizar la solicitud HTTP
page = requests.get('https://www.tvn-2.com/buscador?text=Chiriqu%C3%AD')
soup = BeautifulSoup(page.text, 'html.parser')

# Encontrar todos los artículos
article_items = soup.find_all('article')
print(f'Found {len(article_items)} article items')

for article in article_items:
    # Imprimir el contenido HTML del artículo encontrado para inspección
    # print(article.prettify())
    
    # Buscar el título
    title_tag = article.find('a', class_='title')
    titulo = title_tag.get('title') if title_tag else ''

    # Buscar la imagen
    picture_tag = article.find('picture')
    imagen = ''  # Valor por defecto si no se encuentra ninguna imagen

    if picture_tag:
        source_tags = picture_tag.find_all('source')
        for source in source_tags:
            if 'image/webp' in source.get('type', ''):
                imagen = source.get('data-srcset')
                break
        if imagen == '':
            for source in source_tags:
                if 'image/jpeg' in source.get('type', ''):
                    imagen = source.get('data-srcset')
                    break
        if imagen == '':  # Si no se encuentran sources, buscar en la etiqueta img
            img_tag = picture_tag.find('img')
            imagen = img_tag.get('data-srcset', img_tag.get('src')) if img_tag else ''

    # Buscar la referencia
    referencia = title_tag.get('href') if title_tag else ''

    # Imprimir los resultados
    print(f'Titulo: {titulo}\nImagen: {imagen}\nReferencia: {referencia}\n')
