<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LedControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Led1 = New NationalInstruments.UI.WindowsForms.Led()
        CType(Me.Led1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Led1
        '
        Me.Led1.CaptionBackColor = System.Drawing.Color.Transparent
        Me.Led1.CaptionPosition = NationalInstruments.UI.CaptionPosition.Bottom
        Me.Led1.LedStyle = NationalInstruments.UI.LedStyle.Round3D
        Me.Led1.Location = New System.Drawing.Point(0, 0)
        Me.Led1.MinimumSize = New System.Drawing.Size(5, 5)
        Me.Led1.Name = "Led1"
        Me.Led1.Size = New System.Drawing.Size(45, 43)
        Me.Led1.TabIndex = 0
        '
        'LedControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Led1)
        Me.MinimumSize = New System.Drawing.Size(10, 20)
        Me.Name = "LedControl"
        Me.Size = New System.Drawing.Size(40, 80)
        CType(Me.Led1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Led1 As NationalInstruments.UI.WindowsForms.Led
End Class
