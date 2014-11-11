'------------------------------------------ 
' MinAndMax.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System

Module MinAndMax
    Sub Main()
        Console.WriteLine("Byte:    {0} to {1}", Byte.MinValue, _
                                                 Byte.MaxValue)
        Console.WriteLine("Short:   {0} to {1}", Short.MinValue, _
                                                 Short.MaxValue)
        Console.WriteLine("Integer: {0} to {1}", Integer.MinValue, _
                                                 Integer.MaxValue)
        Console.WriteLine("Long:    {0} to {1}", Long.MinValue, _
                                                 Long.MaxValue)
        Console.WriteLine("Single:  {0} to {1}", Single.MinValue, _
                                                 Single.MaxValue)
        Console.WriteLine("Double:  {0} to {1}", Double.MinValue, _
                                                 Double.MaxValue)
        Console.WriteLine("Decimal: {0} to {1}", Decimal.MinValue, _
                                                 Decimal.MaxValue)
    End Sub
End Module
