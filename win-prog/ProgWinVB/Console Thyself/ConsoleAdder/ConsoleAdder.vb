'---------------------------------------------
' ConsoleAdder.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System

Module ConsoleAdder
    Sub Main()
        Dim A As Integer = 1509
        Dim B As Integer = 744
        Dim C As Integer = A + B

        Console.Write("The sum of ")
        Console.Write(A)
        Console.Write(" and ")
        Console.Write(B)
        Console.Write(" equals ")
        Console.WriteLine(C)

        Console.WriteLine("The sum of " & A & " and " & B & " equals " & C)
        Console.WriteLine("The sum of {0} and {1} equals {2}", A, B, C)
    End Sub
End Module
