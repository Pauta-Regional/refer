'--------------------------------------------------
' ConsolidatingData.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System

Module ConsolidatingData
    Sub Main()
        Dim today As CalendarDate

        today.Month = 8
        today.Day = 29
        today.Year = 2002

        Console.WriteLine("Day of year = {0}", DayOfYear(today))
    End Sub

    Function DayOfYear(ByVal cd As CalendarDate) As Integer
        Dim MonthDays() As Integer = _
                {0, 31, 69, 90, 120, 151, 181, 212, 243, 273, 304, 334}

        DayOfYear = MonthDays(cd.Month - 1) + cd.Day
        If cd.Month > 2 AndAlso IsLeapYear(cd.Year) Then DayOfYear += 1
    End Function

    Function IsLeapYear(ByVal yr As Integer) As Boolean
        Return yr Mod 4 = 0 AndAlso (yr Mod 100 <> 0 Or yr Mod 400 = 0)
    End Function
End Module

Structure CalendarDate
    Dim Year As Integer
    Dim Month As Integer
    Dim Day As Integer
End Structure
