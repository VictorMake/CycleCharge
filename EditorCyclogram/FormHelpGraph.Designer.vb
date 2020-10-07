<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormHelpGraph
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                'If РодительскаяФорма IsNot Nothing Then
                '    РодительскаяФорма.Dispose()
                '    РодительскаяФорма = Nothing
                'End If
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Масштабирование", "", "Панорамирование"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Shift+ВыделитьЛевойКнопкойМыши", "Масштаб по выделенному", "Ctrl+ТянутьЛевойКнопкойМыши", "Панорамирование"}, -1)
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Shift+Alt+ВыделитьЛевойКнопкойМыши", "Масштаб пропорционально выделенному", "Ctrl+ЛеваяСтрелка", "Панорама влево"}, -1)
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Shift+ЛевыйЩелчок", "Масштаб вокруг точки", "Ctrl+ПраваяСтрелка", "Панорама вправо"}, -1)
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Shift+ВерхняяСтрелка", "+ Масштаб изображения в середине области", "Ctrl+ВерхняяСтрелка", "Панорама вверх"}, -1)
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Shift+НижняяСтрелка", "-  Масштаб изображения в середине области", "Ctrl+НижняяСтрелка", "Панорама вниз"}, -1)
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Shift+КолесоМыши", "Масштаб +/Масштаб -", "Ctrl+ПравыйЩелчок", "Откат"}, -1)
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Shift+ПравыйЩелчок", "Откат", "Ctrl+Забой", "Сброс"}, -1)
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"Shift+Забой", "Сброс"}, -1)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormHelpGraph))
        Me.actionListView = New System.Windows.Forms.ListView()
        Me.keys1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.action1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.keys2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.action2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'actionListView
        '
        Me.actionListView.BackColor = System.Drawing.Color.LightSteelBlue
        Me.actionListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.keys1, Me.action1, Me.keys2, Me.action2})
        Me.actionListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.actionListView.ForeColor = System.Drawing.Color.Black
        Me.actionListView.FullRowSelect = True
        Me.actionListView.GridLines = True
        Me.actionListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.actionListView.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9})
        Me.actionListView.Location = New System.Drawing.Point(0, 0)
        Me.actionListView.Name = "actionListView"
        Me.actionListView.Size = New System.Drawing.Size(755, 200)
        Me.actionListView.TabIndex = 10
        Me.actionListView.UseCompatibleStateImageBehavior = False
        Me.actionListView.View = System.Windows.Forms.View.Details
        '
        'keys1
        '
        Me.keys1.Text = "Сочетание клавиш"
        Me.keys1.Width = 269
        '
        'action1
        '
        Me.action1.Text = "Действие"
        Me.action1.Width = 316
        '
        'keys2
        '
        Me.keys2.Text = "Сочетание клавиш"
        Me.keys2.Width = 228
        '
        'action2
        '
        Me.action2.Text = "Действие"
        Me.action2.Width = 137
        '
        'HelpGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 200)
        Me.Controls.Add(Me.actionListView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HelpGraph"
        Me.Text = "Клавиши управления графиком"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents actionListView As System.Windows.Forms.ListView
    Friend WithEvents keys1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents action1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents keys2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents action2 As System.Windows.Forms.ColumnHeader
End Class
