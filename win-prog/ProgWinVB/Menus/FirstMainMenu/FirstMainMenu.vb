'----------------------------------------------
' FirstMainMenu.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FirstMainMenu
    Inherits Form

    Shared Sub Main()
        Application.Run(new FirstMainMenu())
    End Sub

    Sub New()
        Text = "First Main Menu"

        ' Items on File submenu
        Dim miOpen As New MenuItem("&Open...", _
                            AddressOf MenuFileOpenOnClick, _
                            Shortcut.CtrlO)

        Dim miSave As New MenuItem("&Save", _
                            AddressOf MenuFileSaveOnClick, _
                            Shortcut.CtrlS)

        Dim miSaveAs As New MenuItem("Save &As...", _
                            AddressOf MenuFileSaveAsOnClick)

        Dim miDash As New MenuItem("-")
        Dim miExit As New MenuItem("E&xit", _
                            AddressOf MenuFileExitOnClick)

        ' File menu item
        Dim miFile As New MenuItem("&File", _
                            New MenuItem() {miOpen, miSave, miSaveAs, _
                                            miDash, miExit})
        ' Items on Edit submenu
        Dim miCut As New MenuItem("Cu&t", _
                            AddressOf MenuEditCutOnClick, _
                            Shortcut.CtrlX)

        Dim miCopy As New MenuItem("&Copy", _
                            AddressOf MenuEditCopyOnClick, _
                            Shortcut.CtrlC)

        Dim miPaste As New MenuItem("&Paste", _
                            AddressOf MenuEditPasteOnClick, _
                            Shortcut.CtrlV)

        ' Edit menu item
        Dim miEdit As New MenuItem("&Edit", _
                            New MenuItem() {miCut, miCopy, miPaste})

        ' Item on Help submenu
        Dim miAbout As New MenuItem("&About FirstMainMenu...", _
                            AddressOf MenuHelpAboutOnClick)

        ' Help menu item
        Dim miHelp As New MenuItem("&Help", New MenuItem() {miAbout})

        ' Main menu
        Menu = new MainMenu(new MenuItem() {miFile, miEdit, miHelp})
    End Sub

    Private Sub MenuFileOpenOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        MessageBox.Show("File Open item clicked!", Text)
    End Sub

    Private Sub MenuFileSaveOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        MessageBox.Show("File Save item clicked!", Text)
    End Sub

    Private Sub MenuFileSaveAsOnClick(ByVal obj As Object, _
                                      ByVal ea As EventArgs)
        MessageBox.Show("File Save As item clicked!", Text)
    End Sub

    Private Sub MenuFileExitOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Close()
    End Sub

    Private Sub MenuEditCutOnClick(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        MessageBox.Show("Edit Cut item clicked!", Text)
    End Sub

    Private Sub MenuEditCopyOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        MessageBox.Show("Edit Copy item clicked!", Text)
    End Sub

    Private Sub MenuEditPasteOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        MessageBox.Show("Edit Paste item clicked! ", Text)
    End Sub

    Private Sub MenuHelpAboutOnClick(ByVal obj As Object, _
                                     ByVal ea As EventArgs)
        MessageBox.Show(Text & " " & Chr(169) & " 2002 by Charles Petzold")
    End Sub
End Class
