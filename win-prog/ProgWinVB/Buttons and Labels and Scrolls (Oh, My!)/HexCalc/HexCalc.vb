'----------------------------------------
' HexCalc.vb (c) 2002 by Charles Petzold
'----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HexCalc
    Inherits Form

    Private btnResult As Button
    Private lNum As Long = 0
    Private lFirstNum As Long = 0
    Private bNewNumber As Boolean = True
    Private chOperation As Char = "="c

    Shared Sub Main()
        Application.Run(New HexCalc())
    End Sub

    Sub New()
        Text = "Hex Calc"
        Icon = New Icon(Me.GetType(), "HexCalc.ico")
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False

        Dim btn As Button
        btn = New CalcButton(Me, "D", "D"c, 8, 24, 14, 14)
        btn = New CalcButton(Me, "A", "A"c, 8, 40, 14, 14)
        btn = New CalcButton(Me, "7", "7"c, 8, 56, 14, 14)
        btn = New CalcButton(Me, "4", "4"c, 8, 72, 14, 14)
        btn = New CalcButton(Me, "1", "1"c, 8, 88, 14, 14)
        btn = New CalcButton(Me, "0", "0"c, 8, 104, 14, 14)
        btn = New CalcButton(Me, "E", "E"c, 26, 24, 14, 14)
        btn = New CalcButton(Me, "B", "B"c, 26, 40, 14, 14)
        btn = New CalcButton(Me, "8", "8"c, 26, 56, 14, 14)
        btn = New CalcButton(Me, "5", "5"c, 26, 72, 14, 14)
        btn = New CalcButton(Me, "2", "2"c, 26, 88, 14, 14)
        btn = New CalcButton(Me, "Back", Chr(8), 26, 104, 32, 14)
        btn = New CalcButton(Me, "C", "C"c, 44, 40, 14, 14)
        btn = New CalcButton(Me, "F", "F"c, 44, 24, 14, 14)
        btn = New CalcButton(Me, "9", "9"c, 44, 56, 14, 14)
        btn = New CalcButton(Me, "6", "6"c, 44, 72, 14, 14)
        btn = New CalcButton(Me, "3", "3"c, 44, 88, 14, 14)
        btn = New CalcButton(Me, "+", "+"c, 62, 24, 14, 14)
        btn = New CalcButton(Me, "-", "-"c, 62, 40, 14, 14)
        btn = New CalcButton(Me, "*", "*"c, 62, 56, 14, 14)
        btn = New CalcButton(Me, "/", "/"c, 62, 72, 14, 14)
        btn = New CalcButton(Me, "Equals", "="c, 62, 104, 46, 14)
        btn = New CalcButton(Me, "And", "&"c, 80, 24, 28, 14)
        btn = New CalcButton(Me, "Or", "|"c, 80, 40, 28, 14)
        btn = New CalcButton(Me, "Xor", "^"c, 80, 56, 28, 14)
        btn = New CalcButton(Me, "Mod", "%"c, 80, 72, 28, 14)
        btn = New CalcButton(Me, "Left", "<"c, 62, 88, 21, 14)
        btn = New CalcButton(Me, "Rt", ">"c, 87, 88, 21, 14)
        btnResult = New CalcButton(Me, "0", Chr(27), 8, 4, 100, 14)

        For Each btn In Controls
            AddHandler btn.Click, AddressOf ButtonOnClick
        Next btn

        ClientSize = New Size(116, 126)
        Scale(Font.Height / 8.0F)
    End Sub

    Protected Overrides Sub OnKeyPress(ByVal kpea As KeyPressEventArgs)
        Dim chKey As Char = Char.ToUpper(kpea.KeyChar)
        If chKey = vbCr Then chKey = "="c

        Dim ctrl As Control
        For Each ctrl In Controls
            Dim btn As CalcButton = DirectCast(ctrl, CalcButton)
            If chKey = btn.chKey Then
                InvokeOnClick(btn, EventArgs.Empty)
                Exit For
            End If
        Next
    End Sub

    Private Sub ButtonOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim btn As CalcButton = DirectCast(obj, CalcButton)

        If btn.chKey = vbBack Then
            lNum \= 16

        ElseIf btn.chKey = Chr(27) Then
            lNum = 0

        ElseIf Char.IsLetterOrDigit(btn.chKey) Then
            If bNewNumber Then
                lFirstNum = lNum
                lNum = 0
                bNewNumber = False
            End If
            If lNum <= Long.MaxValue \ 16 Then
                If Char.IsDigit(btn.chKey) Then
                    lNum = 16 * lNum + AscW(btn.chKey) - AscW("0")
                Else
                    lNum = 16 * lNum + AscW(btn.chKey) + 10 - AscW("A")
                End If
            End If

        Else
            If Not bNewNumber Then
                Select Case chOperation
                    Case "="c
                        lNum = lNum
                    Case "+"c
                        lNum = lFirstNum + lNum
                    Case "-"c
                        lNum = lFirstNum - lNum
                    Case "*"c
                        lNum = lFirstNum * lNum
                    Case "&"c
                        lNum = lFirstNum And lNum
                    Case "|"c
                        lNum = lFirstNum Or lNum
                    Case "^"c
                        lNum = lFirstNum Xor lNum
                    Case "<"c
                        lNum = lFirstNum * CLng(2 ^ lNum)
                    Case ">"c
                        lNum = lFirstNum \ CLng(2 ^ lNum)
                    Case "/"c
                        If lNum <> 0 Then
                            lNum = lFirstNum \ lNum
                        Else
                            lNum = Long.MaxValue
                        End If

                    Case "%"c
                        If lNum <> 0 Then
                            lNum = lFirstNum Mod lNum
                        Else
                            lNum = Long.MaxValue
                        End If
                    Case Else
                        lNum = 0
                End Select
            End If
            bNewNumber = True
            chOperation = btn.chKey
        End If
        btnResult.Text = String.Format("{0:X}", lNum)
    End Sub
End Class

Class CalcButton
    Inherits Button

    Public chKey As Char

    Sub New(ByVal ctrlParent As Control, _
           ByVal str As String, ByVal chkey As Char, _
           ByVal x As Integer, ByVal y As Integer, _
           ByVal cx As Integer, ByVal cy As Integer)
        Parent = ctrlParent
        Text = str
        Me.chKey = chkey
        Location = New Point(x, y)
        Size = New Size(cx, cy)
        SetStyle(ControlStyles.Selectable, False)
    End Sub
End Class
