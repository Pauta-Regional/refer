'----------------------------------------------
' RunFormBetter.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System.Windows.Forms

Module RunFormBetter
    Sub Main()
        Dim frm As New Form()
        frm.Text = "My Very Own Form"
        Application.Run(frm)
    End Sub
End Module
