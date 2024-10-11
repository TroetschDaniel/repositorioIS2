import pyodbc
import bcrypt

def create_user(username, password, correoElect):
    # Hashear la contraseña del usuario
    hashed_password = bcrypt.hashpw(password.encode('utf-8'), bcrypt.gensalt())

    try:
        # Conectar a la base de datos SQL Server usando autenticación de Windows
        conn = pyodbc.connect(
            'DRIVER={ODBC Driver 17 for SQL Server};'
            'SERVER=DANIELTROETSCH\SQLEXPRESS;'
            'DATABASE=SitioWeb;'
            'Trusted_Connection=yes;'
        )
        cursor = conn.cursor()
        
        # Insertar el nuevo usuario en la base de datos
        cursor.execute('INSERT INTO Usuarios (username, password, correoElect) VALUES (?, ?, ?)', (username, hashed_password.decode('utf-8'), correoElect))
        conn.commit()
        print('Usuario registrado exitosamente.')
        
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
correoElect_input = input('Ingrese su correo electrónico: ')

create_user(username_input, password_input, correoElect_input)
