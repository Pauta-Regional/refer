'---------------------------------------------------------
' NotepadCloneWithRegistry.vb (c) 2002 by Charles Petzold
'---------------------------------------------------------
Imports Microsoft.Win32
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class NotepadCloneWithRegistry
    Inherits NotepadCloneNoMenu

    Protected strProgName As String
    Private rectNormal As Rectangle
    Private strRegKey As String = "Software\ProgrammingWindowsWithVBdotNet\"
    Const strWinState As String = "WindowState"
    Const strLocationX As String = "LocationX"
    Const strLocationY As String = "LocationY"
    Const strWidth As String = "Width"
    Const strHeight As String = "Height"

    Shared Shadows Sub Main()
        Application.Run(New NotepadCloneWithRegistry())
    End Sub

    Sub New()
        strProgName = "Notepad Clone with Registry"
        Text = strProgName
        rectNormal = DesktopBounds
    End Sub

    Protected Overrides Sub OnMove(ByVal ea As EventArgs)
        MyBase.OnMove(ea)
        If WindowState = FormWindowState.Normal Then
            rectNormal = DesktopBounds
        End If
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)
        If WindowState = FormWindowState.Normal Then
            rectNormal = DesktopBounds
        End If
    End Sub

    Protected Overrides Sub OnLoad(ByVal ea As EventArgs)
        MyBase.OnLoad(ea)

        ' Construct complete registry key.
        strRegKey = strRegKey & strProgName

        ' Load registry information.
        Dim regkey As RegistryKey = _
                        Registry.CurrentUser.OpenSubKey(strRegKey)
        If Not regkey Is Nothing Then
            LoadRegistryInfo(regkey)
            regkey.Close()
        End If
    End Sub

    Protected Overrides Sub OnClosed(ByVal ea As EventArgs)
        MyBase.OnClosed(ea)

        ' Save registry information.
        Dim regkey As RegistryKey = _
                        Registry.CurrentUser.OpenSubKey(strRegKey, True)
        If regkey Is Nothing Then
            regkey = Registry.CurrentUser.CreateSubKey(strRegKey)
        End If
        SaveRegistryInfo(regkey)
        regkey.Close()
    End Sub

    Protected Overridable Sub SaveRegistryInfo(ByVal regkey As RegistryKey)
        regkey.SetValue(strWinState, CInt(WindowState))
        regkey.SetValue(strLocationX, rectNormal.X)
        regkey.SetValue(strLocationY, rectNormal.Y)
        regkey.SetValue(strWidth, rectNormal.Width)
        regkey.SetValue(strHeight, rectNormal.Height)
    End Sub

    Protected Overridable Sub LoadRegistryInfo(ByVal regkey As RegistryKey)
        Dim x As Integer = DirectCast(regkey.GetValue(strLocationX, 100), _
                                      Integer)
        Dim y As Integer = DirectCast(regkey.GetValue(strLocationY, 100), _
                                      Integer)
        Dim cx As Integer = DirectCast(regkey.GetValue(strWidth, 300), _
                                       Integer)
        Dim cy As Integer = DirectCast(regkey.GetValue(strHeight, 300), _
                                       Integer)
        rectNormal = New Rectangle(x, y, cx, cy)

        ' Adjust rectangle for any change in desktop size.
        Dim rectDesk As Rectangle = SystemInformation.WorkingArea
        rectNormal.Width = Math.Min(rectNormal.Width, rectDesk.Width)
        rectNormal.Height = Math.Min(rectNormal.Height, rectDesk.Height)
        rectNormal.X -= Math.Max(rectNormal.Right - rectDesk.Right, 0)
        rectNormal.Y -= Math.Max(rectNormal.Bottom - rectDesk.Bottom, 0)

        ' Set form properties.
        DesktopBounds = rectNormal
        WindowState = CType(DirectCast(regkey.GetValue(strWinState, 0), _
                                       Integer), _
                            FormWindowState)
    End Sub
End Class
