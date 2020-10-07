Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports NationalInstruments.UI.WindowsForms

Public Enum AttributeType
    IsString
    IsNumeric
    IsBoolean
    IsComboBox
End Enum

''' <summary>
''' Представляет значения полей записи в таблице Устройства1 или ВеличинаЗагрузки2
''' для представления этих полей в таблице при отображении и редактировании.
''' </summary>
Public Class Attribute
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Property Name As String
    Public Property Description As String
    Public Property Type As AttributeType
    Public Property IsEnabled As Boolean
    Public Property AttributeStringValue As String
    Public Property AttributeStringEdit As TextBox
    Public Property AttributeNumericValue As Double
    Public Property AttributeNumericEdit As NumericEdit
    Public Property AttributeBooleanValue As Boolean
    Public Property AttributeCheckBoxEdit As CheckBox
    Public Property AttributeComboBoxValue As String
    Public Property AttributeComboBoxEdit As ComboBox

    Private counterControls As Integer

    Private ComboBoxArgs As String()
    Public Sub New(inName As String,
                   inDescription As String,
                   inType As AttributeType,
                   inEnabled As Boolean,
                   inAttributeValue As Object,
                   ByVal ParamArray args() As String)
        Name = inName
        Description = inDescription
        Type = inType
        IsEnabled = inEnabled

        If args.Length > 0 Then ComboBoxArgs = args

        AttributeValue = inAttributeValue
    End Sub

    ''' <summary>
    ''' Значение в таблице запроса представлено типом Object, поэтому необходимо приведение для
    ''' внутренненго сохранения.
    ''' </summary>
    ''' <returns></returns>
    Public Property AttributeValue() As Object
        Get
            Select Case Type
                Case AttributeType.IsString
                    AttributeStringValue = AttributeStringEdit.Text
                    Return AttributeStringValue
                Case AttributeType.IsNumeric
                    AttributeNumericValue = AttributeNumericEdit.Value
                    Return AttributeNumericValue
                Case AttributeType.IsBoolean
                    AttributeBooleanValue = AttributeCheckBoxEdit.Checked
                    Return AttributeBooleanValue
                Case AttributeType.IsComboBox
                    AttributeComboBoxValue = AttributeComboBoxEdit.Text
                    Return AttributeComboBoxValue
                Case Else
                    Return AttributeStringValue
            End Select
        End Get
        Set(ByVal value As Object)
            Select Case Type
                Case AttributeType.IsString
                    If AttributeStringEdit Is Nothing Then AttributeStringEdit = NewAttributeStringEdit()
                    AttributeStringValue = Convert.ToString(value)
                    AttributeStringEdit.Text = AttributeStringValue
                Case AttributeType.IsNumeric
                    If AttributeNumericEdit Is Nothing Then AttributeNumericEdit = NewAttributeNumericEdit()
                    AttributeNumericValue = Convert.ToDouble(value)
                    AttributeNumericEdit.Value = AttributeNumericValue
                Case AttributeType.IsBoolean
                    If AttributeCheckBoxEdit Is Nothing Then AttributeCheckBoxEdit = NewAttributeCheckBoxEdit()
                    AttributeBooleanValue = Convert.ToBoolean(value)
                    AttributeCheckBoxEdit.Checked = AttributeBooleanValue
                Case AttributeType.IsComboBox
                    If AttributeComboBoxEdit Is Nothing Then AttributeComboBoxEdit = NewAttributeComboBoxEdit()
                    AttributeComboBoxValue = Convert.ToString(value)
                    AttributeComboBoxEdit.SelectedText = AttributeComboBoxValue
                    AttributeComboBoxEdit.Text = AttributeComboBoxValue
            End Select

            'OnPropertyChanged(NameOf(Me.AttributeValue))
            OnPropertyChanged()
        End Set
    End Property

    Private Function NewAttributeStringEdit() As TextBox
        counterControls += 1

        Dim newTextBox As New TextBox With {
            .Dock = DockStyle.Fill,
            .Location = New Drawing.Point(176, 5),
            .Multiline = True,
            .Name = "TextBoxAttribute" & counterControls,
            .Size = New Drawing.Size(163, 50)
        }

        Return newTextBox
    End Function

    Private Function NewAttributeNumericEdit() As NumericEdit
        counterControls += 1

        Dim newNumericEdit As New NumericEdit With {
            .Dock = DockStyle.Fill,
            .FormatMode = NationalInstruments.UI.NumericFormatMode.CreateSimpleDoubleMode(2),
            .Location = New Drawing.Point(176, 65),
            .Margin = New Padding(3, 5, 3, 0),
            .Name = "NumericEditAttribute" & counterControls,
            .Size = New Drawing.Size(163, 20)
        }

        Return newNumericEdit
    End Function

    Private Function NewAttributeCheckBoxEdit() As CheckBox
        counterControls += 1

        Dim newCheckBox As New CheckBox With {
            .AutoSize = True,
            .Dock = DockStyle.Fill,
            .Location = New Drawing.Point(176, 121),
            .Name = "CheckBoxAttribute" & counterControls,
            .Size = New Drawing.Size(163, 50),
            .Text = "Включить",
            .UseVisualStyleBackColor = True
        }

        Return newCheckBox
    End Function

    ''' <summary>
    '''Заполнение единиц измерения для автоматической подстановке к величине мощности.
    ''' </summary>
    ''' <returns></returns>
    Private Function NewAttributeComboBoxEdit() As ComboBox
        counterControls += 1

        Dim newComboBox As New ComboBox With {
            .Dock = DockStyle.Fill,
            .FormattingEnabled = True,
            .Location = New Drawing.Point(176, 181),
            .Margin = New Padding(3, 5, 3, 3),
            .Name = "ComboBoxAttribute" & counterControls,
            .Size = New Drawing.Size(163, 21)
        }

        If Name = cTarget Then
            AddHandler newComboBox.SelectedValueChanged, AddressOf ComboBoxTarget_SelectedIndexChanged
        End If

        If ComboBoxArgs IsNot Nothing Then newComboBox.Items.AddRange(ComboBoxArgs)
        Return newComboBox
    End Function

    ''' <summary>
    ''' Реализация интерфейса INotifyPropertyChanged для последующего прехвата события в контроле BaseAttributeControl,
    ''' а затем и повторное его генерации.
    ''' </summary>
    ''' <param name="propertyName"></param>
    Public Sub OnPropertyChanged(<CallerMemberName> ByVal Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
    'Protected Sub OnPropertyChanged(Optional ByVal propertyName As String = Nothing)
    '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    'End Sub

    ''' <summary>
    ''' Подписаться на обработку события смены устройства.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ComboBoxTarget_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        OnPropertyChanged(cTarget)
    End Sub
End Class