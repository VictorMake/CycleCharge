Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization.Json
Imports System.Windows.Forms

''' <summary>
''' Компонент визуализации кофигурации оборудования.
''' Используется для редактирования мощности загрузки агрегата. 
''' </summary>
Public Class ConfigurationDevices
    Implements INotifyPropertyChanged
    Implements IEnumerable

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Default Public Property Item(key As Integer) As Device
        Get
            Return allDevices(key)
        End Get
        Set(value As Device)
            allDevices(key) = value
        End Set
    End Property

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return allDevices.GetEnumerator()
    End Function

    ''' <summary>
    ''' Предоставляет универсальную коллекцию, которая поддерживает привязку данных.
    ''' </summary>
    ''' <returns></returns>    
    Public ReadOnly Property DeviceBindingList() As BindingList(Of Device)
        Get
            Return allDevices.DeviceBindingList
        End Get
    End Property

    ''' <summary>
    ''' Возвращает или задает индекс, указывающий текущий выделенный элемент - устройство в ComboBoxDevices.SelectedIndex
    ''' </summary>
    ''' <returns></returns>
    Public Property DeviceSelectedIndex As Integer
        Get
            Return ComboBoxDevices.SelectedIndex
        End Get
        Set
            If Value >= 0 AndAlso Value < ComboBoxDevices.Items.Count Then
                ComboBoxDevices.SelectedIndex = Value
                'OnPropertyChanged(NameOf(DeviceSelectedIndex))
                OnPropertyChanged()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Возвращает или задает индекс, указывающий текущий выделенный элемент - порт в ComboBoxPorts.SelectedIndex
    ''' </summary>
    ''' <returns></returns>
    Public Property PortSelectedIndex As Integer
        Get
            Return ComboBoxPorts.SelectedIndex
        End Get
        Set
            If Value >= 0 AndAlso Value < ComboBoxPorts.Items.Count Then
                ComboBoxPorts.SelectedIndex = Value
                'OnPropertyChanged(NameOf(PortSelectedIndex))
                OnPropertyChanged()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Реализация INotifyPropertyChanged
    ''' </summary>
    ''' <param name="propertyName"></param>
    Public Sub OnPropertyChanged(<CallerMemberName> ByVal Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    'Protected Sub OnPropertyChanged(Optional ByVal propertyName As String = Nothing)
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    'End Sub

    ''' <summary>
    ''' Определяет, содержит ли последовательность указанный элемент, используя компаратор
    ''' проверки на равенство по умолчанию.
    ''' </summary>
    ''' <param name="inNumber"></param>
    ''' <returns></returns>
    Public ReadOnly Property ContainsDevice(inNumber As Integer) As Boolean
        Get
            Return allDevices.Contains(inNumber)
        End Get
    End Property

    '''' <returns></returns>
    ''' <summary>
    ''' Полный путь к файлу настроек
    ''' </summary>
    Private PathFileConfigurationDevices As String
    Private Const FileConfigurationDevices As String = "ConfigurationDevices.json"
    Private allDevices As Devices

    Public Sub New()
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        ' Добавить код инициализации после вызова InitializeComponent().
        ComboBoxDevices.Enabled = False
        ComboBoxPorts.Enabled = False
    End Sub

    ''' <summary>
    ''' Установка всех линий как свободных для настроек.
    ''' </summary>
    Public Sub SetDefaultAllLineBusyOff()
        For Each itemDevice As Device In allDevices
            For Each itemPort As Port In itemDevice
                For Each itemLine As Line In itemPort
                    itemLine.IsBusy = False
                Next
            Next
        Next
    End Sub

    ''' <summary>
    ''' Установка всех линий в выключенное состояние.
    ''' </summary>
    Public Sub SetDefaultAllLineValueOff()
        For Each itemDevice As Device In allDevices
            For Each itemPort As Port In itemDevice
                For Each itemLine As Line In itemPort
                    If itemLine.IsEnabled Then itemLine.LineControl.Value = False
                Next
            Next
        Next
    End Sub

    ''' <summary>
    ''' Синхронизировать занятость линий для текущего агрегата текущего изделия.
    ''' </summary>
    ''' <param name="busyDevices">содержит занятые линии всех других агрегатов из базы</param>
    Public Sub SynchronizeBusyLine(busyDevices As Devices)
        SetDefaultAllLineBusyOff()

        For Each itemDevice As Device In busyDevices
            For Each itemPort As Port In itemDevice
                For Each itemLine As Line In itemPort
                    Dim selectedDevices = From qDevice As Device In allDevices
                                          Where qDevice.Number = itemDevice.Number
                                          Select qDevice

                    If selectedDevices.Count > 0 Then
                        Dim selectedPorts = From qPort As Port In selectedDevices.First
                                            Where qPort.Number = itemPort.Number
                                            Select qPort

                        If selectedPorts.Count > 0 Then
                            Dim selectedLines = From qLine As Line In selectedPorts.First
                                                Where qLine.Number = itemLine.Number
                                                Select qLine
                            If selectedLines.Count > 0 Then
                                selectedLines.First.IsBusy = itemLine.IsBusy ' = True
                            End If
                        End If
                    End If
                Next
            Next
        Next
    End Sub

    ''' <summary>
    ''' Синхронизировать включение линий для величины загрузки выбранного агрегата текущего изделия.
    ''' </summary>
    ''' <param name="devicesWithOnValue">содержит включенные линии для данной мощности загрузки</param>
    Public Sub SynchronizeValueLine(devicesWithOnValue As Devices)
        SetDefaultAllLineValueOff()

        For Each itemDevice As Device In devicesWithOnValue
            For Each itemPort As Port In itemDevice
                For Each itemLine As Line In itemPort
                    Dim selectedDevices = From qDevice As Device In allDevices
                                          Where qDevice.Number = itemDevice.Number
                                          Select qDevice

                    If selectedDevices.Count > 0 Then
                        Dim selectedPorts = From qPort As Port In selectedDevices.First
                                            Where qPort.Number = itemPort.Number
                                            Select qPort

                        If selectedPorts.Count > 0 Then
                            Dim selectedLines = From qLine As Line In selectedPorts.First
                                                Where qLine.Number = itemLine.Number
                                                Select qLine
                            If selectedLines.Count > 0 Then
                                If selectedLines.First.IsEnabled Then
                                    selectedLines.First.LineControl.Value = itemLine.LineControl.Value ' = True
                                End If
                            End If
                        End If
                    End If
                Next
            Next
        Next
    End Sub

    ''' <summary>
    ''' Считывание конфигурации из файла и восстановление иерархии устройств.
    ''' </summary>
    ''' <param name="inPathResourses"></param>
    Public Sub RestoreDevicesFromFile(inPathResourses As String)
        PathFileConfigurationDevices = Path.Combine(inPathResourses, FileConfigurationDevices)

        If Not File.Exists(PathFileConfigurationDevices) Then GreateCongigByDefault()

        ReadDevices()
        InitializeComponentAgain()
    End Sub

    ''' <summary>
    ''' Вызывается при считывании конфигурации или после её обновления.
    ''' </summary>
    ''' <param name="inDevices"></param>
    Public Sub ReinitializeDevices(inDevices As Devices)
        If Not AssertDevicesIsCorrect(inDevices) Then
            MessageBox.Show($"Конфигурация оборудования не содержит корректный состав устройств!",
                            $"Процедура: <{NameOf(ReinitializeDevices)}>", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        allDevices = inDevices
        SerializerDevices(allDevices)
        InitializeComponentAgain()
    End Sub

    ''' <summary>
    ''' Проверка корректности конфигурации состава устройств.
    ''' </summary>
    ''' <param name="inDevices"></param>
    ''' <returns></returns>
    Private Function AssertDevicesIsCorrect(inDevices As Devices) As Boolean
        Return inDevices.DevicesToArray.Count <> 0 OrElse
            inDevices(0).PortsToArray.Count <> 0 OrElse
            inDevices(0).PortsToArray(0).PortLinesToArray.Count <> 0
    End Function

    ''' <summary>
    ''' Инициализация всех контролов заново.
    ''' </summary>
    Private Sub InitializeComponentAgain()
        ComboBoxDevices.Items.Clear()
        ComboBoxPorts.Items.Clear()
        ListBoxFrameByte.Items.Clear()
        ClearTableLayoutPanelPort()
        LabelState.Text = ""
        ComboBoxDevices.Items.AddRange(allDevices.DevicesToArray)
        ComboBoxDevices.DisplayMember = "ToString"
        HideTablePanelBytes()
        EnabledButtons(False)
    End Sub

    ''' <summary>
    ''' Очистить TableLayoutPanelPort от контролов типа BaseLineControl.
    ''' </summary>
    Public Sub ClearTableLayoutPanelPort()
        Dim deletedControls As New List(Of BaseLineControl)

        For Each itemControl As Control In TableLayoutPanelPort.Controls
            If TypeOf itemControl Is BaseLineControl Then
                deletedControls.Add(CType(itemControl, BaseLineControl))
                'RemoveHandler CType(itemControl, BaseLineControl).ControlStateChanged, AddressOf ControlStateChangedAddressCallback ' Пользовательское управление событием
                RemoveHandler CType(itemControl, BaseLineControl).PropertyChanged, New PropertyChangedEventHandler(AddressOf Data_PropertyChanged)
            End If
        Next

        If deletedControls.Count > 0 Then
            Me.SuspendLayout()

            For Each itemControl As Control In deletedControls
                TableLayoutPanelPort.Controls.Remove(itemControl)
            Next

            Me.ResumeLayout(False)
        End If

        LabelCaption.Text = ""
    End Sub

    Private Sub ComboBoxDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDevices.SelectedIndexChanged
        PopulatePortsOnSelectedDevice()
    End Sub

    Private Sub ComboBoxPorts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxPorts.SelectedIndexChanged
        PopulatePortLinesOnSelectedPort()
    End Sub

    Private Sub ListBoxFrameByte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxFrameByte.SelectedIndexChanged
        PopulateControlsOnSelectedPortLine()
    End Sub

    ''' <summary>
    ''' Заново пройтись по обновлениям и подсветиить устройство и порт для выбранной мощности.
    ''' </summary>
    ''' <param name="comparisonDevices"></param>
    Public Sub PopulateAfterChargeChanged(comparisonDevices As Devices)
        ' т.к. в comparisonDevices только по одному устройству и порту для выделенной мощности,
        ' то они однозначно в по нулевому индексу в массиве
        Dim mDevice As Device = comparisonDevices.DevicesToArray(0)
        Dim mPort As Port = mDevice.PortsToArray(0)

        ComboBoxDevices.Text = mDevice.ToString
        PopulatePortsOnSelectedDevice()
        ComboBoxPorts.Text = mPort.ToString
        PopulatePortLinesOnSelectedPort()
        ListBoxFrameByte.SelectedIndex = 0
        PopulateControlsOnSelectedPortLine()
    End Sub

    ''' <summary>
    ''' Заполнить порты при выделении устройства.
    ''' </summary>
    Private Sub PopulatePortsOnSelectedDevice()
        ClearTableLayoutPanelPort()
        ListBoxFrameByte.Items.Clear()
        LabelState.Text = ""
        ComboBoxPorts.Items.Clear()
        ComboBoxPorts.Text = ""
        ComboBoxPorts.Items.AddRange(CType(ComboBoxDevices.Items(ComboBoxDevices.SelectedIndex), Device).PortsToArray)
        ComboBoxPorts.DisplayMember = "ToString"
        HideTablePanelBytes()
        EnabledButtons(False)
        DeviceSelectedIndex = ComboBoxDevices.SelectedIndex
    End Sub

    ''' <summary>
    ''' Заполнить группы линий при выделении порта.
    ''' </summary>
    Private Sub PopulatePortLinesOnSelectedPort()
        ClearTableLayoutPanelPort()
        ListBoxFrameByte.Items.Clear()
        Dim mPort As Port = CType(ComboBoxPorts.Items(ComboBoxPorts.SelectedIndex), Port)
        ListBoxFrameByte.Items.AddRange(mPort.PortLinesToArray)
        ListBoxFrameByte.DisplayMember = "ToString"
        LabelState.Text = $"port{mPort.Number} Состояние"
        SetVisibleTablePanelByte(mPort)
        EnabledButtons(mPort.PortLinesToArray.Count > 0)
        SetNumericTablePanelBytes()
        PortSelectedIndex = ComboBoxPorts.SelectedIndex
    End Sub

    ''' <summary>
    ''' Включение/выключение кнопок.
    ''' </summary>
    ''' <param name="inEnabled"></param>
    Private Sub EnabledButtons(inEnabled As Boolean)
        ButtonUp.Enabled = inEnabled
        ButtonDown.Enabled = inEnabled
        ButtonAllHigh.Enabled = False
        ButtonAllLow.Enabled = False
    End Sub

    ''' <summary>
    ''' Установить видимость фреймов байтов содержащихся в порту.
    ''' </summary>
    ''' <param name="inPort"></param>
    Private Sub SetVisibleTablePanelByte(inPort As Port)
        HideTablePanelBytes()

        For Each itemPortLine As PortLine In inPort.GetEnumeratorPortLine
            TableLayoutPanelByte.Controls.Item($"TablePanelByte{itemPortLine.Number}").Visible = True
        Next
    End Sub

    ''' <summary>
    ''' Скрыть фреймы байтов содержащихся в порту.
    ''' </summary>
    Private Sub HideTablePanelBytes()
        TablePanelByte1.Visible = False
        TablePanelByte2.Visible = False
        TablePanelByte3.Visible = False
        TablePanelByte4.Visible = False
        SetDefaultBackColorTablePanelBytes()
    End Sub

    ''' <summary>
    ''' Установить фон по умолчанию.
    ''' </summary>
    Private Sub SetDefaultBackColorTablePanelBytes()
        TablePanelByte1.BackColor = Drawing.SystemColors.Control
        TablePanelByte2.BackColor = Drawing.SystemColors.Control
        TablePanelByte3.BackColor = Drawing.SystemColors.Control
        TablePanelByte4.BackColor = Drawing.SystemColors.Control
    End Sub

    ''' <summary>
    ''' Заполнить контролы от линий в фрейме байта порта.
    ''' </summary>
    Private Sub PopulateControlsOnSelectedPortLine()
        If ListBoxFrameByte.SelectedIndex = -1 Then Exit Sub

        ClearTableLayoutPanelPort()

        Dim mPortLine As PortLine = CType(ListBoxFrameByte.Items(ListBoxFrameByte.SelectedIndex), PortLine)
        Dim count As Integer = 8

        Me.SuspendLayout()
        For Each itemLine As Line In mPortLine
            If count = 0 Then Exit For

            With itemLine.LineControl
                .Dock = DockStyle.Fill
                .Location = New Drawing.Point(275, 23)
                .MinimumSize = New Drawing.Size(10, 20)
                .Name = itemLine.ToString
                .Size = New Drawing.Size(27, 84)
            End With

            TableLayoutPanelPort.Controls.Add(itemLine.LineControl, count, 1)
            TableLayoutPanelPort.SetRowSpan(itemLine.LineControl, 2)
            'AddHandler itemLine.LineControl.ControlStateChanged, AddressOf ControlStateChangedAddressCallback ' Пользовательское управление событием
            AddHandler itemLine.LineControl.PropertyChanged, New PropertyChangedEventHandler(AddressOf Data_PropertyChanged)
            count -= 1
        Next
        Me.ResumeLayout(False)

        LabelCaption.Text = mPortLine.ToString
        ButtonAllHigh.Enabled = True
        ButtonAllLow.Enabled = True
        SetDefaultBackColorTablePanelBytes()
        TableLayoutPanelByte.Controls.Item($"TablePanelByte{mPortLine.Number}").BackColor = Drawing.Color.Wheat
    End Sub

    ''' <summary>
    ''' Отобразить состояния битов в заголовках байтов активного порта
    ''' </summary>
    Private Sub SetNumericTablePanelBytes()
        If ComboBoxPorts.SelectedIndex = -1 Then Exit Sub

        For Each itemPortLine As PortLine In CType(ComboBoxPorts.Items(ComboBoxPorts.SelectedIndex), Port).GetEnumeratorPortLine
            Dim strByte As String = String.Empty

            For Each itemLine As Line In itemPortLine.LinesToArray.Reverse
                If itemLine.IsEnabled AndAlso itemLine.LineControl.Value Then
                    strByte += "1"
                Else
                    strByte += "0"
                End If
            Next

            TableLayoutPanelByte.Controls.Item($"TablePanelByte{itemPortLine.Number}").Controls.Item($"LabelWordH{itemPortLine.Number}").Text = strByte
        Next
    End Sub

    Private Sub ButtonUp_Click(sender As Object, e As EventArgs) Handles ButtonUp.Click
        If ListBoxFrameByte.SelectedIndex <> -1 AndAlso ListBoxFrameByte.SelectedIndex <> 0 Then
            ListBoxFrameByte.SelectedIndex = ListBoxFrameByte.SelectedIndex - 1
        End If
    End Sub

    Private Sub ButtonDown_Click(sender As Object, e As EventArgs) Handles ButtonDown.Click
        If ListBoxFrameByte.SelectedIndex <> -1 AndAlso ListBoxFrameByte.SelectedIndex <> ListBoxFrameByte.Items.Count - 1 Then
            ListBoxFrameByte.SelectedIndex = ListBoxFrameByte.SelectedIndex + 1
        End If
    End Sub

    Private Sub ButtonAllHigh_Click(sender As Object, e As EventArgs) Handles ButtonAllHigh.Click
        SetAllLevel(True)
    End Sub

    Private Sub ButtonAllLow_Click(sender As Object, e As EventArgs) Handles ButtonAllLow.Click
        SetAllLevel(False)
    End Sub

    ''' <summary>
    ''' Установить состояние для всех линий фрейма в порту.
    ''' </summary>
    ''' <param name="inValue"></param>
    Private Sub SetAllLevel(inValue As Boolean)
        If ListBoxFrameByte.SelectedIndex = -1 Then Exit Sub

        For Each itemLine As Line In CType(ListBoxFrameByte.Items(ListBoxFrameByte.SelectedIndex), PortLine)
            If itemLine.IsEnabled AndAlso Not itemLine.IsBusy Then itemLine.LineControl.Value = inValue
        Next
    End Sub

    '' Пользовательское управление событием
    'Private Sub ControlStateChangedAddressCallback(ByVal sender As Object, ByVal e As ActionEventArgs)
    '    'If InvokeRequired Then
    '    '    Invoke(New EventHandler(Of ActionEventArgs)(AddressOf ControlStateChangedAddressCallback), sender, e)
    '    'Else
    '    '    SetNumericTablePanelBytes()
    '    'End If
    '    UpdateNumericTablePanelBytes()
    'End Sub

    Private Sub Data_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ' что-то сделать когда данные изменились
        If e.PropertyName = "Value" Then
            UpdateNumericTablePanelBytes()
        End If
    End Sub

    Private Sub UpdateNumericTablePanelBytes()
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() UpdateNumericTablePanelBytes()))
        Else
            SetNumericTablePanelBytes()
        End If
    End Sub

#Region "Read/Save Devices"
    ' При сериализации Devices записываются поля помеченные атрибутом <DataMember>. Их достаточно, чтобы полностью восстановить класс.
    ' При обратной десериализации создаётся прокси proxyDevices с тем же типом, а затем он разбирается и создаётся рабочий allDevices.
    '{"DeviceDictionary"
    '	[{"Key"1,"Value":{"Number":1,
    '	"PortDictionary":[
    '		{"Key":0,"Value":{"LineDictionary":[
    '			{"Key":0,"Value":{"IsEnabled":true,"Number":0}},
    '			{"Key":1,"Value":{"IsEnabled":true,"Number":1}},
    '			{"Key":2,"Value":{"IsEnabled":true,"Number":2}},
    '			{"Key":3,"Value":{"IsEnabled":true,"Number":3}},
    '			{"Key":4,"Value":{"IsEnabled":true,"Number":4}},
    '			{"Key":5,"Value":{"IsEnabled":true,"Number":5}},
    '			{"Key":6,"Value":{"IsEnabled":true,"Number":6}},
    '			{"Key":7,"Value":{"IsEnabled":true,"Number":7}},
    '			{"Key":8,"Value":{"IsEnabled":true,"Number":8}},
    '		"Number":0}},
    '			{"Key":1,"Value":{"LineDictionary":[
    '			{"Key":0,"Value":{"IsEnabled":true,"Number":0}},
    '			{"Key":1,"Value":{"IsEnabled":true,"Number":1}},
    '			{"Key":2,"Value":{"IsEnabled":true,"Number":2}},
    '			{"Key":3,"Value":{"IsEnabled":true,"Number":3}},
    '			{"Key":4,"Value":{"IsEnabled":true,"Number":4}},
    '			{"Key":5,"Value":{"IsEnabled":true,"Number":5}},
    '			{"Key":6,"Value":{"IsEnabled":true,"Number":6}},
    '			{"Key":7,"Value":{"IsEnabled":true,"Number":7}}]
    '		,"Number":1}}
    '		]}}
    '	]
    '}
    ''' <summary>
    ''' Сериализация массива настроек для выборочного отображения в файл.
    ''' Сохранение произведенных изменений текущего содержимого в прокси для записи.
    ''' </summary>
    Private Sub SerializerDevices(inDevices As Devices)
        Dim jsonFormatter As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(Devices))

        Using fs As FileStream = New FileStream(PathFileConfigurationDevices, FileMode.Create)
            jsonFormatter.WriteObject(fs, inDevices)
        End Using
    End Sub

    ''' <summary>
    ''' Десериализация из файла в прокси, а из него в коллекцию Devices.
    ''' </summary>
    Private Sub ReadDevices()
        Dim jsonFormatter As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(Devices))
        Dim proxyDevices As Devices

        Using fs As FileStream = New FileStream(PathFileConfigurationDevices, FileMode.Open)
            proxyDevices = CType(jsonFormatter.ReadObject(fs), Devices)
        End Using

        allDevices = New Devices()
        ' произвести полное отображение (копирование) с прокси на реальную коллекцию
        For Each itemDevice As Device In proxyDevices
            Dim newDevice As New Device(itemDevice.Number)

            For Each itemPort As Port In itemDevice
                Dim newPort As New Port(itemPort.Number)

                For Each itemLine As Line In itemPort
                    newPort.Add(itemLine.Number, itemLine.IsEnabled)
                    'newPort(itemLine.Number).IsBusy = itemLine.IsBusy
                Next

                newDevice.Add(newPort)
            Next

            allDevices.Add(newDevice)
        Next
    End Sub

    ''' <summary>
    ''' Создание и запись в файл конфигурации  класса устройств по умолчанию.
    ''' </summary>
    Private Sub GreateCongigByDefault()
        Dim newDevice As New Device(1)

        For portNumber As Integer = 0 To 2
            Dim newPort As New Port(portNumber)

            If portNumber = 0 Then
                For lineNumber As Integer = 0 To 31
                    newPort.Add(lineNumber, True)
                Next
            Else
                For lineNumber As Integer = 0 To 7
                    newPort.Add(lineNumber, True)
                Next
            End If

            newDevice.Add(newPort)
        Next

        ' Запись
        SerializerDevices(New Devices From {newDevice})
    End Sub

    ''' <summary>
    ''' Создать кофигурацию оборудования для передачи компоненту TableLineControl.
    ''' </summary>
    Public Function CreateTestConfigurationDevices() As Devices
        Dim mDevices As New Devices()

        For devNumber As Integer = 1 To 3
            Dim newDevice As New Device(devNumber)

            If devNumber = 1 Then
                For portNumber As Integer = 0 To 2
                    Dim newPort As New Port(portNumber)

                    If portNumber = 0 Then
                        For lineNumber As Integer = 0 To 31
                            newPort.Add(lineNumber, If(lineNumber Mod 2 = 0, True, False))
                        Next
                    ElseIf portNumber = 1 Then
                        For lineNumber As Integer = 0 To 15
                            newPort.Add(lineNumber, If(lineNumber Mod 2 = 0, True, False))
                        Next
                    Else
                        For lineNumber As Integer = 0 To 7
                            newPort.Add(lineNumber, If(lineNumber Mod 2 = 0, True, False))
                        Next
                    End If

                    newDevice.Add(newPort)
                Next
            ElseIf devNumber = 2 Then
                For portNumber As Integer = 0 To 2
                    Dim newPort As New Port(portNumber)

                    If portNumber = 0 Then
                        For lineNumber As Integer = 0 To 7
                            newPort.Add(lineNumber, If(lineNumber Mod 2 = 0, True, False))
                        Next
                    ElseIf portNumber = 1 Then
                        For lineNumber As Integer = 0 To 15
                            newPort.Add(lineNumber, If(lineNumber Mod 2 = 0, True, False))
                        Next
                    Else
                        For lineNumber As Integer = 0 To 31
                            newPort.Add(lineNumber, If(lineNumber Mod 2 = 0, True, False))
                        Next
                    End If

                    newDevice.Add(newPort)
                Next
            Else
                For portNumber As Integer = 0 To 2
                    Dim newPort As New Port(portNumber)

                    For lineNumber As Integer = 0 To 7
                        newPort.Add(lineNumber, If(lineNumber Mod 2 = 0, True, False))
                    Next

                    newDevice.Add(newPort)
                Next
            End If

            mDevices.Add(newDevice)
        Next

        Return mDevices
    End Function

    ''' <summary>
    ''' Создать тестовый набор занятых линий портов.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTestBusyDevices() As Devices
        Dim mDevices As Devices = CreateTestConfigurationDevices()
        mDevices(1)(0)(7).IsBusy = True
        mDevices(2)(0)(2).IsBusy = True
        mDevices(3)(0)(4).IsBusy = True
        Return mDevices
    End Function

    ''' <summary>
    ''' Создать тестовый набор включённых линий портов.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTestLineValueOnDevices(numberCharge As Integer) As Devices
        Dim mDevices As Devices = CreateTestConfigurationDevices()

        If numberCharge = 1 Then
            mDevices(1)(0)(2).LineControl.Value = True
            mDevices(1)(0)(4).LineControl.Value = True
            mDevices(1)(0)(6).LineControl.Value = True
            mDevices(1)(0)(10).LineControl.Value = True
            mDevices(1)(0)(12).LineControl.Value = True
            mDevices(1)(0)(14).LineControl.Value = True
        Else
            mDevices(1)(0)(16).LineControl.Value = True
            mDevices(1)(0)(18).LineControl.Value = True
            mDevices(1)(0)(20).LineControl.Value = True
        End If

        Return mDevices
    End Function
#End Region
End Class
