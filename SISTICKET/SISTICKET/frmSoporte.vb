Public Class frmSoporte
    Private UsuarioID As Integer
    Private ticketsController As New ticketsController()
    Private user As New UserDAO()
    Private Sub frmSoporte_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CargarTickets()
        Dim tickets = ticketsController.GetAllTickets()
        dgvTickets.DataSource = tickets
    End Sub


    Private Sub CargarComboEstado()
        cmbEstado.Items.Add(New With {.Value = 1, .Text = "Pendiente"})
        cmbEstado.Items.Add(New With {.Value = 2, .Text = "En Proceso"})
        cmbEstado.Items.Add(New With {.Value = 3, .Text = "Resuelto"})
        cmbEstado.DisplayMember = "Text"
        cmbEstado.ValueMember = "Value"
    End Sub



End Class