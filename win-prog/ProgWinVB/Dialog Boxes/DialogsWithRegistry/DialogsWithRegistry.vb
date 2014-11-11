'----------------------------------------------------
' DialogsWithRegistry.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports Microsoft.Win32
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class DialogsWithRegistry
    Inherits BetterFontAndColorDialogs

    Const strRegKey As String = _
            "Software\ProgrammingWindowsWithVBdotNet\DialogsWithRegistry"
    Const strFontFace As String = "FontFace"
    Const strFontSize As String = "FontSize"
    Const strFontStyle As String = "FontStyle"
    Const strForeColor As String = "ForeColor"
    Const strBackColor As String = "BackColor"
    Const strCustomClr As String = "CustomColor"

    Shared Shadows Sub Main()
        Application.Run(New DialogsWithRegistry())
    End Sub

    Sub New()
        Text = "Font and Color Dialogs with Registry"

        Dim regkey As RegistryKey = _
                            Registry.CurrentUser.OpenSubKey(strRegKey)

        If Not regkey Is Nothing Then
            Font = New Font( _
                    DirectCast(regkey.GetValue(strFontFace), String), _
                    Single.Parse( _
                        DirectCast(regkey.GetValue(strFontSize), String)), _
                    CType(regkey.GetValue(strFontStyle), FontStyle))
            ForeColor = Color.FromArgb(CInt(regkey.GetValue(strForeColor)))
            BackColor = Color.FromArgb(CInt(regkey.GetValue(strBackColor)))

            Dim i, aiColors(16) As Integer

            For i = 0 To 15
                aiColors(i) = CInt(regkey.GetValue(strCustomClr & i))
            Next i
            clrdlg.CustomColors = aiColors
            regkey.Close()
        End If
    End Sub

    Protected Overrides Sub OnClosed(ByVal ea As EventArgs)
        Dim i As Integer
        Dim regkey As RegistryKey = _
                    Registry.CurrentUser.OpenSubKey(strRegKey, True)

        If regkey Is Nothing Then
            regkey = Registry.CurrentUser.CreateSubKey(strRegKey)
        End If

        regkey.SetValue(strFontFace, Font.Name)
        regkey.SetValue(strFontSize, Font.SizeInPoints.ToString())
        regkey.SetValue(strFontStyle, CInt(Font.Style))
        regkey.SetValue(strForeColor, ForeColor.ToArgb())
        regkey.SetValue(strBackColor, BackColor.ToArgb())

        For i = 0 To 15
            regkey.SetValue(strCustomClr & i, clrdlg.CustomColors(i))
        Next i
        regkey.Close()
    End Sub
End Class
