Imports SISTICKET.UsusaroModel

Public Class LoginController
    Private ReadOnly usuarioModel As New UsusaroModel()

    ' Método para manejar el inicio de sesión
    Public Function IniciarSesion(username As String, password As String) As Integer?
        If String.IsNullOrEmpty(username) Or String.IsNullOrEmpty(password) Then
            Throw New ArgumentException("Usuario o contraseña no pueden estar vacíos.")
        End If

        Return usuarioModel.ValidarUsuario(username, password)
    End Function

End Class
