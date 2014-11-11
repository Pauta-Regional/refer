'-------------------------------------------
' ImagePanel.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Class ImagePanel
    Inherits Panel

    ' Image button size
    Const cxButton As Integer = 100
    Const cyButton As Integer = 100

    Private btnClicked As Button
    Private tip As New ToolTip()
    Private tmr As New Timer()

    ' Fields for Timer Tick event
    Private astrFileNames() As String
    Private i, x, y As Integer

    ' Public event
    Event ImageClicked As EventHandler

    ' Constructor
    Sub New()
        AutoScroll = True
        tmr.Interval = 1
        AddHandler tmr.Tick, AddressOf TimerOnTick
    End Sub

    ' Public properties
    ReadOnly Property ClickedControl() As Control
        Get
            Return btnClicked
        End Get
    End Property

    ReadOnly Property ClickedImage() As Image
        Get
            Try
                Return Image.FromFile(btnClicked.Tag.ToString())
            Catch
                Return Nothing
            End Try
        End Get
    End Property

    ' Public method     
    Sub ShowImages(ByVal strDirectory As String)
        Controls.Clear()
        tip.RemoveAll()
        Try
            astrFileNames = Directory.GetFiles(strDirectory)
        Catch
            Return
        End Try

        i = 0
        x = 0
        y = 0
        tmr.Start()
    End Sub

    ' Event handlers
    Private Sub TimerOnTick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim img As Image

        If i = astrFileNames.Length Then
            tmr.Stop()
            Return
        End If

        Try
            img = Image.FromFile(astrFileNames(i))
        Catch
            i += 1
            Return
        End Try

        Dim cxImage As Integer = img.Width
        Dim cyImage As Integer = img.Height

        ' Convert image to small size for button.
        Dim szf As New SizeF(cxImage / img.HorizontalResolution, _
                             cyImage / img.VerticalResolution)
        Dim fScale As Single = Math.Min(cxButton / szf.Width, _
                                        cyButton / szf.Height)
        szf.Width *= fScale
        szf.Height *= fScale

        Dim sz As Size = Size.Ceiling(szf)
        Dim bitmap As New bitmap(img, sz)
        img.Dispose()

        ' Create button and add to pnl.
        Dim btn As New Button()
        btn.Image = bitmap
        btn.Location = Point.op_Addition(New Point(x, y), _
                       Point.op_Explicit(AutoScrollPosition))
        btn.Size = New Size(cxButton, cyButton)
        btn.Tag = astrFileNames(i)
        AddHandler btn.Click, AddressOf ButtonOnClick
        Controls.Add(btn)

        ' Give button a ToolTip.
        tip.SetToolTip(btn, String.Format("{0}" & vbLf & "{1}x{2}", _
                                Path.GetFileName(astrFileNames(i)), _
                                cxImage, cyImage))

        ' Adjust i, x, and y for next image.
        AdjustXY(x, y)
        i += 1
    End Sub

    Private Sub ButtonOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        btnClicked = DirectCast(obj, Button)
        RaiseEvent ImageClicked(Me, EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)
        AutoScrollPosition = Point.Empty

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim ctrl As Control

        For Each ctrl In Controls
            ctrl.Location = Point.op_Addition(New Point(x, y), _
                            Point.op_Explicit(AutoScrollPosition))
            AdjustXY(x, y)
        Next ctrl
    End Sub

    Private Sub AdjustXY(ByRef x As Integer, ByRef y As Integer)
        y += cyButton
        If y + cyButton > _
                Height - SystemInformation.HorizontalScrollBarHeight Then
            y = 0
            x += cxButton
        End If
    End Sub
End Class
