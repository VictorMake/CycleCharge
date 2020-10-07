<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SwitchControl

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
        Me.Switch1 = New NationalInstruments.UI.WindowsForms.Switch()
        CType(Me.Switch1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Switch1
        '
        Me.Switch1.Caption = "0"
        Me.Switch1.CaptionBackColor = System.Drawing.Color.Transparent
        Me.Switch1.CaptionPosition = NationalInstruments.UI.CaptionPosition.Bottom
        Me.Switch1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Switch1.Location = New System.Drawing.Point(0, 0)
        Me.Switch1.Name = "Switch1"
        Me.Switch1.OnColor = System.Drawing.Color.Maroon
        Me.Switch1.Size = New System.Drawing.Size(40, 80)
        Me.Switch1.SwitchStyle = NationalInstruments.UI.SwitchStyle.VerticalSlide3D
        Me.Switch1.TabIndex = 0
        '
        'SwitchControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Switch1)
        Me.MinimumSize = New System.Drawing.Size(10, 20)
        Me.Name = "SwitchControl"
        Me.Size = New System.Drawing.Size(40, 80)
        CType(Me.Switch1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Switch1 As NationalInstruments.UI.WindowsForms.Switch
End Class
