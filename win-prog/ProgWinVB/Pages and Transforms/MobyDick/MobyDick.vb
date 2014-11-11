'-----------------------------------------
' MobyDick.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class MobyDick
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New MobyDick())
    End Sub

    Sub New()
        Text = "Moby-Dick by Herman Melville"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)

        ' Insert RotateTransform, ScaleTransform, 
        '   TranslateTransform, and other calls here.
        grfx.DrawString("Call me Ishmael. Some years ago" & ChrW(&H2014) & _
                        "never mind how long precisely" & ChrW(&H2014) & _
                        "having little or no money in my purse, and " & _
                        "nothing particular to interest me on shore, I " & _
                        "thought I would sail about a little and see " & _
                        "the watery part of the world. It is a way I " & _
                        "have of driving off the spleen, and " & _
                        "regulating the circulation. Whenever I find " & _
                        "myself growing grim about the mouth; whenever " & _
                        "it is a damp, drizzly November in my soul; " & _
                        "whenever I find myself involuntarily pausing " & _
                        "before coffin warehouses, and bringing up the " & _
                        "rear of every funeral I meet and especially " & _
                        "whenever my hypos get such an upper hand of " & _
                        "me, that it requires a strong moral principle " & _
                        "to prevent me from deliberately stepping into " & _
                        "the street, and methodically knocking " & _
                        "people's hats off" & ChrW(&H2014) & "then, I " & _
                        "account it high time to get to sea as soon as " & _
                        "I can. This is my substitute for pistol " & _
                        "and ball. With a philosophical flourish Cato " & _
                        "throws himself upon his sword; I quietly take " & _
                        "to the ship. There is nothing surprising in " & _
                        "this. If they but knew it, almost all men in " & _
                        "their degree, some time or other, cherish " & _
                        "very nearly the same feelings towards the " & _
                        "ocean with me.", _
                        Font, New SolidBrush(clr), _
                        New RectangleF(0, 0, cx, cy))
    End Sub
End Class
