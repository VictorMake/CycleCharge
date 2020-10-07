<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControlNumeric
    Inherits System.Windows.Forms.UserControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.NumericUpDownValue = New System.Windows.Forms.NumericUpDown()
        CType(Me.NumericUpDownValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumericUpDownValue
        '
        Me.NumericUpDownValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownValue.DecimalPlaces = 2
        Me.NumericUpDownValue.Location = New System.Drawing.Point(0, 10)
        Me.NumericUpDownValue.Margin = New System.Windows.Forms.Padding(0)
        Me.NumericUpDownValue.Name = "NumericUpDownValue"
        Me.NumericUpDownValue.Size = New System.Drawing.Size(96, 22)
        Me.NumericUpDownValue.TabIndex = 1
        Me.NumericUpDownValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UserControlNumeric
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.NumericUpDownValue)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UserControlNumeric"
        Me.Size = New System.Drawing.Size(96, 44)
        CType(Me.NumericUpDownValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents NumericUpDownValue As System.Windows.Forms.NumericUpDown

End Class
