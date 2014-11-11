'-----------------------------------------------------
' KeyExamineWithScroll.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Class KeyExamineWithScroll
    Inherits KeyExamine

    Shared Shadows Sub Main()
        Application.Run(new KeyExamineWithScroll())
    End Sub

    Sub New()
        Text &= " With Scroll"
    End Sub

    ' Define a Win32-like rectangle structure.
    <StructLayout(LayoutKind.Sequential)> _
    Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure

    ' Declare the ScrollWindow call.
    Declare Function ScrollWindow Lib "user32.dll" (ByVal hwnd As IntPtr, _
                    ByVal cx As Integer, ByVal cy As Integer, _
                    ByRef rectScroll As RECT, _
                    ByRef rectClip As RECT) As Integer

    ' Override the method in KeyExamine.
    Protected Overrides Sub ScrollLines()
        Dim rect As RECT
        rect.left = 0
        rect.top = Font.Height
        rect.right = ClientSize.Width
        rect.bottom = ClientSize.Height

        ScrollWindow(Handle, 0, -Font.Height, rect, rect)
    End Sub
End Class
