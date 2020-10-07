Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports MathematicalLibrary
Imports NationalInstruments.Analysis.Math
Imports NationalInstruments.UI

''' <summary>
''' Редактор тиров изделий и программ испытаний.
''' Редактор циклограмм исполнительных устройств из сконфигурированных мощностей.
''' </summary>
Public Class FormEditorCyclogram
    Implements IMdiChildrenWindow

    Private WithEvents TempPointAnnotation As XYPointAnnotation

    Enum NodeType
        <Description("ТипИзделия")>
        TypeEngine = 1
        <Description("ПрограммаИспытаний")>
        TestProgram = 2
        <Description("ЦиклЗагрузки")>
        SycleDevice = 3
        <Description("ПерекладкиЦикла")>
        SycleStage = 4
        <Description("Неизв.")>
        Unknown
    End Enum
    Private currentNodeType As NodeType

    Private ReadOnly Property MainFomMdiParent As FrmMain Implements IMdiChildrenWindow.MainFomMdiParent
    Private Const HIDE_PANEL As String = "<< Скрыть дополнительную панель всех циклограмм"
    Private Const HOW_PANEL As String = ">> Показать дополнительную панель всех циклограмм"
    Private Const NAME_DEVICE_COLUMN_INDEX As Integer = 2
    Private Const NAME_DEVICE_COLUMN_NAME As String = "ИмяУстройстваDataGridViewTextBoxColumn"
    Private Const NAME_DEVICE_COMBO_BOX_COLUMN_INDEX As Integer = 3
    Private Const NAME_DEVICE_COMBO_BOX_COLUMN_NAME As String = "ИмяУстройстваNewDataGridViewTextBoxColumn"
    Private Const NDEX_INSERT_NumericUpDown As Integer = 7

    ''' <summary>
    ''' отменить события DataGridView2ПрограммаИспытаний.SelectionChanged
    ''' </summary>
    Private isCancelEventDataGridView2TestProgram As Boolean
    ''' <summary>
    ''' отменить события DataGridView3ЦиклУправИсполУстр.SelectionChanged
    ''' </summary>
    Private isCancelEventDataGridView3SycleDevices As Boolean
    ''' <summary>
    ''' флаг изменения циклограммы для вопроса о сохранении
    ''' </summary>
    Private isCycleDirty As Boolean
    ''' <summary>
    ''' заполнение произведено
    ''' </summary>
    Private isTableAdapterFilled As Boolean = False
    ''' <summary>
    ''' контролы наследуются от ToolStripControlHost
    ''' </summary>
    Private tsNumericUpDownTimeDuration As ToolStripNumeric
    ''' <summary>
    ''' суммарное время всех перекладкок
    ''' </summary>
    Private maxSumTimeCycle As Double
    Private lvSelectedIndices, lvPreviousSelectedIndices As Integer ' счётчики положения позиции в листе
    ''' <summary>
    ''' Для запоминания KeyЦиклЗагрузки из текущей строки из таблицы ЦиклЗагрузки3 перед сменой позиции для вопроса сохранения незаписанной перекладки
    ''' </summary>
    Private KeyCycleStage As Integer
    ''' <summary>
    ''' Для запоминания ИмяУстройства из текущей строки из таблицы ЦиклЗагрузки3 перед сменой позиции для вопроса сохранения незаписанной перекладки
    ''' </summary>
    Private memoNameDevice As String
    Private mFont As Font
    Private mboldFont As Font

#Region "Event Handlers Form"
    Public Sub New(inMainFomMdiParent As FrmMain)
        ' Использоать system fonts
        SetFont()
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        MainFomMdiParent = inMainFomMdiParent

        InitializeNumericUpDownTimeDuration()
        InitializeListViewCycle()

        SplitContainerGraf.Panel1Collapsed = True
        SplitContainerCycle.Panel1Collapsed = True
        TSComboBoxTimeUnit.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Сделать шрифт окна жирным
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFont()
        mFont = SystemFonts.DefaultFont
        mboldFont = New Font(mFont, FontStyle.Bold)
        Font = mboldFont
    End Sub

    Private Sub FormEditorCyclogram_Load(sender As Object, e As EventArgs) Handles Me.Load
        ShowMessageOnPanel($"Производится попытка загрузки Окна редактора циклограмм <{Me.Text}>...")
        'CompressDataBase()
        FillAllTableAdapters()
        Icon = Icon.FromHandle(Resources.xine.GetHicon()) ' Установить иконку (признак успешной загрузки)
        SplitContainerTree.SplitterWidth = 6 ' Установить высоту разделителя
        YAxis2.Visible = False
        ' Отслеживание изменение пользовательских настроек
        AddHandler Microsoft.Win32.SystemEvents.UserPreferenceChanged, New Microsoft.Win32.UserPreferenceChangedEventHandler(AddressOf Form_UserPreferenceChanged)
        ShowMessageOnPanel($"Окно редактора циклограмм <{Me.Text}> загружено успешно.")
    End Sub

    Private Sub EditorCyclogramForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CheckIsCycleDirty()
    End Sub

    ''' <summary>
    ''' Отследить изменения пользовательских параметров
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form_UserPreferenceChanged(sender As Object, e As Microsoft.Win32.UserPreferenceChangedEventArgs)
        Try
            If Font IsNot SystemFonts.IconTitleFont Then
                ' действует в период выполнения
                Font = SystemFonts.IconTitleFont
                PerformAutoScale()
            End If
        Catch ex As Exception
            ' проглотить
        End Try
    End Sub

    Private Sub TSMenuItemExit_Click(sender As Object, e As EventArgs) Handles TSMenuItemExit.Click
        Close()
    End Sub

#Region "Events controls"
    ''' <summary>
    ''' Показать панель подсказок навигации по графику
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TSMenuItemNavigationPanel_Click(sender As Object, e As EventArgs) Handles TSMenuItemNavigationPanel.Click
        Static frmHelpGraph As FormHelpGraph

        TSMenuItemNavigationPanel.Checked = Not TSMenuItemNavigationPanel.Checked

        If TSMenuItemNavigationPanel.Checked Then
            frmHelpGraph = New FormHelpGraph(Me)
            frmHelpGraph.Show()
            frmHelpGraph.Activate()
            ShowMessageOnPanel($"Выведено справочное окно навигации по графику <{frmHelpGraph.Text}>.")
        Else
            If frmHelpGraph IsNot Nothing Then
                frmHelpGraph.Close()
            End If
        End If
    End Sub

    Private Sub ListViewЦикл_Resize(sender As Object, e As EventArgs) Handles ListViewCycle.Resize
        If IsHandleCreated Then
            For Each itemColumnHeader As ColumnHeader In ListViewCycle.Columns
                itemColumnHeader.Width = ListViewCycle.Width \ ListViewCycle.Columns.Count - 1
            Next
        End If
    End Sub

    Private Sub TSpMenuItemStatusStrip_CheckedChanged(sender As Object, e As EventArgs) Handles TSMenuItemStatusStrip.CheckedChanged
        StatusStripForm.Visible = TSMenuItemStatusStrip.Checked
    End Sub

    Private Sub TSpMenuItemToolbars_CheckedChanged(sender As Object, e As EventArgs) Handles TSMenuItemToolbars.CheckedChanged
        ToolStripForm.Visible = TSMenuItemToolbars.Checked
    End Sub

    Private Sub TSButtonFindAndReplace_ButtonClick(sender As Object, e As EventArgs) Handles TSButtonFindAndReplace.ButtonClick
        MessageLate()
    End Sub

    Private Sub TSMenuItemNewType_Click(sender As Object, e As EventArgs) Handles TSMenuItemNewType.Click
        BindingNavigatorAddNewItem.PerformClick()
    End Sub

    Private Sub TSMenuItemNewProgramm_Click(sender As Object, e As EventArgs) Handles TSMenuItemNewProgramm.Click
        BindingNavigatorAddNewItem1.PerformClick()
    End Sub

    Private Sub TSMenuItemNewDevice_Click(sender As Object, e As EventArgs) Handles TSMenuItemNewDevice.Click
        ' выделить строку и ячейку и наполнить список
        DataGridView3ЦиклУправИсполУстр.Rows(DataGridView3ЦиклУправИсполУстр.Rows.Count - 1).Selected = True
        Dim DataRowItem As DataGridViewRow = DataGridView3ЦиклУправИсполУстр.Rows(DataGridView3ЦиклУправИсполУстр.Rows.Count - 1)

        Using cell As DataGridViewComboBoxCell = CType(DataRowItem.Cells(NAME_DEVICE_COMBO_BOX_COLUMN_NAME), DataGridViewComboBoxCell)
            PopulateComboBoxCellByNameDevices(DataRowItem, cell)
            DataGridView3ЦиклУправИсполУстр.CurrentCell = cell
        End Using

        DataGridView3ЦиклУправИсполУстр.BeginEdit(False)
    End Sub

    Private Sub TSMenuItemNewCycle_Click(sender As Object, e As EventArgs) Handles TSMenuItemNewCycle.Click
        TSButtonAddRecord.PerformClick()
    End Sub
#End Region
#End Region

#Region "Настройка панели типа графиков, листов"
    ' ''' <summary>
    ' ''' Получить изображение из ресурсов по имени
    ' ''' </summary>
    ' ''' <param name="name"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Shared Function GetBitmapResource(name As String) As Bitmap
    '    Return TryCast(My.Resources.ResourceManager.GetObject(name), Bitmap)
    'End Function

#Region "API Splitter"
    Private Sub SplitContainerTree_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainerTree.Paint
        Dim pct As New ProfessionalColorTable()
        Dim bounds As Rectangle = TryCast(sender, SplitContainer).SplitterRectangle
        Dim squares As Integer
        Const maxSquares As Integer = 9
        Const squareSize As Integer = 4
        Const boxSize As Integer = 2

        ' проверить, что действительно что-то нужно менять
        If (bounds.Width > 0) AndAlso (bounds.Height > 0) Then
            Dim g As Graphics = e.Graphics

            ' установить цвет предоставляемый провайдером
            Dim begin As Color = pct.OverflowButtonGradientMiddle
            Dim [end] As Color = pct.OverflowButtonGradientEnd

            ' проверить, что действительно что-то нужно менять
            Using b As Brush = New LinearGradientBrush(bounds, begin, [end], LinearGradientMode.Vertical)
                g.FillRectangle(b, bounds)
            End Using

            ' вычислить прямоугольник
            If (bounds.Width > squareSize) AndAlso (bounds.Height > squareSize) Then
                squares = CInt(Math.Min((bounds.Width / squareSize), maxSquares))

                ' вычислить начало
                Dim start As Integer = (bounds.Width - (squares * squareSize)) \ 2
                Dim Y As Integer = bounds.Y + 1

                ' задать кисть
                Dim dark As Brush = New SolidBrush(pct.GripDark)
                Dim middle As Brush = New SolidBrush(pct.ToolStripBorder)
                Dim light As Brush = New SolidBrush(pct.ToolStripDropDownBackground)

                ' Рисовать зажим
                For idx As Integer = 0 To squares - 1
                    ' Рисовать 
                    g.FillRectangle(dark, start, Y, boxSize, boxSize)
                    g.FillRectangle(light, start + 1, Y + 1, boxSize, boxSize)
                    g.FillRectangle(middle, start + 1, Y + 1, 1, 1)
                    start += squareSize
                Next

                dark.Dispose()
                middle.Dispose()
                light.Dispose()
            End If
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Вставка пользовательских контролов в ToolStripEditCycle Редактор Перекладок.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeNumericUpDownTimeDuration()
        tsNumericUpDownTimeDuration = New ToolStripNumeric
        tsNumericUpDownTimeDuration.NumericControl.NumericUpDownValue.DecimalPlaces = 0
        tsNumericUpDownTimeDuration.NumericControl.NumericUpDownValue.Maximum = 600
        tsNumericUpDownTimeDuration.NumericControl.NumericUpDownValue.TextAlign = HorizontalAlignment.Center
        tsNumericUpDownTimeDuration.NumericControl.Width = 50
        ToolStripEditCycle.Items.Insert(NDEX_INSERT_NumericUpDown, tsNumericUpDownTimeDuration)
    End Sub

    Friend WithEvents ColumnHeaderStageToNumeric As ColumnHeader
    Friend WithEvents ColumnHeaderTimeDuration As ColumnHeader
    Friend WithEvents ColumnHeaderTimeUnit As ColumnHeader
    Friend WithEvents ColumnHeaderTimeFromStart As ColumnHeader
    Friend WithEvents ColumnHeaderStageToText As ColumnHeader

    ''' <summary>
    ''' Настройка ListViewCycle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeListViewCycle()
        'ListViewCycle.Columns.Clear() 
        ' Установить вид просмотра по умолчанию.
        ListViewCycle.View = View.Details
        ' Запретить пользователю редактировать item text.
        ListViewCycle.LabelEdit = False
        ' Позвольте пользователю перестраивать столбцы.
        ListViewCycle.AllowColumnReorder = False
        ' Показать check boxes.
        ListViewCycle.CheckBoxes = False
        ' Выделение item и subitems когда произведено выделение
        ListViewCycle.FullRowSelect = True
        ' Показать grid lines.
        ListViewCycle.GridLines = True
        ListViewCycle.MultiSelect = True

        ' Назначить ImageList для ListView.
        ListViewCycle.LargeImageList = ImageListCycle
        ListViewCycle.SmallImageList = ImageListCycle

        ' Сортировать элементы{пункты} в списке в возрастающем порядке.
        'ListViewCycle.Sorting = SortOrder.Ascending

        ' Создать columns для items и subitems.
        ColumnHeaderStageToNumeric = New ColumnHeader("StageToNumeric") With {.Text = "Числовое значение"}
        ColumnHeaderTimeDuration = New ColumnHeader("TimeDuration") With {.Text = "Длительность"}
        ColumnHeaderTimeUnit = New ColumnHeader("TimeUnit") With {.Text = "Ед. времени"}
        ColumnHeaderTimeFromStart = New ColumnHeader("FromStart") With {.Text = "С начала"}
        ColumnHeaderStageToText = New ColumnHeader("StageToText") With {.Text = "Величина загрузки"}

        ListViewCycle.Columns.AddRange({ColumnHeaderStageToNumeric, ColumnHeaderTimeDuration, ColumnHeaderTimeUnit, ColumnHeaderTimeFromStart, ColumnHeaderStageToText})

        For Each itemColumnHeader As ColumnHeader In ListViewCycle.Columns
            itemColumnHeader.TextAlign = HorizontalAlignment.Center
            itemColumnHeader.Width = ListViewCycle.Width \ ListViewCycle.Columns.Count - 1
        Next
    End Sub
#End Region

    ''' <summary>
    ''' Заполнить адаптеры доступа к данным.
    ''' Установить обработчики смены и добавления строк.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillAllTableAdapters()
        ' используется одно подключение на все адаптеры
        Dim mOleDbConnection As OleDbConnection = New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
        mOleDbConnection.Open()

        ТипИзделия1TableAdapter.Connection = mOleDbConnection
        ПрограммаИспытаний2TableAdapter.Connection = mOleDbConnection
        ЦиклЗагрузки3TableAdapter.Connection = mOleDbConnection
        ПерекладкиЦикла4TableAdapter.Connection = mOleDbConnection

        ' стандартные методы заполнения, их в коде заменим на созданные выборки по ключу
        ТипИзделия1TableAdapter.Fill(CycleDataSet1.ТипИзделия1)

        mOleDbConnection.Close()
        isTableAdapterFilled = True

        ' переупорядочить колонки из-за возможного смещениия после мастера настройки
        DataGridView3ЦиклУправИсполУстр.Columns(NAME_DEVICE_COLUMN_NAME).DisplayIndex = NAME_DEVICE_COLUMN_INDEX
        DataGridView3ЦиклУправИсполУстр.Columns(NAME_DEVICE_COMBO_BOX_COLUMN_NAME).DisplayIndex = NAME_DEVICE_COMBO_BOX_COLUMN_INDEX

        ' обработчики смены и добавления строк
        AddHandler CycleDataSet1.ТипИзделия1.ТипИзделия1RowChanged, New CycleDataSet.ТипИзделия1RowChangeEventHandler(AddressOf On1ТипИзделияRowChangeEventHandler)
        AddHandler CycleDataSet1.ТипИзделия1.ТипИзделия1RowDeleted, New CycleDataSet.ТипИзделия1RowChangeEventHandler(AddressOf On1ТипИзделияRowChangeEventHandler)
        AddHandler CycleDataSet1.ТипИзделия1.TableNewRow, New DataTableNewRowEventHandler(AddressOf On1ТипИзделияTableNewRowEventHandler)

        AddHandler CycleDataSet1.ПрограммаИспытаний2.ПрограммаИспытаний2RowChanged, New CycleDataSet.ПрограммаИспытаний2RowChangeEventHandler(AddressOf On2ПрограммаИспытанийRowChangeEventHandler)
        AddHandler CycleDataSet1.ПрограммаИспытаний2.ПрограммаИспытаний2RowDeleted, New CycleDataSet.ПрограммаИспытаний2RowChangeEventHandler(AddressOf On2ПрограммаИспытанийRowChangeEventHandler)
        AddHandler CycleDataSet1.ПрограммаИспытаний2.TableNewRow, New DataTableNewRowEventHandler(AddressOf On2ПрограммаИспытанийTableNewRowEventHandler)

        AddHandler CycleDataSet1.ЦиклЗагрузки3.ЦиклЗагрузки3RowChanged, New CycleDataSet.ЦиклЗагрузки3RowChangeEventHandler(AddressOf On3ЦиклУправИсполУстрRowChangeEventHandler)
        AddHandler CycleDataSet1.ЦиклЗагрузки3.ЦиклЗагрузки3RowDeleted, New CycleDataSet.ЦиклЗагрузки3RowChangeEventHandler(AddressOf On3ЦиклУправИсполУстрRowDeletedEventHandler)
        AddHandler CycleDataSet1.ЦиклЗагрузки3.TableNewRow, New DataTableNewRowEventHandler(AddressOf On3ЦиклУправИсполУстрTableNewRowEventHandler)

        AddHandler CycleDataSet1.ПерекладкиЦикла4.ПерекладкиЦикла4RowChanged, New CycleDataSet.ПерекладкиЦикла4RowChangeEventHandler(AddressOf On4ПерекладкиЦиклаRowChangeEventHandler)
        AddHandler CycleDataSet1.ПерекладкиЦикла4.ПерекладкиЦикла4RowDeleted, New CycleDataSet.ПерекладкиЦикла4RowChangeEventHandler(AddressOf On4ПерекладкиЦиклаRowChangeEventHandler)
        AddHandler CycleDataSet1.ПерекладкиЦикла4.TableNewRow, New DataTableNewRowEventHandler(AddressOf On4ПерекладкиЦиклаTableNewRowEventHandler)

        ' заголовки в таблицах верхней иерархии скрыть вначале, остальные в иерархии скрываются после навигации
        HideDataGridViewKEYColumns(DataGridView1ТипИзделия)
        HideDataGridViewKEYColumns(DataGridView2ПрограммаИспытаний)
        HideDataGridViewKEYColumns(DataGridView3ЦиклУправИсполУстр)

        ' вначале выключить доступ редактирования
        EnabledDataGridTestProgramsAndSycleDevices(False)

        ' сделать выделенные первые строки таблиц
        If BindingSource1ТипИзделия.Count > 0 Then
            'BindingSource1ТипИзделия.MoveLast()
            'DataGridView1ТипИзделия.Rows(DataGridView1ТипИзделия.Rows.Count - 1).Selected = True
            BindingSource1ТипИзделия.MoveFirst()
            DataGridView1ТипИзделия.Rows(0).Selected = True
        End If

        LoadCurrentDeviceCycle()
    End Sub

#Region "PopulateComboBoxColumn"
    'Private Sub PopulateComboBoxColumnNameDevices()
    '    Dim chargeParameters(MainFomMdiParent.MyClassCalculation.CalculatedParams.CalcDictionary.Count - 1) As String
    '    Dim I As Integer

    '    For Each itemChargeParameter As String In MainFomMdiParent.MyClassCalculation.CalculatedParams.CalcDictionary.Keys
    '        chargeParameters(I) = itemChargeParameter
    '        I += 1
    '    Next

    '    Using tempDGViewTextBoxColumn As DataGridViewComboBoxColumn = CType(DataGridView3ЦиклУправИсполУстр.Columns(NAME_DEVICE_COMBO_BOX_COLUMN_NAME), DataGridViewComboBoxColumn)
    '        tempDGViewTextBoxColumn.Items.Clear()
    '        If chargeParameters.Count > 0 Then tempDGViewTextBoxColumn.Items.AddRange(chargeParameters)
    '    End Using
    'End Sub

    ''' <summary>
    ''' При вставке в ячейку для редактирования заполнить ComboBox значениями текущего имени и плюс имена которые ещё не используются.
    ''' </summary>
    ''' <param name="currentDataGridViewRow"></param>
    ''' <param name="cell"></param>
    Private Sub PopulateComboBoxCellByNameDevices(currentDataGridViewRow As DataGridViewRow, ByRef cell As DataGridViewComboBoxCell)
        Dim chargeParameters As New List(Of String)

        If DataGridView3ЦиклУправИсполУстр.Rows.Count = 1 Then
            For Each itemChargeParameter As String In MainFomMdiParent.MyClassCalculation.CalculatedParams.CalcDictionary.Keys
                chargeParameters.Add(itemChargeParameter)
            Next
        Else
            Dim exceptNameDevices As New List(Of String)

            For Each DataRowItem As DataGridViewRow In DataGridView3ЦиклУправИсполУстр.Rows
                If currentDataGridViewRow Is DataRowItem Then Continue For

                If Not IsDBNull(DataRowItem.Cells(NAME_DEVICE_COLUMN_NAME).Value) Then
                    Dim RowsCellsText As String = CStr(DataRowItem.Cells(NAME_DEVICE_COLUMN_NAME).Value)
                    If RowsCellsText IsNot Nothing Then
                        exceptNameDevices.Add(RowsCellsText)
                    End If
                End If
            Next

            For Each itemChargeParameter As String In MainFomMdiParent.MyClassCalculation.CalculatedParams.CalcDictionary.Keys
                If Not exceptNameDevices.Contains(itemChargeParameter) Then chargeParameters.Add(itemChargeParameter)
            Next
        End If

        cell.DataSource = Nothing
        cell.Items.Clear()
        If chargeParameters.Count > 0 Then cell.Items.AddRange(chargeParameters.ToArray)
    End Sub

    ''' <summary>
    ''' Заполнить список DataGridViewComboBoxCell
    ''' </summary>
    <MethodImpl(MethodImplOptions.Synchronized)>
    Private Sub PopulateDataGridViewComboBoxCell()
        DataGridView3ЦиклУправИсполУстр.Columns(NAME_DEVICE_COMBO_BOX_COLUMN_NAME).DataPropertyName = Nothing

        For Each itemDataRow As DataGridViewRow In DataGridView3ЦиклУправИсполУстр.Rows
            ' в зависимости от выбранного устройства в листе, заполняется лист имён сетевых переменных
            Dim rowCellText As String = CStr(itemDataRow.Cells(NAME_DEVICE_COLUMN_NAME).Value)

            Using cell As DataGridViewComboBoxCell = CType(itemDataRow.Cells(NAME_DEVICE_COMBO_BOX_COLUMN_NAME), DataGridViewComboBoxCell)
                PopulateComboBoxCellByNameDevices(itemDataRow, cell)
                If rowCellText IsNot Nothing Then
                    ' присвоить значение из скрытой ячейки
                    cell.Value = itemDataRow.Cells(NAME_DEVICE_COLUMN_NAME).Value
                End If
            End Using
        Next

        LabelDevicesCount.Text = String.Format("{0} записей", CycleDataSet1.ЦиклЗагрузки3.Rows.Count)
    End Sub
#End Region

#Region "Обработчики RowChanged RowDeleted таблиц"
    Private Sub On1ТипИзделияRowChangeEventHandler(sender As Object, e As CycleDataSet.ТипИзделия1RowChangeEvent)
        BNSaveTypeEngine.Enabled = True
    End Sub
    Private Sub On1ТипИзделияTableNewRowEventHandler(sender As Object, e As DataTableNewRowEventArgs)
        BNSaveTypeEngine.Enabled = True
    End Sub

    Private Sub On2ПрограммаИспытанийRowChangeEventHandler(sender As Object, e As CycleDataSet.ПрограммаИспытаний2RowChangeEvent)
        BNSaveTestPrograms.Enabled = True
    End Sub
    Private Sub On2ПрограммаИспытанийTableNewRowEventHandler(sender As Object, e As DataTableNewRowEventArgs)
        Dim rowTemp As CycleDataSet.ПрограммаИспытаний2Row = CType(e.Row, CycleDataSet.ПрограммаИспытаний2Row)
        rowTemp.keyТипИзделия = -1
        BNSaveTestPrograms.Enabled = True
    End Sub

    'Private Sub DataGridView3ЦиклУправИсполУстр_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView3ЦиклУправИсполУстр.UserDeletedRow
    ' вызывается вслед за On3ЦиклУправИсполУстрRowDeleted
    'End Sub
    Private Sub On3ЦиклУправИсполУстрRowDeletedEventHandler(sender As Object, e As CycleDataSet.ЦиклЗагрузки3RowChangeEvent)
        BNSaveSycleDevices.Enabled = True
        currentNodeType = NodeType.SycleDevice
        TimerUpdate.Enabled = True ' для записи изменений
    End Sub

    Private Sub On3ЦиклУправИсполУстрRowChangeEventHandler(sender As Object, e As CycleDataSet.ЦиклЗагрузки3RowChangeEvent)
        BNSaveSycleDevices.Enabled = True
    End Sub
    Private Sub On3ЦиклУправИсполУстрTableNewRowEventHandler(sender As Object, e As DataTableNewRowEventArgs)
        Dim rowTemp As CycleDataSet.ЦиклЗагрузки3Row = CType(e.Row, CycleDataSet.ЦиклЗагрузки3Row)
        rowTemp.keyПрограммаИспытаний = -1
        BNSaveSycleDevices.Enabled = True
    End Sub

    Private Sub DataGridView3ЦиклУправИсполУстр_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView3ЦиклУправИсполУстр.UserAddedRow
        BNSaveSycleDevices.Enabled = True
    End Sub

    Private Sub On4ПерекладкиЦиклаRowChangeEventHandler(sender As Object, e As CycleDataSet.ПерекладкиЦикла4RowChangeEvent)
        BNSaveSycleStage.Enabled = True
    End Sub
    Private Sub On4ПерекладкиЦиклаTableNewRowEventHandler(sender As Object, e As DataTableNewRowEventArgs)
        Dim rowTemp As CycleDataSet.ПерекладкиЦикла4Row = CType(e.Row, CycleDataSet.ПерекладкиЦикла4Row)
        rowTemp.keyЦиклЗагрузки = -1
        'rowTemp.keyЦиклЗагрузки = CType(rowTemp.GetParentRow("3ЦиклограммаЗапуска4ПерекладкиЦикла"), CycleDataSet.ЦиклЗагрузки3Row).keyЦиклЗагрузки
        BNSaveSycleStage.Enabled = True
    End Sub
#End Region

    ''' <summary>
    ''' После удаления записи из таблицы, необходимо внести изменения в базе данных 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TimerUpdate_Tick(sender As Object, e As EventArgs) Handles TimerUpdate.Tick
        TimerUpdate.Enabled = False
        Select Case currentNodeType
            Case NodeType.TypeEngine
                SaveTypeEngine()
            Case NodeType.TestProgram
                SaveTestProgram()
            Case NodeType.SycleDevice
                SaveSycleDevices()
            Case NodeType.SycleStage
                SaveSycleStage()
            Case NodeType.Unknown
                Exit Select
        End Select
    End Sub

    ''' <summary>
    ''' Свойства DeleteItem BindingNavigator не связано с кнопкой Delete для контролов в данной процедуре
    ''' чтобы не вызывать преждевременное удаление без диалога
    ''' Для остальных кнопок Delete будет производится автоматическое удаление
    ''' </summary>
    ''' <param name="removeNodeType"></param>
    ''' <remarks></remarks>
    Private Sub RemoveCurrentRecordBindingSource(removeNodeType As NodeType)
        Dim text As String = String.Empty
        Dim tempBindingSource As BindingSource = Nothing

        Select Case removeNodeType
            Case NodeType.TypeEngine
                text = "<Тип Изделия>"
                tempBindingSource = BindingSource1ТипИзделия
            Case NodeType.TestProgram
                text = "<Программа Испытаний>"
                tempBindingSource = BindingSource2ПрограммаИспытаний
            Case NodeType.SycleDevice
                text = "<Цикл Управления Исполнительным Устройством>"
                tempBindingSource = BindingSource3ЦиклУправИсполУстр
            Case NodeType.Unknown
                Exit Select
        End Select

        If tempBindingSource.Current IsNot Nothing Then
            Dim Result As DialogResult = MessageBox.Show($"Вы уверены, что хотите удалить запись {text} и все связанные с ней записи", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If Result = DialogResult.Yes Then
                tempBindingSource.RemoveCurrent()
                currentNodeType = removeNodeType
                TimerUpdate.Enabled = True
                ShowMessageOnPanel($"Произведено удаление записи: {text} и всх связанных с ней записей.")
            End If
        End If
    End Sub

#Region "Каскадно загрузить BindingSource->TableAdapter->Таблицы"
    ''' <summary>
    ''' 1 Тип Изделия. Каскадно загрузить BindingSource->TableAdapter->Таблицы.
    ''' </summary>
    Private Sub LoadTestPrograms()
        If BindingSource1ТипИзделия.Current Is Nothing Then ' если родитель равен нулю, то и дочерний приравнять нулю
            BindingSource4ПерекладкиЦикла.DataSource = Nothing
            BindingSource3ЦиклУправИсполУстр.DataSource = Nothing
            BindingSource2ПрограммаИспытаний.DataSource = Nothing
            Exit Sub
        Else
            isCancelEventDataGridView2TestProgram = True
            isCancelEventDataGridView3SycleDevices = True

            If BindingSource2ПрограммаИспытаний.DataSource Is Nothing Then
                BindingSource2ПрограммаИспытаний.DataSource = CycleDataSet1
                BindingSource2ПрограммаИспытаний.DataMember = CycleDataSet1.ПрограммаИспытаний2.ToString
            End If
            If BindingSource3ЦиклУправИсполУстр.DataSource Is Nothing Then
                BindingSource3ЦиклУправИсполУстр.DataSource = CycleDataSet1
                BindingSource3ЦиклУправИсполУстр.DataMember = CycleDataSet1.ЦиклЗагрузки3.ToString
            End If
            If BindingSource4ПерекладкиЦикла.DataSource Is Nothing Then
                BindingSource4ПерекладкиЦикла.DataSource = CycleDataSet1
                BindingSource4ПерекладкиЦикла.DataMember = CycleDataSet1.ПерекладкиЦикла4.ToString
            End If

            isCancelEventDataGridView2TestProgram = False
            isCancelEventDataGridView3SycleDevices = False
        End If

        Dim rowParent As CycleDataSet.ТипИзделия1Row = CType(CType(BindingSource1ТипИзделия.Current, DataRowView).Row, CycleDataSet.ТипИзделия1Row)
        Dim keyTypeEngine As Integer = rowParent.keyТипИзделия

        If keyTypeEngine > 0 Then
            isCancelEventDataGridView2TestProgram = True
            ПрограммаИспытаний2TableAdapter.FillByKeyТипИзделия(CycleDataSet1.ПрограммаИспытаний2, keyTypeEngine)
            isCancelEventDataGridView2TestProgram = False
            LoadSycleDevices()
            HideDataGridViewKEYColumns(DataGridView2ПрограммаИспытаний)
        Else
            isCancelEventDataGridView2TestProgram = True
            ПрограммаИспытаний2TableAdapter.FillByKeyТипИзделия(CycleDataSet1.ПрограммаИспытаний2, 0)
            isCancelEventDataGridView2TestProgram = False

            isCancelEventDataGridView3SycleDevices = True
            ЦиклЗагрузки3TableAdapter.FillByKeyПрограммаИспытаний(CycleDataSet1.ЦиклЗагрузки3, 0)
            PopulateDataGridViewComboBoxCell()
            isCancelEventDataGridView3SycleDevices = False
        End If

        BNSaveTestPrograms.Enabled = False
        LoadCurrentDeviceCycle()
    End Sub

    ''' <summary>
    ''' 2 Программа Испытаний. Каскадно загрузить BindingSource->TableAdapter->Таблицы.
    ''' </summary>
    Private Sub LoadSycleDevices()
        CheckIsCycleDirty()

        If BindingSource2ПрограммаИспытаний.Current Is Nothing Then ' если родитель равен нулю, то и дочерний приравнять нулю
            BindingSource3ЦиклУправИсполУстр.DataSource = Nothing
            Exit Sub
        Else
            isCancelEventDataGridView3SycleDevices = True
            If BindingSource3ЦиклУправИсполУстр.DataSource Is Nothing Then
                BindingSource3ЦиклУправИсполУстр.DataSource = CycleDataSet1
                BindingSource3ЦиклУправИсполУстр.DataMember = CycleDataSet1.ЦиклЗагрузки3.ToString
            End If
            isCancelEventDataGridView3SycleDevices = False
        End If

        Dim rowParent As CycleDataSet.ПрограммаИспытаний2Row = CType(CType(BindingSource2ПрограммаИспытаний.Current, DataRowView).Row, CycleDataSet.ПрограммаИспытаний2Row)
        Dim keyTestProgram As Integer = rowParent.keyПрограммаИспытаний

        If keyTestProgram > 0 Then
            isCancelEventDataGridView3SycleDevices = True
            ЦиклЗагрузки3TableAdapter.FillByKeyПрограммаИспытаний(CycleDataSet1.ЦиклЗагрузки3, keyTestProgram)
            PopulateDataGridViewComboBoxCell()
            isCancelEventDataGridView3SycleDevices = False
            LoadSycleStage()
            HideDataGridViewKEYColumns(DataGridView3ЦиклУправИсполУстр)
        Else
            isCancelEventDataGridView3SycleDevices = True
            ЦиклЗагрузки3TableAdapter.FillByKeyПрограммаИспытаний(CycleDataSet1.ЦиклЗагрузки3, 0)
            PopulateDataGridViewComboBoxCell()
            isCancelEventDataGridView3SycleDevices = False
        End If

        BNSaveSycleDevices.Enabled = False
        LoadCurrentDeviceCycle()
    End Sub

    ''' <summary>
    ''' 3 Цикл УправИсполУстр. Каскадно загрузить BindingSource->TableAdapter->Таблицы.
    ''' </summary>
    Private Sub LoadSycleStage()
        If BindingSource3ЦиклУправИсполУстр.Current Is Nothing Then ' если родитель равен нулю, то и дочерний приравнять нулю
            BindingSource4ПерекладкиЦикла.DataSource = Nothing
            Exit Sub
        Else
            If BindingSource4ПерекладкиЦикла.DataSource Is Nothing Then
                BindingSource4ПерекладкиЦикла.DataSource = CycleDataSet1
                BindingSource4ПерекладкиЦикла.DataMember = CycleDataSet1.ПерекладкиЦикла4.ToString
            End If
        End If

        'If BindingSource3ЦиклУправИсполУстр.Current IsNot Nothing Then
        If DataGridView3ЦиклУправИсполУстр.Rows.Count > 1 AndAlso Not CType(BindingSource3ЦиклУправИсполУстр.Current, DataRowView).IsNew Then
            Dim rowParent As CycleDataSet.ЦиклЗагрузки3Row = CType(CType(BindingSource3ЦиклУправИсполУстр.Current, DataRowView).Row, CycleDataSet.ЦиклЗагрузки3Row)

            If rowParent IsNot Nothing Then
                ' по rowParent.Устройство узнать Устройства1.keyУстройство 
                Dim mAttributesChargeParameter As AttributesChargeParameter = GetAttributesChargeParameter(rowParent.ИмяУстройства, MainFomMdiParent.PathDBaseCycle)

                If rowParent.keyЦиклЗагрузки > 0 Then
                    ПерекладкиЦикла4TableAdapter.FillBykeyЦиклЗагрузкиANDkeyУстройства(CycleDataSet1.ПерекладкиЦикла4, rowParent.keyЦиклЗагрузки, mAttributesChargeParameter.KeyDevice)
                    HideDataGridViewKEYColumns(DataGridView4ПерекладкиЦикла)
                Else
                    CycleDataSet1.ПерекладкиЦикла4.Clear()
                    BindingSource4ПерекладкиЦикла.DataSource = Nothing
                    ListViewCycle.Items.Clear()
                    ToolStripEditCycle.Enabled = False
                End If
            End If
        End If

        BNSaveSycleStage.Enabled = False
    End Sub
#End Region

#Region "ОбновитьTableAdapter"
    Private Sub UpdateTableAdapterTypeEngine()
        Validate()
        BindingSource1ТипИзделия.EndEdit()

        Dim deletedChildRecords = CType(CycleDataSet1.ТипИзделия1.GetChanges(DataRowState.Deleted), CycleDataSet.ТипИзделия1DataTable)
        Dim newChildRecords = CType(CycleDataSet1.ТипИзделия1.GetChanges(DataRowState.Added), CycleDataSet.ТипИзделия1DataTable)
        Dim modifiedChildRecords = CType(CycleDataSet1.ТипИзделия1.GetChanges(DataRowState.Modified), CycleDataSet.ТипИзделия1DataTable)

        Try
            If deletedChildRecords IsNot Nothing Then ТипИзделия1TableAdapter.Update(deletedChildRecords)
            If modifiedChildRecords IsNot Nothing Then ТипИзделия1TableAdapter.Update(modifiedChildRecords)
            If newChildRecords IsNot Nothing Then ТипИзделия1TableAdapter.Update(newChildRecords)

            CycleDataSet1.ТипИзделия1.AcceptChanges()
        Catch ex As Exception
            Dim text As String = ex.ToString
            MessageBox.Show(text, $"Ошибка обновления в процедуре {NameOf(UpdateTableAdapterTypeEngine)}.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If deletedChildRecords IsNot Nothing Then deletedChildRecords.Dispose()
            If modifiedChildRecords IsNot Nothing Then modifiedChildRecords.Dispose()
            If newChildRecords IsNot Nothing Then newChildRecords.Dispose()
        End Try
    End Sub

    Private Sub UpdateTableAdapterTestProgram()
        Validate()
        BindingSource2ПрограммаИспытаний.EndEdit()

        Dim deletedChildRecords = CType(CycleDataSet1.ПрограммаИспытаний2.GetChanges(DataRowState.Deleted), CycleDataSet.ПрограммаИспытаний2DataTable)
        Dim newChildRecords = CType(CycleDataSet1.ПрограммаИспытаний2.GetChanges(DataRowState.Added), CycleDataSet.ПрограммаИспытаний2DataTable)
        Dim modifiedChildRecords = CType(CycleDataSet1.ПрограммаИспытаний2.GetChanges(DataRowState.Modified), CycleDataSet.ПрограммаИспытаний2DataTable)

        Try
            If deletedChildRecords IsNot Nothing Then ПрограммаИспытаний2TableAdapter.Update(deletedChildRecords)
            If modifiedChildRecords IsNot Nothing Then ПрограммаИспытаний2TableAdapter.Update(modifiedChildRecords)

            If newChildRecords IsNot Nothing Then
                For Each RowItem As DataRow In newChildRecords.Rows
                    Dim m2ПрограммаИспытанийRow As CycleDataSet.ПрограммаИспытаний2Row = CType(RowItem, CycleDataSet.ПрограммаИспытаний2Row)

                    If m2ПрограммаИспытанийRow.keyТипИзделия = -1 Then
                        Dim typeEngineCurrentRow As CycleDataSet.ТипИзделия1Row = CType(CType(BindingSource1ТипИзделия.Current, DataRowView).Row, CycleDataSet.ТипИзделия1Row)

                        If typeEngineCurrentRow IsNot Nothing Then m2ПрограммаИспытанийRow.keyТипИзделия = typeEngineCurrentRow.keyТипИзделия
                    End If
                Next
                ПрограммаИспытаний2TableAdapter.Update(newChildRecords)
            End If

            CycleDataSet1.ПрограммаИспытаний2.AcceptChanges()
        Catch ex As Exception
            Dim text As String = ex.ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(UpdateTableAdapterTestProgram)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If deletedChildRecords IsNot Nothing Then deletedChildRecords.Dispose()
            If modifiedChildRecords IsNot Nothing Then modifiedChildRecords.Dispose()
            If newChildRecords IsNot Nothing Then newChildRecords.Dispose()
        End Try
    End Sub

    Private Sub UpdateTableAdapterSycleDevices()
        Validate()
        BindingSource3ЦиклУправИсполУстр.EndEdit()

        Dim deletedChildRecords = CType(CycleDataSet1.ЦиклЗагрузки3.GetChanges(DataRowState.Deleted), CycleDataSet.ЦиклЗагрузки3DataTable)
        Dim newChildRecords = CType(CycleDataSet1.ЦиклЗагрузки3.GetChanges(DataRowState.Added), CycleDataSet.ЦиклЗагрузки3DataTable)
        Dim modifiedChildRecords = CType(CycleDataSet1.ЦиклЗагрузки3.GetChanges(DataRowState.Modified), CycleDataSet.ЦиклЗагрузки3DataTable)

        Try
            If deletedChildRecords IsNot Nothing Then ЦиклЗагрузки3TableAdapter.Update(deletedChildRecords)
            If modifiedChildRecords IsNot Nothing Then ЦиклЗагрузки3TableAdapter.Update(modifiedChildRecords)

            If newChildRecords IsNot Nothing Then
                For Each itemRow As DataRow In newChildRecords.Rows
                    Dim m3ЦиклУправИсполУстрRow As CycleDataSet.ЦиклЗагрузки3Row = CType(itemRow, CycleDataSet.ЦиклЗагрузки3Row)

                    If m3ЦиклУправИсполУстрRow.keyПрограммаИспытаний = -1 Then
                        Dim testProgramCurrentRow As CycleDataSet.ПрограммаИспытаний2Row = CType(CType(BindingSource2ПрограммаИспытаний.Current, DataRowView).Row, CycleDataSet.ПрограммаИспытаний2Row)

                        If testProgramCurrentRow IsNot Nothing Then m3ЦиклУправИсполУстрRow.keyПрограммаИспытаний = testProgramCurrentRow.keyПрограммаИспытаний
                    End If
                Next
                ЦиклЗагрузки3TableAdapter.Update(newChildRecords)
            End If

            CycleDataSet1.ЦиклЗагрузки3.AcceptChanges()
        Catch ex As Exception
            Dim text As String = ex.ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(UpdateTableAdapterSycleDevices)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If deletedChildRecords IsNot Nothing Then deletedChildRecords.Dispose()
            If modifiedChildRecords IsNot Nothing Then modifiedChildRecords.Dispose()
            If newChildRecords IsNot Nothing Then newChildRecords.Dispose()
        End Try
    End Sub

    Private Sub UpdateTableAdapterSycleStage()
        Validate()
        BindingSource4ПерекладкиЦикла.EndEdit()

        Dim deletedChildRecords = CType(CycleDataSet1.ПерекладкиЦикла4.GetChanges(DataRowState.Deleted), CycleDataSet.ПерекладкиЦикла4DataTable)
        Dim newChildRecords = CType(CycleDataSet1.ПерекладкиЦикла4.GetChanges(DataRowState.Added), CycleDataSet.ПерекладкиЦикла4DataTable)
        Dim modifiedChildRecords = CType(CycleDataSet1.ПерекладкиЦикла4.GetChanges(DataRowState.Modified), CycleDataSet.ПерекладкиЦикла4DataTable)

        Try
            If deletedChildRecords IsNot Nothing Then ПерекладкиЦикла4TableAdapter.Update(deletedChildRecords)
            If modifiedChildRecords IsNot Nothing Then ПерекладкиЦикла4TableAdapter.Update(modifiedChildRecords)

            If newChildRecords IsNot Nothing Then
                For Each itemRow As DataRow In newChildRecords.Rows
                    Dim m4ПерекладкиЦиклаRow As CycleDataSet.ПерекладкиЦикла4Row = CType(itemRow, CycleDataSet.ПерекладкиЦикла4Row)

                    'If IsDBNull(ПерекладкиЦиклаRow.keyЦиклЗагрузки) Then
                    'не работает ПерекладкиЦиклаRow.GetParentRow("3ЦиклограммаЗапуска4ПерекладкиЦикла")
                    'ПерекладкиЦиклаRow.keyЦиклЗагрузки = CType(ПерекладкиЦиклаRow.GetParentRow("3ЦиклограммаЗапуска4ПерекладкиЦикла"), CycleDataSet.ЦиклЗагрузки3Row).keyЦиклЗагрузки
                    If m4ПерекладкиЦиклаRow.keyЦиклЗагрузки = -1 Then
                        Dim m3ЦиклУправИсполУстрRow As CycleDataSet.ЦиклЗагрузки3Row = CType(CType(BindingSource3ЦиклУправИсполУстр.Current, DataRowView).Row, CycleDataSet.ЦиклЗагрузки3Row)

                        If m3ЦиклУправИсполУстрRow IsNot Nothing Then m4ПерекладкиЦиклаRow.keyЦиклЗагрузки = m3ЦиклУправИсполУстрRow.keyЦиклЗагрузки
                    End If
                Next
                ПерекладкиЦикла4TableAdapter.Update(newChildRecords)
            End If

            CycleDataSet1.ПерекладкиЦикла4.AcceptChanges()
        Catch ex As Exception
            Dim text As String = ex.ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(UpdateTableAdapterSycleStage)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If deletedChildRecords IsNot Nothing Then deletedChildRecords.Dispose()
            If modifiedChildRecords IsNot Nothing Then modifiedChildRecords.Dispose()
            If newChildRecords IsNot Nothing Then newChildRecords.Dispose()
        End Try
    End Sub
#End Region

#Region "Handles Навигация по сеткам"
    'Private Sub DataGridView3ЦиклУправИсполУстр_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView3ЦиклУправИсполУстр.DataError
    '    ' перехват сообщения ошибки связанной с заполнением поля типа Функции, после отсоединения адаптера
    '    'MessageBox.Show(e.ToString)
    'End Sub

    Private Sub EnabledDataGridTestProgramsAndSycleDevices(isEnabled As Boolean)
        DataGridView2ПрограммаИспытаний.Enabled = isEnabled
        BindingNavigator2ПрограммаИспытаний.Enabled = isEnabled

        DataGridView3ЦиклУправИсполУстр.Enabled = isEnabled
        BindingNavigator3ЦиклУправИсполУстр.Enabled = isEnabled
    End Sub

    Private Sub DataGridView1ТипИзделия_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1ТипИзделия.CellValueChanged
        BNSaveTypeEngine.Enabled = True
    End Sub
    Private Sub DataGridView1ТипИзделия_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1ТипИзделия.SelectionChanged
        If Not isTableAdapterFilled Then Exit Sub

        CycleDataSet1.EnforceConstraints = False
        BindingSource2ПрограммаИспытаний.DataSource = Nothing

        EnabledDataGridTestProgramsAndSycleDevices(False)
        LoadTestPrograms()

        ' разрешить добавлять записи на следующем уровне
        If CycleDataSet1.ТипИзделия1.Rows.Count > 0 Then
            If BindingSource2ПрограммаИспытаний.DataSource IsNot Nothing AndAlso (CycleDataSet1.ПрограммаИспытаний2.Rows.Count > 0 OrElse CycleDataSet1.ТипИзделия1.Rows.Count > 0) Then
                DataGridView2ПрограммаИспытаний.Enabled = True
                BindingNavigator2ПрограммаИспытаний.Enabled = True

                If BindingSource3ЦиклУправИсполУстр.DataSource IsNot Nothing AndAlso (CycleDataSet1.ЦиклЗагрузки3.Rows.Count > 0 OrElse CycleDataSet1.ПрограммаИспытаний2.Rows.Count > 0) Then
                    DataGridView3ЦиклУправИсполУстр.Enabled = True
                    BindingNavigator3ЦиклУправИсполУстр.Enabled = True
                Else
                    DataGridView3ЦиклУправИсполУстр.Enabled = False
                    BindingNavigator3ЦиклУправИсполУстр.Enabled = False
                End If
            Else
                DataGridView2ПрограммаИспытаний.Enabled = False
                BindingNavigator2ПрограммаИспытаний.Enabled = False

                DataGridView3ЦиклУправИсполУстр.Enabled = False
                BindingNavigator3ЦиклУправИсполУстр.Enabled = False
            End If
        End If

        Try
            CycleDataSet1.EnforceConstraints = True
        Catch ex As ConstraintException
            Dim text As String = ex.GetType().ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(DataGridView1ТипИзделия_SelectionChanged)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub DataGridView2ПрограммаИспытаний_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2ПрограммаИспытаний.CellValueChanged
        BNSaveTestPrograms.Enabled = True
    End Sub
    Private Sub DataGridView2ПрограммаИспытаний_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView2ПрограммаИспытаний.SelectionChanged
        If Not isTableAdapterFilled Then Exit Sub
        If isCancelEventDataGridView2TestProgram Then Exit Sub

        CycleDataSet1.EnforceConstraints = False
        CheckIsCycleDirty()
        BindingSource3ЦиклУправИсполУстр.DataSource = Nothing

        LoadSycleDevices()

        If BindingSource2ПрограммаИспытаний.DataSource IsNot Nothing AndAlso (CycleDataSet1.ПрограммаИспытаний2.Rows.Count > 0 OrElse CycleDataSet1.ТипИзделия1.Rows.Count > 0) Then
            DataGridView2ПрограммаИспытаний.Enabled = True
            BindingNavigator2ПрограммаИспытаний.Enabled = True

            If BindingSource3ЦиклУправИсполУстр.DataSource IsNot Nothing AndAlso (CycleDataSet1.ЦиклЗагрузки3.Rows.Count > 0 OrElse CycleDataSet1.ПрограммаИспытаний2.Rows.Count > 0) Then
                DataGridView3ЦиклУправИсполУстр.Enabled = True
                BindingNavigator3ЦиклУправИсполУстр.Enabled = True
            Else
                DataGridView3ЦиклУправИсполУстр.Enabled = False
                BindingNavigator3ЦиклУправИсполУстр.Enabled = False
            End If
        Else
            DataGridView2ПрограммаИспытаний.Enabled = False
            BindingNavigator2ПрограммаИспытаний.Enabled = False

            DataGridView3ЦиклУправИсполУстр.Enabled = False
            BindingNavigator3ЦиклУправИсполУстр.Enabled = False
        End If

        Try
            CycleDataSet1.EnforceConstraints = True
        Catch ex As ConstraintException
            Dim text As String = ex.GetType().ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(DataGridView2ПрограммаИспытаний_SelectionChanged)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        If BindingSource3ЦиклУправИсполУстр.Current IsNot Nothing Then
            DataGridView3ЦиклУправИсполУстр.Rows(0).Selected = True
        End If
    End Sub

    Private Sub DataGridView3ЦиклУправИсполУстр_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3ЦиклУправИсполУстр.CellValueChanged
        If IsHandleCreated Then
            If e.ColumnIndex = NAME_DEVICE_COMBO_BOX_COLUMN_INDEX Then
                ' скрытому полю связанного с таблицей присвоить значенее несвязанного списка
                Dim oldNameDeviceCell As DataGridViewCell = DataGridView3ЦиклУправИсполУстр.Rows(DataGridView3ЦиклУправИсполУстр.CurrentCell.RowIndex).Cells(NAME_DEVICE_COLUMN_NAME)
                Dim newNameDeviceCell As DataGridViewCell = DataGridView3ЦиклУправИсполУстр.Rows(DataGridView3ЦиклУправИсполУстр.CurrentCell.RowIndex).Cells(NAME_DEVICE_COMBO_BOX_COLUMN_NAME)

                If IsDBNull(CType(BindingSource3ЦиклУправИсполУстр.Current, DataRowView).Row("ИмяУстройства")) Then
                    ListViewCycle.Items.Clear()
                    ' это новая строка при добавлении, значит в ячейку сразу вставить выбранный из comboBox агрегат
                    oldNameDeviceCell.Value = newNameDeviceCell.Value
                End If

                If CStr(oldNameDeviceCell.Value) <> CStr(newNameDeviceCell.Value) Then
                    If CycleDataSet1.ЦиклЗагрузки3.GetChanges(DataRowState.Added) IsNot Nothing Then
                        ' пока не записана менять можно
                        oldNameDeviceCell.Value = newNameDeviceCell.Value
                    Else
                        ' для записанной записи менять нельзя
                        newNameDeviceCell.Value = oldNameDeviceCell.Value
                    End If
                End If
                ToolStripEditCycle.Enabled = False
            End If
        End If

        BNSaveSycleDevices.Enabled = True
    End Sub

    Private Sub DataGridView3ЦиклУправИсполУстр_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView3ЦиклУправИсполУстр.SelectionChanged
        If Not isTableAdapterFilled Then Exit Sub
        If isCancelEventDataGridView3SycleDevices Then Exit Sub

        CheckIsCycleDirty()
        CycleDataSet1.EnforceConstraints = False
        BindingSource4ПерекладкиЦикла.DataSource = Nothing
        ListViewCycle.Items.Clear()

        If BindingSource3ЦиклУправИсполУстр.Current IsNot Nothing Then
            If DataGridView3ЦиклУправИсполУстр.Rows.Count > 1 AndAlso Not CType(BindingSource3ЦиклУправИсполУстр.Current, DataRowView).IsNew Then
                LoadSycleStage()
                ' при удалении происходит смена текущей позиции BindingSource3ЦиклУправИсполУстр.Current, а изменения ещё не зафиксированы
                ' поэтому, чтобы не было ошибок при отображении графика, проверить наличие удалённых, и не зафиксированных записей в таблице
                Using deletedChildRecords As DataTable = CycleDataSet1.ЦиклЗагрузки3.GetChanges(DataRowState.Deleted)
                    If deletedChildRecords Is Nothing Then
                        LoadCurrentDeviceCycle()
                    End If
                End Using
            End If
        End If

        Try
            CycleDataSet1.EnforceConstraints = True
        Catch ex As ConstraintException
            Dim text As String = ex.GetType().ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(DataGridView3ЦиклУправИсполУстр_SelectionChanged)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ''' <summary>
    '''проверка на корректное введение числа 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView4ПерекладкиЦикла_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4ПерекладкиЦикла.CellValueChanged
        If IsHandleCreated Then
            If e.ColumnIndex = DataGridView4ПерекладкиЦикла.Columns("TimeActionDataGridViewTextBoxColumn").Index Then
                TextBoxValidating(DataGridView4ПерекладкиЦикла, CType(DataGridView4ПерекладкиЦикла.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewTextBoxCell), ErrorProvider4ПерекладкиЦикла)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Проверка корректного ввода положительного значения числа
    ''' </summary>
    ''' <param name="dgv"></param>
    ''' <param name="sender"></param>
    ''' <param name="ErrorProvider"></param>
    ''' <remarks></remarks>
    Private Sub TextBoxValidating(ByRef dgv As DataGridView, ByRef sender As DataGridViewTextBoxCell, ByRef ErrorProvider As ErrorProvider)
        If CSng(sender.Value) <= 0 Then
            Const ErrorMessage As String = "Значение должно быть положительным числом"
            ErrorProvider.SetError(dgv, ErrorMessage)

            If dgv.Rows(dgv.CurrentRow.Index).ErrorText = "" Then
                ' текст ошибки только если нет ошибки условия
                dgv.Rows(dgv.CurrentRow.Index).ErrorText = ErrorMessage
            End If
        Else
            ' Очистить ошибку
            ErrorProvider.SetError(dgv, "")
            dgv.Rows(dgv.CurrentRow.Index).ErrorText = ""
        End If
    End Sub
#End Region

#Region "Handles Save_Click Delete_Click"
    Private Sub BNSaveTypeEngine_Click(sender As Object, e As EventArgs) Handles BNSaveTypeEngine.Click
        If BindingSource1ТипИзделия.Current IsNot Nothing Then
            Dim keyTypeEngine As Integer = CType(CType(BindingSource1ТипИзделия.Current, DataRowView).Row, CycleDataSet.ТипИзделия1Row).keyТипИзделия

            SaveTypeEngine()

            ' если запись только одна (вставлена) то разрешить добавлять записи на следующем уровне
            If CycleDataSet1.ТипИзделия1.Rows.Count = 1 Then
                DataGridView2ПрограммаИспытаний.Enabled = True
                BindingNavigator2ПрограммаИспытаний.Enabled = True
            End If

            ' переместиться в позицию последнего редактирования
            'If BindingSource1ТипИзделия.SupportsSearching <> True Then
            '    MessageBox.Show("Поиск невозможен.")
            'Else
            Dim foundIndex As Integer = BindingSource1ТипИзделия.Find("keyТипИзделия", keyTypeEngine)

            If foundIndex > -1 Then
                BindingSource1ТипИзделия.Position = foundIndex
            Else
                BindingSource1ТипИзделия.MoveLast()
                'MessageBox.Show("Запись с индексом " & keyTypeEngine & " не найдена.")
            End If
            'End If
        End If
    End Sub

    Private Sub BNDeleteTypeEngine_Click(sender As Object, e As EventArgs) Handles BNDeleteTypeEngine.Click
        RemoveCurrentRecordBindingSource(NodeType.TypeEngine)
    End Sub

    Private Sub BNSaveTestPrograms_Click(sender As Object, e As EventArgs) Handles BNSaveTestPrograms.Click
        Dim keyTestProgram As Integer = CType(CType(BindingSource2ПрограммаИспытаний.Current, DataRowView).Row, CycleDataSet.ПрограммаИспытаний2Row).keyПрограммаИспытаний
        Dim newTestProgramName As String = CType(CType(BindingSource2ПрограммаИспытаний.Current, DataRowView).Row, CycleDataSet.ПрограммаИспытаний2Row).ИмяПрограммы
        Dim countQueryPrograms As Integer
        'Dim queryPrograms As IEnumerable(Of CycleDataSet.ПрограммаИспытаний2Row) = From selectProgramm As CycleDataSet.ПрограммаИспытаний2Row In CycleDataSet1.ПрограммаИспытаний2.Rows
        '                                                                           Where selectProgramm.ИмяПрограммы = newTestProgramName AndAlso selectProgramm.RowState <> DataRowState.Added AndAlso (selectProgramm.RowState <> DataRowState.Modified AndAlso selectProgramm.RowState <> DataRowState.Unchanged)
        '                                                                           Select selectProgramm

        'countQueryPrograms = queryPrograms.Count

        Dim sSQL As String = "SELECT Count(*) FROM ПрограммаИспытаний2 WHERE ПрограммаИспытаний2.ИмяПрограммы='" & newTestProgramName & "'"
        Using cnn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            cnn.Open()
            Dim cmd As New OleDbCommand(sSQL, cnn) With {.CommandType = CommandType.Text}
            countQueryPrograms = CInt(cmd.ExecuteScalar)
        End Using

        If countQueryPrograms > 0 Then
            MessageBox.Show($"Имя программы <{newTestProgramName}> уже используется!{Environment.NewLine}Добавьте к имени программы суффикс, например номер изделия.",
                                        "Проверка имени программы",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        SaveTestProgram()
        ' переместиться в позицию последнего редактирования
        'If BindingSource2ПрограммаИспытаний.SupportsSearching <> True Then
        '    MessageBox.Show("Поиск невозможен.")
        'Else
        Dim foundIndex As Integer = BindingSource2ПрограммаИспытаний.Find("keyПрограммаИспытаний", keyTestProgram)

        If foundIndex > -1 Then
            BindingSource2ПрограммаИспытаний.Position = foundIndex
        Else
            BindingSource2ПрограммаИспытаний.MoveLast()
            'MessageBox.Show("Запись с индексом " & keyTestProgram & " не найдена.")
        End If
        'End If
    End Sub

    Private Sub BNDeleteTestPrograms_Click(sender As Object, e As EventArgs) Handles BNDeleteTestPrograms.Click
        RemoveCurrentRecordBindingSource(NodeType.TestProgram)
    End Sub

    Private Sub BNSaveSycleDevices_Click(sender As Object, e As EventArgs) Handles BNSaveSycleDevices.Click
        SaveSycleDevices()

        Try
            ' переместиться в позицию последнего редактирования
            'If BindingSource3ЦиклУправИсполУстр.SupportsSearching <> True Then
            '    MessageBox.Show("Поиск невозможен.")
            'Else
            Dim keySycleDevice As Integer = CType(CType(BindingSource3ЦиклУправИсполУстр.Current, DataRowView).Row, CycleDataSet.ЦиклЗагрузки3Row).keyЦиклЗагрузки
            If BindingSource3ЦиклУправИсполУстр.Current IsNot Nothing Then
                Dim foundIndex As Integer = BindingSource3ЦиклУправИсполУстр.Find("keyЦиклЗагрузки", keySycleDevice)

                If foundIndex > -1 Then
                    BindingSource3ЦиклУправИсполУстр.Position = foundIndex
                Else
                    BindingSource3ЦиклУправИсполУстр.MoveLast()
                    'MessageBox.Show("Запись с индексом " & keySycleDevice & " не найдена.")
                End If
                'End If
            End If
        Catch ex As ConstraintException
            Dim text As String = ex.GetType().ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(BNSaveSycleDevices_Click)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub BNDeleteSycleStage_Click(sender As Object, e As EventArgs) Handles BNDeleteSycleStage.Click
        RemoveCurrentRecordBindingSource(NodeType.SycleDevice)
    End Sub

    Private Sub BNSaveПерекладкиЦикла_Click(sender As Object, e As EventArgs) Handles BNSaveSycleStage.Click
        SaveDBaseCyclogram()
    End Sub

    Private Sub SaveDBaseCyclogram()
        Dim keySycleStage As Integer = CType(CType(BindingSource4ПерекладкиЦикла.Current, DataRowView).Row, CycleDataSet.ПерекладкиЦикла4Row).keyПерекладкиЦикла

        SaveSycleStage()
        ' переместиться в позицию последнего редактирования
        'If BindingSource4ПерекладкиЦикла.SupportsSearching <> True Then
        '    MessageBox.Show("Поиск невозможен.")
        'Else
        Dim foundIndex As Integer = BindingSource4ПерекладкиЦикла.Find("keyПерекладкиЦикла", keySycleStage)
        If foundIndex > -1 Then
            BindingSource4ПерекладкиЦикла.Position = foundIndex
        Else
            BindingSource4ПерекладкиЦикла.MoveLast()
            'MessageBox.Show("Запись с индексом " & keySycleStage & " не найдена.")
        End If
        'End If
    End Sub

    Private Sub BNDeleteCycleShelf_Click(sender As Object, e As EventArgs) Handles BNDeleteCycleShelf.Click
        currentNodeType = NodeType.SycleStage
        TimerUpdate.Enabled = True
    End Sub
#End Region

#Region "СохранитьИзменения"
    ''' <summary>
    ''' Сохранить изменения типа изделия.
    ''' </summary>
    Private Sub SaveTypeEngine()
        UpdateTableAdapterTypeEngine()

        If BindingSource1ТипИзделия.Current Is Nothing Then
            BindingSource2ПрограммаИспытаний.DataSource = Nothing
            BindingSource3ЦиклУправИсполУстр.DataSource = Nothing
            BindingSource4ПерекладкиЦикла.DataSource = Nothing
            Exit Sub
        End If

        ТипИзделия1TableAdapter.Fill(CycleDataSet1.ТипИзделия1)
        'BindingSource1ТипИзделия.MoveLast()
        BNSaveTypeEngine.Enabled = False
    End Sub

    ''' <summary>
    ''' Сохранить изменения программы испытаний.
    ''' </summary>
    Private Sub SaveTestProgram()
        UpdateTableAdapterTestProgram()
        CycleDataSet1.EnforceConstraints = False
        LoadTestPrograms()

        Try
            CycleDataSet1.EnforceConstraints = True
        Catch ex As ConstraintException
            Dim text As String = ex.GetType().ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(SaveTestProgram)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        BNSaveTestPrograms.Enabled = False
    End Sub

    ''' <summary>
    ''' Сохранить изменения циклов управления агрегатами.
    ''' </summary>
    Private Sub SaveSycleDevices()
        ' при удалении происходит модификация коллекции, а изменения ещё не зафиксированы
        ' поэтому, чтобы не было ошибок, проверить наличие удалённых, и не зафиксированных записей в таблице
        Using deletedChildRecords As DataTable = CycleDataSet1.ЦиклЗагрузки3.GetChanges(DataRowState.Deleted)
            If deletedChildRecords Is Nothing Then
                For Each temp3ЦиклУправИсполУстрRow As CycleDataSet.ЦиклЗагрузки3Row In CycleDataSet1.ЦиклЗагрузки3.Rows
                    ' проверить имя (по ключу проверить нельзя т.к. он уникальный
                    Dim queryDevices As IEnumerable(Of CycleDataSet.ЦиклЗагрузки3Row) = From selectDevice As CycleDataSet.ЦиклЗагрузки3Row In CType(CycleDataSet1.ЦиклЗагрузки3.Rows, IEnumerable(Of CycleDataSet.ЦиклЗагрузки3Row))
                                                                                        Where selectDevice.ИмяУстройства = temp3ЦиклУправИсполУстрRow.ИмяУстройства
                                                                                        Select selectDevice
                    If queryDevices.Count > 1 Then
                        MessageBox.Show($"Устройство <{CStr(temp3ЦиклУправИсполУстрRow("ИмяУстройства"))}> не может быть в цикле более одного раза!{Environment.NewLine}Назначьте другое имя агрегата.",
                                        "Проверка состава циклограммы",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Next
            End If
        End Using

        UpdateTableAdapterSycleDevices()

        CycleDataSet1.EnforceConstraints = False
        LoadSycleDevices()

        Try
            CycleDataSet1.EnforceConstraints = True
        Catch ex As ConstraintException
            Dim text As String = ex.GetType().ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(SaveSycleDevices)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        BNSaveSycleDevices.Enabled = False
    End Sub

    ''' <summary>
    ''' Сохранить изменения перекладки цикла.
    ''' </summary>
    Private Sub SaveSycleStage()
        UpdateTableAdapterSycleStage()
        CycleDataSet1.EnforceConstraints = False
        LoadSycleStage()

        Try
            CycleDataSet1.EnforceConstraints = True
        Catch ex As ConstraintException
            Dim text As String = ex.GetType().ToString
            MessageBox.Show(text, $"Исключение в процедуре <{NameOf(SaveSycleStage)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        BNSaveSycleStage.Enabled = False
    End Sub
#End Region

#Region "Редактор циклограммы"
    ''' <summary>
    ''' Загрузить циклограмму текущей выделенной строки агрегата.
    ''' Вызывается при каждом изменении таблицы агрегатов.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCurrentDeviceCycle()
        If BindingSource3ЦиклУправИсполУстр.Current Is Nothing Then
            ToolStripEditCycle.Enabled = False
        Else
            Dim currentRow As CycleDataSet.ЦиклЗагрузки3Row = CType(CType(BindingSource3ЦиклУправИсполУстр.Current, DataRowView).Row, CycleDataSet.ЦиклЗагрузки3Row)
            Dim sSQL As String = "SELECT Устройства1.*, ВеличинаЗагрузки2.* " &
            "FROM Устройства1 RIGHT JOIN ВеличинаЗагрузки2 ON Устройства1.keyУстройства = ВеличинаЗагрузки2.keyУстройства " &
            "WHERE (((Устройства1.ИмяУстройства)='" & currentRow.ИмяУстройства & "')) " &
            "ORDER BY ВеличинаЗагрузки2.ЧисловоеЗначение;"

            Using cnn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
                cnn.Open()
                Dim cmd As New OleDbCommand(sSQL, cnn) With {.CommandType = CommandType.Text}

                TSComboBoxStage.BeginUpdate()
                TSComboBoxStage.ComboBox.DataSource = Nothing

                Dim ChargeValueToTexts As New List(Of ChargeValueToText)

                Using drDataReader As OleDbDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                    If drDataReader.HasRows Then
                        Do While drDataReader.Read()
                            ChargeValueToTexts.Add(New ChargeValueToText(Convert.ToDouble(drDataReader("ЧисловоеЗначение")), CStr(drDataReader("ВеличинаЗагрузки"))))
                        Loop
                    End If

                    TSComboBoxStage.ComboBox.DataSource = ChargeValueToTexts
                    TSComboBoxStage.ComboBox.DisplayMember = "TextCharge"
                    TSComboBoxStage.ComboBox.ValueMember = "Value"

                    If TSComboBoxStage.Items.Count > 0 Then
                        TSComboBoxStage.SelectedIndex = 0
                    End If
                End Using

                TSComboBoxStage.EndUpdate()
            End Using

            Dim mAttributesChargeParameter As AttributesChargeParameter = GetAttributesChargeParameter(currentRow.ИмяУстройства, MainFomMdiParent.PathDBaseCycle)
            YAxis1.Range = New Range(mAttributesChargeParameter.RangeOfChangingValueMin - 1, mAttributesChargeParameter.RangeOfChangingValueMax + 1)
            YAxis1.Caption = mAttributesChargeParameter.UnitOfMeasure
            LabelNameChargeParameter.Text = currentRow.ИмяУстройства
            LabelUnitOfMeasure.Text = mAttributesChargeParameter.UnitOfMeasure
            LabelMinMax.Text = $"min({mAttributesChargeParameter.RangeOfChangingValueMin}) : max({mAttributesChargeParameter.RangeOfChangingValueMax})"
            LabelCaption.Text = mAttributesChargeParameter.Description

            PopulateListViewCurrentDeviceCycle()
            UpdateGraphSelectDevice()
            isCycleDirty = False
            TSButtonSaveCycle.Enabled = False
            PrepareButtonsRecord()

            If BindingSource4ПерекладкиЦикла.DataSource IsNot Nothing Then
                ToolStripEditCycle.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Обновить поле общее время исполнения циклограммы.
    ''' </summary>
    Private Sub UpdateIntervalListViewItems()
        Dim sumTimeCycle As Double
        Dim timeUnit As String ' Ед.изм.
        Dim interval As Double ' Длительность
        Dim stageValue As Single

        For Each lvItem As ListViewItem In ListViewCycle.Items
            interval = CDbl(lvItem.SubItems(1).Text)
            timeUnit = lvItem.SubItems(2).Text

            If timeUnit = "Мин" Then
                interval *= 60
            ElseIf timeUnit = "Час" Then
                interval *= 3600
            End If

            sumTimeCycle += interval
            lvItem.SubItems(3).Text = sumTimeCycle.ToString

            If lvItem.Index = 0 Then
                stageValue = 0
            Else
                stageValue = Convert.ToSingle(ListViewCycle.Items(lvItem.Index - 1).SubItems(0).Text)
            End If

            lvItem.ImageIndex = GetImageIndex(stageValue, Convert.ToSingle(ListViewCycle.Items(lvItem.Index).SubItems(0).Text))
        Next
    End Sub

    ''' <summary>
    ''' Из таблицы связанной с данными загрузить в ListViewCycle редактора.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateListViewCurrentDeviceCycle()
        Dim items(ListViewCycle.Columns.Count - 1) As String
        Dim mforeColor As Color = Color.Black
        Dim mbackColor As Color = Color.White
        Dim mfont As Font = New Font("Times New Roman", 10, FontStyle.Bold)

        ListViewCycle.BeginUpdate()
        ListViewCycle.Items.Clear()

        For Each rowCycleShelf As CycleDataSet.ПерекладкиЦикла4Row In CycleDataSet1.ПерекладкиЦикла4.Rows
            items(0) = CStr(rowCycleShelf.ЧисловоеЗначение)
            items(1) = CStr(rowCycleShelf.TimeAction)
            items(2) = rowCycleShelf.TimeValue
            items(3) = "0"
            items(4) = rowCycleShelf.ВеличинаЗагрузки

            'If drDataReader("Parametr") = cRud Then
            '    mfont = New Font("Times New Roman", 10, FontStyle.Bold) 'FontStyle.Regular)
            '    mforeColor = Color.Black
            '    mbackColor = Color.White
            'ElseIf drDataReader("Parametr") = cN2 Then
            '    mfont = New Font("Arial", 10, FontStyle.Bold)
            '    mforeColor = Color.Red
            '    mbackColor = Color.Yellow
            'End If
            ListViewCycle.Items.Add(New ListViewItem(items, 1, mforeColor, mbackColor, mfont))
        Next

        ListViewCycle.EndUpdate()
    End Sub

    ''' <summary>
    ''' Добавить Строку
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TSButtonAddRecord_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonAddRecord.Click
        Dim items(ListViewCycle.Columns.Count - 1) As String
        Dim mforeColor As Color = Color.Black
        Dim mbackColor As Color = Color.White
        Dim mfont As Font = New Font("Times New Roman", 10, FontStyle.Bold)
        Dim startCharge, stopCharge As Single ' величина загрузки

        If ListViewCycle.Items.Count > 0 Then
            startCharge = Convert.ToSingle(ListViewCycle.Items(ListViewCycle.Items.Count - 1).SubItems(0).Text)
        End If
        stopCharge = Convert.ToSingle(CType(TSComboBoxStage.SelectedItem, ChargeValueToText).Value)

        If Not IsStageShelf() Then Exit Sub

        items(0) = stopCharge.ToString
        items(1) = tsNumericUpDownTimeDuration.NumericControl.NumericUpDownValue.Text
        items(2) = TSComboBoxTimeUnit.Text
        items(3) = "0"
        items(4) = CType(TSComboBoxStage.SelectedItem, ChargeValueToText).TextCharge

        'If Str(0) = cRud Then 
        '    mforeColor = Color.Black
        '    mbackColor = Color.White
        'ElseIf Str(0) = cN2 Then
        '    mfont = New Font("Arial", 10, FontStyle.Bold)
        '    mforeColor = Color.Blue
        '    mbackColor = Color.Magenta
        'End If

        ListViewCycle.Items.Add(New ListViewItem(items, GetImageIndex(startCharge, stopCharge), mforeColor, mbackColor, mfont))
        ListViewCycle.EnsureVisible(ListViewCycle.Items.Count - 1)
        ListViewCycle.Refresh()
        ListViewCycle.MultiSelect = False
        ListViewCycle.Items(ListViewCycle.Items.Count - 1).Selected = True
        ListViewCycle.MultiSelect = True
        isCycleDirty = True
        UpdateGraphSelectDevice()
    End Sub

    Private Sub TSButtonEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonEditRecord.Click
        Dim mforeColor As Color = Color.Black
        Dim mbackColor As Color = Color.White
        Dim mfont As Font = New Font("Times New Roman", 10, FontStyle.Bold)
        Dim startCharge, stopCharge As Single ' величина загрузки

        For Each lvItem As ListViewItem In ListViewCycle.SelectedItems
            If lvItem.Index - 1 >= 0 Then
                startCharge = Convert.ToSingle(ListViewCycle.Items(lvItem.Index - 1).SubItems(0).Text)
            Else
                startCharge = 0
            End If

            stopCharge = Convert.ToSingle(CType(TSComboBoxStage.SelectedItem, ChargeValueToText).Value) ' Convert.ToSingle(TSipComboBoxВеличина.Text)

            If Not IsStageShelf() Then Exit Sub

            lvItem.SubItems(0).Text = stopCharge.ToString
            lvItem.SubItems(1).Text = tsNumericUpDownTimeDuration.NumericControl.NumericUpDownValue.Text
            lvItem.SubItems(2).Text = TSComboBoxTimeUnit.Text
            lvItem.SubItems(4).Text = CType(TSComboBoxStage.SelectedItem, ChargeValueToText).TextCharge

            'If DomainUpDownПараметр.Text = cRud Then
            '    mfont = New Font("Times New Roman", 10, FontStyle.Bold) 'FontStyle.Regular)
            '    mforeColor = Color.Black
            '    mbackColor = Color.White
            'ElseIf DomainUpDownПараметр.Text = cN2 Then
            '    mfont = New Font("Arial", 10, FontStyle.Bold)
            '    mforeColor = Color.Blue
            '    mbackColor = Color.Magenta
            'End If

            lvItem.ForeColor = mforeColor
            lvItem.BackColor = mbackColor
            lvItem.Font = mfont
            lvItem.ImageIndex = GetImageIndex(startCharge, stopCharge) ' определить imageIndex для текущего
        Next

        ListViewCycle.Refresh()
        isCycleDirty = True
        UpdateGraphSelectDevice()
    End Sub

    Private Function GetImageIndex(startStage As Single, stopStage As Single) As Integer
        Dim imageIndex As Integer = 0

        If startStage = stopStage Then
            imageIndex = 1
        ElseIf startStage < stopStage Then
            imageIndex = 0
        ElseIf startStage > stopStage Then
            imageIndex = 2
        End If

        Return imageIndex
    End Function

    Private Sub TSButtonUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonUp.Click
        StartMoveRow(1)
    End Sub

    Private Sub TSButtonDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonDown.Click
        StartMoveRow(-1)
    End Sub

    ''' <summary>
    ''' Запустить перемещения выделенной строки
    ''' </summary>
    ''' <param name="direct"></param>
    Private Sub StartMoveRow(direct As Integer)
        EnableButtons(False)
        lvPreviousSelectedIndices = lvSelectedIndices
        lvSelectedIndices += direct
        MoveSelectedListViewItem()
        EnableButtons(True)
    End Sub

    ''' <summary>
    ''' Управление доступом к контролам
    ''' </summary>
    ''' <param name="isEnable"></param>
    Private Sub EnableButtons(isEnable As Boolean)
        ToolStripEditCycle.Enabled = isEnable
    End Sub

    ''' <summary>
    ''' Переместить выделенную строку в листе или блок строк
    ''' </summary>
    Private Sub MoveSelectedListViewItem()
        With ListViewCycle
            .Focus()
            If .Items.Count > 0 AndAlso .SelectedIndices.Count <> 0 Then
                If .SelectedIndices.Count > 1 Then
                    ' если вверх и впереди есть невыделенная строка, то эту строку переместить в положение за выделенным блоком
                    ' если вниз и за выделенным блоком есть невыделенная строка, то переместить эту строку впереди перед выделенным блоком
                    ' блок заново выделить
                    If lvPreviousSelectedIndices < lvSelectedIndices AndAlso .SelectedIndices(0) <> 0 Then
                        Dim lvItemClone As ListViewItem = CType(.Items(.SelectedIndices(0) - 1).Clone, ListViewItem) ' эта строка переместится
                        Dim lastIndex As Integer = .SelectedIndices(.SelectedIndices.Count - 1)
                        ' запомнить индекс последней выделенной строки перед удалением
                        ' удалить строку перед выделением
                        .Items.RemoveAt(.SelectedIndices(0) - 1)
                        ' вставить клонированную строку, которая только-что удалена
                        .Items.Insert(lastIndex, lvItemClone)
                    ElseIf lvPreviousSelectedIndices > lvSelectedIndices AndAlso .SelectedIndices(.SelectedIndices.Count - 1) <> .Items.Count - 1 Then
                        Dim lvItemClone As ListViewItem = CType(.Items(.SelectedIndices(.SelectedIndices.Count - 1) + 1).Clone, ListViewItem) ' эта строка переместится
                        Dim firstIndex As Integer = .SelectedIndices(0)
                        ' запомнить индекс первой выделенной строки перед удалением
                        ' удалить строку перед выделением
                        .Items.RemoveAt(.SelectedIndices(.SelectedIndices.Count - 1) + 1)
                        ' вставить клонированную строку, которая только-что удалена
                        .Items.Insert(firstIndex, lvItemClone)
                    End If
                Else
                    .MultiSelect = False
                    Dim selectedIndex As Integer = .SelectedIndices(.SelectedIndices.Count - 1)
                    If lvPreviousSelectedIndices < lvSelectedIndices Then ' вверх
                        If selectedIndex <> 0 Then SwapListViewItem(selectedIndex, selectedIndex - 1)
                    Else
                        If selectedIndex <> .Items.Count - 1 Then SwapListViewItem(selectedIndex, selectedIndex + 1)
                    End If
                End If
            End If

            .MultiSelect = True
        End With

        isCycleDirty = True
        UpdateGraphSelectDevice()
    End Sub

    ''' <summary>
    ''' Обмен содержиммым между соседними строками
    ''' </summary>
    ''' <param name="inFrom"></param>
    ''' <param name="inTo"></param>
    Private Sub SwapListViewItem(ByVal inFrom As Integer, ByVal inTo As Integer)
        Dim text As String
        Dim число, длительность, ЕдВремени, загрузка As String
        Dim indexIcon As Integer
        Dim mforeColor, mbackColor As Color
        Dim mfont As Font

        With ListViewCycle
            Dim currentListViewItem As ListViewItem = .Items(inFrom)
            Dim targetListViewItem As ListViewItem = .Items(inTo)

            ' запомнить предыдущего
            text = targetListViewItem.Text
            число = targetListViewItem.SubItems(0).Text
            длительность = targetListViewItem.SubItems(1).Text
            ЕдВремени = targetListViewItem.SubItems(2).Text
            загрузка = targetListViewItem.SubItems(4).Text
            mforeColor = targetListViewItem.ForeColor
            mbackColor = targetListViewItem.BackColor
            mfont = targetListViewItem.Font
            indexIcon = targetListViewItem.ImageIndex

            ' запись перемещаемого в предыдущий
            targetListViewItem.Text = currentListViewItem.Text
            targetListViewItem.Name = currentListViewItem.Text
            targetListViewItem.SubItems(0).Text = currentListViewItem.SubItems(0).Text
            targetListViewItem.SubItems(1).Text = currentListViewItem.SubItems(1).Text
            targetListViewItem.SubItems(2).Text = currentListViewItem.SubItems(2).Text
            targetListViewItem.SubItems(4).Text = currentListViewItem.SubItems(4).Text
            targetListViewItem.ForeColor = currentListViewItem.ForeColor
            targetListViewItem.BackColor = currentListViewItem.BackColor
            targetListViewItem.Font = currentListViewItem.Font
            targetListViewItem.ImageIndex = currentListViewItem.ImageIndex

            ' перезапись в перемещаемый сохраненные
            currentListViewItem.Text = text
            currentListViewItem.Name = text
            currentListViewItem.SubItems(0).Text = число
            currentListViewItem.SubItems(1).Text = длительность
            currentListViewItem.SubItems(2).Text = ЕдВремени
            currentListViewItem.SubItems(4).Text = загрузка
            currentListViewItem.ForeColor = mforeColor
            currentListViewItem.BackColor = mbackColor
            currentListViewItem.Font = mfont
            currentListViewItem.ImageIndex = indexIcon

            ' выделить
            .Items(inFrom).Selected = False
            targetListViewItem.EnsureVisible()
            .Items(inTo).Selected = True
        End With
    End Sub

    Private Sub TSButtonSaveCycle_Click(sender As Object, e As EventArgs) Handles TSButtonSaveCycle.Click
        SaveListViewCyclogram()
    End Sub

    ''' <summary>
    ''' Сохранить набранную циклограмму для агрегата
    ''' </summary>
    Private Sub SaveListViewCyclogram()
        PopulateTableCycleShelfsFromListView(KeyCycleStage, memoNameDevice)
        SaveDBaseCyclogram()
        UpdateGraphCyclograms()
    End Sub

    ''' <summary>
    ''' Вывести диалоговое окно для проведения записи не сохранённых изменений
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckIsCycleDirty()
        If isCycleDirty Then
            TSButtonSaveCycle.Enabled = False
            Dim text As String = "Были произведены изменения при редактировании циклограммы." & Environment.NewLine & "Произвести сохранение изменений?"
            If MessageBox.Show(text, $"Сохранение изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                SaveListViewCyclogram()
            End If
        End If
        isCycleDirty = False
    End Sub

    ''' <summary>
    ''' Запись перекладки из листа редактора в связанную с данными таблицу
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateTableCycleShelfsFromListView(inKeyCycleStage As Integer, inMemoNameDevice As String)
        TSButtonSaveCycle.Enabled = False

        If ListViewCycle.Items.Count > 0 AndAlso BindingSource3ЦиклУправИсполУстр.Current IsNot Nothing Then
            ' не работают в сязанной таблице
            'CycleDataSet1.ПерекладкиЦикла4.Rows.Clear()
            'DataGridView4ПерекладкиЦикла.Rows.Clear()
            For Each rowCycleShelf As CycleDataSet.ПерекладкиЦикла4Row In CycleDataSet1.ПерекладкиЦикла4.Rows
                rowCycleShelf.Delete()
            Next

            Try
                For Each lvItem As ListViewItem In ListViewCycle.Items
                    'drDataRow = dtDataTable.NewRow()
                    'drDataRow = myDataSet.Tables(strТаблица).NewRow()
                    Dim newRowCycleShelf As CycleDataSet.ПерекладкиЦикла4Row = CycleDataSet1.ПерекладкиЦикла4.NewПерекладкиЦикла4Row
                    newRowCycleShelf.BeginEdit()
                    newRowCycleShelf.keyЦиклЗагрузки = inKeyCycleStage
                    newRowCycleShelf.keyУстройства = GetAttributesChargeParameter(inMemoNameDevice, MainFomMdiParent.PathDBaseCycle).KeyDevice
                    newRowCycleShelf.ЧисловоеЗначение = Convert.ToSingle(lvItem.SubItems(0).Text)
                    newRowCycleShelf.TimeAction = Convert.ToSingle(lvItem.SubItems(1).Text)
                    newRowCycleShelf.TimeValue = lvItem.SubItems(2).Text
                    newRowCycleShelf.ВеличинаЗагрузки = lvItem.SubItems(4).Text
                    newRowCycleShelf.EndEdit()

                    'dtDataTable.Rows.Add(newRowCycleShelf)
                    CycleDataSet1.ПерекладкиЦикла4.AddПерекладкиЦикла4Row(newRowCycleShelf)
                Next
            Catch ex As Exception
                MessageBox.Show(ex.ToString, $"Исключение в процедуре <{NameOf(PopulateTableCycleShelfsFromListView)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If

        isCycleDirty = False
        ShowMessageOnPanel($"Запись циклограммы для агрегата <{LabelNameChargeParameter.Text}> произведена успешно.")
    End Sub

    ''' <summary>
    ''' Список копируемых строк
    ''' </summary>
    Private ListViewItemsMemo As List(Of ListViewItem)
    ''' <summary>
    ''' Список строк начиная с строки после выделенной и до конца листа
    ''' </summary>
    Private ListViewItemsToEnd As List(Of ListViewItem)

    ''' <summary>
    ''' Удалить строки
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TSButtonCutRecords_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonCutRecords.Click
        CopySelectedListViewItemsToMemo()

        For Each lvItem As ListViewItem In ListViewCycle.SelectedItems
            ListViewCycle.Items.Remove(lvItem)
        Next

        UpdateAfterListViewCycleModification()
    End Sub

    Private Sub TSButtonCopyRecords_Click(sender As Object, e As EventArgs) Handles TSButtonCopyRecords.Click
        CopySelectedListViewItemsToMemo()
        EnabledButtonsRecord()
    End Sub

    Private Sub TSButtonPasteRecords_Click(sender As Object, e As EventArgs) Handles TSButtonPasteRecords.Click
        PasteSelectedListViewItemsFromMemo()
    End Sub

    ''' <summary>
    ''' Скопировать выделенные записи в буфер
    ''' </summary>
    Private Sub CopySelectedListViewItemsToMemo()
        'If ListViewItemsMemo Is Nothing Then ListViewItemsMemo = New List(Of ListViewItem)
        ListViewItemsMemo = New List(Of ListViewItem)

        With ListViewCycle
            If .Items.Count > 0 AndAlso .SelectedIndices.Count <> 0 Then
                For Each lvItem As ListViewItem In .SelectedItems
                    ListViewItemsMemo.Add(CType(lvItem.Clone, ListViewItem))
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' Вставить записи из буфера
    ''' </summary>
    Private Sub PasteSelectedListViewItemsFromMemo()
        ListViewItemsToEnd = New List(Of ListViewItem)

        With ListViewCycle
            If .Items.Count > 0 AndAlso .SelectedIndices.Count <> 0 Then
                Dim selectedNextIndex As Integer = .SelectedIndices(.SelectedIndices.Count - 1) + 1

                ' скопировать во временный буфер строки начиная с строки после выделенной и до конца листа
                ' с одновременным их удалением
                For I As Integer = 1 To .Items.Count - selectedNextIndex
                    ListViewItemsToEnd.Add(CType(.Items(selectedNextIndex).Clone, ListViewItem))
                    ' удалить строку после выделения
                    .Items.RemoveAt(selectedNextIndex)
                Next
            End If
            ' вначале вставить из листа с вырезанными или скопированными строкам
            .Items.AddRange(ListViewItemsMemo.ToArray)

            ' нужно вставить после выделения
            ' а затем вставить остаток
            If ListViewItemsToEnd.Count > 0 Then .Items.AddRange(ListViewItemsToEnd.ToArray)

            ' для последующего добавления нужно опять клонировать
            Dim ListViewItemsTemp As New List(Of ListViewItem)
            For Each lvItem As ListViewItem In ListViewItemsMemo
                ListViewItemsTemp.Add(CType(lvItem.Clone, ListViewItem))
            Next
            ListViewItemsMemo = ListViewItemsTemp
        End With

        UpdateAfterListViewCycleModification()
    End Sub

    ''' <summary>
    ''' Обновить График
    ''' </summary>
    Private Sub UpdateAfterListViewCycleModification()
        ListViewCycle.Refresh()
        isCycleDirty = True
        UpdateGraphSelectDevice()
        EnabledButtonsRecord()
    End Sub

    ''' <summary>
    ''' Включить видимость кнопок Cut, Copy, Paste
    ''' </summary>
    Private Sub EnabledButtonsRecord()
        TSButtonCutRecords.Enabled = ListViewCycle.SelectedItems.Count > 0
        TSButtonCopyRecords.Enabled = TSButtonCutRecords.Enabled
        TSButtonPasteRecords.Enabled = If(ListViewItemsMemo Is Nothing, False, ListViewItemsMemo.Count > 0)
    End Sub

    ''' <summary>
    ''' Очистка буферов
    ''' </summary>
    Private Sub PrepareButtonsRecord()
        ListViewItemsMemo = Nothing
        ListViewItemsToEnd = Nothing
        EnabledButtonsRecord()
    End Sub

    ''' <summary>
    ''' Очистить Все Строки
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TSButtonClearRecords_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonClearRecords.Click
        ListViewCycle.Items.Clear()
        isCycleDirty = True
        UpdateGraphSelectDevice()
    End Sub

    ''' <summary>
    ''' По тексту lvItem.SubItems(0).Text единицы измерения выделить ComboBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ListViewЦикл_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListViewCycle.SelectedIndexChanged
        For Each lvItem As ListViewItem In ListViewCycle.SelectedItems
            Dim query As IEnumerable(Of ChargeValueToText) = From itemChargeValueToText As ChargeValueToText In TSComboBoxStage.Items.Cast(Of ChargeValueToText)
                                                             Where itemChargeValueToText.Value.ToString = lvItem.SubItems(0).Text
                                                             Select itemChargeValueToText
            If query.Count > 0 Then
                TSComboBoxStage.SelectedItem = query(0)
            End If

            tsNumericUpDownTimeDuration.NumericControl.NumericUpDownValue.Text = lvItem.SubItems(1).Text
            TSComboBoxTimeUnit.Text = lvItem.SubItems(2).Text
        Next

        EnabledButtonsRecord()
    End Sub

    ''' <summary>
    ''' Обновить график циклограммы веделенного устройтва.
    ''' Рисовать график по точкам и добавить аннотации.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateGraphSelectDevice()
        ' для данного цикла выбрать все устройства
        ' в цикле по устройствам делать запросы на выборку
        ' в цикле по записям настроить плоты и теги  в масштабе для перекладок цикла для данного устройства
        Dim timeUnit As String ' Ед Изм Время
        Dim stage As String = Nothing
        Dim charge, timeDuration As Double
        Dim I As Integer = 0
        Dim plot As ScatterPlot = New ScatterPlot() With {.LineColor = Color.Lime, .PointColor = Color.Red, .PointStyle = PointStyle.SolidCircle}
        Const OFFSET_X As Single = 10
        Const OFFSET_Y As Single = -20
        Dim sumTimeCycle As Double
        Dim pointsCount As Integer = ListViewCycle.Items.Count * 2 - 1
        Dim XPoints(pointsCount) As Double
        Dim YPoints(pointsCount) As Double

        'XyCursor1.XPosition = 0
        'XyCursor1.YPosition = 0
        'XyCursor1.Plot = Nothing
        ScatterGraphSelectDevice.Annotations.Clear()
        ScatterGraphSelectDevice.Plots.Clear()

        For Each lvItem As ListViewItem In ListViewCycle.Items
            ' определяем массив координат для точек данной загрузки
            stage = lvItem.Text
            charge = Convert.ToDouble(lvItem.SubItems(0).Text)
            timeDuration = Convert.ToDouble(lvItem.SubItems(1).Text) ' Длительность
            timeUnit = lvItem.SubItems(2).Text  ' Ед.изм.

            If timeUnit = "Мин" Then
                timeDuration *= 60
            ElseIf timeUnit = "Час" Then
                timeDuration *= 3600
            End If

            If I = 0 Then ' это 1 точка
                XPoints(I) = 0
                YPoints(I) = charge
                XPoints(I + 1) = Math.Round(timeDuration, 3)
                YPoints(I + 1) = charge
                sumTimeCycle += timeDuration
            Else
                XPoints(I) = Math.Round(sumTimeCycle, 3)
                YPoints(I) = charge
                sumTimeCycle += timeDuration
                XPoints(I + 1) = Math.Round(sumTimeCycle, 3)
                YPoints(I + 1) = charge
            End If
            I += 2
        Next

        I = 0
        ScatterGraphSelectDevice.Plots.Add(plot)
        ' привязка к осям если они разные
        plot.XAxis = XAxis1
        plot.YAxis = YAxis1
        plot.PlotXY(XPoints, YPoints)
        'XyCursor1.Plot = plot

        For I = 0 To pointsCount Step 2
            stage = ListViewCycle.Items(I \ 2).Text
            charge = CDbl(ListViewCycle.Items(I \ 2).SubItems(0).Text)
            timeDuration = CDbl(ListViewCycle.Items(I \ 2).SubItems(1).Text)
            timeUnit = ListViewCycle.Items(I \ 2).SubItems(2).Text

            With ScatterGraphSelectDevice
                ' смещение в пикселях
                TempPointAnnotation = New XYPointAnnotation With {
                    .ArrowColor = Color.Aqua,
                    .ArrowHeadStyle = ArrowStyle.SolidStealth,
                    .ArrowLineWidth = 1.0!,
                    .ArrowTailSize = New Size(20, 15),
                    .Caption = String.Format("{0} : {1} ({2})", charge, timeDuration, timeUnit),
                    .CaptionAlignment = New AnnotationCaptionAlignment(BoundsAlignment.None, OFFSET_X, OFFSET_Y),
                    .CaptionFont = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte)),
                    .ShapeFillColor = Color.Red,
                    .ShapeSize = New Size(5, 5),
                    .ShapeStyle = ShapeStyle.Oval,
                    .ShapeZOrder = AnnotationZOrder.AbovePlot,
                    .XAxis = XAxis1,
                    .XPosition = XPoints(I),
                    .YAxis = YAxis1,
                    .YPosition = YPoints(I)
                }
                TempPointAnnotation.SetPosition(XPoints(I), YPoints(I))
                .Annotations.Add(TempPointAnnotation)
            End With
        Next

        Dim currentRow As CycleDataSet.ЦиклЗагрузки3Row = CType(CType(BindingSource3ЦиклУправИсполУстр.Current, DataRowView).Row, CycleDataSet.ЦиклЗагрузки3Row)
        KeyCycleStage = currentRow.keyЦиклЗагрузки
        memoNameDevice = currentRow.ИмяУстройства
        ' определим максимальное значение из всех sumTimeCycle
        sumTimeCycle = Math.Round(sumTimeCycle, 3)
        LabelTimeAllCyrcle.Text = sumTimeCycle.ToString & " секунд"
        TSButtonSaveCycle.Enabled = True

        UpdateIntervalListViewItems()
        UpdateGraphCyclograms()
    End Sub

    ''' <summary>
    ''' Для дискретных(логических) переменных график рисуется как ступеньки
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsStageShelf() As Boolean
        Dim message As String = String.Empty
        Dim timeDuration As Double = Double.Parse(tsNumericUpDownTimeDuration.NumericControl.NumericUpDownValue.Text)

        If Math.Round(timeDuration, 2) = 0 Then
            message += "Для дискретных длительность перекладки не может равняться нулю!"
        End If

        If message <> String.Empty Then
            MessageBox.Show(message, "Проверка дискретных перекладок", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ShowMessageOnPanel(message)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Раскрыть дополнительную панель
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonShowMore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonShowMore.Click
        TSMenuItemShowAllGraphs.Checked = Not TSMenuItemShowAllGraphs.Checked
    End Sub

    Private Sub TSMenuItemShowAllGraphs_CheckedChanged(sender As Object, e As EventArgs) Handles TSMenuItemShowAllGraphs.CheckedChanged
        ShowPanelAllGraphs()
    End Sub

    Private Sub ShowPanelAllGraphs()
        If TSMenuItemShowAllGraphs.Checked Then
            ButtonShowMore.Image = Resources.up ' Registration.ProjectResources.SINEWAVE - можно и так
            ButtonShowMore.ToolTipText = HIDE_PANEL
            SplitContainerGraf.Panel1Collapsed = False
            'SplitContainerForm.SplitterDistance = SplitContainerForm.SplitterDistance - 24
        Else
            ButtonShowMore.Image = Resources.down
            ButtonShowMore.ToolTipText = HOW_PANEL
            SplitContainerGraf.Panel1Collapsed = True
            'SplitContainerForm.SplitterDistance = SplitContainerForm.SplitterDistance + 24
        End If
    End Sub

    Private Sub TSMenuItemShowNavigation_CheckedChanged(sender As Object, e As EventArgs) Handles TSMenuItemShowNavigation.CheckedChanged
        SplitContainerForm.Panel1Collapsed = Not TSMenuItemShowNavigation.Checked
    End Sub
#End Region

#Region "График по устройствам"
    ''' <summary>
    ''' Обновить График По Всем Устройствам.
    ''' Вывод шлейфов всех устройств после перезаписи или загрузки новой программы.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateGraphCyclograms()
        Dim AllPointsTimeDuration As New List(Of Double()) ' для храненеия массивов Длительность
        Dim AllPointsStageToNumeric As New List(Of Double()) ' для храненеия массивов Величина
        Dim AllSumTimeCycle As New List(Of Double) ' Суммарное Время Перекладки

        PrepareCollectionsForUpdateGraph(AllPointsTimeDuration, AllPointsStageToNumeric, AllSumTimeCycle)

        '--- Второй этап, если нет ошибки -------------------------------------
        Dim offset, offset2 As Double
        Dim plotCount As Integer = CycleDataSet1.ЦиклЗагрузки3.Rows.Count ' кол. Шлейфов
        Dim slope As Double = 1 / plotCount
        Dim xoffset As Single = 10
        Dim yoffset As Single
        Dim ColorsNet As Color() = {Color.Blue, Color.Red, Color.DarkGreen, Color.DarkOrange, Color.DarkCyan, Color.Magenta, Color.Maroon, Color.Black}
        Dim maximumY, minimumY As Double

        For indexDevice As Integer = 0 To CycleDataSet1.ЦиклЗагрузки3.Rows.Count - 1
            If indexDevice > AllPointsTimeDuration.Count - 1 Then Continue For
            If AllPointsTimeDuration.Item(indexDevice).Length = 0 Then Exit For

            ' привязка к осям если они разные
            Dim plot As ScatterPlot = New ScatterPlot With {
                .LineColor = ColorsNet(indexDevice Mod 7),
                .PointColor = Color.Red,
                .PointStyle = PointStyle.SolidCircle,
                .XAxis = XAxis2,
                .YAxis = YAxis2,
                .Tag = CycleDataSet1.ЦиклЗагрузки3.Rows(indexDevice).Item("ИмяУстройства")
            }
            ScatterGraphCycle.Plots.Add(plot)

            If indexDevice = 0 Then
                offset2 = 0
            Else
                offset2 = (2 / plotCount) * indexDevice * 1.01
            End If

            ' нормализовать и привести щлейф для загрузки
            Dim maximumX As Double = AllSumTimeCycle.Item(indexDevice)
            Dim mean As Double = 0
            Dim standartDeviation As Double = 0
            Dim scale As Double = 0

            ' нормализовать входной вектор
            Dim normalizePointsStage As Double() = ArrayOperation.Normalize1D(AllPointsStageToNumeric.Item(indexDevice), mean, standartDeviation)
            ' скалировать входной вектор к диапазогу [-1;1]
            Dim scaleNormalizePointsStage As Double() = ArrayOperation.Scale1D(normalizePointsStage, offset, scale)
            ' произвести линеаризацию вектора к данному диапазону с  учётом смещения
            Dim linearScaleNormalizePointsStage As Double() = ArrayOperation.LinearEvaluation1D(scaleNormalizePointsStage, slope, offset2)
            ' найти мин и макс значения
            ArrayOperation.MaxMin1D(linearScaleNormalizePointsStage, maximumY, minimumY)
            ' заполнить график значениями
            plot.PlotXY(AllPointsTimeDuration.Item(indexDevice), linearScaleNormalizePointsStage)
            ' привязать курсор к графику
            XyCursor1.Plot = plot

            For I As Integer = 0 To UBound(AllPointsTimeDuration.Item(indexDevice)) - 1 Step 2
                If indexDevice = CycleDataSet1.ЦиклЗагрузки3.Rows.Count - 1 Then
                    ' смещение в пикселях
                    If AllPointsStageToNumeric.Item(indexDevice)(I) = 0 Then
                        yoffset = -20
                    Else
                        yoffset = 20
                    End If
                Else
                    yoffset = -20
                End If

                TempPointAnnotation = New XYPointAnnotation With {
                    .ArrowColor = Color.Gold,
                    .ArrowHeadStyle = ArrowStyle.SolidStealth,
                    .ArrowLineWidth = 1.0!,
                    .ArrowTailSize = New Size(20, 15),
                    .Caption = $"{CStr(Math.Round(AllPointsStageToNumeric.Item(indexDevice)(I), 3))} на {CStr(AllPointsTimeDuration.Item(indexDevice)(I))}(ой) сек",
                    .CaptionAlignment = New AnnotationCaptionAlignment(BoundsAlignment.None, xoffset, yoffset),
                    .CaptionFont = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte)),
                    .CaptionForeColor = Color.Magenta,
                    .ShapeFillColor = Color.Red, ' идёт первым
                    .ShapeSize = New Size(5, 5),
                    .ShapeStyle = ShapeStyle.Oval,
                    .ShapeZOrder = AnnotationZOrder.AbovePlot,
                    .XAxis = XAxis2,
                    .XPosition = AllPointsTimeDuration.Item(indexDevice)(I),
                    .YAxis = YAxis2,
                    .YPosition = linearScaleNormalizePointsStage(I)
                }
                TempPointAnnotation.SetPosition(AllPointsTimeDuration.Item(indexDevice)(I), linearScaleNormalizePointsStage(I))
                ScatterGraphCycle.Annotations.Add(TempPointAnnotation)
            Next

            ' привязать к началу шлейфа аннотацию с пояснением к какому агрегату он относится
            Dim deviceЦиклЗагрузки3Row As CycleDataSet.ЦиклЗагрузки3Row = CType(CycleDataSet1.ЦиклЗагрузки3.Rows(indexDevice), CycleDataSet.ЦиклЗагрузки3Row)
            ' смещение по в пикселях
            xoffset = 10
            yoffset = -20
            TempPointAnnotation = New XYPointAnnotation With {
                .ArrowColor = plot.LineColor,
                .ArrowHeadStyle = ArrowStyle.SolidStealth,
                .ArrowLineWidth = 2.0!,
                .ArrowTailSize = New Size(20, 15),
                .Caption = deviceЦиклЗагрузки3Row.ИмяУстройства,
                .CaptionAlignment = New AnnotationCaptionAlignment(BoundsAlignment.None, xoffset, yoffset),
                .CaptionFont = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte)),
                .CaptionForeColor = plot.LineColor,
                .ShapeSize = New Size(5, 5),
                .ShapeStyle = ShapeStyle.Oval,
                .ShapeZOrder = AnnotationZOrder.AbovePlot,
                .XAxis = XAxis2,
                .XPosition = AllPointsTimeDuration.Item(indexDevice)(0),
                .YAxis = YAxis2,
                .YPosition = linearScaleNormalizePointsStage(0)
            }

            TempPointAnnotation.SetPosition(AllPointsTimeDuration.Item(indexDevice)(0), linearScaleNormalizePointsStage(0))
            ScatterGraphCycle.Annotations.Add(TempPointAnnotation)
        Next indexDevice

        ' привязка к осям если они разные
        'plot.XAxis = XAxis2
        'plot.YAxis = YAxis2

        ' определим максимальное значение из всех sumTimeCycle
        If AllSumTimeCycle.Count > 0 Then ArrayOperation.MaxMin1D(AllSumTimeCycle.ToArray, maxSumTimeCycle, minimumY)
        SelectScatterPlot(memoNameDevice)
    End Sub

    ''' <summary>
    ''' Выделить шлейф связанный с текущей строкой устройства
    ''' </summary>
    ''' <param name="nameDevice"></param>
    Private Sub SelectScatterPlot(nameDevice As String)
        For Each itemPlot As ScatterPlot In ScatterGraphCycle.Plots
            If itemPlot.Tag IsNot Nothing Then
                If itemPlot.Tag.ToString = nameDevice Then
                    itemPlot.LineWidth = 3
                Else
                    itemPlot.LineWidth = 1
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Подготовить коллекции для построения графиков циклограмм.
    ''' </summary>
    ''' <param name="refAllPointsTimeDuration"></param>
    ''' <param name="refAllPointsStageToNumeric"></param>
    ''' <param name="refAllSumTimeCycle"></param>
    Private Sub PrepareCollectionsForUpdateGraph(ByRef refAllPointsTimeDuration As List(Of Double()),
                                                 ByRef refAllPointsStageToNumeric As List(Of Double()),
                                                 ByRef refAllSumTimeCycle As List(Of Double))
        ' для данного цикла выбрать все устройства
        ' в цикле по устройствам делать запросы на выборку
        ' в цикле по записям настроить плоты и теги  в масштабе для перекладок цикла для данного устройства
        If CycleDataSet1.ЦиклЗагрузки3.Rows.Count = 0 Then Exit Sub

        Dim I As Integer
        Dim cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
        'Dim AllTimeUnits As New List(Of List(Of String)) ' для храненеия TimeUnits
        Dim TimeDurations As New List(Of Double)
        Dim Stages As New List(Of Double)
        Dim TimeUnits As New List(Of String)
        Dim pointsCount As Integer ' кол. Точек
        Dim pointsStageToNumeric() As Double ' Величина
        Dim pointsTimeDuration() As Double ' Длительность
        Dim position As Integer
        Dim sumTimeCycle As Double

        XyCursor1.XPosition = 0
        XyCursor1.YPosition = 0
        XyCursor1.Plot = Nothing
        ScatterGraphCycle.Annotations.Clear()
        ScatterGraphCycle.Plots.Clear()

        Try
            cn.Open()
            Dim cmd As OleDbCommand = cn.CreateCommand
            cmd.CommandType = CommandType.Text

            For Each itemЦиклЗагрузки3Row As CycleDataSet.ЦиклЗагрузки3Row In CycleDataSet1.ЦиклЗагрузки3.Rows
                Dim nameDevice As String = CType(itemЦиклЗагрузки3Row, CycleDataSet.ЦиклЗагрузки3Row).ИмяУстройства

                cmd.CommandText = "SELECT ЦиклЗагрузки3.*, ПерекладкиЦикла4.* " &
                    "FROM ЦиклЗагрузки3 RIGHT JOIN ПерекладкиЦикла4 ON ЦиклЗагрузки3.keyЦиклЗагрузки = ПерекладкиЦикла4.keyЦиклЗагрузки " &
                    "WHERE (((ЦиклЗагрузки3.keyЦиклЗагрузки)= " & itemЦиклЗагрузки3Row.keyЦиклЗагрузки.ToString & ")) " &
                    "ORDER BY ПерекладкиЦикла4.keyПерекладкиЦикла;"
                Dim rdr As OleDbDataReader = cmd.ExecuteReader

                Do While rdr.Read()
                    Stages.Add(Convert.ToDouble(rdr("ЧисловоеЗначение")))
                    TimeDurations.Add(Convert.ToDouble(rdr("TimeAction"))) ' Длительность
                    TimeUnits.Add(rdr("TimeValue").ToString) ' Ед.изм.
                Loop

                rdr.Close()

                If itemЦиклЗагрузки3Row.keyПрограммаИспытаний = -1 Then
                    ' для вновь добавленной строки сообщение об отсутствии перекладок можно не выводить
                    Exit Sub
                End If

                If TimeDurations.Count = 0 Then
                    MessageBox.Show($"Для устройства <{itemЦиклЗагрузки3Row.ИмяУстройства}> {Environment.NewLine}цикл исполнения отсутствует. Необходимо ввести хотя бы один уровень.",
                                    "Загрузка цикла", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    BindingSource3ЦиклУправИсполУстр.Position = position
                    Exit For
                End If

                pointsCount = TimeDurations.Count * 2 - 1
                'ReDim_pointsTimeDuration(pointsCount)
                'ReDim_pointsStageToNumeric(pointsCount)
                Re.Dim(pointsTimeDuration, pointsCount)
                Re.Dim(pointsStageToNumeric, pointsCount)
                sumTimeCycle = 0
                I = 0

                For indexPower As Integer = 0 To TimeDurations.Count - 1
                    ' определяем массив координат для точек данной загрузки
                    Dim charge As Double = Stages(indexPower) ' Старт
                    Dim timeDuration As Double = TimeDurations(indexPower) ' Длительность
                    Dim timeUnit As String = TimeUnits(indexPower) ' Ед.изм.

                    If timeUnit = "Мин" Then
                        timeDuration *= 60
                    ElseIf timeUnit = "Час" Then
                        timeDuration *= 3600
                    End If

                    If I = 0 Then 'это 1 точка
                        pointsTimeDuration(I) = 0
                        pointsStageToNumeric(I) = charge
                        pointsTimeDuration(I + 1) = Math.Round(timeDuration, 3)
                        pointsStageToNumeric(I + 1) = charge
                        sumTimeCycle += timeDuration
                    Else
                        pointsTimeDuration(I) = Math.Round(sumTimeCycle, 3)
                        pointsStageToNumeric(I) = charge
                        sumTimeCycle += timeDuration
                        pointsTimeDuration(I + 1) = Math.Round(sumTimeCycle, 3)
                        pointsStageToNumeric(I + 1) = charge
                    End If
                    I += 2
                Next

                refAllPointsTimeDuration.Add(CType(pointsTimeDuration.Clone, Double()))
                refAllPointsStageToNumeric.Add(CType(pointsStageToNumeric.Clone, Double()))
                'AllTimeUnits.Add(TimeUnits.ToList) '.Clone отсутствует
                refAllSumTimeCycle.Add(sumTimeCycle)

                I = 0
                Stages.Clear()
                TimeDurations.Clear()
                TimeUnits.Clear()
                sumTimeCycle = 0
                position += 1
            Next
        Catch ex As Exception
            Dim text As String = ex.ToString
            MessageBox.Show(text, $"Ошибка обновления циклограммы в процедуре <{NameOf(PrepareCollectionsForUpdateGraph)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If (cn.State = ConnectionState.Open) Then
                cn.Close()
            End If
        End Try
    End Sub
#End Region

    Private Sub ShowMessageOnPanel(ByVal message As String)
        TSStatusLabelMessage.Text = message
    End Sub

    Private Shared Sub MessageLate()
        MessageBox.Show("Функция будет реализована позднее.", "Тест", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class