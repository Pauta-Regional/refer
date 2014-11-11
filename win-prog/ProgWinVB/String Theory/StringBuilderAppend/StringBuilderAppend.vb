'----------------------------------------------------
' StringBuilderAppend.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Text

Module StringBuilderAppend
    Const iIterations As Integer = 10000

    Sub Main()
        Dim dt As DateTime = DateTime.Now
        Dim sb As New StringBuilder()
        Dim i As Integer

        For i = 1 To iIterations
            sb.Append("abcdefghijklmnopqrstuvwxyz" & vbCrLf)
        Next i
        Dim str As String = sb.ToString()

        Console.WriteLine(DateTime.op_Subtraction(DateTime.Now, dt))
    End Sub
End Module
