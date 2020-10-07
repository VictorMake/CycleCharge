Imports System.ComponentModel
Imports System.Reflection
Imports CycleCharge.FormWindowsManager

''' <summary>
''' Класс работы с перечислителем EnumWindowsForms
''' для удобных логических операций
''' </summary>
''' <remarks></remarks>
Friend Class WorkWithEnum
    Private mListEnumDescriptions As List(Of String)
    Private mListEnumNames As List(Of String)

    Public ReadOnly Property ListEnumDescriptions As List(Of String)
        Get
            Return mListEnumDescriptions
        End Get
    End Property

    Public ReadOnly Property ListEnumNames As List(Of String)
        Get
            Return mListEnumNames
        End Get
    End Property

    Public Sub New()
        PopulateListEnumNamesAndDescriptions()
    End Sub

    Private Sub PopulateListEnumNamesAndDescriptions()
        mListEnumDescriptions = New List(Of String)
        mListEnumNames = New List(Of String)

        ' получить все аттрибуты перечислителя для создания списка возможных окон в системе
        For Each value In [Enum].GetValues(GetType(EnumWindowsForms))
            Dim fi As FieldInfo = GetType(EnumWindowsForms).GetField([Enum].GetName(GetType(EnumWindowsForms), value))
            Dim dna As DescriptionAttribute = DirectCast(System.Attribute.GetCustomAttribute(fi, GetType(DescriptionAttribute)), DescriptionAttribute)

            If dna IsNot Nothing Then
                mListEnumDescriptions.Add(dna.Description)
            Else
                'mСписокОкон.Add(EnumWindowsForms.РедакторЦиклограмм.ToString())
                mListEnumDescriptions.Add("Нет описания")
            End If
        Next

        ' то же самое по другому
        'For Each c In TypeDescriptor.GetConverter(GetType(WindowsForms)).GetStandardValues
        '    Dim dna As DescriptionAttribute = GetType(WindowsForms).GetField([Enum].GetName(GetType(WindowsForms), c)).GetCustomAttributes(GetType(DescriptionAttribute), True)(0)
        '    If dna IsNot Nothing Then
        '        mListEnumDescriptions.Add(dna.Description)
        '    Else
        '        mListEnumDescriptions.Add(WindowsForms.РедакторПерекладок.ToString())
        '    End If
        'Next     

        mListEnumNames.AddRange([Enum].GetNames(GetType(EnumWindowsForms)).ToArray)
    End Sub

    ''' <summary>
    ''' Перечисление имен и значений
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNameAndValueIterateBaseEnum() As String
        Dim sOut As String = "Перечисление имен: "
        Dim names = [Enum].GetNames(GetType(EnumWindowsForms))

        ' Output: все имена перечисления
        For Each name In names
            sOut &= name & ", "
        Next

        sOut = sOut.Remove(sOut.Length - 2)
        sOut += Environment.NewLine & "Перечисление значений: "

        Dim values = [Enum].GetValues(GetType(EnumWindowsForms))

        ' Output: 1 2 4 8 16 ...
        For Each value In values
            sOut &= CStr(value) & ", "
        Next

        sOut = sOut.Remove(sOut.Length - 2)

        Return sOut
    End Function

    Public Function CheckPattern(typeForm As String, namesWindowsTag As List(Of String)) As String
        Dim patternOpenWindows As EnumWindowsForms ' шаблон открытых окон
        Dim messageWarning As String = String.Empty
        Const MESSAGE_WINDOW As String = "загружено окно -> "

        ' получить все аттрибуты перечислителя для прохода по списку возможных окон в системе
        For Each value In [Enum].GetValues(GetType(EnumWindowsForms))
            Dim fi As FieldInfo = GetType(EnumWindowsForms).GetField([Enum].GetName(GetType(EnumWindowsForms), value))
            Dim dna As DescriptionAttribute = DirectCast(System.Attribute.GetCustomAttribute(fi, GetType(DescriptionAttribute)), DescriptionAttribute)

            If dna IsNot Nothing Then
                ' если открыто Окно совпадающее с дескрипттором элемента в перечислении, то сделать логическое добавление в шаблон
                If namesWindowsTag.Contains(dna.Description) Then
                    patternOpenWindows = patternOpenWindows Or CType(value, EnumWindowsForms)
                End If
            End If
        Next

        Dim enumBadWindows As EnumWindowsForms = FormWindowsManager.LimitationWindows(typeForm) ' перечислитель списка Окон при которых открытие нового Окна запрещено
        Dim currentScaleCheck As EnumWindowsForms = enumBadWindows And patternOpenWindows ' Перечислитель, обладающий некоторыми свойствами, заданными образцом.

        If currentScaleCheck > 0 Then ' проверить, есть ли хоть одно вхождение
            ' пройти по всем членам EnumWindowsForms
            For Each value In [Enum].GetValues(GetType(EnumWindowsForms))
                If patternOpenWindows.HasFlag(CType(value, [Enum])) AndAlso enumBadWindows.HasFlag(CType(value, [Enum])) Then
                    ' совпали члены в 2-х перечислениях, значить добавить в сообщение
                    Dim fi As FieldInfo = GetType(EnumWindowsForms).GetField([Enum].GetName(GetType(EnumWindowsForms), value))
                    Dim dna As DescriptionAttribute = DirectCast(System.Attribute.GetCustomAttribute(fi, GetType(DescriptionAttribute)), DescriptionAttribute)

                    If dna IsNot Nothing Then
                        messageWarning += MESSAGE_WINDOW & dna.Description & vbCrLf
                    End If
                End If
            Next
        End If

        Return messageWarning
    End Function

    ''' <summary>
    ''' Выдать строку имён пречислений, содержащихся в свойстве
    ''' </summary>
    ''' <param name="enumMembers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEnumNamesAndDescriptions(enumMembers As EnumWindowsForms) As String
        Dim allNames As String = String.Empty
        Dim values = [Enum].GetValues(GetType(EnumWindowsForms))
        Dim equals, hasFlag As Boolean

        For Each value In values
            equals = False
            hasFlag = False

            If enumMembers.Equals(value) Then
                ' Точно
                equals = True
            ElseIf enumMembers.HasFlag(CType(value, [Enum])) Then
                ' Содержит
                hasFlag = True
            End If

            If equals OrElse hasFlag Then
                ' выбрать имена
                'allNames += CType(value, EnumWindowsForms).ToString & " -> "

                ' выбрать описание <Description>
                Dim fi As FieldInfo = GetType(EnumWindowsForms).GetField([Enum].GetName(GetType(EnumWindowsForms), value))
                Dim dna As DescriptionAttribute = DirectCast(System.Attribute.GetCustomAttribute(fi, GetType(DescriptionAttribute)), DescriptionAttribute)

                If dna IsNot Nothing Then
                    'allNames += dna.Description & ", "
                    allNames = dna.Description
                Else
                    allNames += "Нет описания" & ", "
                End If
                If equals Then
                    ' совпадение точное, далее искать не надо
                    Exit For
                End If
            End If

        Next

        If allNames = String.Empty Then
            allNames = "Ни чего не содержит!!!"
        Else
            'allNames = allNames.Remove(allNames.Length - 2)
        End If

        Return allNames
    End Function

    ''' <summary>
    ''' Перечислитель, обладающий свойствами, заданных образцом.
    ''' (все свойства образца)
    ''' </summary>
    Public Function IsEnumMembersHavePattern(enumMembers As EnumWindowsForms, pattern As EnumWindowsForms) As Boolean
        Return (enumMembers And pattern) = pattern
    End Function

    ''' <summary>
    ''' Перечислитель, не обладающий всеми свойствами, заданными образцом (ни одно из свойств образца).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsEnumMembersHaveNotAllPattern(enumMembers As EnumWindowsForms, pattern As EnumWindowsForms) As Boolean
        Return (Not enumMembers And pattern) = pattern
    End Function

    ''' <summary>
    ''' Перечислитель, обладающий некоторыми свойствами, заданными образцом.
    ''' </summary>
    Public Function IsEnumMembersHaveSomePattern(enumMembers As EnumWindowsForms, pattern As EnumWindowsForms) As Boolean
        'If currentScale > 0 Then ' некоторыми свойствами
        Dim currentScale As EnumWindowsForms = enumMembers And pattern
        Return currentScale > 0 AndAlso currentScale < pattern
    End Function

    ''' <summary>
    ''' Перечислитель, обладающий только свойствами, заданными образцом.
    ''' </summary>
    Public Function IsEnumMembersHaveOnlyPattern(enumMembers As EnumWindowsForms, pattern As EnumWindowsForms) As Boolean
        Return ((enumMembers And pattern) = pattern) AndAlso ((enumMembers And Not pattern) = 0)
    End Function
End Class

'Public Shared Sub Test()
'    Dim n As Integer = 10
'    Dim testJob As JobWithEnumWindowsForms = New JobWithEnumWindowsForms(n)
'    testJob.Pattern = EnumWindowsForms.РедакторЦиклограмм Or EnumWindowsForms.РедакторСетевыхПеременных

'    Console.WriteLine("Все имена и значения базового перечисления:")
'    Console.WriteLine(testJob.mWorkWithEnum.GetNameAndValueIterateBaseEnum())
'    testJob.FormCandidates()
'    Dim Candidates As EnumWindowsForms() = testJob.GetCandidates
'    Dim strCand() As String = testJob.GetStrCandidates
'    Console.WriteLine()
'    Console.WriteLine("Требования заданные образцом:" & System.Environment.NewLine &
'                      "Знание C# и WEB технологии")
'    Console.WriteLine()

'    For i As Integer = 0 To n - 1
'        Console.WriteLine("Свойства кандидата: {0} = {1} => с именами свойств: {2}", i, Candidates(i).ToString, testJob.mWorkWithEnum.GetEnumNamesAndDescriptions(Candidates(i)))
'        Console.WriteLine("Перечень значений: {0}", strCand(i))
'    Next

'    Console.WriteLine()
'    Console.WriteLine("Список кандидатов, обладающих свойствами, заданных образцом = {0}", testJob.IsCandidatesHavePattern)
'    PrintArrayList(testJob.CandidatesHavePattern())
'    Console.WriteLine()
'    Console.WriteLine("Список кандидатов, не обладающих всеми свойствами, заданными образцом = {0}", testJob.IsCandidatesHaveNotAllPattern)
'    PrintArrayList(testJob.CandidatesHaveNotAllPattern())
'    Console.WriteLine()
'    Console.WriteLine("Список кандидатов, обладающих некоторыми свойствами, заданными образцом = {0}", testJob.IsCandidatesHaveSomePattern)
'    PrintArrayList(testJob.CandidatesHaveSomePattern())
'    Console.WriteLine()
'    Console.WriteLine("Список кандидатов, обладающих только свойствами, заданными образцом = {0}", testJob.IsCandidatesHaveOnlyPattern)
'    PrintArrayList(testJob.CandidatesHaveOnlyPattern())

'    Console.ReadLine()
'End Sub
'Private Shared Sub PrintArrayList(arrCan As List(Of String))
'    For i As Integer = 0 To arrCan.Count - 1
'        Console.WriteLine(arrCan(i))
'    Next
'End Sub



' ''' <summary>
' ''' Тестовый класс для проверки работы с EnumWindowsForms
' ''' </summary>
' ''' <remarks></remarks>
'Friend Class JobWithEnumWindowsForms
'    Dim n As Integer ' число претендентов
'    Dim candProps() As EnumWindowsForms
'    Dim strCand() As String
'    Dim rnd As Random
'    Public Property mWorkWithEnum As New WorkWithEnum
'    Public Property Pattern As EnumWindowsForms

'    Public Sub New()
'        n = 10
'        candProps = New EnumWindowsForms(n - 1) {}

'        strCand = New String(n - 1) {}
'        rnd = New Random
'    End Sub

'    Public Sub New(n As Integer)
'        Me.n = n
'        candProps = New EnumWindowsForms(n - 1) {}

'        strCand = New String(n - 1) {}
'        rnd = New Random
'    End Sub

'    Public Sub New(pp As EnumWindowsForms())

'        n = pp.Length
'        candProps = New EnumWindowsForms(n - 1) {}

'        strCand = New String(n - 1) {}
'        rnd = New Random
'    End Sub


'    ''' <summary>
'    ''' Тест с кандидатами
'    ''' </summary>
'    ''' <remarks></remarks>
'    Public Sub FormCandidates()
'        Dim properties As Integer = [Enum].GetValues(GetType(EnumWindowsForms)).Length
'        Dim p, q, currentProps As Integer
'        Dim strQ As String

'        For i = 0 To n - 1
'            currentProps = 0
'            strQ = ""
'            For j = 0 To properties - 1
'                p = rnd.Next(2)
'                q = Convert.ToInt32(Math.Pow(2, j))
'                If p = 1 Then
'                    currentProps += q
'                    strQ += CType(q, EnumWindowsForms) & ", "
'                End If
'            Next
'            candProps(i) = CType(currentProps, EnumWindowsForms)
'            If strQ <> "" Then
'                strCand(i) = strQ.Remove(strQ.Length - 2)
'            Else
'                strCand(i) = ""
'            End If
'        Next
'    End Sub

'    Public Function GetStrCandidates() As String()
'        Return strCand
'    End Function

'    Public Function GetCandidates() As EnumWindowsForms()
'        Return candProps
'    End Function

'    ''' <summary>
'    ''' Список кандидатов, обладающих
'    ''' свойствами, заданных образцом.
'    ''' (все свойства образца)
'    ''' </summary>
'    Public Function CandidatesHavePattern() As List(Of String)
'        Dim temp As New List(Of String)
'        For i As Integer = 0 To candProps.Length - 1
'            'If (candProps(i) And Pattern) = Pattern Then
'            If mWorkWithEnum.IsEnumMembersHavePattern(candProps(i), Pattern) Then
'                temp.Add("кандидат[" & i & "]")
'            End If
'        Next
'        Return temp
'    End Function

'    ''' <summary>
'    ''' Список кандидатов, не обладающих
'    ''' всеми свойствами, заданными образцом.
'    ''' (ни одно из свойств образца)
'    ''' </summary>
'    Public Function CandidatesHaveNotAllPattern() As List(Of String)
'        Dim temp As New List(Of String)
'        For i As Integer = 0 To candProps.Length - 1 'n - 1
'            'If (Not candProps(i) And Pattern) = Pattern Then
'            If mWorkWithEnum.IsEnumMembersHaveNotAllPattern(candProps(i), Pattern) Then
'                temp.Add("кандидат[" & i & "]")
'            End If
'        Next
'        Return temp
'    End Function

'    ''' <summary>
'    ''' Список кандидатов, обладающих
'    ''' некоторыми свойствами, заданными образцом.
'    ''' </summary>
'    Public Function CandidatesHaveSomePattern() As List(Of String)
'        Dim temp As New List(Of String)
'        For i As Integer = 0 To candProps.Length - 1 ' n - 1
'            'currentScale = candProps(i) And Pattern
'            'If currentScale > 0 AndAlso currentScale < Pattern Then ' некоторые, но не все
'            If mWorkWithEnum.IsEnumMembersHaveSomePattern(candProps(i), Pattern) Then
'                'If currentScale > 0 Then ' некоторыми свойствами
'                temp.Add("кандидат[" & i & "]")
'            End If
'        Next
'        Return temp
'    End Function

'    ''' <summary>
'    ''' Список кандидатов, обладающих
'    ''' только свойствами, заданными образцом.
'    ''' </summary>
'    Public Function CandidatesHaveOnlyPattern() As List(Of String)
'        Dim temp As New List(Of String)
'        For i As Integer = 0 To candProps.Length - 1 'n - 1
'            'If ((candProps(i) And Pattern) = Pattern) AndAlso ((candProps(i) And Not Pattern) = 0) Then
'            If mWorkWithEnum.IsEnumMembersHaveOnlyPattern(candProps(i), Pattern) Then
'                temp.Add("кандидат[" & i & "]")
'            End If
'        Next
'        Return temp
'    End Function

'    ''' <summary>
'    ''' Есть хоть один кандидат, обладающих свойствами, заданных образцом.
'    ''' (все свойства образца)
'    ''' </summary>
'    Public Function IsCandidatesHavePattern() As Boolean
'        Return CandidatesHavePattern.Count > 0
'    End Function

'    ''' <summary>
'    ''' Есть хоть один кандидат, не обладающих всеми свойствами, заданными образцом (ни одно из свойств образца).
'    ''' </summary>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Function IsCandidatesHaveNotAllPattern() As Boolean
'        Return CandidatesHaveNotAllPattern.Count > 0
'    End Function

'    ''' <summary>
'    ''' Есть хоть один кандидат, обладающих некоторыми свойствами, заданными образцом.
'    ''' </summary>
'    Public Function IsCandidatesHaveSomePattern() As Boolean
'        Return CandidatesHaveSomePattern.Count > 0
'    End Function

'    ''' <summary>
'    ''' Есть хоть один кандидат, обладающих только свойствами, заданными образцом.
'    ''' </summary>
'    Public Function IsCandidatesHaveOnlyPattern() As Boolean
'        Return CandidatesHaveOnlyPattern.Count > 0
'    End Function
'End Class