Imports System.ComponentModel
Imports System.Windows.Forms

''' <summary>
''' Менеджер управления дочерними Оконами в табличном виде
''' </summary>
''' <remarks></remarks>
Friend Class FormWindowsManager
    Implements IEnumerable

#Region "IEnumerable"
    ''' <summary>
    ''' внутренняя коллекция для управления формами
    ''' </summary>
    Private Shared mTabledForms As New Dictionary(Of String, Form)
    ''' <summary>
    ''' внутренний счетчик для подсчета созданных форм можно использовать в заголовке
    ''' </summary>
    Private Shared mFormsCounter As Integer = 0

    ''' <summary>
    ''' число текущих загруженных форм
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Count() As Integer
        Get
            Return mTabledForms.Count
        End Get
    End Property

    '''' <summary>
    '''' Оболочка коллекции окон
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Shared ReadOnly Property TabledForms() As Dictionary(Of String, Form)
    '    Get
    '        Return mTabledForms
    '    End Get
    'End Property

    ''' <summary>
    ''' элемент коллекции
    ''' </summary>
    ''' <param name="key"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Item(ByVal key As String) As Form
        Get
            Return mTabledForms.Item(key)
        End Get
    End Property

    ''' <summary>
    ''' перечислитель
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return mTabledForms.GetEnumerator
    End Function

    ''' <summary>
    ''' удаление по номеру или имени или объекту?
    ''' </summary>
    ''' <param name="key"></param>
    ''' <remarks></remarks>
    Public Sub Remove(ByVal key As String) ' Shared убрал так как надо удалять закрывая саму форму, а она уже удаляет из коллекции
        ' если целый тип то по плавающему индексу, а если строковый то по ключу
        mTabledForms.Remove(key)
        mFormsCounter -= 1
    End Sub

    Public Shared Sub Clear()
        mTabledForms.Clear()
    End Sub

    Public Shared Function ContainsKey(ByVal key As String) As Boolean
        Return mTabledForms.ContainsKey(key)
    End Function

    ''' <summary>
    ''' Возвращает имена всех загруженных дочрних окон.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function Keys() As Dictionary(Of String, Form).KeyCollection
        Return mTabledForms.Keys
    End Function

    Protected Overrides Sub Finalize()
        mTabledForms = Nothing
        MyBase.Finalize()
    End Sub
#End Region

    '--- константы на формы окон ---------------------------------------------
    Private Const РЕДАКТОР_МОЩНОСТЕЙ As String = "Редактор мощностей"
    Private Const РЕДАКТОР_ЦИКЛОГРАММ As String = "Редактор циклограмм"
    Private Const ПРОИГРЫВАТЕЛЬ_ЦИКЛОГРАММ As String = "Проигрыватель циклограмм"

    ''' <summary>
    ''' Перечислитель загруженных окон
    ''' обязательно должен быть TAG формы совпадающий с Const
    ''' </summary>
    ''' <remarks></remarks>
    <Flags()> Public Enum EnumWindowsForms
        'None = 0'нельзя с таким окном
        <Description(РЕДАКТОР_МОЩНОСТЕЙ)>
        РедакторМощностей = 1
        <Description(РЕДАКТОР_ЦИКЛОГРАММ)>
        РедакторЦиклограмм = 2
        <Description(ПРОИГРЫВАТЕЛЬ_ЦИКЛОГРАММ)>
        ПроигрывательЦиклограмм = 4
    End Enum

    ''' <summary>
    ''' Подготовить словарь перечислений для рабочих Окон,
    ''' при которых загрузка этих окон не возможна
    ''' </summary>
    Public Shared LimitationWindows As Dictionary(Of String, EnumWindowsForms) = New Dictionary(Of String, EnumWindowsForms) From {
            {РЕДАКТОР_МОЩНОСТЕЙ,
                              EnumWindowsForms.РедакторЦиклограмм Or
                              EnumWindowsForms.ПроигрывательЦиклограмм},
            {РЕДАКТОР_ЦИКЛОГРАММ,
                              EnumWindowsForms.ПроигрывательЦиклограмм Or
                              EnumWindowsForms.РедакторМощностей},
            {ПРОИГРЫВАТЕЛЬ_ЦИКЛОГРАММ,
                              EnumWindowsForms.РедакторЦиклограмм Or
                              EnumWindowsForms.РедакторМощностей}
        }

    Private Shared ReadOnly ChildWindows As Dictionary(Of String, CreatorMdiChildrenWindow) = New Dictionary(Of String, CreatorMdiChildrenWindow) From {
        {РЕДАКТОР_МОЩНОСТЕЙ, New CreatorEditorPower},
        {РЕДАКТОР_ЦИКЛОГРАММ, New CreatorEditorCyclogram},
        {ПРОИГРЫВАТЕЛЬ_ЦИКЛОГРАММ, New CreatorPlayerCyclogram}
    }

    ''' <summary>
    ''' Добавление окна в коллекцию и вывод в родительское Окно
    ''' </summary>
    ''' <param name="typeForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateNewForm(inMainForm As FrmMain, ByVal typeForm As String) As Boolean
        Dim CAPTION As String = $"Загрузка нового окна в процедуре: {NameOf(CreateNewForm)}"

        If mTabledForms.ContainsKey(typeForm) Then
            Dim text As String = String.Format("Окно с именем {0} уже загружено!", typeForm)
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Dim newForm As FrmMain = Nothing

        Try
            Dim mWorkWithEnum As New WorkWithEnum
            Dim messageWarning As String = mWorkWithEnum.CheckPattern(typeForm, mTabledForms.Keys.ToList)

            ' Клиентский код работает с экземпляром конкретного создателя, хотя и
            ' через его базовый интерфейс. Пока клиент продолжает работать с
            ' создателем через базовый интерфейс, можно передать ему любой
            ' подкласс создателя.
            If messageWarning = String.Empty Then
                newForm = CType(ChildWindows(typeForm).GetWindow(inMainForm), FrmMain)
            Else
                ShowMessageWarning(typeForm, messageWarning, CAPTION)
                Return False
            End If

            If newForm.Tag.ToString <> typeForm Then
                Dim text As String = String.Format("Tag окна {0} отличается от имени формы в перечислителе: {1}", newForm.Tag, typeForm)
                MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If

            inMainForm.AddChildWindow(newForm)
            mTabledForms.Add(newForm.Tag.ToString, newForm)
            mFormsCounter += 1

            ' здесь провести проверку на корректность и только если прошло продолжить
            If mTabledForms.ContainsKey(typeForm) Then
                ' добавить обработчик события Closed новой формы, которое используется здесь 
                ' чтобы знать когда она закрывается и обработать из централизованного места
                AddHandler newForm.Closed, AddressOf FormWindowsManager.PanelBaseForm_Closed
                ' добавить обработчик события SaveWhileClosingCancelled такой чтобы знать
                ' использование прерывания Cancel button когда было напоминание сохранения несохраненных данных
                'AddHandler frm.SaveWhileClosingCancelled, AddressOf Forms.PanelBaseForm_SaveWhileClosingCancelled
                ' добавить обработчик события ExitApplicaiton чтобы знать когда необходимо выгрузить
                ' приложение выбрав Exit menu из формы
                'AddHandler frm.ExitApplication, AddressOf Forms.PanelBaseForm_ExitApplication
                newForm.Show() ' проверка успешна, значит показать форму 

                Return True
            Else
                Return False
            End If
        Catch exp As Exception
            Dim text As String = exp.ToString
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error)

            If FormWindowsManager.Count = 0 Then
                ' передать снова ошибку в Main, где возможно выгрузить процесс
                Throw 'exp FxCop
            End If

            If newForm IsNot Nothing Then newForm.Close()

            Return False
        End Try
    End Function

    Private Shared Sub ShowMessageWarning(typeForm As String, messageWarning As String, caption As String)
        Dim text As String = $"Загрузка окна {typeForm} не может быть произведена по следующей причине: {vbCrLf}{messageWarning}{vbCrLf}Устраните препятствующую причину и повторите попытку."
        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Это событие получено когда форма была признана корректной и закрывается
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Shared Sub PanelBaseForm_Closed(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim frm As Form = CType(sender, Form)

            ' Удалить обработчики событий, добавленных при создании формы
            RemoveHandler frm.Closed, AddressOf FormWindowsManager.PanelBaseForm_Closed
            'RemoveHandler frm.SaveWhileClosingCancelled, AddressOf Forms.PanelBaseForm_SaveWhileClosingCancelled
            'RemoveHandler frm.ExitApplication, AddressOf Forms.PanelBaseForm_ExitApplication

            ' вызвать функцию очистки 
            'FormsPanelManager.FormClosed(frm)
            ' удалить форму которая закрывается из внутренней коллекции
            'm_КоллекцияПанелейМоториста.Remove(frm.GetHashCode.ToString())
            CType(frm.MdiParent, FrmMain).UnCheckedListView(frm.Tag.ToString)
            mTabledForms.Remove(frm.Tag.ToString)
            mFormsCounter -= 1

            ' если не имеется более форм, то выгрузить процесс
            ' это вызывается только из добавленных в коллекцию форм
            ' корневая форма это событие не вызывает
            'If m_КоллекцияПанелейМоториста.Count = 0 Then
            '    Application.Exit()
            ''End If
        Catch exp As Exception
            Dim CAPTION As String = $"Процедура: {NameOf(PanelBaseForm_Closed)}"
            Dim text As String = exp.ToString
            MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class