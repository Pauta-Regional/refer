//----------------------------------------------
// StyleComboBox.cs (c) 2005 by Charles Petzold
//----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class StyleComboBox : ComboBox
{
    public string FamilyName
    {
        set
        {
            TryStyle(value, FontStyle.Regular, "Regular");
            TryStyle(value, FontStyle.Italic, "Italic");
            TryStyle(value, FontStyle.Bold, "Bold");
            TryStyle(value, FontStyle.Bold | FontStyle.Italic, "Bold Italic");
            SelectedIndex = 0;
        }
    }
    public FontStyle FontStyle
    {
        set
        {
            if ((value & FontStyle.Bold & FontStyle.Italic) != 0)
                SelectedItem = "Bold Italic";

            else if ((value & FontStyle.Bold) != 0)
                SelectedItem = "Bold";

            else if ((value & FontStyle.Italic) != 0)
                SelectedItem = "Italic";
            else
                SelectedItem = "Regular";
        }
        get
        {
            if (SelectedItem.ToString() == "Bold Italic")
                return FontStyle.Bold | FontStyle.Italic;

            else if (SelectedItem.ToString() == "Bold")
                return FontStyle.Bold;

            else if (SelectedItem.ToString() == "Italic")
                return FontStyle.Italic;

            return FontStyle.Regular;
        }
    }
    void TryStyle(string strFamilyName, FontStyle fntstyle, string strStyle)
    {
        int index = FindStringExact(strStyle);

        try
        {
            new Font(strFamilyName, 12, fntstyle);

            if (index == -1)
                Items.Add(strStyle);
        }
        catch
        {
            if (index != -1)
                Items.Remove(strStyle);
        }
    }
}