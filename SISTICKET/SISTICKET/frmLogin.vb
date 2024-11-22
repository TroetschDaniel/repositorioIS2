Imports System.Data.SqlClient
Imports Microsoft.Data.SqlClient
Imports SISTICKET.UsusaroModel ' Si creaste una carpeta "Modelos"
Imports SISTICKET.LoginController ' Si creaste una carpeta "Controladores"

Public Class frmLogin
    'obj de controlador de logica de negocio
    Private ReadOnly logincontroller As New LoginController()

    ' Evento que se ejecuta al cargar el formulario
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtContrasena.PasswordChar = "*" ' Configurar para que la contraseña se oculte
    End Sub

    ' Evento para manejar el botón "Iniciar Sesión"
    Private Sub btnIniciarSesion_Click(sender As Object, e As EventArgs) Handles btnIniciarSesion.Click

        Dim username As String = txtUsuario.Text.Trim()
        Dim password As String = txtContrasena.Text.Trim()

        If String.IsNullOrEmpty(username) Or String.IsNullOrEmpty(password) Then
            MessageBox.Show("Por favor, ingresa usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim role As Integer? = logincontroller.IniciarSesion(username, password)

            If role.HasValue Then

                Select Case role.Value
                    Case 1
                        'ADMIN
                        Dim adminForm As New frmAdminEquipos(1)
                        adminForm.Show()
                        Me.Hide()

                    Case 2
                        'USUARIO
                        Dim usuarioForm As New frmAdminEquipos(1) ' Instancia el formulario frmAdminEquipos
                        Dim crearTicketForm As New frmCrearTicket(1) ' Instancia el formulario frmCrearTicket y pasa el ID del usuario
                        crearTicketForm.Show() ' Muestra el formulario frmCrearTicket
                        Me.Hide() ' Oculta el formulario actual (frmLogin)

                    Case 3
                        'SOPORTE
                        Dim soporteForm As New frmSoporte()
                        soporteForm.Show()
                        Me.Hide()


                End Select


            End If
        Catch ex As Exception
            MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Evento para manejar el botón "Salir"
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Application.Exit()
    End Sub
End Class
