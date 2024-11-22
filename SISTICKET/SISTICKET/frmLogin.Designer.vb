<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        txtUsuario = New TextBox()
        txtContrasena = New TextBox()
        btnIniciarSesion = New Button()
        btnSalir = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(465, 58)
        Label1.Name = "Label1"
        Label1.Size = New Size(493, 54)
        Label1.TabIndex = 0
        Label1.Text = "BIENVENIDO A SISTICKETS"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 13.8F)
        Label2.Location = New Point(588, 210)
        Label2.Name = "Label2"
        Label2.Size = New Size(92, 31)
        Label2.TabIndex = 1
        Label2.Text = "Usuario"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 13.8F)
        Label3.Location = New Point(588, 317)
        Label3.Name = "Label3"
        Label3.Size = New Size(129, 31)
        Label3.TabIndex = 2
        Label3.Text = "Contraseña"
        ' 
        ' txtUsuario
        ' 
        txtUsuario.Font = New Font("Segoe UI", 13.8F)
        txtUsuario.Location = New Point(588, 244)
        txtUsuario.Name = "txtUsuario"
        txtUsuario.Size = New Size(221, 38)
        txtUsuario.TabIndex = 3
        ' 
        ' txtContrasena
        ' 
        txtContrasena.Font = New Font("Segoe UI", 13.8F)
        txtContrasena.Location = New Point(588, 351)
        txtContrasena.Name = "txtContrasena"
        txtContrasena.Size = New Size(221, 38)
        txtContrasena.TabIndex = 4
        ' 
        ' btnIniciarSesion
        ' 
        btnIniciarSesion.Font = New Font("Segoe UI", 16.2F)
        btnIniciarSesion.Location = New Point(418, 479)
        btnIniciarSesion.Name = "btnIniciarSesion"
        btnIniciarSesion.Size = New Size(186, 75)
        btnIniciarSesion.TabIndex = 5
        btnIniciarSesion.Text = "Iniciar sesión"
        btnIniciarSesion.UseVisualStyleBackColor = True
        ' 
        ' btnSalir
        ' 
        btnSalir.Font = New Font("Segoe UI", 16.2F)
        btnSalir.Location = New Point(789, 479)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(186, 75)
        btnSalir.TabIndex = 6
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = True
        ' 
        ' frmLogin
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1339, 733)
        Controls.Add(btnSalir)
        Controls.Add(btnIniciarSesion)
        Controls.Add(txtContrasena)
        Controls.Add(txtUsuario)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "frmLogin"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtUsuario As TextBox
    Friend WithEvents txtContrasena As TextBox
    Friend WithEvents btnIniciarSesion As Button
    Friend WithEvents btnSalir As Button

End Class
