Public Class ChargeValueToText
    ''' <summary>
    ''' ЧисловоеЗначение
    ''' </summary>
    ''' <returns></returns>
    Public Property Value As Double

    ''' <summary>
    ''' ВеличинаЗагрузки
    ''' </summary>
    Public Property TextCharge As String
    Public Sub New(inValue As Double, inTextCharge As String)
        Value = inValue
        TextCharge = inTextCharge
    End Sub
End Class
