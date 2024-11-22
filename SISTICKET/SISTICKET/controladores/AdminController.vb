Public Class AdminController
    Dim modelo_Usuario As New UsusaroModel()
    Public Function getDataUser(usuario_id As Integer) As UserDAO
        Dim result As UserDAO
        result = modelo_Usuario.getUserData(usuario_id)
        Return result
    End Function

End Class
