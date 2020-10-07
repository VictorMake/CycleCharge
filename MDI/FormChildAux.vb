Imports System.Windows.Forms

''' <summary>
''' Класс управления дочерними окнами в менеджере окон
''' </summary>
''' <remarks></remarks>
Public Class FormChildAux

    ''' <summary>
    ''' Добавить имя окна в список
    ''' </summary>
    ''' <param name="noticeDescription"></param>
    ''' <remarks></remarks>
    Public Sub AddPanelListViewItems(ByVal noticeDescription As String, imageIndex As Integer)
        ListViewForm.Items.Add(noticeDescription, imageIndex)
    End Sub

    ''' <summary>
    ''' в цикле просмотреть дочерние для родителя  Me.MdiParent
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListViewItemsRebuildChecked()
        For Each frm As Form In MdiParent.MdiChildren
            'If TypeOf frm Is frmBasePanelMotorist Then
            For Each itemListView As ListViewItem In ListViewForm.Items
                'If TempListView.Text = CType(frm, frmBasePanelMotorist).ИмяПанелиМоториста Then
                If itemListView.Text = frm.Tag.ToString Then
                    itemListView.Checked = True
                    Exit For
                End If
            Next
            'End If
        Next frm

        AddHandlerListViewItemChecked()
    End Sub

    Public Sub CloseAllCheckedWindows()
        For Each frm As Form In MdiParent.MdiChildren
            'If TypeOf frm Is frmBasePanelMotorist Then
            For Each itemListView As ListViewItem In ListViewForm.Items
                'If TempListView.Text = CType(frm, frmBasePanelMotorist).ИмяПанелиМоториста Then
                If itemListView.Text = frm.Tag.ToString Then
                    If itemListView.Checked = True Then
                        itemListView.Checked = False
                    End If
                End If
            Next
            'End If
        Next frm
        ' лучше наверно такой вариант, когда закрываются все, даже выведенные из родительской панели окна 
        'For Each TempListView As ListViewItem In ListView1.Items
        '    If TempListView.Checked = True Then
        '        TempListView.Checked = False
        '    End If
        'Next
    End Sub

    ''' <summary>
    ''' Снять отметки с надписей
    ''' </summary>
    ''' <param name="formTag"></param>
    ''' <remarks></remarks>
    Public Sub UnCheckedListView(ByVal formTag As String)
        For Each itemListView As ListViewItem In ListViewForm.Items
            If itemListView.Text = formTag Then
                itemListView.Checked = False
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' Добавить обработчик ItemChecked
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddHandlerListViewItemChecked()
        AddHandler ListViewForm.ItemChecked, AddressOf ListViewItemChecked
    End Sub

    ''' <summary>
    ''' Удалить обработчик ItemChecked
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RemoveHandlerListViewItemChecked()
        RemoveHandler ListViewForm.ItemChecked, AddressOf ListViewItemChecked
    End Sub

    ''' <summary>
    ''' Удалить все пункты меню
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearListViewItems()
        ListViewForm.Items.Clear()
    End Sub

    ''' <summary>
    ''' Загрузка окна по выделенной строке в списке 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ListViewItemChecked(ByVal sender As Object, ByVal e As ItemCheckedEventArgs)
        Dim statusMessage As String
        Dim selectedItem As ListViewItem = CType(e.Item, ListViewItem)

        Try
            If selectedItem.Checked Then
                Try
                    ' при создании автоматом добавляется в коллекцию
                    If Not FormWindowsManager.CreateNewForm(CType(MdiParent, FrmMain), selectedItem.Text) Then ' там проверка на корректность
                        'selectedItem.CheckState = CheckState.Unchecked
                        selectedItem.Checked = False
                    End If

                Catch ex As Exception
                    Dim CAPTION As String = $"Загрузка окна в процедуре: {NameOf(ListViewItemChecked)}"
                    Const TEXT As String = "Ошибка в создании нового окна: " & vbCrLf
                    MessageBox.Show(TEXT & ex.ToString, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Else
                If FormWindowsManager.Count > 0 Then
                    If FormWindowsManager.ContainsKey(selectedItem.Text) Then
                        FormWindowsManager.Item(selectedItem.Text).Close() ' там и удаляется
                    End If
                End If
            End If
        Catch ex As Exception
            Dim CAPTION As String = selectedItem.Text
            Dim text As String = ex.ToString
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        If selectedItem.Checked Then
            statusMessage = "Загружена панель "
        Else
            statusMessage = "Выгружена панель "
        End If

        CType(MdiParent, FrmMain).СообщениеНаПанель(statusMessage & selectedItem.Text)
    End Sub

    Public Sub LoadForm(NameWindowForm As String)
        'If ListView1.Items.ContainsKey(NameWindowForm) Then
        'Dim TempListView As ListViewItem = ListView1.Items(NameWindowForm)
        For Each itemListView As ListViewItem In ListViewForm.Items
            'If TempListView.Text = CType(frm, frmBasePanelMotorist).ИмяПанелиМоториста Then
            If itemListView.Text = NameWindowForm Then
                If itemListView.Checked Then
                    ' ищем окно связанное с TempListView
                    For Each frm As Form In MdiParent.MdiChildren
                        If itemListView.Text = frm.Tag.ToString Then
                            frm.Select()
                            Exit For
                        End If
                    Next frm
                Else
                    ' загрузить окно
                    itemListView.Checked = True
                End If
                Exit For
            End If
        Next
        'End If
    End Sub

    'Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
    '    If Not ListView1.SelectedItems.Count = 0 Then
    '        Dim frm As New ChildForm2

    '        frm.Icon = System.Drawing.Icon.FromHandle(CType(Me.ImageList1.Images.Item(0), Bitmap).GetHicon)
    '        frm.Text = ListView1.SelectedItems.Item(0).Text
    '        frm.TextBox1.Text = "This is " + ListView1.SelectedItems.Item(0).Text

    '        frm.MdiParent = Me.MdiParent
    '        frm.Show()
    '    End If
    'End Sub
End Class