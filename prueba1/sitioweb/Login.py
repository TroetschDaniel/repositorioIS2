import pyodbc
import bcrypt

def login_user(username, password):
    try:
        # Conectar a la base de datos SQL Server usando autenticación de Windows
        conn = pyodbc.connect(
            'DRIVER={ODBC Driver 17 for SQL Server};'
            'SERVER=DANIELTROETSCH\SQLEXPRESS;'
            'DATABASE=SitioWeb;'
            'Trusted_Connection=yes;'
        )
        cursor = conn.cursor()
        
        # Buscar el usuario por nombre de usuario
        cursor.execute('SELECT password FROM Usuarios WHERE username = ?', (username,))
        result = cursor.fetchone()
        
        if result:
            # Comparar la contraseña ingresada con el hash almacenado
            stored_password = result[0]
            if bcrypt.checkpw(password.encode('utf-8'), stored_password.encode('utf-8')):
                print('Inicio de sesión exitoso.')
            else:
                print('Contraseña incorrecta.')
        else:
            print('Usuario no encontrado.')
        
    except pyodbc.Error as ex:
        sqlstate = ex.args[1]
        print(f"Error al conectar a la base de datos: {sqlstate}")
        
    finally:
        try:
            conn.close()
        except:
            pass

# Ejemplo de uso
username_input = input('Ingrese su nombre de usuario: ')
password_input = input('Ingrese su contraseña: ')

login_user(username_input, password_input)
