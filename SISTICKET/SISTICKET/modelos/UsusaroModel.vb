Imports System.DirectoryServices
Imports Microsoft.Data.SqlClient

Public Class UsusaroModel
    Private connectionString As String = "Data Source=DANIELTROETSCH\SQLEXPRESS;Initial Catalog=SISTICKET;Integrated Security=True;TrustServerCertificate=True"


    ' Método para validar el usuario y obtener el rol
    Public Function ValidarUsuario(username As String, password As String) As Integer?


        Try
            'MessageBox.Show("LLEGO LA WEA " + connectionString.ToString, "Advertencia")
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT RolID FROM Usuarios WHERE Nombre = @Usuario AND Contraseña = @Contrasena"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Usuario", username)
                    command.Parameters.AddWithValue("@Contrasena", password)

                    Dim role As Object = command.ExecuteScalar()
                    If role IsNot Nothing Then
                        Return Convert.ToInt32(role)
                    End If
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception($"Error al conectar con la base de datos: {ex.Message}")
        End Try

        Return Nothing
    End Function

    Public Function getUserData(id As Integer) As UserDAO
        Dim result As New UserDAO

        Try
            'MessageBox.Show("LLEGO LA WEA " + connectionString.ToString, "Advertencia")
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM Usuarios WHERE UsuarioID = @id"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@id", id)

                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then

                            result.UsuarioID = Convert.ToInt32(reader("UsuarioID"))
                            result.Nombre = reader("Nombre").ToString()
                            result.RolID = Convert.ToInt32(reader("RolID"))
                            result.Correo = reader("Correo").ToString()

                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception($"Error al conectar con la base de datos: {ex.Message}")
        End Try

        Return result
    End Function

End Class
