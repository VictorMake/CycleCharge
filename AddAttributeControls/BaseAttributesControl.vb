Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

''' <summary>
''' По набору Attributes содержащего в себе всю необходимую информацию и контролы
''' генерируется панель для отображения и ввода этих свойств.
''' Это базовый класс.
''' </summary>
Public MustInherit Class BaseAttributesControl
    Implements INotifyPropertyChanged
    Implements IEnumerable

    Private mControlCaption As String

    Public Property ControlCaption As String
        Get
            Return mControlCaption
        End Get
        Set
            mControlCaption = Value
            'OnPropertyChanged(NameOf(Me.ControlCaption))
            OnPropertyChanged()
        End Set
    End Property

    Friend MustOverride Sub InitializeAttributeControl()
    Friend MustOverride Sub PropertyComboBoxChanged()
    ''' <summary>
    ''' Согласовать связанные свойства
    ''' </summary>
    Friend MustOverride Sub AdjustProperties()

#Region "Пользовательское управление событием"
    'Public Event ControlStateChanged(sender As Object, e As ActionEventArgs)

    'Friend Sub OnControlStateChanged(sender As Object, e As ActionEventArgs)
    '    RaiseEvent ControlStateChanged(sender, e)
    'End Sub
#End Region

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    ''' <summary>
    ''' Реализация интерфейса INotifyPropertyChanged.
    ''' </summary>
    ''' <param name="propertyName"></param>
    Public Sub OnPropertyChanged(<CallerMemberName> ByVal Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
    'Protected Sub OnPropertyChanged(Optional ByVal propertyName As String = Nothing)
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    'End Sub

    ''' <summary>
    ''' Корневой контейнер для представления записей из таблицы.
    ''' </summary>
    Friend WithEvents TLPanelAttributes As TableLayoutPanel
    Private counterControls, counterRows As Integer

    Protected Sub New()
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        ' Добавить код инициализации после вызова InitializeComponent().
    End Sub

    Public Sub New(inCaption As String)
        Me.New

        ControlCaption = inCaption
        Attributes = New Dictionary(Of String, Attribute)
    End Sub

    Public Property Attributes As Dictionary(Of String, Attribute)

    Default Public Property Item(key As String) As Object
        Get
            Return Attributes(key).AttributeValue
        End Get
        Set(value As Object)
            Attributes(key).AttributeValue = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return CalcDictionary.GetEnumerator()
    'End Function

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each keysCalc As String In Attributes.Keys.ToArray
            Yield Attributes(keysCalc)
        Next
    End Function

    ''' <summary>
    ''' Добавить аттрибут в коллекцию
    ''' </summary>
    Public Sub SetAttributeValue(inAttributeName As String, inDescription As String, inAttributeType As AttributeType, inEnabled As Boolean, inAttributeValue As Object, ByVal ParamArray args() As String)
        Attributes.Add(inAttributeName, New Attribute(inAttributeName, inDescription, inAttributeType, inEnabled, inAttributeValue, args))
        AddHandler Attributes(inAttributeName).PropertyChanged, New PropertyChangedEventHandler(AddressOf Data_PropertyChanged)
    End Sub

    ''' <summary>
    ''' Перехват и повторная генерация событи изменения свойства.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Data_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ' что-то сделать когда данные изменились
        If e.PropertyName = cTarget Then
            OnPropertyChanged(e.PropertyName)
            PropertyComboBoxChanged()
        End If
    End Sub

    ''' <summary>
    ''' Вызов из унаследованных контролов для соблюдения полиморфизма.
    ''' </summary>
    Friend Sub PopulateTLPanelAttributes()
        Me.SuspendLayout()
        TLPanelAttributes = New TableLayoutPanel With {
            .CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset,
            .ColumnCount = 2,
            .Dock = DockStyle.Fill,
            .Location = New Drawing.Point(3, 3),
            .Name = "TLPanelAttributes",
            .RowCount = Attributes.Where(Function(itemAttribute) itemAttribute.Value.IsEnabled).ToArray.Count,
            .Size = New Drawing.Size(516, 264)
        }

        With TLPanelAttributes
            .SuspendLayout()
            ' добавить 2 столбца
            .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 60.0!))
            .ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 40.0!))

            ' строки по аттрибутам в соответствии с типом
            For Each itemAttribute As Attribute In Attributes.Values
                If itemAttribute.IsEnabled Then
                    'добавить новую строку
                    .RowStyles.Add(New RowStyle(SizeType.Percent, (1 / Attributes.Values.Count) * 100.0))
                    ' встроку добавить контролы
                    .Controls.Add(NewLabelDescription(itemAttribute.Description), 0, counterRows)

                    Select Case itemAttribute.Type
                        Case AttributeType.IsString
                            .Controls.Add(itemAttribute.AttributeStringEdit, 1, counterRows)
                        Case AttributeType.IsNumeric
                            .Controls.Add(itemAttribute.AttributeNumericEdit, 1, counterRows)
                        Case AttributeType.IsBoolean
                            .Controls.Add(itemAttribute.AttributeCheckBoxEdit, 1, counterRows)
                        Case AttributeType.IsComboBox
                            .Controls.Add(itemAttribute.AttributeComboBoxEdit, 1, counterRows)
                    End Select

                    counterRows += 1
                End If
            Next

            .ResumeLayout(False)
            .PerformLayout()
        End With

        Controls.Add(TLPanelAttributes)
        Me.ResumeLayout(False)
    End Sub

    Private Function NewLabelDescription(inDescription As String) As Label
        counterControls += 1

        Dim newLabel As New Label With {
            .AutoSize = True,
            .Dock = DockStyle.Fill,
            .Location = New Drawing.Point(5, 2),
            .Name = "LabelDescription" & counterControls,
            .Size = New Drawing.Size(163, 56),
            .Text = inDescription,
            .TextAlign = Drawing.ContentAlignment.MiddleLeft
        }
        Return newLabel
    End Function
End Class
