'-----------------------------------------------
' InheritTheForm.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Module InhertTheForm
    Sub Main()
        Dim frm As New InheritFromForm()
        frm.Text = "Inherit the Form"
        frm.BackColor = Color.White

        Application.Run(frm)
    End Sub
End Module

Class InheritFromForm
    Inherits Form
End Class
