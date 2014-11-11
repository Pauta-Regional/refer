'-------------------------------------------------
' OldFashionedMenu.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class OldFashionedMenu
    Inherits Form

    Private mmMain, mmFile, mmEdit As MainMenu

    Shared Sub Main()
        Application.Run(New OldFashionedMenu())
    End Sub

    Sub New()
        Text = "Old-Fashioned Menu"

        Dim eh As EventHandler = AddressOf MenuOnClick

        mmMain = New MainMenu(New MenuItem() _
        { _
            New MenuItem("MAIN:"), _
            New MenuItem("&File", AddressOf MenuFileOnClick), _
            New MenuItem("&Edit", AddressOf MenuEditOnClick) _
        })
        mmFile = New MainMenu(New MenuItem() _
        { _
            New MenuItem("FILE:"), _
            New MenuItem("&New", eh), _
            New MenuItem("&Open...", eh), _
            New MenuItem("&Save", eh), _
            New MenuItem("Save &As...", eh), _
            New MenuItem("(&Main)", AddressOf MenuMainOnClick) _
        })
        mmEdit = New MainMenu(New MenuItem() _
        { _
            New MenuItem("EDIT:"), _
            New MenuItem("Cu&t", eh), _
            New MenuItem("&Copy", eh), _
            New MenuItem("&Paste", eh), _
            New MenuItem("De&lete", eh), _
            New MenuItem("(&Main)", AddressOf MenuMainOnClick) _
        })

        Menu = mmMain
    End Sub

    Private Sub MenuMainOnClick(ByVal obj As Object, _
                                ByVal ea As EventArgs)
        Menu = mmMain
    End Sub

    Private Sub MenuFileOnClick(ByVal obj As Object, _
                                ByVal ea As EventArgs)
        Menu = mmFile
    End Sub

    Private Sub MenuEditOnClick(ByVal obj As Object, _
                                ByVal ea As EventArgs)
        Menu = mmEdit
    End Sub

    Private Sub MenuOnClick(ByVal obj As Object, _
                            ByVal ea As EventArgs)
        MessageBox.Show("Menu item clicked!", Text)
    End Sub
End Class
