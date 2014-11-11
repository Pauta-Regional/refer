'---------------------------------------------------
' CheckAndRadioCheck.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CheckAndRadioCheck
    Inherits Form

    Private miColor, miFill As MenuItem

    Shared Sub Main()
        Application.Run(New CheckAndRadioCheck())
    End Sub

    Sub New()
        Text = "Check and Radio Check"
        ResizeRedraw = True

        Dim astrColor() As String = {"Black", "Blue", "Green", "Cyan", _
                                     "Red", "Magenta", "Yellow", "White"}

        Dim ami(astrColor.Length + 1) As MenuItem
        Dim ehColor As EventHandler = AddressOf MenuFormatColorOnClick
        Dim i As Integer

        For i = 0 To astrColor.GetUpperBound(0)
            ami(i) = New MenuItem(astrColor(i), ehColor)
            ami(i).RadioCheck = True
        Next i
        miColor = ami(0)
        miColor.Checked = True

        ami(astrColor.Length) = New MenuItem("-")
        miFill = New MenuItem("&Fill", AddressOf MenuFormatFillOnClick)
        ami(astrColor.Length + 1) = miFill

        Dim mi As New MenuItem("&Format", ami)
        Menu = New MainMenu(New MenuItem() {mi})
    End Sub

    Private Sub MenuFormatColorOnClick(ByVal obj As Object, _
                                       ByVal ea As EventArgs)
        miColor.Checked = False
        miColor = DirectCast(obj, MenuItem)
        miColor.Checked = True
        Invalidate()
    End Sub

    Private Sub MenuFormatFillOnClick(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        Dim mi As MenuItem = DirectCast(obj, MenuItem)
        mi.Checked = Not mi.Checked
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics

        If miFill.Checked Then
            Dim br As New SolidBrush(Color.FromName(miColor.Text))
            grfx.FillEllipse(br, 0, 0, ClientSize.Width - 1, _
                                       ClientSize.Height - 1)
        Else
            Dim pn As New Pen(Color.FromName(miColor.Text))
            grfx.DrawEllipse(pn, 0, 0, ClientSize.Width - 1, _
                                       ClientSize.Height - 1)
        End If
    End Sub
End Class
