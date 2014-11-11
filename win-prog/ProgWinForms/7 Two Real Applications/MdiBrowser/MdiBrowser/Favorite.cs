//-----------------------------------------
// Favorite.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//-----------------------------------------
using System;

public class Favorite : IComparable<Favorite>
{
    string strTitle, strUrl;

    // Public properties.
    public string Title
    {
        get { return strTitle; }
        set { strTitle = value; }
    }
    public string Url
    {
        get { return strUrl; }
        set { strUrl = value; }
    }
    // Constructors.
    public Favorite()
    {
    }
    public Favorite(string strTitle, string strUrl)
    {
        Title = strTitle;
        Url = strUrl;
    }
    // Method to implement IComparable interface.
    public int CompareTo(Favorite fav)
    {
        return Title.CompareTo(fav.Title);
    }
}

