<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSoporte
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
        dgvTickets = New DataGridView()
        Label2 = New Label()
        cmbEstado = New ComboBox()
        btnActualizar = New Button()
        CType(dgvTickets, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 24F)
        Label1.Location = New Point(28, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(189, 54)
        Label1.TabIndex = 0
        Label1.Text = "SOPORTE"
        ' 
        ' dgvTickets
        ' 
        dgvTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTickets.Location = New Point(323, 118)
        dgvTickets.Name = "dgvTickets"
        dgvTickets.RowHeadersWidth = 51
        dgvTickets.Size = New Size(728, 367)
        dgvTickets.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(28, 149)
        Label2.Name = "Label2"
        Label2.Size = New Size(221, 20)
        Label2.TabIndex = 2
        Label2.Text = "Actualizar el estado de un ticket"
        ' 
        ' cmbEstado
        ' 
        cmbEstado.FormattingEnabled = True
        cmbEstado.Location = New Point(28, 182)
        cmbEstado.Name = "cmbEstado"
        cmbEstado.Size = New Size(228, 28)
        cmbEstado.TabIndex = 3
        ' 
        ' btnActualizar
        ' 
        btnActualizar.Location = New Point(28, 287)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Size = New Size(221, 57)
        btnActualizar.TabIndex = 4
        btnActualizar.Text = "Actualizar"
        btnActualizar.UseVisualStyleBackColor = True
        ' 
        ' frmSoporte
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1092, 600)
        Controls.Add(btnActualizar)
        Controls.Add(cmbEstado)
        Controls.Add(Label2)
        Controls.Add(dgvTickets)
        Controls.Add(Label1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "frmSoporte"
        Text = "frmSeguimientoTicket"
        CType(dgvTickets, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents dgvTickets As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbEstado As ComboBox
    Friend WithEvents btnActualizar As Button
End Class
