' ----------------------------------------------------
' MouseConnectWaitCursor.vb (c) 2002 by Charles Petzold
' ----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MouseConnectWaitCursor
    Inherits MouseConnect

    Shared Shadows Sub Main()
        Application.Run(New MouseConnectWaitCursor())
    End Sub

    Sub New()
        Text = "Mouse Connect with Wait Cursor"
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Cursor.Current = Cursors.WaitCursor
        Cursor.Show()
        MyBase.OnPaint(pea)
        Cursor.Hide()
        Cursor.Current = Cursors.Arrow
    End Sub
End Class
