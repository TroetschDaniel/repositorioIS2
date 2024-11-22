Imports System.DirectoryServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Microsoft.Data.SqlClient
Public Class TicketModel
    Private connectionString As String = "Data Source=DANIELTROETSCH\SQLEXPRESS;Initial Catalog=SISTICKET;Integrated Security=True;TrustServerCertificate=True"
    'metodo para obtener la información de los tickets

    Public Function GetAllTickets() As List(Of TicketDAO)
        Dim tickets As New List(Of TicketDAO)()

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM Tickets"
                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Crea un nuevo objeto TicketDAO para cada fila
                            Dim ticket As New TicketDAO With {
                                .ticketID = Convert.ToInt32(reader("TicketID")),
                                .usuarioID = Convert.ToInt32(reader("UsuarioID")),
                                .tipoSoporteID = Convert.ToInt32(reader("TipoSoporteID")),
                                .estadoID = Convert.ToInt32(reader("EstadoID")),
                                .descripcion = reader("Descripcion").ToString(),
                                .fechaCreacion = Convert.ToDateTime(reader("FechaCreacion")),
                                .fechaCierre = If(IsDBNull(reader("FechaCierre")), Nothing, Convert.ToDateTime(reader("FechaCierre")))
                            }

                            ' Agrega el ticket a la lista
                            tickets.Add(ticket)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception($"Error al obtener los tickets: {ex.Message}")
        End Try

        Return tickets
    End Function

    ' Crear un ticket
    Public Sub CreateTicket(ticket As TicketDAO)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "INSERT INTO Tickets (UsuarioID, TipoSoporteID, EstadoID, Descripcion) VALUES (@UsuarioID, @TipoSoporteID, @EstadoID, @Descripcion)"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UsuarioID", ticket.usuarioID)
                    command.Parameters.AddWithValue("@TipoSoporteID", ticket.tipoSoporteID)
                    command.Parameters.AddWithValue("@EstadoID", ticket.estadoID)
                    command.Parameters.AddWithValue("@Descripcion", ticket.descripcion)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al crear el ticket", ex)
        End Try
    End Sub

    Public Sub CreateUserTicket(ticket As TicketDAO)
        Try
            Dim query As String = "INSERT INTO Tickets (UsuarioID, TipoSoporteID, EstadoID, Descripcion, FechaCreacion) 
                               VALUES (@UsuarioID, @TipoSoporteID, @EstadoID, @Descripcion, @FechaCreacion)"
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UsuarioID", ticket.usuarioID)
                    command.Parameters.AddWithValue("@TipoSoporteID", ticket.tipoSoporteID)
                    command.Parameters.AddWithValue("@EstadoID", 1) ' Estado inicial: Pendiente
                    command.Parameters.AddWithValue("@Descripcion", ticket.descripcion)
                    command.Parameters.AddWithValue("@FechaCreacion", Date.Now)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al crear el ticket: " & ex.Message)
        End Try
    End Sub

    ' Actualizar un ticket
    Public Sub UpdateTicket(ticket As TicketDAO)
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                ' Corregimos la declaración de la consulta SQL
                Dim query As String = "UPDATE Tickets " &
                                  "SET UsuarioID = @UsuarioID, " &
                                  "TipoSoporteID = @TipoSoporteID, " &
                                  "EstadoID = @EstadoID, " &
                                  "Descripcion = @Descripcion, " &
                                  "FechaCierre = @FechaCierre " &
                                  "WHERE TicketID = @TicketID"

                Using command As New SqlCommand(query, connection)
                    ' Agregar los parámetros necesarios
                    command.Parameters.AddWithValue("@UsuarioID", ticket.usuarioID)
                    command.Parameters.AddWithValue("@TipoSoporteID", ticket.tipoSoporteID)
                    command.Parameters.AddWithValue("@EstadoID", ticket.estadoID)
                    command.Parameters.AddWithValue("@Descripcion", ticket.descripcion)
                    ' Si la fecha de cierre es Nothing, insertar NULL
                    If ticket.fechaCierre.HasValue Then
                        command.Parameters.AddWithValue("@FechaCierre", ticket.fechaCierre.Value)
                    Else
                        command.Parameters.AddWithValue("@FechaCierre", DBNull.Value)
                    End If

                    ' Agregar el parámetro de ticketID correctamente
                    command.Parameters.AddWithValue("@TicketID", ticket.ticketID)  ' Aquí falta el parámetro ticketID

                    ' Ejecutar la consulta
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al actualizar el ticket: " & ex.Message)
        End Try
    End Sub

    ' Eliminar un ticket
    Public Sub DeleteTicket(ticketID As Integer)
        Try
            ' Consulta SQL para eliminar el ticket
            Dim query As String = "DELETE FROM Tickets WHERE TicketID = @TicketID"

            ' Ejecutar la consulta con el ticketID
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@TicketID", ticketID)
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar el ticket: " & ex.Message)
        End Try
    End Sub


End Class
