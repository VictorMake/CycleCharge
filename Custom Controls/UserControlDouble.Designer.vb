<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControlDouble
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
        Me.NumericUpDownTime = New NationalInstruments.UI.WindowsForms.NumericEdit()
        CType(Me.NumericUpDownTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumericUpDownTime
        '
        Me.NumericUpDownTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDownTime.FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(2)
        Me.NumericUpDownTime.Location = New System.Drawing.Point(0, 8)
        Me.NumericUpDownTime.Name = "NumericUpDownTime"
        Me.NumericUpDownTime.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange
        Me.NumericUpDownTime.Range = New NationalInstruments.UI.Range(0.0R, 100.0R)
        Me.NumericUpDownTime.Size = New System.Drawing.Size(81, 20)
        Me.NumericUpDownTime.TabIndex = 25
        Me.NumericUpDownTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'UserControlDouble
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.NumericUpDownTime)
        Me.Name = "UserControlDouble"
        Me.Size = New System.Drawing.Size(81, 36)
        CType(Me.NumericUpDownTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NumericUpDownTime As NationalInstruments.UI.WindowsForms.NumericEdit

End Class
