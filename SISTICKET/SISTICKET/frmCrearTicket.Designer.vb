<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCrearTicket
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        cmbTipoSoporte = New ComboBox()
        Label5 = New Label()
        txtDescripcion = New TextBox()
        btnEnviar = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(41, 42)
        Label1.Name = "Label1"
        Label1.Size = New Size(274, 54)
        Label1.TabIndex = 0
        Label1.Text = "CREAR TICKET"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 12F)
        Label2.Location = New Point(420, 150)
        Label2.Name = "Label2"
        Label2.Size = New Size(265, 28)
        Label2.TabIndex = 1
        Label2.Text = "Seleccione el tipo de soporte"
        ' 
        ' cmbTipoSoporte
        ' 
        cmbTipoSoporte.Font = New Font("Segoe UI", 12F)
        cmbTipoSoporte.FormattingEnabled = True
        cmbTipoSoporte.Location = New Point(448, 182)
        cmbTipoSoporte.Margin = New Padding(3, 4, 3, 4)
        cmbTipoSoporte.Name = "cmbTipoSoporte"
        cmbTipoSoporte.Size = New Size(209, 36)
        cmbTipoSoporte.TabIndex = 2
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F)
        Label5.Location = New Point(437, 289)
        Label5.Name = "Label5"
        Label5.Size = New Size(236, 28)
        Label5.TabIndex = 5
        Label5.Text = "Descripción del problema"
        ' 
        ' txtDescripcion
        ' 
        txtDescripcion.Font = New Font("Segoe UI", 12F)
        txtDescripcion.Location = New Point(377, 321)
        txtDescripcion.Margin = New Padding(3, 4, 3, 4)
        txtDescripcion.Name = "txtDescripcion"
        txtDescripcion.Size = New Size(355, 34)
        txtDescripcion.TabIndex = 6
        ' 
        ' btnEnviar
        ' 
        btnEnviar.Font = New Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnEnviar.Location = New Point(464, 425)
        btnEnviar.Margin = New Padding(3, 4, 3, 4)
        btnEnviar.Name = "btnEnviar"
        btnEnviar.Size = New Size(165, 78)
        btnEnviar.TabIndex = 7
        btnEnviar.Text = "Enviar Ticket"
        btnEnviar.UseVisualStyleBackColor = True
        ' 
        ' frmCrearTicket
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1082, 600)
        Controls.Add(btnEnviar)
        Controls.Add(txtDescripcion)
        Controls.Add(Label5)
        Controls.Add(cmbTipoSoporte)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "frmCrearTicket"
        Text = "frmCrearTicket"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbTipoSoporte As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents btnEnviar As Button
End Class
