'---------------------------------------------
' AllAboutFont.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class AllAboutFont
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New AllAboutFont())
    End Sub

    Sub New()
        Text = "All About Font"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawString( _
                "Name: " & Font.Name & vbLf & _
                "FontFamily: " & Font.FontFamily.ToString() & vbLf & _
                "FontStyle: " & Font.Style.ToString() & vbLf & _
                "Bold: " & Font.Bold & vbLf & _
                "Italic: " & Font.Italic & vbLf & _
                "Underline: " & Font.Underline & vbLf & _
                "Strikeout: " & Font.Strikeout & vbLf & _
                "Size: " & Font.Size & vbLf & _
                "GraphicsUnit: " & Font.Unit.ToString() & vbLf & _
                "SizeInPoints: " & Font.SizeInPoints & vbLf & _
                "Height: " & Font.Height & vbLf & _
                "GdiCharSet: " & Font.GdiCharSet & vbLf & _
                "GdiVerticalFont: " & Font.GdiVerticalFont & vbLf & _
                "GetHeight(): " & Font.GetHeight() & vbLf & _
                "GetHeight(grfx): " & Font.GetHeight(grfx) & vbLf & _
                "GetHeight(100 DPI): " & Font.GetHeight(100), _
                Font, New SolidBrush(clr), PointF.Empty)
    End Sub
End Class
