<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdminEquipos
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
        NombreUsuario = New Label()
        DataGridView1 = New DataGridView()
        btnGuardarCambios = New Button()
        btnEliminar = New Button()
        MenuStrip1 = New MenuStrip()
        SalirToolStripMenuItem = New ToolStripMenuItem()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' NombreUsuario
        ' 
        NombreUsuario.AutoSize = True
        NombreUsuario.Font = New Font("Segoe UI", 24F)
        NombreUsuario.Location = New Point(12, 24)
        NombreUsuario.Name = "NombreUsuario"
        NombreUsuario.Size = New Size(314, 54)
        NombreUsuario.TabIndex = 0
        NombreUsuario.Text = "nombre del user"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(279, 121)
        DataGridView1.Margin = New Padding(3, 4, 3, 4)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.Size = New Size(898, 349)
        DataGridView1.TabIndex = 3
        ' 
        ' btnGuardarCambios
        ' 
        btnGuardarCambios.Font = New Font("Segoe UI", 13.8F)
        btnGuardarCambios.Location = New Point(63, 173)
        btnGuardarCambios.Margin = New Padding(3, 4, 3, 4)
        btnGuardarCambios.Name = "btnGuardarCambios"
        btnGuardarCambios.Size = New Size(141, 97)
        btnGuardarCambios.TabIndex = 4
        btnGuardarCambios.Text = "Guardar Cambios"
        btnGuardarCambios.UseVisualStyleBackColor = True
        ' 
        ' btnEliminar
        ' 
        btnEliminar.Font = New Font("Segoe UI", 13.8F)
        btnEliminar.Location = New Point(63, 329)
        btnEliminar.Margin = New Padding(3, 4, 3, 4)
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Size = New Size(141, 97)
        btnEliminar.TabIndex = 5
        btnEliminar.Text = "Eliminar Registros"
        btnEliminar.UseVisualStyleBackColor = True
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {SalirToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(1206, 28)
        MenuStrip1.TabIndex = 6
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' SalirToolStripMenuItem
        ' 
        SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        SalirToolStripMenuItem.Size = New Size(52, 24)
        SalirToolStripMenuItem.Text = "Salir"
        ' 
        ' frmAdminEquipos
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1206, 799)
        Controls.Add(btnEliminar)
        Controls.Add(btnGuardarCambios)
        Controls.Add(DataGridView1)
        Controls.Add(NombreUsuario)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Margin = New Padding(3, 4, 3, 4)
        Name = "frmAdminEquipos"
        Text = "frmAdminEquipos"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents NombreUsuario As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnGuardarCambios As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SalirToolStripMenuItem As ToolStripMenuItem
End Class
