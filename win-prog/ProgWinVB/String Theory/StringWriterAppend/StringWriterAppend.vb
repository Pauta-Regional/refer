'---------------------------------------------------
' StringWriterAppend.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System
Imports System.IO

Module StringWriterAppend
    Const iIterations As Integer = 10000

    Sub Main()
        Dim dt As DateTime = DateTime.Now
        Dim sw As New StringWriter()
        Dim i As Integer

        For i = 1 To iIterations
            sw.WriteLine("abcdefghijklmnopqrstuvwxyz" & vbCrLf)
        Next i
        Dim str As String = sw.ToString()

        Console.WriteLine(DateTime.op_Subtraction(DateTime.Now, dt))
    End Sub
End Module
