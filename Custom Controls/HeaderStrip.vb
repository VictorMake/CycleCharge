Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

'Namespace System.Windows.Forms.Samples
Public Class HeaderStrip
    Inherits ToolStrip

    Private _headerStyle As AreaHeaderStyle = AreaHeaderStyle.Large
    Private _pr As ToolStripProfessionalRenderer = Nothing

#Region "Public API"

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'Этот вызов является обязательным для конструктора компонентов.
        InitializeComponent()
        ' Установить Dock
        Dock = DockStyle.Top
        GripStyle = ToolStripGripStyle.Hidden
        AutoSize = False

        ' Задать перерисовку - переопределения базового рисования
        SetRenderer()

        ' Отслеживание изменений пользовательских предпочтений
        AddHandler Microsoft.Win32.SystemEvents.UserPreferenceChanged, New Microsoft.Win32.UserPreferenceChangedEventHandler(AddressOf HeaderStrip_UserPreferenceChanged)

        ' Настроить стиль
        SetHeaderStyle()
    End Sub

    <DefaultValue(AreaHeaderStyle.Large)> _
    Public Property HeaderStyle() As AreaHeaderStyle
        Get
            Return _headerStyle
        End Get
        Set(value As AreaHeaderStyle)
            ' Задать значение
            If _headerStyle <> value Then
                _headerStyle = value

                ' Настроить стиль
                SetHeaderStyle()
            End If
        End Set
    End Property
#End Region

#Region "Overrides"
    Protected Overrides Sub OnRendererChanged(e As EventArgs)
        ' Вызвать базовый метод
        MyBase.OnRendererChanged(e)

        ' Работа может глючить с настройками перерисовки в конструкторе
        SetRenderer()
    End Sub
#End Region

#Region "Private Implementation"
    Private Sub SetHeaderStyle()
        ' Получить системный фонт
        Dim font As Font = SystemFonts.MenuFont

        If _headerStyle = AreaHeaderStyle.Large Then
            Me.Font = New Font("Arial", font.SizeInPoints + 3.75F, FontStyle.Bold)
            ForeColor = Color.White
        Else
            Me.Font = font
            ForeColor = Color.Black
        End If

        ' Только вычислить размер
        Dim tsl As New ToolStripLabel() With {.Font = Me.Font, .Text = "I"}

        ' Задать размер
        Height = tsl.GetPreferredSize(Size.Empty).Height + 6
    End Sub

    Private Sub SetRenderer()
        ' Задать перерисовку - переопределёние пререрисовки фона
        'If (TypeOf Me.Renderer Is ToolStripProfessionalRenderer) AndAlso (Me.Renderer <> _pr) Then
        If (TypeOf Renderer Is ToolStripProfessionalRenderer) AndAlso (Renderer IsNot _pr) Then
            If _pr Is Nothing Then
                ' Только обмен выдать если настроено на профессиональную перерисовку
                ' Квадратные углы
                _pr = New ToolStripProfessionalRenderer() With {.RoundedEdges = False}

                ' Улучшить перерисовку (используя свой цвет)
                '_pr.RenderToolStripBackground += New ToolStripRenderEventHandler(AddressOf Renderer_RenderToolStripBackground)
                AddHandler _pr.RenderToolStripBackground, New ToolStripRenderEventHandler(AddressOf Renderer_RenderToolStripBackground)
            End If

            ' настроить внешний вид
            Renderer = _pr
        End If
    End Sub
#End Region

#Region "Event Handlers"
    Private Sub Renderer_RenderToolStripBackground(sender As Object, e As ToolStripRenderEventArgs)
        Dim start As Color
        Dim [end] As Color

        If TypeOf Renderer Is ToolStripProfessionalRenderer Then
            Dim pr As ToolStripProfessionalRenderer = TryCast(Renderer, ToolStripProfessionalRenderer)

            ' Задать цвета для пользовательской перерисовки
            If _headerStyle = AreaHeaderStyle.Large Then
                start = pr.ColorTable.OverflowButtonGradientMiddle
                [end] = pr.ColorTable.OverflowButtonGradientEnd
            Else
                start = pr.ColorTable.MenuStripGradientEnd
                [end] = pr.ColorTable.MenuStripGradientBegin
            End If

            ' Задать область заливкм
            Dim bounds As New Rectangle(Point.Empty, e.ToolStrip.Size)

            ' Удостовериться, что это нужно сделать
            If (bounds.Width > 0) AndAlso (bounds.Height > 0) Then
                Using b As Brush = New LinearGradientBrush(bounds, start, [end], LinearGradientMode.Vertical)
                    e.Graphics.FillRectangle(b, bounds)
                End Using
            End If
        End If
    End Sub

    Private Sub HeaderStrip_UserPreferenceChanged(sender As Object, e As Microsoft.Win32.UserPreferenceChangedEventArgs)
        SetHeaderStyle()
    End Sub
#End Region
End Class

#Region "AreaHeaderStyle"
Public Enum AreaHeaderStyle
    Large = 0
    Small = 1
End Enum
#End Region
