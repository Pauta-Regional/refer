// ------------------------------------------------
// SuperDate.cs from "Programming in the Key of C#"
// ------------------------------------------------
using System;

class SuperDate: ExtDate, IComparable
{
    // Constructors

    public SuperDate()
    {
    }
    public SuperDate(int iYear, int iMon, int iDay): base(iYear, iMon, iDay)
    {
    }
    public SuperDate(int iCommonEraDay)
    {
        CommonEraDay = iCommonEraDay;
    }

    // Equality operators

    public static bool operator == (SuperDate sdLeft, SuperDate sdRight)
    {
        return sdLeft.CommonEraDay == sdRight.CommonEraDay;
    }
    public static bool operator != (SuperDate sdLeft, SuperDate sdRight)
    {
        return !(sdLeft == sdRight);
    }
    
    // Relational operators

    public static bool operator < (SuperDate sdLeft, SuperDate sdRight)
    {
        return sdLeft.CommonEraDay < sdRight.CommonEraDay;
    }
    public static bool operator > (SuperDate sdLeft, SuperDate sdRight)
    {
        return sdLeft.CommonEraDay > sdRight.CommonEraDay;
    }
    public static bool operator <= (SuperDate sdLeft, SuperDate sdRight)
    {
        return !(sdLeft > sdRight);
    }
    public static bool operator >= (SuperDate sdLeft, SuperDate sdRight)
    {
        return !(sdLeft < sdRight);
    }

    // Arithmetic operators

    public static SuperDate operator + (SuperDate sdLeft, int iRight)
    {
        return new SuperDate(sdLeft.CommonEraDay + iRight);
    }
    public static SuperDate operator + (int iLeft, SuperDate sdRight)
    {
        return sdRight + iLeft;
    }
    public static int operator - (SuperDate sdLeft, SuperDate sdRight)
    {
        return sdLeft.CommonEraDay - sdRight.CommonEraDay;
    }
    public static SuperDate operator - (SuperDate sdLeft, int iRight)
    {
        return new SuperDate(sdLeft.CommonEraDay - iRight);
    }

    // Unary operators

    public static SuperDate operator ++ (SuperDate sd)
    {
        return new SuperDate(sd.CommonEraDay + 1);
    }
    public static SuperDate operator -- (SuperDate sd)
    {
        return new SuperDate(sd.CommonEraDay - 1);
    }

    // Explicit casts

    public static explicit operator int (SuperDate sd)
    {
        return sd.CommonEraDay;
    }
    public static explicit operator SuperDate (int iCommonEraDay)
    {
        return new SuperDate(iCommonEraDay);
    }

    // Overrides of methods in System.Object

    public override bool Equals(object obj)
    {
        return obj is SuperDate && this == (SuperDate) obj;
    }
    public override int GetHashCode()
    {
        return CommonEraDay;
    }

    // Implementation of IComparable interface

    public int CompareTo(object obj)
    {
        if (obj == null)
            return 1;

        if (!(obj is SuperDate))
            throw new ArgumentException();

        return this - (SuperDate) obj;
    }
}

