'-----------------------------------------------
' FormProperties.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System.Drawing
Imports System.Windows.Forms

Module FormProperties
    Sub Main()
        Dim frm As New Form()

        frm.Text = "Form Properties"
        frm.BackColor = Color.BlanchedAlmond
        frm.Width *= 2
        frm.Height \= 2
        frm.FormBorderStyle = FormBorderStyle.FixedSingle
        frm.MaximizeBox = False
        frm.Cursor = Cursors.Hand
        frm.StartPosition = FormStartPosition.CenterScreen

        Application.Run(frm)
    End Sub
End Module
