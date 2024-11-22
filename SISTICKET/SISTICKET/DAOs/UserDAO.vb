Public Class UserDAO

    ' Atributos o propiedades del usuario
    Public Property UsuarioID As Integer ' ID único del usuario
    Public Property Nombre As String ' Nombre o username
    Public Property Contrasena As String ' Contraseña
    Public Property RolID As Integer ' ID del rol

    Public Property Correo As String ' Correo electrónico (opcional)


    ' Constructor para inicializar la clase
    Public Sub New()
        ' Constructor vacío por si se requiere inicialización sin datos
    End Sub

    Public Sub New(usuarioID As Integer, nombre As String, contrasena As String, rolID As Integer, correo As String)
        Me.UsuarioID = usuarioID
        Me.Nombre = nombre
        Me.Contrasena = contrasena
        Me.RolID = rolID
        Me.Correo = correo

    End Sub

    ' Método para representar al usuario como texto
    Public Overrides Function ToString() As String
        Return $"{Nombre})"
    End Function


End Class
