'-------------------------------------------------
' ShowFormAndSleep.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System.Threading
Imports System.Windows.Forms

Module ShowFormAndSleep
    Sub Main()
        Dim frm As New Form()
        frm.Show()
        Thread.Sleep(2500)
        frm.Text = "My First Form"
        Thread.Sleep(2500)
    End Sub
End Module
