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
        Me.RadioButton���������� = New System.Windows.Forms.RadioButton()
        Me.RadioButton��������� = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbl��������� = New System.Windows.Forms.Label()
        Me.lbl��� = New System.Windows.Forms.Label()
        Me.NumUpDn������������� = New System.Windows.Forms.NumericUpDown()
        Me.Panel3.SuspendLayout()
        CType(Me.NumUpDn�������������, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadioButton����������
        '
        Me.RadioButton����������.AutoSize = True
        Me.RadioButton����������.Location = New System.Drawing.Point(9, 67)
        Me.RadioButton����������.Name = "RadioButton����������"
        Me.RadioButton����������.Size = New System.Drawing.Size(89, 17)
        Me.RadioButton����������.TabIndex = 12
        Me.RadioButton����������.TabStop = True
        Me.RadioButton����������.Text = "����������"
        Me.RadioButton����������.UseVisualStyleBackColor = True
        '
        'RadioButton���������
        '
        Me.RadioButton���������.AutoSize = True
        Me.RadioButton���������.Checked = True
        Me.RadioButton���������.Location = New System.Drawing.Point(9, 0)
        Me.RadioButton���������.Name = "RadioButton���������"
        Me.RadioButton���������.Size = New System.Drawing.Size(79, 17)
        Me.RadioButton���������.TabIndex = 11
        Me.RadioButton���������.TabStop = True
        Me.RadioButton���������.Text = "���������"
        Me.RadioButton���������.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.lbl���������)
        Me.Panel3.Controls.Add(Me.lbl���)
        Me.Panel3.Controls.Add(Me.NumUpDn�������������)
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
        Me.Label5.Text = "���������"
        '
        'lbl���������
        '
        Me.lbl���������.AutoSize = True
        Me.lbl���������.Location = New System.Drawing.Point(3, 33)
        Me.lbl���������.Name = "lbl���������"
        Me.lbl���������.Size = New System.Drawing.Size(13, 13)
        Me.lbl���������.TabIndex = 2
        Me.lbl���������.Text = "0"
        '
        'lbl���
        '
        Me.lbl���.AutoSize = True
        Me.lbl���.Location = New System.Drawing.Point(53, 13)
        Me.lbl���.Name = "lbl���"
        Me.lbl���.Size = New System.Drawing.Size(25, 13)
        Me.lbl���.TabIndex = 1
        Me.lbl���.Text = "���"
        '
        'NumUpDn�������������
        '
        Me.NumUpDn�������������.Location = New System.Drawing.Point(6, 10)
        Me.NumUpDn�������������.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumUpDn�������������.Name = "NumUpDn�������������"
        Me.NumUpDn�������������.Size = New System.Drawing.Size(44, 20)
        Me.NumUpDn�������������.TabIndex = 0
        Me.NumUpDn�������������.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'UserControlCycle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Controls.Add(Me.RadioButton����������)
        Me.Controls.Add(Me.RadioButton���������)
        Me.Controls.Add(Me.Panel3)
        Me.Name = "UserControlCycle"
        Me.Size = New System.Drawing.Size(102, 87)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.NumUpDn�������������, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RadioButton���������� As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton��������� As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbl��������� As System.Windows.Forms.Label
    Friend WithEvents lbl��� As System.Windows.Forms.Label
    Friend WithEvents NumUpDn������������� As System.Windows.Forms.NumericUpDown

End Class
