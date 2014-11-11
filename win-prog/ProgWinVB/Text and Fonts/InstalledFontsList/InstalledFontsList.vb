'---------------------------------------------------
' InstalledFontsList.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Windows.Forms

Class InstalledFontsList
    Inherits FamiliesList

    Shared Shadows Sub Main()
        Application.Run(New InstalledFontsList())
    End Sub

    Sub New()
        Text = "InstalledFontCollection List"
    End Sub

    Protected Overrides Function GetFontFamilyArray _
                                (ByVal grfx As Graphics) As FontFamily()
        Dim fc As New InstalledFontCollection()
        Return fc.Families
    End Function
End Class
