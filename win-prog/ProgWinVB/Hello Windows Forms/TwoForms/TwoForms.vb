'-----------------------------------------
' TwoForms.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System.Windows.Forms

Module TwoForms
    Sub Main()
        Dim frm1 As New Form()
        Dim frm2 As New Form()

        frm1.Text = "Form passed to Run()"
        frm2.Text = "Second frm"
        frm2.Show()

        Application.Run(frm1)

        MessageBox.Show("Application.Run() has returned " & _
                        "control back to Main. Bye, bye!", _
                        "TwoForms")
    End Sub
End Module
