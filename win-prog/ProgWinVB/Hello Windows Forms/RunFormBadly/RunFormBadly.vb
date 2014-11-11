'---------------------------------------------
' RunFormBadly.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System.Windows.Forms

Module RunFormBadly
    Sub Main()
        Dim frm As New Form()
        frm.Text = "Not a Good Idea..."
        frm.Visible = True
        Application.Run()
    End Sub
End Module
