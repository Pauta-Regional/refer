'--------------------------------------------
' TextColumns.vb (c) 2002 by Charles Petzold
'--------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

Class TextColumns
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new TextColumns())
    End Sub

    Sub New()
        Text = "Edith Wharton's ""The Age of Innocence"""
        Font = new Font("Times New Roman", 10)
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim x, iChars, iLines As Integer
        Dim str As String = AgeOfInnocence.Text
        Dim strfmt As New StringFormat()

        ' Set units of points while converting dimensions.
        Dim aptf() As PointF = {New PointF(cx, cy)}
        grfx.TransformPoints(CoordinateSpace.Device, _
                             CoordinateSpace.Page, aptf)

        grfx.PageUnit = GraphicsUnit.Point
        grfx.TransformPoints(CoordinateSpace.Page, _
                             CoordinateSpace.Device, aptf)

        Dim fcx As Single = aptf(0).X
        Dim fcy As Single = aptf(0).Y

        ' StringFormat properties, flags, and tabs.
        strfmt.HotkeyPrefix = HotkeyPrefix.Show
        strfmt.Trimming = StringTrimming.Word
        strfmt.FormatFlags = strfmt.FormatFlags Or StringFormatFlags.NoClip
        strfmt.SetTabStops(0, New Single() {18})

        ' Display text.
        For x = 0 To CInt(fcx) Step 156
            If (str.Length <= 0) Then Exit For

            Dim rectf As New RectangleF(x, 0, 144, _
                                        fcy - Font.GetHeight(grfx))
            grfx.DrawString(str, Font, br, rectf, strfmt)
            grfx.MeasureString(str, Font, rectf.Size, strfmt, _
                               iChars, iLines)
            str = str.Substring(iChars)
        Next x
    End Sub
End Class
