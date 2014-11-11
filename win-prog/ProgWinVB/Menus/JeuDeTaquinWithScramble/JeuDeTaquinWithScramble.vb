'--------------------------------------------------------
' JeuDeTaquinWithScramble.vb (c) 2002 by Charles Petzold
'--------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class JeuDeTaquinWithScramble
    Inherits JeuDeTaquin

    Shared Shadows Sub Main()
        Application.Run(New JeuDeTaquinWithScramble())
    End Sub

    Sub New()
        Menu = New MainMenu(New MenuItem() { _
                    New MenuItem("&Scramble!", _
                            AddressOf MenuScrambleOnClick)})
    End Sub

    Private Sub MenuScrambleOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Randomize()
    End Sub
End Class
