Public Class TicketDAO
    Public Property ticketID As Integer
    Public Property usuarioID As Integer
    Public Property tipoSoporteID As Integer
    Public Property estadoID As Integer
    Public Property descripcion As String
    Public Property fechaCreacion As DateTime
    Public Property fechaCierre As DateTime?

    Public Sub New()
        ' Constructor vacío para flexibilidad
    End Sub

    Public Sub New(ticketID As Integer, usuarioID As Integer, tipoSoporteID As Integer, estadoID As Integer, descripcion As String, fechaCreacion As DateTime, fechaCierre As DateTime?)
        Me.ticketID = ticketID
        Me.usuarioID = usuarioID
        Me.tipoSoporteID = tipoSoporteID
        Me.estadoID = estadoID
        Me.descripcion = descripcion
        Me.fechaCreacion = fechaCreacion
        Me.fechaCierre = fechaCierre
    End Sub
End Class
