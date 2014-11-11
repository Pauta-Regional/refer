'------------------------------------------------
' GetFamiliesList.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class GetFamiliesList
    Inherits FamiliesList

    Shared Shadows Sub Main()
        Application.Run(New GetFamiliesList())
    End Sub

    Sub New()
        Text = "Font GetFamilies List"
    End Sub

    Protected Overrides Function GetFontFamilyArray _
                                (ByVal grfx As Graphics) As FontFamily()
        Return FontFamily.GetFamilies(grfx)
    End Function
End Class
