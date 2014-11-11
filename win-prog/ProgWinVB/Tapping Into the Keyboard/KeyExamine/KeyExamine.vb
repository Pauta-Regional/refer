'-------------------------------------------
' KeyExamine.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class KeyExamine
    Inherits Form

    ' Enum and struct definitions for storage of key events
    Private Enum EventType
        None
        KeyDown
        KeyUp
        KeyPress
    End Enum

    Private Structure KeyEvent
        Public evttype As EventType
        Public evtargs As EventArgs
    End Structure

    ' Storage of key events
    Const iNumLines As Integer = 25
    Private iNumValid As Integer = 0
    Private iInsertIndex As Integer = 0
    Private akeyevt(iNumLines) As KeyEvent

    ' Text positioning
    Private xEvent, xChar, xCode, xMods, xData As Integer
    Private xShift, xCtrl, xAlt, xRight As Integer

    Shared Sub Main()
        Application.Run(New KeyExamine())
    End Sub

    Sub New()
        Text = "Key Examine"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        xEvent = 0
        xChar = xEvent + 5 * Font.Height
        xCode = xChar + 5 * Font.Height
        xMods = xCode + 8 * Font.Height
        xData = xMods + 8 * Font.Height
        xShift = xData + 8 * Font.Height
        xCtrl = xShift + 5 * Font.Height
        xAlt = xCtrl + 5 * Font.Height
        xRight = xAlt + 5 * Font.Height

        ClientSize = New Size(xRight, Font.Height * (iNumLines + 1))
        FormBorderStyle = FormBorderStyle.Fixed3D
        MaximizeBox = False
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal kea As KeyEventArgs)
        akeyevt(iInsertIndex).evttype = EventType.KeyDown
        akeyevt(iInsertIndex).evtargs = kea
        OnKey()
    End Sub

    Protected Overrides Sub OnKeyUp(ByVal kea As KeyEventArgs)
        akeyevt(iInsertIndex).evttype = EventType.KeyUp
        akeyevt(iInsertIndex).evtargs = kea
        OnKey()
    End Sub

    Protected Overrides Sub OnKeyPress(ByVal kpea As KeyPressEventArgs)
        akeyevt(iInsertIndex).evttype = EventType.KeyPress
        akeyevt(iInsertIndex).evtargs = kpea
        OnKey()
    End Sub

    Private Sub OnKey()
        If iNumValid < iNumLines Then
            Dim grfx As Graphics = CreateGraphics()
            DisplayKeyInfo(grfx, iInsertIndex, iInsertIndex)
            grfx.Dispose()
        Else
            ScrollLines()
        End If
        iInsertIndex = (iInsertIndex + 1) Mod iNumLines
        iNumValid = Math.Min(iNumValid + 1, iNumLines)
    End Sub

    Protected Overridable Sub ScrollLines()
        Dim rect As New Rectangle(0, Font.Height, _
                                  ClientSize.Width, _
                                  ClientSize.Height - Font.Height)
        ' I wish I could scroll here!
        Invalidate(rect)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim i As Integer

        BoldUnderline(grfx, "Event", xEvent, 0)
        BoldUnderline(grfx, "KeyChar", xChar, 0)
        BoldUnderline(grfx, "KeyCode", xCode, 0)
        BoldUnderline(grfx, "Modifiers", xMods, 0)
        BoldUnderline(grfx, "KeyData", xData, 0)
        BoldUnderline(grfx, "Shift", xShift, 0)
        BoldUnderline(grfx, "Control", xCtrl, 0)
        BoldUnderline(grfx, "Alt", xAlt, 0)

        If iNumValid < iNumLines Then
            For i = 0 To iNumValid - 1
                DisplayKeyInfo(grfx, i, i)
            Next i
        Else
            For i = 0 To iNumLines - 1
                DisplayKeyInfo(grfx, i, (iInsertIndex + i) Mod iNumLines)
            Next i
        End If
    End Sub

    Private Sub BoldUnderline(ByVal grfx As Graphics, ByVal str As String, _
                              ByVal x As Integer, ByVal y As Integer)
        ' Draw the text bold.
        Dim br As New SolidBrush(ForeColor)
        grfx.DrawString(str, Font, br, x, y)
        grfx.DrawString(str, Font, br, x + 1, y)
        ' Underline the text.
        Dim szf As SizeF = grfx.MeasureString(str, Font)
        grfx.DrawLine(New Pen(ForeColor), x, y + szf.Height, _
                                          x + szf.Width, y + szf.Height)
    End Sub

    Private Sub DisplayKeyInfo(ByVal grfx As Graphics, _
                               ByVal y As Integer, ByVal i As Integer)
        Dim br As New SolidBrush(ForeColor)
        y = (1 + y) * Font.Height          ' Convert y to pixel coordinate

        grfx.DrawString(akeyevt(i).evttype.ToString(), Font, br, xEvent, y)

        If akeyevt(i).evttype = EventType.KeyPress Then
            Dim kpea As KeyPressEventArgs = _
                        DirectCast(akeyevt(i).evtargs, KeyPressEventArgs)
            Dim str As String = _
                        String.Format(ChrW(&H202D) & "{0} (&H{1:X4})", _
                                      kpea.KeyChar, AscW(kpea.KeyChar))

            grfx.DrawString(str, Font, br, xChar, y)
        Else
            Dim kea As KeyEventArgs = _
                        DirectCast(akeyevt(i).evtargs, KeyEventArgs)
            Dim str As String = _
                        String.Format("{0} ({1})", _
                                      kea.KeyCode, CInt(kea.KeyCode))
            grfx.DrawString(Str, Font, br, xCode, y)
            grfx.DrawString(kea.Modifiers.ToString(), Font, br, xMods, y)
            grfx.DrawString(kea.KeyData.ToString(), Font, br, xData, y)
            grfx.DrawString(kea.Shift.ToString(), Font, br, xShift, y)
            grfx.DrawString(kea.Control.ToString(), Font, br, xCtrl, y)
            grfx.DrawString(kea.Alt.ToString(), Font, br, xAlt, y)
        End If
    End Sub
End Class
