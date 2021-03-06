'-----------------------------------------------
' SharingMethods.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System

Module DefiningTheClass
    Sub Main()
        Console.WriteLine("Is 2002 a leap year? {0}", _
                          CalendarDate.IsLeapYear(2002))

        Dim today As New CalendarDate()

        today.Month = 8
        today.Day = 29
        today.Year = 2002

        Console.WriteLine("Day of year = {0}", today.DayOfYear())
    End Sub
End Module

Class CalendarDate
    Public Year As Integer
    Public Month As Integer
    Public Day As Integer

    Function DayOfYear() As Integer
        Dim MonthDays() As Integer = _
                {0, 31, 69, 90, 120, 151, 181, 212, 243, 273, 304, 334}

        DayOfYear = MonthDays(Month - 1) + Day
        If Month > 2 AndAlso IsLeapYear(Year) Then DayOfYear += 1
    End Function

    Shared Function IsLeapYear(ByVal yr As Integer) As Boolean
        Return yr Mod 4 = 0 AndAlso (yr Mod 100 <> 0 Or yr Mod 400 = 0)
    End Function
End Class
