'-----------------------------------------
' WarpText.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class WarpText
    Inherits FontMenuForm

    Private iWarpMode As Integer = 0

    Shared Shadows Sub Main()
        Application.Run(New WarpText())
    End Sub

    Sub New()
        Text = "Warp Text - " & CType(iWarpMode, WarpMode).ToString()
        Menu.MenuItems.Add("&Toggle!", AddressOf MenuToggleOnClick)
        strText = "WARP"
        fnt = New Font("Arial Black", 24)
    End Sub

    Private Sub MenuToggleOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        iWarpMode = iWarpMode Xor 1
        Text = "Warp Text - " & CType(iWarpMode, WarpMode).ToString()
        Invalidate()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()

        ' Add text to the path.
        path.AddString(strText, fnt.FontFamily, fnt.Style, _
                       100, New PointF(0, 0), New StringFormat())

        ' Warp the path.
        Dim rectfBounds As RectangleF = path.GetBounds()
        Dim aptfDest() As PointF = {New PointF(cx \ 3, 0), _
                                    New PointF(2 * cx \ 3, 0), _
                                    New PointF(0, cy), _
                                    New PointF(cx, cy)}
        path.Warp(aptfDest, rectfBounds, New Matrix(), _
                  CType(iWarpMode, WarpMode))

        ' Fill the path.
        grfx.FillPath(New SolidBrush(clr), path)
    End Sub
End Class
