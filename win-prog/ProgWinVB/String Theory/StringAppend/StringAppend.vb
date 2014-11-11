'---------------------------------------------
' StringAppend.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System

Module StringAppend
    Const iIterations As Integer = 10000

    Sub Main()
        Dim dt As DateTime = DateTime.Now
        Dim str As String = String.Empty
        Dim i As Integer

        For i = 1 To iIterations
            str &= "abcdefghijklmnopqrstuvwxyz" & vbCrLf
        Next i

        Console.WriteLine(DateTime.op_Subtraction(DateTime.Now, dt))
    End Sub
End Module
