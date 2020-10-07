<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControlCycle
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RadioButtonНепрерывно = New System.Windows.Forms.RadioButton()
        Me.RadioButtonПовторить = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblВыполнено = New System.Windows.Forms.Label()
        Me.lblРаз = New System.Windows.Forms.Label()
        Me.NumUpDnЧислоПовторов = New System.Windows.Forms.NumericUpDown()
        Me.Panel3.SuspendLayout()
        CType(Me.NumUpDnЧислоПовторов, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioButtonНепрерывно
        '
        Me.RadioButtonНепрерывно.AutoSize = True
        Me.RadioButtonНепрерывно.Location = New System.Drawing.Point(9, 67)
        Me.RadioButtonНепрерывно.Name = "RadioButtonНепрерывно"
        Me.RadioButtonНепрерывно.Size = New System.Drawing.Size(89, 17)
        Me.RadioButtonНепрерывно.TabIndex = 12
        Me.RadioButtonНепрерывно.TabStop = True
        Me.RadioButtonНепрерывно.Text = "Непрерывно"
        Me.RadioButtonНепрерывно.UseVisualStyleBackColor = True
        '
        'RadioButtonПовторить
        '
        Me.RadioButtonПовторить.AutoSize = True
        Me.RadioButtonПовторить.Checked = True
        Me.RadioButtonПовторить.Location = New System.Drawing.Point(9, 0)
        Me.RadioButtonПовторить.Name = "RadioButtonПовторить"
        Me.RadioButtonПовторить.Size = New System.Drawing.Size(79, 17)
        Me.RadioButtonПовторить.TabIndex = 11
        Me.RadioButtonПовторить.TabStop = True
        Me.RadioButtonПовторить.Text = "Повторить"
        Me.RadioButtonПовторить.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.lblВыполнено)
        Me.Panel3.Controls.Add(Me.lblРаз)
        Me.Panel3.Controls.Add(Me.NumUpDnЧислоПовторов)
        Me.Panel3.Location = New System.Drawing.Point(3, 9)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(95, 52)
        Me.Panel3.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "выполнено"
        '
        'lblВыполнено
        '
        Me.lblВыполнено.AutoSize = True
        Me.lblВыполнено.Location = New System.Drawing.Point(3, 33)
        Me.lblВыполнено.Name = "lblВыполнено"
        Me.lblВыполнено.Size = New System.Drawing.Size(13, 13)
        Me.lblВыполнено.TabIndex = 2
        Me.lblВыполнено.Text = "0"
        '
        'lblРаз
        '
        Me.lblРаз.AutoSize = True
        Me.lblРаз.Location = New System.Drawing.Point(53, 13)
        Me.lblРаз.Name = "lblРаз"
        Me.lblРаз.Size = New System.Drawing.Size(25, 13)
        Me.lblРаз.TabIndex = 1
        Me.lblРаз.Text = "раз"
        '
        'NumUpDnЧислоПовторов
        '
        Me.NumUpDnЧислоПовторов.Location = New System.Drawing.Point(6, 10)
        Me.NumUpDnЧислоПовторов.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumUpDnЧислоПовторов.Name = "NumUpDnЧислоПовторов"
        Me.NumUpDnЧислоПовторов.Size = New System.Drawing.Size(44, 20)
        Me.NumUpDnЧислоПовторов.TabIndex = 0
        Me.NumUpDnЧислоПовторов.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'UserControlCycle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Controls.Add(Me.RadioButtonНепрерывно)
        Me.Controls.Add(Me.RadioButtonПовторить)
        Me.Controls.Add(Me.Panel3)
        Me.Name = "UserControlCycle"
        Me.Size = New System.Drawing.Size(102, 87)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.NumUpDnЧислоПовторов, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButtonНепрерывно As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonПовторить As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblВыполнено As System.Windows.Forms.Label
    Friend WithEvents lblРаз As System.Windows.Forms.Label
    Friend WithEvents NumUpDnЧислоПовторов As System.Windows.Forms.NumericUpDown

End Class
