Imports System.ComponentModel
Imports System.Windows.Forms
Imports MathematicalLibrary

''' <summary>
''' Класс агрегата (элемента коллекции) участвующего в циклограмме.
''' </summary>
''' <remarks></remarks>
Friend Class AggregateCycle
    Implements INotifyPropertyChanged
    Implements IEnumerable

    ' реализация генерации события изменения чего-либо для отслеживания этого события в родительском классе
    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

#Region "Propertys"
    ''' <summary>
    ''' Необходимо сообщение для логирования.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsNeedUpdate As Boolean
    ''' <summary>
    ''' Текущая мощность возрасла по сравнению с предыдущей.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsPowerUp As Boolean
    Public Property DefaultValue As Single
    Private mCurrentPower As Single
    ''' <summary>
    ''' Текущая вычисленная мощность.
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrentPower As Single
        Get
            Return mCurrentPower
        End Get
        Set
            IsNeedUpdate = Value <> mCurrentPower
            If IsNeedUpdate Then IsPowerUp = Value > mCurrentPower
            mCurrentPower = Value
        End Set
    End Property

    ''' <summary>
    ''' Key Устройства
    ''' </summary>
    ''' <returns></returns>
    Public Property KeyAggregate() As Integer
    ''' <summary>
    ''' Имя Устройства
    ''' </summary>
    ''' <returns></returns>
    Public Property NameAggregate() As String

    Private mPointsTimeDuration As Double()
    ''' <summary>
    ''' Точки времени перекладок
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PointsTimeDuration() As Double()
        Get
            Return mPointsTimeDuration
        End Get
    End Property

    Private mPointsStageToNumeric As Single()
    ''' <summary>
    ''' Точки значений перекладок
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PointsStageToNumeric() As Single()
        Get
            Return mPointsStageToNumeric
        End Get
    End Property

    Private mSumTimeCycle As Double
    Public ReadOnly Property SumTimeCycle As Double
        Get
            Return mSumTimeCycle
        End Get
    End Property

    Private mTarget As String
    Public ReadOnly Property Target As String
        Get
            Return mTarget
        End Get
    End Property

    Private mPort As Integer
    Public ReadOnly Property Port As Integer
        Get
            Return mPort
        End Get
    End Property

    'Private mTag As Integer = 0

    'Public Property Tag() As Integer
    '    Get
    '        Return mTag
    '    End Get
    '    Set(value As Integer)
    '        mTag = value
    '        OnPropertyChanged("Tag")
    '    End Set
    'End Property

    Private mIsPowerEmpty As Boolean

    ''' <summary>
    ''' Величина Загрузки Не Существует
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsPowerEmpty() As Boolean
        Get
            Return mIsPowerEmpty
        End Get
    End Property
#End Region

#Region "Implements IEnumerable"
    Private Property Powers As Dictionary(Of Single, StagePower)
    Default Public Property Item(key As Single) As StagePower
        Get
            Return Powers(key)
        End Get
        Set(value As StagePower)
            Powers(key) = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return PortDictionary.GetEnumerator()
    'End Function

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each key As Single In Powers.Keys.ToArray
            Yield Powers(key)
        Next
    End Function

    Public Sub Add(ByVal newStageToNumeric As Single)
        If Not Exist(newStageToNumeric) Then
            Exit Sub
        End If
        Powers.Add(newStageToNumeric, New StagePower(newStageToNumeric))
    End Sub

    Private Function Exist(ByVal newStageToNumeric As Single) As Boolean
        If Powers.ContainsKey(newStageToNumeric) Then
            MessageBox.Show($"Величина загрузки равная <{newStageToNumeric}> уже существует!", "Ошибка добавления загрузки", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function

    Public ReadOnly Property PowersToArray() As StagePower()
        Get
            Return Powers.Values.ToArray()
        End Get
    End Property
#End Region

    Public Sub New(inKeyAggregate As Integer,
                   inNameAggregate As String,
                   inDefaultValue As Single,
                   inTarget As String,
                   inPort As Integer)

        KeyAggregate = inKeyAggregate
        NameAggregate = inNameAggregate
        DefaultValue = inDefaultValue
        mTarget = inTarget
        mPort = inPort
        Powers = New Dictionary(Of Single, StagePower)
    End Sub

    'Protected Sub OnPropertyChanged(propertyName As String)
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    'End Sub

    ''' <summary>
    ''' Сформировать точки уровней мощностей загрузок.
    ''' </summary>
    ''' <param name="inStages"></param>
    ''' <param name="inTimeDurations"></param>
    ''' <param name="inTimeUnits"></param>
    Public Sub PopulatePointsStageToNumeric(ByVal inStages As List(Of Single),
                                           ByVal inTimeDurations As List(Of Double),
                                           ByVal inTimeUnits As List(Of String))
        ' точек для вычисления промежуточных значений в 2 раза больше числа перекладок
        Dim pointsCount As Integer = inTimeDurations.Count * 2 - 1
        'ReDim_mPointsStageToNumeric(pointsCount) ' Величина
        'ReDim_mPointsTimeDuration(pointsCount)  ' Длительность
        Re.Dim(mPointsStageToNumeric, pointsCount) ' Величина
        Re.Dim(mPointsTimeDuration, pointsCount)  ' Длительность

        Dim I As Integer
        mIsPowerEmpty = False

        For indexPower As Integer = 0 To inTimeDurations.Count - 1
            ' определяем массив координат для точек данной загрузки
            Dim charge As Single = inStages(indexPower)
            Dim timeDuration As Double = inTimeDurations(indexPower) ' Длительность
            Dim timeUnit As String = CStr(inTimeUnits(indexPower)) ' Ед.изм.

            'проверить наличие данной величины
            If Not Powers.ContainsKey(charge) Then
                MessageBox.Show($"Величина загрузки цикла равная <{charge}> в устройстве <{NameAggregate}> не существует!",
                                "Проверка уровней загрузки", MessageBoxButtons.OK, MessageBoxIcon.Information)
                mIsPowerEmpty = True
            End If

            If timeUnit = "Мин" Then
                timeDuration *= 60
            ElseIf timeUnit = "Час" Then
                timeDuration *= 3600
            End If

            If I = 0 Then ' это 1 точка
                mPointsTimeDuration(I) = 0
                mPointsStageToNumeric(I) = charge
                mPointsTimeDuration(I + 1) = Math.Round(timeDuration, 3)
                mPointsStageToNumeric(I + 1) = charge
                mSumTimeCycle += timeDuration
            Else
                mPointsTimeDuration(I) = Math.Round(SumTimeCycle, 3)
                mPointsStageToNumeric(I) = charge
                mSumTimeCycle += timeDuration
                mPointsTimeDuration(I + 1) = Math.Round(SumTimeCycle, 3)
                mPointsStageToNumeric(I + 1) = charge
            End If

            I += 2
        Next
    End Sub

    ''' <summary>
    ''' Установить мощность в текущем срезе времени или по умолчанию.
    ''' </summary>
    ''' <param name="isDefaul"></param>
    ''' <param name="currentTime"></param>
    Public Sub SetPower(isDefaul As Boolean, ByVal currentTime As Double)
        If isDefaul Then
            CurrentPower = DefaultValue
        Else
            CurrentPower = GetPowerAtCurrentTime(currentTime)
        End If
    End Sub

    ''' <summary>
    ''' Выдать значение мощности в текущем значении времени.
    ''' </summary>
    ''' <param name="currentTime"></param>
    ''' <returns></returns>
    Private Function GetPowerAtCurrentTime(ByVal currentTime As Double) As Single
        If currentTime <= 0 Then
            Return PointsStageToNumeric(0)
        ElseIf currentTime > PointsTimeDuration(PointsTimeDuration.GetUpperBound(0)) Then
            Return PointsStageToNumeric(PointsStageToNumeric.GetUpperBound(0))
        End If

        ' Если Array не содержит заданного значения, метод возвращает отрицательное целое число. 
        ' Этот оператор битового дополнения (~) можно применить для создания индекса (в Visual Basic к отрицательному результату применяется оператор Xor -1). 
        ' Если индекс больше или равен размеру массива, в этом массиве нет элементов, превышающих значение value. 

        Dim searchTime As Integer = Array.BinarySearch(PointsTimeDuration, currentTime)
        If searchTime < 0 Then  ' индекс первого элемента больше искомого
            'ВремяIndex = ВремяIndex Xor -1
            searchTime = -searchTime - 1
        End If

        Return PointsStageToNumeric(searchTime)
    End Function

    Public Function PointsStageToNumericConvertToDouble() As Double()
        Return ConvertToDouble(PointsStageToNumeric)
    End Function

    Public Shared Function ConvertToSingle(ByVal data As Double()) As Single()
        Dim arrSingle As Single() = New Single(data.Length - 1) {}

        For i = 0 To data.Length - 1
            arrSingle(i) = CSng(data(i))
        Next

        Return arrSingle
    End Function

    Public Shared Function ConvertToDouble(ByVal data As Single()) As Double()
        Dim arrDouble As Double() = New Double(data.Length - 1) {}

        For i = 0 To data.Length - 1
            arrDouble(i) = data(i)
        Next

        Return arrDouble
    End Function
End Class
