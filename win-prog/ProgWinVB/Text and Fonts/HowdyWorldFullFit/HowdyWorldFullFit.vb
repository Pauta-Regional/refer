'--------------------------------------------------
' HowdyWorldFullFit.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HowdyWorldFullFit
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New HowdyWorldFullFit())
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
        Dim fScaleHorz As Single = cx / szf.Width
        Dim fScaleVert As Single = cy / szf.Height

        grfx.ScaleTransform(fScaleHorz, fScaleVert)
        grfx.DrawString(Text, fnt, New SolidBrush(clr), 0, 0)
    End Sub
End Class
