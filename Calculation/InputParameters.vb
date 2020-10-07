Imports System.Windows.Forms
''' <summary>
''' Входные аргументы
''' </summary>
''' <remarks></remarks>
Public Class InputParameters
    Implements IEnumerable

    Public Const conN1 As String = "N1"
    Public Const conN2 As String = "N2"
    Public Const conRUD As String = "aRUD"

    Private mN1 As Double
    Public Property N1() As Double
        Get
            Return mN1
        End Get
        Set(ByVal value As Double)
            mN1 = value
            InputParameterDictionary(conN1).InputValue = value
        End Set
    End Property


    Private mN2 As Double
    Public Property N2() As Double
        Get
            Return mN2
        End Get
        Set(ByVal value As Double)
            mN2 = value
            InputParameterDictionary(conN2).InputValue = value
        End Set
    End Property

    Private mRUD As Double
    Public Property RUD() As Double
        Get
            Return mRUD
        End Get
        Set(ByVal value As Double)
            mRUD = value
            InputParameterDictionary(conRUD).InputValue = value
        End Set
    End Property

    Private Property InputParameterDictionary As Dictionary(Of String, InputParameter)

    Public Sub New()
        InputParameterDictionary = New Dictionary(Of String, InputParameter) From {
            {conN1, New InputParameter With {.Name = conN1}},
            {conN2, New InputParameter With {.Name = conN2}},
            {conRUD, New InputParameter With {.Name = conRUD}}}
    End Sub

    Default Public Property Item(key As String) As Double
        Get
            Return InputParameterDictionary(key).InputValue
        End Get
        Set(value As Double)
            InputParameterDictionary(key).InputValue = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return CalcDictionary.GetEnumerator()
    'End Function

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each keysCalc As String In InputParameterDictionary.Keys.ToArray
            Yield InputParameterDictionary(keysCalc)
        Next
    End Function

    Public Function Count() As Integer
        Return InputParameterDictionary.Count
    End Function

    ''' <summary>
    ''' Связать индикатор и цифровое поле в словарях с расчётным параметром по ключу
    ''' для отображения при обновлении данных от регистратора
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="inINumericPointer"></param>
    Public Sub BindingWithControls(key As String, inINumericPointer As ToolStripTextBox)
        InputParameterDictionary(key).ControlNumericPointer = inINumericPointer
    End Sub

    ''' <summary>
    ''' Отменить связи с индикаторами и цифровыми полями все элементы коллекции.
    ''' </summary>
    Public Sub UnBindingAllControls()
        For Each key As String In InputParameterDictionary.Keys.ToArray
            InputParameterDictionary(key).ControlNumericPointer = Nothing
        Next
    End Sub
End Class

Public Class InputParameter
    Private mInputValue As Double
    Public Property Name As String
    Public Property ControlNumericPointer As ToolStripTextBox
    Public Property InputValue As Double
        Get
            Return mInputValue
        End Get
        Set
            mInputValue = Value
            If ControlNumericPointer IsNot Nothing Then ControlNumericPointer.Text = Convert.ToString(Math.Round(Value, 2))
        End Set
    End Property
End Class