Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
''' Базовый контрол для реализации ленты контролов как кнопки.
''' </summary>
''' <remarks></remarks>
Public Class BaseStripButton
    Inherits ToolStrip

    Private _pr As ToolStripProfessionalRenderer = Nothing

#Region "Public API"

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        ' Этот вызов является обязательным для конструктора компонентов.
        InitializeComponent()
        ' Установить Dock
        Dock = DockStyle.Fill

        GripStyle = ToolStripGripStyle.Hidden
        Margin = New Padding(0)
        CanOverflow = False
        AutoSize = False

        ' Задать перерисовку - переопределения базового рисования
        SetRenderer()

        ' Задать Fonts
        OnSetFonts()

        ' Отследить пользовательские изменения
        AddHandler Microsoft.Win32.SystemEvents.UserPreferenceChanged, New Microsoft.Win32.UserPreferenceChangedEventHandler(AddressOf StackStrip_UserPreferenceChanged)
    End Sub

    '<DefaultValue(AreaHeaderStyle.Large)> _
    'Public Property HeaderStyle() As AreaHeaderStyle
    '    Get
    '        Return _headerStyle
    '    End Get
    '    Set(value As AreaHeaderStyle)
    '        ' Save value
    '        If _headerStyle <> value Then
    '            _headerStyle = value

    '            ' Set Header Style
    '            SetHeaderStyle()
    '        End If
    '    End Set
    'End Property
#End Region

#Region "Protected API"
    Protected Overridable Sub OnSetRenderer(pr As ToolStripProfessionalRenderer)
        ' Обработчик sub-classes
    End Sub

    Protected Overridable Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)
        If StackStripRenderer IsNot Nothing Then
            ' настроить цветв представителя перерисовки
            Dim start As Color = StackStripRenderer.ColorTable.ToolStripGradientMiddle
            Dim [end] As Color = StackStripRenderer.ColorTable.ToolStripGradientEnd

            ' Размер рисования
            Dim bounds As New Rectangle(Point.Empty, e.ToolStrip.Size)

            ' Удостоверится, что необходима перерисовка
            If (bounds.Width > 0) AndAlso (bounds.Height > 0) Then
                Using b As Brush = New LinearGradientBrush(bounds, start, [end], LinearGradientMode.Vertical)
                    e.Graphics.FillRectangle(b, bounds)
                End Using
            End If

            ' Рисовать бордюр
            'e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, bounds);
            e.Graphics.DrawLine(SystemPens.ControlDarkDark, bounds.X, bounds.Y, bounds.Width - 1, bounds.Y)
            e.Graphics.DrawLine(SystemPens.ControlDarkDark, bounds.X, bounds.Y, bounds.X, bounds.Height - 1)
            e.Graphics.DrawLine(SystemPens.ControlDarkDark, bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Height - 1)
        End If
    End Sub

    Protected ReadOnly Property StackStripRenderer() As ToolStripProfessionalRenderer
        Get
            Return _pr
        End Get
    End Property

    Protected Overridable Sub OnSetFonts()
        ' Установить базовые шрифты
    End Sub
#End Region

#Region "Overrides"
    Protected Overrides Sub OnRendererChanged(e As EventArgs)
        ' Вызвать метод контейнера
        MyBase.OnRendererChanged(e)

        ' В режиме разработки могут быть ошибки
        SetRenderer()
    End Sub
#End Region

    '#Region "Private Implementation"
    '    Private Sub SetHeaderStyle()
    '        ' Получить системный шрифт
    '        Dim font As Font = SystemFonts.MenuFont

    '        If _headerStyle = AreaHeaderStyle.Large Then
    '            Me.Font = New Font("Arial", font.SizeInPoints + 3.75F, FontStyle.Bold)
    '            Me.ForeColor = System.Drawing.Color.White
    '        Else
    '            Me.Font = font
    '            Me.ForeColor = System.Drawing.Color.Black
    '        End If

    '        ' Только один путь расчёта размера
    '        Dim tsl As New ToolStripLabel()
    '        tsl.Font = Me.Font
    '        tsl.Text = "I"

    '        ' Set Size
    '        Me.Height = tsl.GetPreferredSize(Size.Empty).Height + 6
    '    End Sub

    '#End Region

    '#Region "Event Handlers"
    '    Private Sub Renderer_RenderToolStripBackground(sender As Object, e As ToolStripRenderEventArgs)
    '        Dim start As Color
    '        Dim [end] As Color

    '        If TypeOf Me.Renderer Is ToolStripProfessionalRenderer Then
    '            Dim pr As ToolStripProfessionalRenderer = TryCast(Me.Renderer, ToolStripProfessionalRenderer)

    '            ' Setup colors from the provided renderer
    '            If _headerStyle = AreaHeaderStyle.Large Then
    '                start = pr.ColorTable.OverflowButtonGradientMiddle
    '                [end] = pr.ColorTable.OverflowButtonGradientEnd
    '            Else
    '                start = pr.ColorTable.MenuStripGradientEnd
    '                [end] = pr.ColorTable.MenuStripGradientBegin
    '            End If

    '            ' Размер перерисовки
    '            Dim bounds As New Rectangle(Point.Empty, e.ToolStrip.Size)

    '            ' Удостоверится что работа необходима
    '            If (bounds.Width > 0) AndAlso (bounds.Height > 0) Then
    '                Using b As Brush = New LinearGradientBrush(bounds, start, [end], LinearGradientMode.Vertical)
    '                    e.Graphics.FillRectangle(b, bounds)
    '                End Using
    '            End If
    '        End If
    '    End Sub

    '    Private Sub HeaderStrip_UserPreferenceChanged(sender As Object, e As Microsoft.Win32.UserPreferenceChangedEventArgs)
    '        SetHeaderStyle()
    '    End Sub
    '#End Region

#Region "Private API"
    Private Sub SetRenderer()
        ' Задать перерисовку - переопределёние пререрисовки фона
        If (TypeOf Renderer Is ToolStripProfessionalRenderer) AndAlso (Renderer IsNot _pr) Then
            If _pr Is Nothing Then
                ' Только обмен выдать если настроено на профессиональную перерисовку
                ' Квадратные углы
                _pr = New ToolStripProfessionalRenderer() With {.RoundedEdges = False}

                ' Улучшить перерисовку (используя свой цвет)
                '_pr.RenderToolStripBackground += New ToolStripRenderEventHandler(AddressOf BaseStackStrip_RenderToolStripBackground)
                AddHandler _pr.RenderToolStripBackground, New ToolStripRenderEventHandler(AddressOf BaseStackStrip_RenderToolStripBackground)

                ' overridable method
                OnSetRenderer(_pr)
            End If

            ' настроить внешний вид
            Renderer = _pr
        End If
    End Sub
#End Region

#Region "Event Handlers"
    Private Sub BaseStackStrip_RenderToolStripBackground(sender As Object, e As ToolStripRenderEventArgs)
        ' вызвать переопределённый
        OnRenderToolStripBackground(e)
    End Sub

    Private Sub StackStrip_UserPreferenceChanged(sender As Object, e As Microsoft.Win32.UserPreferenceChangedEventArgs)
        ' сбросить шрифт
        OnSetFonts()
    End Sub
#End Region

End Class
