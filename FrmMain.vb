Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports BaseForm
Imports CycleCharge.FormWindowsManager
Imports MathematicalLibrary

' в этой форме организовать счетчик перемещений
' имя класса состоит из имени DLL (varName) и имени главной формы визуального наследования (FrmMain)
' Dim ClassName As String = varName & ".FrmMain"
' имя класса DLL и тег визуально наследуемой формы должны совпадать (в данном случае Me.Tag=Chamber)
' assembly name  и root namespace - FormOne на странице свойств проекта также совпадает
Public Class FrmMain
    Public WithEvents MyClassCalculation As ClassCalculation ' локальная переменная для свойства ClassCalculation
    'Private mClassCalculation As IClassCalculation
    Public Overrides Property ClassCalculation() As IClassCalculation
        Get
            Return MyClassCalculation
        End Get
        Set(ByVal value As IClassCalculation)
            MyClassCalculation = CType(value, ClassCalculation)
        End Set
    End Property

    Private mOwnCatalogue As String ' = IO.Path.Combine(IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), Me.Tag)
    Public Overrides Property OwnCatalogue() As String
        Get
            Return mOwnCatalogue
        End Get
        Set(ByVal value As String)
            mOwnCatalogue = value

            If Not Directory.Exists(mOwnCatalogue) Then
                MessageBox.Show(Me, $"Рабочий каталог {OwnCatalogue} для модуля расчета отсутствует. Необходимо его скопировать.",
                                Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'System.Windows.Forms.Application.Exit()
                'System.Environment.Exit(0)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Видима DLL или нет, т.е. имеет ли другие окна или она только вычисляет
    ''' </summary>
    ''' <remarks></remarks>
    Private mIsDLLVisible As Boolean = False
    Public Overrides ReadOnly Property IsDLLVisible() As Boolean
        Get
            Return mIsDLLVisible
        End Get
    End Property

    Public Overrides Sub GetValueTuningParameters()
        If MyClassCalculation IsNot Nothing Then MyClassCalculation.GetValuesTuningParameters()
    End Sub

    ''' <summary>
    ''' Флаг что происходит обновление записей таблиц из-за добавления/удаления новых агрегатов.
    ''' Получение расчётных параметров связанных с ними временно не возможен.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsReloadParameters As Boolean

#Region "MDI"
    Public Property PathDBaseCycle As String
    Public Property PathResourses As String
    Public Property PathChannels As String
    Private Enum SampleViewMode
        Simple
        AdvancedBottom
        AdvancedRight
        AdvancedLeft
    End Enum
    Private viewMode As SampleViewMode

    Private Const DBaseCycle As String = "Cycle.mdb"
    Private Const DBaseChannels As String = "Channels.mdb"

    ''' <summary>
    ''' Режим мультидокумент
    ''' </summary>
    Private IsUseWindowManager As Boolean = True
    '--- Классы -------------------------------------------------------------
    Private mWorkWithEnum As WorkWithEnum
    Private mManagerChargeParameters As ChargeParameters
#End Region

#Region "FrmMain"
    Private Sub FrmMain_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        mOwnCatalogue = Path.Combine(Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), Me.Tag.ToString)

        'WindowManagerPanel1.AutoDetectMdiChildWindows = False
        'MyBase.WindowManagerPanel1.CloseAllWindows

        ' выполняется после MyBase_Load
        mIsDLLVisible = True 'False ' это свойство определяет поведение расчетного модуля

        If mIsDLLVisible Then
            'Dim m_ChildFormNumber As Integer
            'For I As Integer = 1 To 1
            '    Dim ChildForm As New Explorer 'System.Windows.Forms.Form
            '    ' сделать дочерним для этой MDI формы перед её выводом.
            '    ChildForm.MdiParent = Me

            '    m_ChildFormNumber += 1
            '    ChildForm.Text = "Window " & m_ChildFormNumber
            '    ChildForm.Show()
            '    'MyBase.WindowManagerPanel1.SetActiveWindow(CType(ChildForm, System.Windows.Forms.Form))
            '    'MyBase.WindowManagerPanel1.AddWindow(ChildForm)
            'Next

            'MyBase.Manager.ПутьКБазеПараметров = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) & "\Ресурсы\МодулиРасчета\" & Me.Name & ".mdb"
            ' каталог нужен только для видимых форм, где производится сохранение результатов работы
            OwnCatalogue = Path.Combine(MyBase.Manager.PathCatalog, Tag.ToString)
            PathResourses = OwnCatalogue
        End If

        MyBase.FrmBaseLoad()
        PathDBaseCycle = Path.Combine(PathResourses, DBaseCycle)
        PathChannels = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(PathResourses)), DBaseChannels)
        If Not CheckFiles() Then Exit Sub

        ' сделать невозможным закрытие формы
        Manager.LoadConfiguration() ' из XML

        ' идет вслед за Me.Manager.СчитатьНастройки()
        ' в VarFormDetermineDevices инициализируется ПараметрыПоляНакопленные, которая используется в конструкторе New ClassCalculation(
        MyClassCalculation = New ClassCalculation(Manager) With {.MainFomMdiParent = Me}
        ClassCalculation = MyClassCalculation

        Manager.FillCombo()

        mManagerChargeParameters = New ChargeParameters With {.MainFomMdiParent = Me}
        ReloadParameters()

        ' если какое-то сообщение будет до загрузки сеток то перерисовка их будет вызывать исключения, поэтому
        ' MyClassCalculation.GetValuesTuningParameters() идет после Me.Manager.FillCombo()
        MyClassCalculation.GetValuesTuningParameters()

        'If Not mIsDLLVisible Then Me.Hide()

        '' установить фокус вначале на mdi child
        ''Me.MdiChildren(0).BringToFront()
        '' эквивалентный метод: 
        ''WindowManagerPanel1.SetActiveWindow(MdiChildren.Count - 1)
        ''рекомендуемый (хотя и не необходимый) использовать WindowManager methods
        'Me.WindowManagerPanel1.SetActiveWindow(CType(VarFormDetermineDevices, System.Windows.Forms.Form))
        'Application.DoEvents()
        'Thread.Sleep(10000)
        'Application.DoEvents()

        'WindowManagerPanel1.ShowCloseButton = True
        'WindowManagerPanel1.ShowLayoutButtons = True

        mWorkWithEnum = New WorkWithEnum
        InitializeSampleView()
        ' контролировать изменение сетевого интерфейса
        ' Отслеживание изменение пользовательских настроек
        AddHandler Microsoft.Win32.SystemEvents.UserPreferenceChanged, New Microsoft.Win32.UserPreferenceChangedEventHandler(AddressOf Form_UserPreferenceChanged)

        DoubleBuffered = True ' избавиться от мерцания
        ToolBoxPanel.Visible = False
        Me.Text = "Автоматизированное управление загрузками по циклограммам"
    End Sub

    ''' <summary>
    ''' Загрузка агрегатов и создание таблиц параметров.
    ''' Возможна повторная загрузка при изменении агрегатов в редакторе.
    ''' </summary>
    Public Sub ReloadParameters()
        IsReloadParameters = True
        mManagerChargeParameters.LoadParameters(PathDBaseCycle)
        ' затем связать коллекцию расчётных параметров с контролами
        Me.PopulateCalculatedParameters()
        IsReloadParameters = False
    End Sub

    ''' <summary>
    ''' Сохранить положение окна в ресурсах
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Проверить выключение производит пользователь или Панель Управления
        'Dim messageBoxVB As New System.Text.StringBuilder()
        'messageBoxVB.AppendFormat("{0} = {1}", "CloseReason", e.CloseReason)
        'messageBoxVB.AppendLine()
        'messageBoxVB.AppendFormat("{0} = {1}", "Cancel", e.Cancel)
        'messageBoxVB.AppendLine()
        'MessageBox.Show(messageBoxVB.ToString(), "FormClosing Event")

        'If e.CloseReason = CloseReason.UserClosing Then
        '    Const MESSAGE As String = "Вы действительно хотите завершить работу всех Окон приложения?"
        '    Const CAPTION As String = "Внимание!"
        '    Dim Buttons As MessageBoxButtons = MessageBoxButtons.YesNo
        '    Dim Result As DialogResult = MessageBox.Show(MESSAGE, CAPTION, Buttons, MessageBoxIcon.Question)

        '    If Result = DialogResult.No Then
        '        'Close()
        '        e.Cancel = True
        '    End If
        'End If

        If Not Me.IsWindowClosed Then e.Cancel = True

        If e.Cancel = False Then
            ' закрыть все окна через WindowManagerPanel1
            If IsUseWindowManager Then
                CType(WindowManagerPanel1.AuxiliaryWindow, FormChildAux).CloseAllCheckedWindows()
            Else
                Dim _ChildAuxForm As FormChildAux = TryGetChildAuxForm()

                If _ChildAuxForm IsNot Nothing Then
                    _ChildAuxForm.CloseAllCheckedWindows()
                End If
            End If
        End If

        Dim channelLast As String = "Channel" & Me.MyClassCalculation.TuningParams.StandNumber.DigitalValue.ToString  ' имя последней таблицы каналов данного стенда
        mManagerChargeParameters.UpdateTableCycleFromSettingsChannels(PathChannels, channelLast, PathDBaseCycle)
    End Sub

    Private Sub FrmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        mManagerChargeParameters = Nothing
    End Sub

#Region "NetworkInterface Online Handling"
    Private Sub Form_UserPreferenceChanged(sender As Object, e As Microsoft.Win32.UserPreferenceChangedEventArgs)
        If Font IsNot SystemFonts.IconTitleFont Then
            ' откликается только во время выполнения
            Font = SystemFonts.IconTitleFont
            PerformAutoScale()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Заполнить Расчетные параметры на основании каналов ManagerChargeParameters
    ''' </summary>
    Private Sub PopulateCalculatedParameters()
        Me.MyClassCalculation.CalculatedParams.CalcDictionary.Clear()

        For Each itemChargeParameter As ChargeParameter In mManagerChargeParameters
            Me.MyClassCalculation.CalculatedParams.CalcDictionary.Add(itemChargeParameter.NameParameter, New Parameter With {.Name = itemChargeParameter.NameParameter})
        Next
    End Sub

    ''' <summary>
    ''' Проверка наличия файлов базы данных и конфигураций
    ''' </summary>
    Private Function CheckFiles() As Boolean
        Dim success As Boolean = True

        If FileNotExists(PathDBaseCycle) Then
            MessageBox.Show($"В каталоге нет файла {DBaseCycle}!",
                            NameOf(CheckFiles), MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Environment.Exit(0) ' End
            success = False
        End If

        If FileNotExists(PathChannels) Then
            MessageBox.Show($"В каталоге нет файла {PathChannels}!",
                            NameOf(CheckFiles), MessageBoxButtons.OK, MessageBoxIcon.Error)
            success = False
            'Environment.Exit(0) ' End
        End If

        Return success
    End Function

#Region "MDI интерфейс"
    ''' <summary>
    ''' Настройка вида отображения мультидокумента
    ''' </summary>
    Private Sub InitializeSampleView()
        If IsUseWindowManager Then
            InitializeUsingWindowManager()
        Else
            InitializeUsingClassicMDI()
        End If
    End Sub

    ''' <summary>
    ''' Инициализация табличного вида
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeUsingWindowManager()
        ' показать WindowManagerPanel
        WindowManagerPanel1.AutoDetectMdiChildWindows = True
        WindowManagerPanel1.Visible = True

        ' показать Window Menu (смотри WindowMenuItem_Popup для большего кол. деталей)
        WindowMenuItem.Visible = True
        ' скрыть классическое MDI Window Menu если оно видимо
        ClassicMdiWindowMenuItem.Visible = False

        SetupWindowManagerProperties(SampleViewMode.AdvancedRight)

        ' установить фокус на первое окно
        ' можно также просто использовать Form.Show/Form.BringToFront, но здесь меньше моргание
        'If MdiChildren.Length > 2 Then
        '    WindowManagerPanel1.SetActiveWindow(0)
        'End If

        ' установить фокус на первое дочернее окно mdi
        'MdiChildren(0).BringToFront()
        ' Подобный метод: 
        'WindowManagerPanel1.SetActiveWindow(0)
        ' лучше использовать методы WindowManager
    End Sub

    ''' <summary>
    ''' Инициализация классического MDI
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeUsingClassicMDI()
        ' выключить WindowManagerPanel
        WindowManagerPanel1.AutoDetectMdiChildWindows = False
        WindowManagerPanel1.Visible = False

        ' скрыть специальное Window Menu
        WindowMenuItem.Visible = False
        ' показать Classic MDI Window Menu
        ClassicMdiWindowMenuItem.Visible = True

        ' настроить менеджер
        SetupWindowManagerProperties(SampleViewMode.Simple)

        LoadAuxWindow()
        'LayoutMdi(MdiLayout.Cascade)
        СозданиеМенюДляОконПриложения()

        ' установить фокус на первое окно
        MdiChildren(0).BringToFront()
    End Sub

    ''' <summary>
    ''' Настройка свойств менеджера окон
    ''' </summary>
    ''' <param name="inViewMode"></param>
    ''' <remarks></remarks>
    Private Sub SetupWindowManagerProperties(ByVal inViewMode As SampleViewMode)
        Select Case inViewMode
            Case SampleViewMode.Simple
                ToolBoxPanel.Width = 100
                ' освободить пристыковку вспомогательного окна 
                If WindowManagerPanel1.AuxiliaryWindow IsNot Nothing Then
                    WindowManagerPanel1.AuxiliaryWindow.Close()
                    WindowManagerPanel1.AuxiliaryWindow = Nothing
                End If

                With WindowManagerPanel1
                    .Orientation = MDIWindowManager.WindowManagerOrientation.Top
                    ' следующие свойства просто обычные настройки в дезайн режиме, но можно настроить и программным путём
                    .AllowUserVerticalRepositioning = False
                    .Top = .GetMDIClientAreaBounds.Top
                    .ShowTitle = False
                    .Height = 26
                    .AutoHide = True
                End With
            Case SampleViewMode.AdvancedBottom, SampleViewMode.AdvancedRight, SampleViewMode.AdvancedLeft
                ToolBoxPanel.Width = 100

                With WindowManagerPanel1
                    ' следующие свойства просто обычные настройки в дезайн режиме, но можно настроить и программным путём
                    .ShowTitle = True
                    .Height = 55
                    .AutoHide = False

                    Select Case inViewMode
                        Case SampleViewMode.AdvancedBottom
                            .Orientation = MDIWindowManager.WindowManagerOrientation.Bottom
                            .AllowUserVerticalRepositioning = True
                            .Top = 400
                        Case SampleViewMode.AdvancedRight
                            .Orientation = MDIWindowManager.WindowManagerOrientation.Right
                            .AllowUserVerticalRepositioning = False
                            .Top = .GetMDIClientAreaBounds.Top
                            .Width = Width - 200
                        Case SampleViewMode.AdvancedLeft
                            .Orientation = MDIWindowManager.WindowManagerOrientation.Left
                            .AllowUserVerticalRepositioning = False
                            .Top = .GetMDIClientAreaBounds.Top
                            .Width = Width - 200
                    End Select

                    ' настройка вспомогательного окна будут управляться WindowManagerPanel но не показывают их в табличных закладках
                    ' взамен это пристыковка - как сорт оперирования порядком окон достигается 2 или 3 панельным приложением
                    If WindowManagerPanel1.AuxiliaryWindow Is Nothing Then
                        Dim frm As New FormChildAux() With {.FormBorderStyle = Windows.Forms.FormBorderStyle.None}

                        WindowManagerPanel1.AuxiliaryWindow = frm
                        frm.Show()
                        'WindowManagerPanel1.AuxiliaryWindow.Show()
                        'If Not HasChildren Then
                        ' загружены подставная WindowManager.DummyForm и ChildAuxForm формы
                        If Not MdiChildren.Length > 2 Then ' ни какая панель не была до этого загружена
                            СозданиеМенюДляОконПриложения()
                        Else
                            ''''''''''''  ShowFiles(strПутьПанелиМоториста) ' создать список
                            PopulateListChildWindowItems()
                            GetChildAuxForm.ListViewItemsRebuildChecked() 'если панель уже загружена, то отметить её в списке
                        End If
                    End If

                    If inViewMode = SampleViewMode.AdvancedBottom Then
                        .AutoHide = True
                    Else
                        .AutoHide = False
                    End If
                End With
        End Select

        viewMode = inViewMode
    End Sub

    Private Sub CloseAllChildren()
        If IsUseWindowManager Then
            WindowManagerPanel1.CloseAllWindows()
        Else
            For Each frm As Form In MdiChildren
                If Not (TypeOf frm Is FormChildAux) Then
                    frm.Close()
                End If
            Next frm
            ' впомогательную панель ChildAuxForm закрыть в последнюю очередь, т.к. она нужна для пометок панелей в листе
            For Each frm As Form In MdiChildren
                frm.Close()
            Next frm
        End If
    End Sub

    Public Sub AddChildWindow(ByVal frm As Form)
        frm.MdiParent = Me
        If TypeOf frm Is FormChildAux Then
            frm.Show()
        End If
    End Sub

    Private Sub LoadAuxWindow()
        Dim frm As New FormChildAux
        AddChildWindow(frm)
    End Sub
#End Region
#End Region

    Private Sub MyClassCalculation_DataError(sender As Object, e As DataErrorEventArgs) Handles MyClassCalculation.DataError
        ' sender.Manager.ПроверкаСоответствияПройдена - узнать что-то
        ' если произошла ошибка в классе или ошибка была специально сгенерирована то вывести сообщение
        Dim TITLE As String = "Подсчет для модуля " & Text
        MessageBox.Show($"Ошибка в подсчете ClassCalculation.dll:{ Environment.NewLine}{e.Message}{Environment.NewLine}{e.Description}",
                        TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    ''' <summary>
    ''' Выбрать окно для исправления настроек
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DesableAuxForm_And_LoadEditor()
        Dim nameWindowsEditForm As String = String.Empty
        ''Dim mChoiceEditorDialog As New ChoiceEditorDialog()

        'If ChoiceEditorDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    If ChoiceEditorDialog.AppExit Then
        '        'gMainForm.Close() - будет задавать вопрос и можно не закрыть приложение
        '        Application.Exit()
        '        Exit Sub
        '    Else
        '        nameWindowsEditForm = ChoiceEditorDialog.ChoiseEditor
        '    End If
        'End If

        If IsUseWindowManager Then
            CType(WindowManagerPanel1.AuxiliaryWindow, FormChildAux).LoadForm(nameWindowsEditForm)
            WindowManagerPanel1.AuxiliaryWindow.Enabled = False
        Else
            Dim _ChildAuxForm As FormChildAux = TryGetChildAuxForm()

            If _ChildAuxForm IsNot Nothing Then
                _ChildAuxForm.LoadForm(nameWindowsEditForm)
                _ChildAuxForm.Enabled = False
            End If
        End If
    End Sub

    Public Sub СообщениеНаПанель(ByVal message As String)
        TSSLabelStatus.Text = message
    End Sub

    ''' <summary>
    ''' Вывести сообщение об ошибке.
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ShowError(message As String)
        TSSLabelStatus.Text = message

        ' получить из Enum описание Description Окна (или Const Окна)
        Dim descriptionEnum As String = mWorkWithEnum.GetEnumNamesAndDescriptions(EnumWindowsForms.ПроигрывательЦиклограмм)

        ' получить форму
        If FormWindowsManager.Keys.Contains(descriptionEnum) Then
            CType(FormWindowsManager.Item(descriptionEnum), FormPlayerCyclogram).TextError.Text = message
            CType(FormWindowsManager.Item(descriptionEnum), FormPlayerCyclogram).TextError.Visible = True
        End If
    End Sub

#Region "Вычисление и обновление расчётных параметров"
    '''' <summary>
    '''' Получить значения всех величин загрузок в исполняемой циклограмме.
    '''' </summary>
    'Public Sub GetChargeParametersFromCycle()
    '    ' получить из Enum описание Description Окна (или Const Окна)
    '    Dim descriptionEnum As String = mWorkWithEnum.GetEnumNamesAndDescriptions(EnumWindowsForms.ПроигрывательЦиклограмм)

    '    ' получить форму
    '    If FormWindowsManager.Keys.Contains(descriptionEnum) Then
    '        ' отладка
    '        ''Dim rnd As New Random()
    '        'For Each itemChargeParameter As ChargeParameter In mManagerChargeParameters
    '        '    If chargeValue > 200.0 Then chargeValue = 0.0
    '        '    ' отладка
    '        '    itemChargeParameter.ValueChargeParameter = chargeValue 'rnd.Next(-1, 1) ' itemChargeParameter.NumberChannel +1
    '        '    chargeValue += 1.0
    '        'Next

    '        CType(FormWindowsManager.Item(descriptionEnum), FormPlayerCyclogram).GetChargeParametersFromCycle()
    '    End If
    'End Sub
#End Region

#Region "Event"

#Region "Шаблонные"
    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        StatusStripForm.Visible = StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Закрыть все дочерние формы указанного родителя.
        For Each childForm As Form In MdiChildren
            childForm.Close()
        Next
    End Sub
#End Region

    Private Sub WindowMenuItem_Popup(ByVal sender As Object, ByVal e As EventArgs) Handles WindowMenuItem.MouseDown 'WindowMenuItem.Paint
        ' Использовать расширение WindowManagerдля загрузки и показа листа всех окон
        ' удалить все старые пункты меню
        'For index As Integer = WindowMenuItem.MenuItems.Count - 1 To 0 Step -1
        '    Dim mnu As MenuItem = WindowMenuItem.MenuItems.Item(index)
        '    If TypeOf mnu Is MDIWindowManager.WrappedWindowMenuItem Then
        '        WindowMenuItem.MenuItems.Remove(mnu)'MenuItem старый тип и в DropDownItems не работает
        '    End If
        'Next index
        Dim listTSMenuItem As New List(Of ToolStripMenuItem)

        For Each tempMenuItem As ToolStripItem In WindowMenuItem.DropDownItems
            If TypeOf tempMenuItem Is ToolStripMenuItem Then
                ' то же самое
                'If (item.GetType() Is GetType(ToolStripMenuItem)) Then
                If tempMenuItem.Tag.ToString = "WrappedWindowMenuItem" Then
                    listTSMenuItem.Add(CType(tempMenuItem, ToolStripMenuItem))
                    ' удалять в цикле по коллекции нельзя
                    'WindowMenuItem.DropDownItems.Remove(menuItem)
                End If
            End If
        Next

        For Each tempMenuItem As ToolStripMenuItem In listTSMenuItem
            WindowMenuItem.DropDownItems.Remove(tempMenuItem)
        Next

        Dim copyWindowMoreWindowsMenuItem As ToolStripMenuItem = WindowMoreWindowsMenuItem
        WindowMenuItem.DropDownItems.Remove(WindowMoreWindowsMenuItem)

        ' задать первые 9 оконных пунктов и добавить их в меню
        Dim menuItems As MenuItem() = WindowManagerPanel1.GetAllWindowsMenu(9, True)

        If menuItems IsNot Nothing AndAlso menuItems.Length > 0 Then
            'WindowMenuItem.MenuItems.AddRange(menuItems)
            For Each mnu As MenuItem In menuItems
                ' создадим копию с поведением MenuItem но типа ToolStripMenuItem
                Dim copyTSMenuItem As New ToolStripMenuItem

                If mnu.Checked Then
                    copyTSMenuItem.Checked = True
                    copyTSMenuItem.CheckState = CheckState.Checked
                End If

                copyTSMenuItem.Name = mnu.Name
                copyTSMenuItem.Size = New Size(204, 22)
                copyTSMenuItem.Text = mnu.Text
                copyTSMenuItem.ToolTipText = mnu.Text
                copyTSMenuItem.Tag = "WrappedWindowMenuItem"
                AddHandler copyTSMenuItem.Click, AddressOf OnTSMenuItemMouseClick
                WindowMenuItem.DropDownItems.Add(copyTSMenuItem)
            Next
        End If

        ' переместить "more windows" пункт меню в конец
        'WindowMoreWindowsMenuItem.Index = WindowMenuItem.MenuItems.Count - 1
        WindowMenuItem.DropDownItems.Add(copyWindowMoreWindowsMenuItem)
    End Sub

    Private Sub OnTSMenuItemMouseClick(ByVal sender As Object, ByVal e As EventArgs)
        ' перенаправить обработку из ToolStripMenuItem в MenuItem для WindowManagerPanel1
        ' соответствие находится по .Text
        Dim tsMenuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim menuItems As MenuItem() = WindowManagerPanel1.GetAllWindowsMenu(9, True)

        For Each mnu As MenuItem In menuItems
            If mnu.Text = tsMenuItem.Text Then
                mnu.PerformClick()
                Exit For
            End If
        Next
    End Sub

#Region "Управление одиночным окном"
    Private Sub WindowHTileMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WindowHTileMenuItem.Click
        WindowManagerPanel1.HTileWrappedWindow(WindowManagerPanel1.GetActiveWindow())
    End Sub

    Private Sub WindowTileMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WindowTileMenuItem.Click
        WindowManagerPanel1.TileOrUntileWrappedWindow(WindowManagerPanel1.GetActiveWindow())
    End Sub

    Private Sub WindowPopOutMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WindowPopOutMenuItem.Click
        WindowManagerPanel1.PopOutWrappedWindow(WindowManagerPanel1.GetActiveWindow())
    End Sub

    Private Sub WindowCloseAllMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WindowCloseAllMenuItem.Click
        CloseAllChildren()
    End Sub
#End Region

    Private Sub WindowMoreWindowsMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles WindowMoreWindowsMenuItem.Click
        WindowManagerPanel1.ShowAllWindowsDialog()
    End Sub

#Region "Дополнительная панель"
    Private Sub ViewSimpleMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewSimpleMenuItem.Click
        SetupWindowManagerProperties(SampleViewMode.Simple)
    End Sub

    Private Sub ViewAdvRightMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewAdvRightMenuItem.Click
        SetupWindowManagerProperties(SampleViewMode.AdvancedRight)
    End Sub

    Private Sub ViewAdvBottomMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewAdvBottomMenuItem.Click
        SetupWindowManagerProperties(SampleViewMode.AdvancedBottom)
    End Sub

    Private Sub ViewAdvLeftMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewAdvLeftMenuItem.Click
        SetupWindowManagerProperties(SampleViewMode.AdvancedLeft)
    End Sub
#End Region

    Private Sub ViewShowTitleMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewShowTitleMenuItem.Click
        WindowManagerPanel1.ShowTitle = Not WindowManagerPanel1.ShowTitle

        If WindowManagerPanel1.ShowTitle Then
            WindowManagerPanel1.Height = 42
        Else
            WindowManagerPanel1.Height = 26
        End If
    End Sub

#Region "TabStile"
    Private Sub ViewShowIconsMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewShowIconsMenuItem.Click
        WindowManagerPanel1.ShowIcons = Not WindowManagerPanel1.ShowIcons
    End Sub

    Private Sub ViewShowLayoutButtonsMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewShowLayoutButtonsMenuItem.Click
        WindowManagerPanel1.ShowLayoutButtons = Not WindowManagerPanel1.ShowLayoutButtons
    End Sub

    Private Sub ViewShowCloseButtonMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewShowCloseButtonMenuItem.Click
        WindowManagerPanel1.ShowCloseButton = Not WindowManagerPanel1.ShowCloseButton
    End Sub

    Private Sub ViewTabStylesClassicMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewTabStylesClassicMenuItem.Click
        WindowManagerPanel1.Style = MDIWindowManager.TabStyle.ClassicTabs
    End Sub

    Private Sub ViewTabStylesModernMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewTabStylesModernMenuItem.Click
        WindowManagerPanel1.Style = MDIWindowManager.TabStyle.ModernTabs
    End Sub

    Private Sub ViewTabStylesFlatHiliteMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewTabStylesFlatHiliteMenuItem.Click
        WindowManagerPanel1.Style = MDIWindowManager.TabStyle.FlatHiliteTabs
    End Sub

    Private Sub ViewTabStylesAngledHiliteMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewTabStylesAngledHiliteMenuItem.Click
        WindowManagerPanel1.Style = MDIWindowManager.TabStyle.AngledHiliteTabs
    End Sub

    'Private Sub ViewTabStylesMoreInfoMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    MsgBox("Здесь по умолчанию Таб стиль, представленный WindowManagerPanel. Можно переопределить и полностью управлять Таб перерисовкой в событии <<TabPaint>>." & Environment.NewLine & Environment.NewLine _
    '& "Дополнительно можно создать полностью пользовательский 'TabProviders.' MDIWindowManager сейчас содержит дополнительно TabProvider вызываемый SystemTabsProvider, который использует стандартный .NET TabControl для представления ТАБ. " _
    '& "Для удобства можно совершать так же представление Alternate Tabs Sample используя TabRenderMode свойство в дизайнере." & Environment.NewLine & Environment.NewLine _
    ', MsgBoxStyle.Information)
    'End Sub

    Private Sub ViewButtonStylesStandardMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewButtonStylesStandardMenuItem.Click
        WindowManagerPanel1.ButtonRenderMode = MDIWindowManager.ButtonRenderMode.Standard
    End Sub

    Private Sub ViewButtonStylesSystemMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewButtonStylesSystemMenuItem.Click
        WindowManagerPanel1.ButtonRenderMode = MDIWindowManager.ButtonRenderMode.System
    End Sub

    Private Sub ViewButtonStylesProMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewButtonStylesProMenuItem.Click
        WindowManagerPanel1.ButtonRenderMode = MDIWindowManager.ButtonRenderMode.Professional
    End Sub
#End Region

    Private Sub SwitchToClassicMdiMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SwitchToClassicMdiMenuItem.Click
        CloseAllChildren()
        IsUseWindowManager = Not IsUseWindowManager
        InitializeSampleView()
    End Sub

    Private Sub ViewMenuItem_Popup(ByVal sender As Object, ByVal e As EventArgs) Handles ViewMenuItem.Paint
        ' установить Checked свойства только во время перерисовки
        ViewSimpleMenuItem.Enabled = IsUseWindowManager
        ViewAdvRightMenuItem.Enabled = IsUseWindowManager
        ViewAdvBottomMenuItem.Enabled = IsUseWindowManager
        ViewAdvLeftMenuItem.Enabled = IsUseWindowManager
        ViewAppearanceMenuItem.Enabled = IsUseWindowManager

        ViewSimpleMenuItem.Checked = False
        ViewAdvRightMenuItem.Checked = False
        ViewAdvBottomMenuItem.Checked = False
        ViewAdvLeftMenuItem.Checked = False

        Select Case viewMode
            Case SampleViewMode.Simple
                ViewSimpleMenuItem.Checked = True
            Case SampleViewMode.AdvancedRight
                ViewAdvRightMenuItem.Checked = True
            Case SampleViewMode.AdvancedBottom
                ViewAdvBottomMenuItem.Checked = True
            Case SampleViewMode.AdvancedLeft
                ViewAdvLeftMenuItem.Checked = True
        End Select

        ViewShowTitleMenuItem.Checked = WindowManagerPanel1.ShowTitle
        ViewShowIconsMenuItem.Checked = WindowManagerPanel1.ShowIcons
        ViewShowLayoutButtonsMenuItem.Checked = WindowManagerPanel1.ShowLayoutButtons
        ViewShowCloseButtonMenuItem.Checked = WindowManagerPanel1.ShowCloseButton

        ViewTabStylesClassicMenuItem.Checked = False
        ViewTabStylesModernMenuItem.Checked = False
        ViewTabStylesFlatHiliteMenuItem.Checked = False
        ViewTabStylesAngledHiliteMenuItem.Checked = False

        Select Case WindowManagerPanel1.Style
            Case MDIWindowManager.TabStyle.ClassicTabs
                ViewTabStylesClassicMenuItem.Checked = True
            Case MDIWindowManager.TabStyle.ModernTabs
                ViewTabStylesModernMenuItem.Checked = True
            Case MDIWindowManager.TabStyle.FlatHiliteTabs
                ViewTabStylesFlatHiliteMenuItem.Checked = True
            Case MDIWindowManager.TabStyle.AngledHiliteTabs
                ViewTabStylesAngledHiliteMenuItem.Checked = True
        End Select

        ViewButtonStylesStandardMenuItem.Checked = False
        ViewButtonStylesSystemMenuItem.Checked = False
        ViewButtonStylesProMenuItem.Checked = False

        Select Case WindowManagerPanel1.ButtonRenderMode
            Case MDIWindowManager.ButtonRenderMode.Standard
                ViewButtonStylesStandardMenuItem.Checked = True
            Case MDIWindowManager.ButtonRenderMode.System
                ViewButtonStylesSystemMenuItem.Checked = True
            Case MDIWindowManager.ButtonRenderMode.Professional
                ViewButtonStylesProMenuItem.Checked = True
        End Select

        If IsUseWindowManager Then
            SwitchToClassicMdiMenuItem.Text = "Переключить В Классический Мультидокумент"
        Else
            SwitchToClassicMdiMenuItem.Text = "Переключить В Табличный Мультидокумент"
        End If
    End Sub

#End Region

#Region "Работа с окнами"
    'Private Sub WindowManagerPanel1_WindowPoppingIn(sender As Object, e As MDIWindowManager.WrappedWindowCancelEventArgs) Handles WindowManagerPanel1.WindowPoppingIn
    '    Console.WriteLine(e.WrappedWindow.Window.Name)
    'End Sub

    'Private Sub WindowManagerPanel1_WindowPoppingOut(sender As Object, e As MDIWindowManager.WrappedWindowCancelEventArgs) Handles WindowManagerPanel1.WindowPoppingOut
    '    Console.WriteLine(e.WrappedWindow.Window.Name)
    'End Sub

    Public Function TryGetChildAuxForm() As FormChildAux
        Dim _ChildAuxForm As FormChildAux = Nothing

        For Each frm As Form In MdiChildren
            If TypeOf frm Is FormChildAux Then
                _ChildAuxForm = CType(frm, FormChildAux)
            End If
        Next

        Return _ChildAuxForm
    End Function

    ''' <summary>
    ''' получить ChildAuxForm форму или создать в случае отсутствия
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetChildAuxForm() As FormChildAux
        Dim _ChildAuxForm As FormChildAux = Nothing

        If IsUseWindowManager Then
            If WindowManagerPanel1.AuxiliaryWindow IsNot Nothing Then
                _ChildAuxForm = CType(WindowManagerPanel1.AuxiliaryWindow, FormChildAux)
            Else
                _ChildAuxForm = New FormChildAux With {
                    .FormBorderStyle = FormBorderStyle.None
                }
                If WindowManagerPanel1.Orientation = MDIWindowManager.WindowManagerOrientation.Top Then
                    WindowManagerPanel1.Orientation = MDIWindowManager.WindowManagerOrientation.Right
                End If

                WindowManagerPanel1.AuxiliaryWindow = _ChildAuxForm
                _ChildAuxForm.Show()
                СозданиеМенюДляОконПриложения()
            End If
        Else
            For Each frm As Form In MdiChildren
                If TypeOf frm Is FormChildAux Then
                    _ChildAuxForm = CType(frm, FormChildAux)
                End If
            Next

            If _ChildAuxForm Is Nothing Then
                _ChildAuxForm = New FormChildAux
                AddChildWindow(_ChildAuxForm)
                СозданиеМенюДляОконПриложения()
            End If

            ' не работает т.к. ChildAuxForm может быть по любым индексом
            'If TypeOf MdiChildren(0) Is ChildAuxForm Then
            '    _ChildAuxForm = CType(MdiChildren(0), ChildAuxForm)
            'Else
            '    _ChildAuxForm = New ChildAuxForm
            '    AddChildWindow(_ChildAuxForm)
            'End If
        End If

        Return _ChildAuxForm
    End Function

    Public Sub СозданиеМенюДляОконПриложения()
        GetChildAuxForm.RemoveHandlerListViewItemChecked()

        Try
            УдалитьВсеМенюПанелейОкон()
            ' ShowFiles(strПутьПанелиМоториста)
            PopulateListChildWindowItems()
        Catch ex As Exception
            'Dim CAPTION As String = "Считывание каталога " & strПутьПанелиМоториста
            'Dim text As String = ex.ToString
            'MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        GetChildAuxForm.AddHandlerListViewItemChecked()
    End Sub

    Public Sub UnCheckedListView(ByVal formTag As String)
        GetChildAuxForm.UnCheckedListView(formTag)
    End Sub

    Public Sub УдалитьВсеМенюПанелейОкон()
        Dim arrName() As String

        'If FormWindowsManager.TabledForms IsNot Nothing Then
        If FormWindowsManager.Count > 0 Then
            'Dim keyColl As Dictionary(Of String, frmBasePanelMotorist).KeyCollection = FormsPanelManager.КоллекцияПанелейМоториста.Keys
            Dim keyColl As Dictionary(Of String, Form).KeyCollection = FormWindowsManager.Keys

            'ReDim_arrName(keyColl.Count - 1)
            Re.Dim(arrName, keyColl.Count - 1)
            keyColl.CopyTo(arrName, 0)

            For Each itemKeyPanel As String In arrName
                FormWindowsManager.Item(itemKeyPanel).Close()
            Next

            FormWindowsManager.Clear() 'по идеи она уже чистая
            ' затем очистка коллекции настроек
        End If

        ' далее удалить все пункты меню
        GetChildAuxForm.ClearListViewItems()
    End Sub

    Private Sub PopulateListChildWindowItems()
        Dim indexImageIndex As Integer

        For Each itemStr As String In mWorkWithEnum.ListEnumDescriptions 'СписокОкон
            GetChildAuxForm.AddPanelListViewItems(itemStr, indexImageIndex)
            indexImageIndex += 1
        Next
    End Sub
#End Region

End Class
