'----------------------------------------------------
' ModelessColorScroll.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ModelessColorScroll
    Inherits Form

    Shared Sub Main()
        Application.Run(New ModelessColorScroll())
    End Sub

    Sub New()
        Text = "Modeless Color Scroll"

        Dim dlg As New ColorScrollDialogBox()
        dlg.Owner = Me
        dlg.Color = BackColor
        AddHandler dlg.Changed, AddressOf ColorScrollOnChanged
        dlg.Show()
    End Sub

    Sub ColorScrollOnChanged(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As ColorScrollDialogBox = _
                            DirectCast(obj, ColorScrollDialogBox)
        BackColor = dlg.Color
    End Sub
End Class
