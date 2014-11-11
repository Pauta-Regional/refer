'------------------------------------------------
' ProgramWithIcon.vb  2001 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class ProgramWithIcon
    Inherits Form

    Shared Sub Main()
        Application.Run(new ProgramWithIcon())
    End Sub

    Sub New()
        Text = "Program with Icon"
        Icon = New Icon(GetType(ProgramWithIcon), "ProgramWithIcon.ico")
    End Sub
End Class
