Public Class AggregateAttributesControl
    'Inherits System.Windows.Forms.UserControl
    ' закоментировать для работы в окне дизайнера
    Inherits BaseAttributesControl

#Region "Properties"
    ''' <summary>
    ''' ИмяУстройства
    ''' </summary>
    ''' <returns></returns>
    Public Property DeviceName As String
        Get
            Return CStr(Me(cDeviceName))
        End Get
        Set
            Me(cDeviceName) = Value
            'OnPropertyChanged(NameOf(Me.DeviceName))
        End Set
    End Property

    ''' <summary>
    ''' Описание
    ''' </summary>
    ''' <returns></returns>
    Public Property Description As String
        Get
            Return CStr(Me(cDescription))
        End Get
        Set
            Me(cDescription) = Value
            'OnPropertyChanged(NameOf(Me.Description))
        End Set
    End Property

    ''' <summary>
    ''' Имя оборудования (Dev,SCXI) 
    ''' </summary>
    ''' <returns></returns>
    Public Property Target As String
        Get
            Return CStr(Me(cTarget))
        End Get
        Set
            If CStr(Me(cTarget)) <> Value Then
                Me(cTarget) = Value
                'OnPropertyChanged(NameOf(Me.Target))
                OnPropertyChanged()
            End If
        End Set
    End Property

    ''' <summary>
    ''' НомерПорта
    ''' </summary>
    ''' <returns></returns>
    Public Property PortNumber As Integer
        Get
            Return CInt(Me(cPortNumber))
        End Get
        Set
            Me(cPortNumber) = Value
            'OnPropertyChanged(NameOf(Me.PortNumber))
        End Set
    End Property

    ''' <summary>
    ''' ВысокийУровеньПорта
    ''' </summary>
    ''' <returns></returns>
    Public Property PortHighLevel As Integer
        Get
            Return CInt(Me(cPortHighLevel))
        End Get
        Set
            Me(cPortHighLevel) = Value
            'OnPropertyChanged(NameOf(Me.PortHighLevel))
        End Set
    End Property

    ''' <summary>
    ''' ЕдиницаИзмерения
    ''' </summary>
    ''' <returns></returns>
    Public Property UnitOfMeasure As String
        Get
            Return CStr(Me(cUnitOfMeasure))
        End Get
        Set
            Me(cUnitOfMeasure) = Value
            'OnPropertyChanged(NameOf(Me.UnitOfMeasure))
        End Set
    End Property

    ''' <summary>
    ''' ДиапазонИзмененияMin
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeOfChangingValueMin As Double
        Get
            Return CDbl(Me(cRangeOfChangingValueMin))
        End Get
        Set
            Me(cRangeOfChangingValueMin) = Value
            AlarmValueMin = Value - 1
            'OnPropertyChanged(NameOf(Me.RangeOfChangingValueMin))
        End Set
    End Property

    ''' <summary>
    ''' ДиапазонИзмененияMax
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeOfChangingValueMax As Double
        Get
            Return CDbl(Me(cRangeOfChangingValueMax))
        End Get
        Set
            Me(cRangeOfChangingValueMax) = Value
            AlarmValueMax = Value - 1
            'OnPropertyChanged(NameOf(Me.RangeOfChangingValueMax))
        End Set
    End Property

    ''' <summary>
    ''' РазносУмин
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeYmin As Double
        Get
            Return CDbl(Me(cRangeYmin))
        End Get
        Set
            Me(cRangeYmin) = Value
            'OnPropertyChanged(NameOf(Me.RangeYmin))
        End Set
    End Property

    ''' <summary>
    ''' РазносУмакс
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeYmax As Double
        Get
            Return CDbl(Me(cRangeYmax))
        End Get
        Set
            Me(cRangeYmax) = Value
            'OnPropertyChanged(NameOf(Me.RangeYmax))
        End Set
    End Property

    ''' <summary>
    ''' АварийноеЗначениеМин
    ''' </summary>
    ''' <returns></returns>
    Public Property AlarmValueMin As Double
        Get
            Return CDbl(Me(cAlarmValueMin))
        End Get
        Set
            Me(cAlarmValueMin) = Value
            'OnPropertyChanged(NameOf(Me.AlarmValueMin))
        End Set
    End Property

    ''' <summary>
    ''' АварийноеЗначениеМакс
    ''' </summary>
    ''' <returns></returns>
    Public Property AlarmValueMax As Double
        Get
            Return CDbl(Me(cAlarmValueMax))
        End Get
        Set
            Me(cAlarmValueMax) = Value
            'OnPropertyChanged(NameOf(Me.AlarmValueMax))
        End Set
    End Property

    ''' <summary>
    ''' Видимость
    ''' </summary>
    ''' <returns></returns>
    Public Property IsVisible As Boolean
        Get
            Return CBool(Me(cIsVisible))
        End Get
        Set
            Me(cIsVisible) = Value
            'OnPropertyChanged(NameOf(Me.IsVisible))
        End Set
    End Property

    ''' <summary>
    ''' ВидимостьРегистратор
    ''' </summary>
    ''' <returns></returns>
    Public Property IsVisibleRegistration As Boolean
        Get
            Return CBool(Me(cIsVisibleRegistration))
        End Get
        Set
            Me(cIsVisibleRegistration) = Value
            'OnPropertyChanged(NameOf(Me.IsVisibleRegistration))
        End Set
    End Property
#End Region

    Private Targets As String()
    Private PortNumbers As String()

    Private UnitOfMeasureArgs As String() = {
        "Давление",
        "Обороты",
        "Ток",
        "Температура",
        "Вибрация",
        "Расход"
    }

    Public Sub New(inCaption As String, inTargets As String(), inPortNumbers As String())
        MyBase.New(inCaption)

        Targets = inTargets
        PortNumbers = inPortNumbers

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        ' Добавить код инициализации после вызова InitializeComponent().
    End Sub

    Friend Overrides Sub InitializeAttributeControl()
        Dim rand As New Random(CInt(Date.Now.Ticks And &HFFFF))

        SetAttributeValue(cDeviceName, "Имя устройства", AttributeType.IsString, True, "")
        SetAttributeValue(cDescription, "Описание", AttributeType.IsString, True, "")
        SetAttributeValue(cTarget, "Имя оборудования", AttributeType.IsComboBox, True, "", Targets)
        SetAttributeValue(cPortNumber, "Номер порта", AttributeType.IsComboBox, True, "", PortNumbers)
        SetAttributeValue(cPortHighLevel, "Высокий уровень порта", AttributeType.IsNumeric, False, 1)
        SetAttributeValue(cUnitOfMeasure, "Индикатор", AttributeType.IsComboBox, True, "Давление", UnitOfMeasureArgs)
        SetAttributeValue(cRangeOfChangingValueMin, "Минимальная величина", AttributeType.IsNumeric, True, 0)
        SetAttributeValue(cRangeOfChangingValueMax, "Максимальная величина", AttributeType.IsNumeric, True, 100)
        SetAttributeValue(cRangeYmin, "Разнос по оси Y мин.", AttributeType.IsNumeric, False, rand.Next(5, 15))
        SetAttributeValue(cRangeYmax, "Разнос по оси Y макс.", AttributeType.IsNumeric, False, rand.Next(85, 95))
        SetAttributeValue(cAlarmValueMin, "Минимальная аварийная величина", AttributeType.IsNumeric, False, RangeOfChangingValueMin - 1)
        SetAttributeValue(cAlarmValueMax, "Максимальная аварийная величина", AttributeType.IsNumeric, False, RangeOfChangingValueMax - 1)
        SetAttributeValue(cIsVisible, "Видимость на снимках", AttributeType.IsBoolean, False, True)
        SetAttributeValue(cIsVisibleRegistration, "Видимость при регистрации", AttributeType.IsBoolean, False, True)

        PopulateTLPanelAttributes()
    End Sub

    Friend Overrides Sub PropertyComboBoxChanged()
        Dim queryPorts As String() = (From port In PortNumbers
                                      Where port.StartsWith(Target)
                                      Select port).ToArray

        If queryPorts.Count <> 0 Then
            Dim queryPortNumbers As String() = (From number In queryPorts
                                                Select number.Replace($"{Target}/port", "")).ToArray

            Attributes(cPortNumber).AttributeComboBoxEdit.Items.Clear()
            Attributes(cPortNumber).AttributeComboBoxEdit.Text = "0"
            Attributes(cPortNumber).AttributeComboBoxEdit.Items.AddRange(queryPortNumbers)
        End If
    End Sub

    Friend Overrides Sub AdjustProperties()
        If RangeOfChangingValueMin = RangeOfChangingValueMax Then
            RangeOfChangingValueMax *= 1.1
        End If

        If RangeOfChangingValueMin > RangeOfChangingValueMax Then
            Swap(Of Double)(RangeOfChangingValueMin, RangeOfChangingValueMax)
        End If

        AlarmValueMin = RangeOfChangingValueMin - 1
        AlarmValueMax = RangeOfChangingValueMax - 1
    End Sub

    Private Shared Sub Swap(Of T)(ByRef lhs As T, ByRef rhs As T)
        Dim temp As T
        temp = lhs
        lhs = rhs
        rhs = temp
    End Sub
End Class
