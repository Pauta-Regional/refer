'--------------------------------------------------
' MyDocumentsFolder.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Windows.Forms

Module MyDocumentsFolder
    Sub Main()
        MessageBox.Show( _
            Environment.GetFolderPath(Environment.SpecialFolder.Personal), _
            "My Documents Folder")
    End Sub
End Module
