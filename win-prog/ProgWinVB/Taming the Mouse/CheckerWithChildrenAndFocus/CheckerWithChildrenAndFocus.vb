'------------------------------------------------------------
' CheckerWithChildrenAndFocus.vb (c) 2002 by Charles Petzold
'------------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CheckerWithChildrenAndFocus
    Inherits CheckerWithChildren

    Shared Shadows Sub Main()
        Application.Run(new CheckerWithChildrenAndFocus())
    End Sub

    Sub New()
        Text = "Checker with Children and Focus"
    End Sub

    Protected Overrides Sub CreateChildren()
        ReDim actrlChild(yNum, xNum)
        Dim x, y As Integer

        For y = 0 To yNum - 1
            For x = 0 To xNum - 1
                actrlChild(y, x) = New CheckerChildWithFocus()
                actrlChild(y, x).Parent = Me
            Next x
        Next y
    End Sub
End Class
