Public Class ticketsController
    Private modelo_Ticket As New TicketModel

    ' Método para obtener todos los tickets (renombrado para consistencia)
    Public Function GetAllTickets() As List(Of TicketDAO)
        Try
            Return modelo_Ticket.GetAllTickets()
        Catch ex As Exception
            Throw New Exception("Error al obtener los tickets: " & ex.Message)
        End Try
    End Function

    ' Método para crear un ticket
    Public Sub CreateTicket(ticket As TicketDAO)
        Try
            ValidateTicket(ticket)
            modelo_Ticket.CreateTicket(ticket)
        Catch ex As Exception
            Throw New Exception("Error al crear el ticket: " & ex.Message)
        End Try
    End Sub

    ' Método para actualizar un ticket
    Public Sub UpdateTicket(ticket As TicketDAO)
        Try
            ValidateTicket(ticket)
            modelo_Ticket.UpdateTicket(ticket)
        Catch ex As Exception
            Throw New Exception("Error al actualizar el ticket: " & ex.Message)
        End Try
    End Sub

    ' Método para eliminar un ticket
    Public Sub DeleteTicket(ticketID As Integer)
        Try
            If ticketID <= 0 Then
                Throw New ArgumentException("ID de ticket no válido")
            End If
            modelo_Ticket.DeleteTicket(ticketID)
        Catch ex As Exception
            Throw New Exception("Error al eliminar el ticket: " & ex.Message)
        End Try
    End Sub

    ' Método privado para validaciones
    Private Sub ValidateTicket(ticket As TicketDAO)
        If String.IsNullOrWhiteSpace(ticket.descripcion) Then
            Throw New ArgumentException("La descripción del ticket no puede estar vacía.")
        End If

        If ticket.usuarioID <= 0 Then
            Throw New ArgumentException("El ID de usuario no es válido.")
        End If

        If ticket.tipoSoporteID <= 0 Then
            Throw New ArgumentException("El tipo de soporte no es válido.")
        End If

        If ticket.estadoID <= 0 Then
            Throw New ArgumentException("El estado del ticket no es válido.")
        End If
    End Sub




End Class