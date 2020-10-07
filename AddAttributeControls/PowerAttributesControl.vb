Public Class PowerAttributesControl
    'Inherits System.Windows.Forms.UserControl
    ' закоментировать для работы в окне дизайнера
    Inherits BaseAttributesControl

#Region "Properties"
    ''' <summary>
    ''' ВеличинаЗагрузки
    ''' </summary>
    ''' <returns></returns>
    Public Property ChargeValueToText As String
        Get
            Return Me(cChargeValueToText)
        End Get
        Set
            Me(cChargeValueToText) = Value
            'OnPropertyChanged(NameOf(Me.ChargeValueToText))
        End Set
    End Property

    ''' <summary>
    ''' ЧисловоеЗначение
    ''' </summary>
    ''' <returns></returns>
    Public Property StageToNumeric As Double
        Get
            Return Me(cStageToNumeric)
        End Get
        Set
            Me(cStageToNumeric) = Value
            'OnPropertyChanged(NameOf(Me.StageToNumeric))
        End Set
    End Property

    ''' <summary>
    ''' Примечание
    ''' </summary>
    ''' <returns></returns>
    Public Property Description As String
        Get
            Return Me(cDescriptionCharge)
        End Get
        Set
            Me(cDescriptionCharge) = Value
            'OnPropertyChanged(NameOf(Me.Description))
        End Set
    End Property
#End Region

    Public Sub New(inCaption As String)
        MyBase.New(inCaption)

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        ' Добавить код инициализации после вызова InitializeComponent().
    End Sub

    Friend Overrides Sub InitializeAttributeControl()
        SetAttributeValue(cChargeValueToText, "Величина загрузки", AttributeType.IsString, True, "")
        SetAttributeValue(cStageToNumeric, "Числовое значение", AttributeType.IsNumeric, True, 0)
        SetAttributeValue(cDescriptionCharge, "Примечание", AttributeType.IsString, True, "")

        PopulateTLPanelAttributes()
    End Sub

    Friend Overrides Sub PropertyComboBoxChanged()
        ' нет реализации
    End Sub

    Friend Overrides Sub AdjustProperties()
        ' нет реализации
    End Sub
End Class
