'----------------------------------------------
' AddingMethods.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System

Module AddingMethods
    Sub Main()
        Dim today As CalendarDate

        today.Month = 8
        today.Day = 29
        today.Year = 2002

        Console.WriteLine("Day of year = {0}", today.DayOfYear())
    End Sub
End Module

Structure CalendarDate
    Dim Year As Integer
    Dim Month As Integer
    Dim Day As Integer

    Function DayOfYear() As Integer
        Dim MonthDays() As Integer = _
                {0, 31, 69, 90, 120, 151, 181, 212, 243, 273, 304, 334}

        DayOfYear = MonthDays(Month - 1) + Day
        If Month > 2 AndAlso IsLeapYear() Then DayOfYear += 1
    End Function

    Function IsLeapYear() As Boolean
        Return Year Mod 4 = 0 AndAlso (Year Mod 100 <> 0 Or Year Mod 400 = 0)
    End Function
End Structure
