''' <summary>
''' Настроечные параметры
''' </summary>
''' <remarks></remarks>
Public Class TuningParameters
    Public Const conFrequency As String = "Frequency"
    Public Const conStandNumber As String = "StandNumber"

    Private mFrequency As Dual = New Dual
    ''' <summary>
    ''' частота сбора регистратора
    ''' </summary>
    ''' <returns></returns>
    Public Property Frequency() As Dual
        Get
            Return mFrequency
        End Get
        Set(ByVal value As Dual)
            If value.DigitalValue < 1.0 OrElse value.DigitalValue > 20.0 Then
                value.DigitalValue = 10.0
            End If
            mFrequency = value
            TuningDictionary(conFrequency) = value
        End Set
    End Property

    Private mStandNumber As Dual = New Dual
    ''' <summary>
    ''' номер стенда
    ''' </summary>
    ''' <returns></returns>
    Public Property StandNumber() As Dual
        Get
            Return mStandNumber
        End Get
        Set(ByVal value As Dual)
            mStandNumber = value
            TuningDictionary(conStandNumber) = value
        End Set
    End Property

    Public Property TuningDictionary As Dictionary(Of String, Dual)

    Public Sub New()
        TuningDictionary = New Dictionary(Of String, Dual) From {
        {conFrequency, Frequency},
        {conStandNumber, StandNumber}}
    End Sub
End Class

Public Class Dual
    ''' <summary>
    ''' Логика Или Число
    ''' </summary>
    ''' <returns></returns>
    Public Property IsLogicalOrDigital As Boolean
    ''' <summary>
    ''' Цифровое Значение
    ''' </summary>
    ''' <returns></returns>
    Public Property DigitalValue As Double
    ''' <summary>
    ''' Логическое Значение
    ''' </summary>
    ''' <returns></returns>
    Public Property LogicalValue As Boolean
End Class

