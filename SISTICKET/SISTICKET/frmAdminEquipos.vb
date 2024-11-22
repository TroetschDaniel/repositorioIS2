Imports System.ComponentModel

Public Class frmAdminEquipos

    Private UsuarioID As Integer
    Private admin_controller As New AdminController()
    Private ticketController As New ticketsController()
    Private user As New UserDAO()

    Public Sub New(usuario_id As Integer)
        InitializeComponent()

        UsuarioID = usuario_id
        user = admin_controller.getDataUser(usuario_id)

        If user.Nombre IsNot Nothing Then
            NombreUsuario.Text = $"Bienvenido: {user.Nombre}"
        End If
    End Sub

    Private Sub frmAdminEquipos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTickets() ' Llamar la función para cargar los tickets al iniciar
        ConfigurarDataGridViewInicial()
        CargarTickets()
    End Sub

    ' Función para cargar los tickets en el DataGridView
    Private Sub LoadTickets()
        Try
            ' Llamamos al controlador para obtener todos los tickets
            Dim tickets As List(Of TicketDAO) = ticketController.GetAllTickets()

            ' Creamos un BindingList para que los datos se muestren correctamente en el DataGridView
            Dim bindingList As New BindingList(Of TicketDAO)(tickets)
            Dim source As New BindingSource(bindingList, Nothing)

            ' Asignamos la fuente de datos al DataGridView
            DataGridView1.DataSource = source
        Catch ex As Exception
            MessageBox.Show($"Error al cargar los tickets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigurarDataGridViewInicial()
        DataGridView1.AutoGenerateColumns = True
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = True
        DataGridView1.ReadOnly = False
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub btnGuardarCambios_Click(sender As Object, e As EventArgs) Handles btnGuardarCambios.Click
        Try
            Dim ticketController As New ticketsController
            Dim listaTickets As New List(Of TicketDAO)

            ' Procesar filas existentes o nuevas
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.IsNewRow Then Continue For ' Ignora la nueva fila

                Try
                    ' Validar que los campos necesarios no sean nulos
                    If row.Cells("UsuarioID").Value Is Nothing OrElse
                   row.Cells("TipoSoporteID").Value Is Nothing OrElse
                   row.Cells("EstadoID").Value Is Nothing OrElse
                   row.Cells("Descripcion").Value Is Nothing Then
                        Continue For
                    End If

                    ' Crear el objeto TicketDAO
                    Dim ticket As New TicketDAO With {
                .ticketID = If(IsDBNull(row.Cells("TicketID").Value), 0, Convert.ToInt32(row.Cells("TicketID").Value)),
                .usuarioID = Convert.ToInt32(row.Cells("UsuarioID").Value),
                .tipoSoporteID = Convert.ToInt32(row.Cells("TipoSoporteID").Value),
                .estadoID = Convert.ToInt32(row.Cells("EstadoID").Value),
                .descripcion = row.Cells("Descripcion").Value.ToString,
                .fechaCreacion = If(IsDBNull(row.Cells("FechaCreacion").Value), Date.Now, Convert.ToDateTime(row.Cells("FechaCreacion").Value))
            }

                    listaTickets.Add(ticket)
                Catch ex As Exception
                    MessageBox.Show($"Error al procesar la fila {row.Index + 1}: {ex.Message}")
                    Continue For
                End Try
            Next

            ' Crear o actualizar los tickets
            For Each ticket In listaTickets
                Try
                    If ticket.ticketID = 0 Then
                        ticketController.CreateTicket(ticket) ' Crear ticket nuevo
                    Else
                        ticketController.UpdateTicket(ticket) ' Actualizar ticket existente
                    End If
                Catch ex As Exception
                    MessageBox.Show($"Error al procesar ticket {ticket.ticketID}: {ex.Message}")
                End Try
            Next

            ' Después de la operación de actualización, actualiza los datos en el DataGridView
            MessageBox.Show("Cambios guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarTickets() ' Recarga los datos actualizados

        Catch ex As Exception
            MessageBox.Show($"Error al guardar cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarTickets()
        Try
            ' Llamamos al controlador para obtener todos los tickets
            Dim tickets As List(Of TicketDAO) = ticketController.GetAllTickets()

            ' Creamos un BindingList para que los datos se muestren correctamente en el DataGridView
            Dim bindingList As New BindingList(Of TicketDAO)(tickets)
            Dim source As New BindingSource(bindingList, Nothing)

            ' Asignamos la fuente de datos al DataGridView
            DataGridView1.DataSource = source
            ConfigurarDataGridViewInicial() ' Configura las propiedades del DGV
        Catch ex As Exception
            MessageBox.Show($"Error al cargar los tickets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            ' Verificar si se seleccionó una fila en el DataGridView
            If DataGridView1.SelectedRows.Count > 0 Then
                ' Obtener el TicketID de la fila seleccionada
                Dim ticketID As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("TicketID").Value)

                ' Eliminar el ticket de la base de datos
                ticketController.DeleteTicket(ticketID)

                ' Recargar los datos del DataGridView
                MessageBox.Show("Ticket eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarTickets() ' Recarga los datos actualizados después de la eliminación
            Else
                MessageBox.Show("Por favor, seleccione un ticket para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error al eliminar el ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Dim frmLogin As New frmLogin()
        frmLogin.Show()
        Application.Exit()
    End Sub
End Class
