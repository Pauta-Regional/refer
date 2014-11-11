'-------------------------------------------
' HowdyWorld.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HowdyWorld
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New HowdyWorld())
    End Sub

    Sub New()
        Text = "Howdy, world!"
        MinimumSize = _
                Size.op_Addition(SystemInformation.MinimumWindowSize, _
                                 New Size(0, 1))
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim fnt As New Font("Times New Roman", 10, FontStyle.Italic)
        Dim szf As SizeF = grfx.MeasureString(Text, fnt)
        Dim fScale As Single = Math.Min(cx / szf.Width, cy / szf.Height)

        fnt = New Font(fnt.Name, fScale * fnt.SizeInPoints, fnt.Style)
        szf = grfx.MeasureString(Text, fnt)
        grfx.DrawString(Text, fnt, New SolidBrush(clr), _
                        (cx - szf.Width) / 2, (cy - szf.Height) / 2)
    End Sub
End Class
