'----------------------------------------------------
' CheckerWithChildren.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class CheckerWithChildren
    Inherits Form

    Protected Const xNum As Integer = 5
    Protected Const yNum As Integer = 4
    Protected actrlChild(,) As CheckerChild

    Shared Sub Main()
        Application.Run(New CheckerWithChildren())
    End Sub

    Sub New()
        Text = "Checker with Children"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        CreateChildren()
        OnResize(EventArgs.Empty)
    End Sub

    Protected Overridable Sub CreateChildren()
        ReDim actrlChild(yNum - 1, xNum - 1)
        Dim x, y As Integer

        For y = 0 To yNum - 1
            For x = 0 To xNum - 1
                actrlChild(y, x) = New CheckerChild()
                actrlChild(y, x).Parent = Me
            Next x
        Next y
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        Dim cxBlock As Integer = ClientSize.Width \ xNum
        Dim cyBlock As Integer = ClientSize.Height \ yNum
        Dim x, y As Integer

        For y = 0 To yNum - 1
            For x = 0 To xNum - 1
                actrlChild(y, x).Location = _
                            New Point(x * cxBlock, y * cyBlock)
                actrlChild(y, x).Size = New Size(cxBlock, cyBlock)
            Next x
        Next y
    End Sub
End Class
