'------------------------------------------
' MultiCopy.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Class MultiCopy
    Inherits Form

    Const strFormat As String = "MultiCopy.InternalFormat"
    Private afValues(,) As Single = {{0.12F, 3.45F, 6.78F, 9.01F}, _
                                     {2.34F, 5.67F, 8.9F, 1.23F}, _
                                     {4.56F, 7.89F, 0.12F, 3.45F}}
    Shared Sub Main()
        Application.Run(New MultiCopy())
    End Sub

    Sub New()
        Text = "Multi Copy"
        Menu = New MainMenu()

        ' Edit menu
        Dim mi As New MenuItem("&Edit")
        Menu.MenuItems.Add(mi)

        ' Edit Copy menu item
        mi = New MenuItem("&Copy")
        AddHandler mi.Click, AddressOf MenuEditCopyOnClick
        mi.Shortcut = Shortcut.CtrlC
        Menu.MenuItems(0).MenuItems.Add(mi)
    End Sub

    Private Sub MenuEditCopyOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Dim data As New DataObject()

        ' Define internal clipboard format.
        Dim ms As New MemoryStream()
        Dim bw As New BinaryWriter(ms)
        Dim iRow, iCol As Integer

        bw.Write(afValues.GetLength(0))
        bw.Write(afValues.GetLength(1))

        For iRow = 0 To afValues.GetUpperBound(0)
            For iCol = 0 To afValues.GetUpperBound(1)
                bw.Write(afValues(iRow, iCol))
            Next iCol
        Next iRow

        bw.Close()
        data.SetData(strFormat, ms)

        ' Define CSV clipboard format.
        ms = New MemoryStream()
        Dim sw As New StreamWriter(ms)

        For iRow = 0 To afValues.GetUpperBound(0)
            For iCol = 0 To afValues.GetUpperBound(1)
                sw.Write(afValues(iRow, iCol))

                If iCol < afValues.GetUpperBound(1) Then
                    sw.Write(",")
                Else
                    sw.WriteLine()
                End If
            Next iCol
        Next iRow

        sw.Write("\0")
        sw.Close()
        data.SetData(DataFormats.CommaSeparatedValue, ms)

        ' Define String clipboard format.
        Dim strw As New StringWriter()

        For iRow = 0 To afValues.GetUpperBound(0)
            For iCol = 0 To afValues.GetUpperBound(1)
                strw.Write(afValues(iRow, iCol))
                If iCol < afValues.GetLength(1) - 1 Then
                    strw.Write(vbTab)
                Else
                    strw.WriteLine()
                End If
            Next iCol
        Next iRow
        strw.Close()
        data.SetData(strw.ToString())

        Clipboard.SetDataObject(data, False)
    End Sub
End Class
