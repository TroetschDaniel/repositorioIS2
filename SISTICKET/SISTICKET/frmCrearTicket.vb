Public Class frmCrearTicket
    Private UsuarioID As Integer
    Private ticketController As New ticketsController()

    Public Sub New(usuario_id As Integer)
        InitializeComponent()
        UsuarioID = usuario_id ' Asigna el ID del usuario autenticado
    End Sub

    Private Sub frmCrearTicket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarTiposDeSoporte()
    End Sub

    ' Método para cargar los tipos de soporte en el ComboBox
    Private Sub CargarTiposDeSoporte()
        Try
            'Dim tiposDeSoporte As List(Of KeyValuePair(Of Integer, String)) = ticketsController.GetTiposDeSoporte()

            ' Configura el ComboBox
            Dim tiposDeSoporte As New Dictionary(Of Integer, String) From {
        {1, "Soporte técnico"},
        {2, "Mantenimiento preventivo"},
        {3, "Consulta general"}
    }

            cmbTipoSoporte.DataSource = New BindingSource(tiposDeSoporte, Nothing)
            cmbTipoSoporte.DisplayMember = "Value" ' Muestra el nombre al usuario
            cmbTipoSoporte.ValueMember = "Key"    ' Usa el ID internamente
            cmbTipoSoporte.SelectedIndex = 0      ' Selecciona el primer elemento por defecto
        Catch ex As Exception
            MessageBox.Show($"Error al cargar los tipos de soporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Evento al hacer clic en el botón "Enviar Ticket"
    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Try
            ' Validación básica
            If cmbTipoSoporte.SelectedValue Is Nothing OrElse String.IsNullOrWhiteSpace(txtDescripcion.Text) Then
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Crear un nuevo ticket
            Dim tipoSoporteID As Integer = Convert.ToInt32(cmbTipoSoporte.SelectedValue)

            Dim nuevoTicket As New TicketDAO With {
                .usuarioID = UsuarioID,
                .tipoSoporteID = CInt(cmbTipoSoporte.SelectedValue),
                .descripcion = txtDescripcion.Text.Trim(),
                .fechaCreacion = Date.Now,
                .estadoID = 1 ' Estado inicial: Pendiente
            }

            ' Llamar al controlador para crear el ticket
            ticketController.CreateTicket(nuevoTicket)

            MessageBox.Show("Ticket creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim frmLogin As New frmLogin()

            frmLogin.show()
            Application.Exit()

        Catch ex As Exception
            MessageBox.Show($"Error al crear el ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class