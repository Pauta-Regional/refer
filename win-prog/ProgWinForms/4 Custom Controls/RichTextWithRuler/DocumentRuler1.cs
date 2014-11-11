//-----------------------------------------------
// DocumentRuler1.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public partial class DocumentRuler : Control
{
    // Private fields.
    int iLeftMargin;
    float fDpi;
    Control ctrlDocument;

    // RulerSlider objects.
    LeftIndent rsLeftIndent = new LeftIndent();
    RightIndent rsRightIndent = new RightIndent();
    FirstLineIndent rsFirstIndent = new FirstLineIndent();
    TextWidth rsTextWidth = new TextWidth();
    List<RulerSlider> rsCollection = new List<RulerSlider>();

    // Constructor.
    public DocumentRuler()
    {
        Dock = DockStyle.Top;
        ResizeRedraw = true;
        TabStop = false;
        Height = 23;
        Font = new Font(Font.Name, 14, GraphicsUnit.Pixel);

        Graphics grfx = CreateGraphics();
        fDpi = grfx.DpiX;
        grfx.Dispose();

        rsCollection.Add(rsLeftIndent);
        rsCollection.Add(rsRightIndent);
        rsCollection.Add(rsFirstIndent);
        rsCollection.Add(rsTextWidth);
    }

    // Public properties.
    public float TextWidth
    {
        get { return rsTextWidth.Value; }
        set
        {
            rsTextWidth.Value = value;
            CalculateDisplayOffsets();
        }
    }
    public float LeftIndent
    {
        get { return rsLeftIndent.Value; } 
        set 
        {
            rsLeftIndent.Value = value;
            CalculateDisplayOffsets();
        }
    }
    public float RightIndent
    {
        get { return rsRightIndent.Value; }
        set
        {
            rsRightIndent.Value = value;
            CalculateDisplayOffsets();
        }
    }
    public float FirstLineIndent
    {
        get { return rsFirstIndent.Value; }
        set
        {
            rsFirstIndent.Value = value;
            CalculateDisplayOffsets();
        }
    }
    public float[] Tabs
    {
        get 
        {
            List<float> fTabs = new List<float>();
        
            foreach (RulerSlider rs in rsCollection)
                if (rs is Tab)
                    fTabs.Add(rs.Value);

            // RichTextBox wants tabs in numeric order
            float[] afTabs = fTabs.ToArray();
            Array.Sort(afTabs); 
            return afTabs;
        }            
        set 
        {
            // First, delete tabs that aren't in value array.
            List<Tab> rsTabsDelete = new List<Tab>();

            foreach (RulerSlider rs in rsCollection)
                if (rs is Tab && (Array.IndexOf(value, rs.Value) == -1))
                    rsTabsDelete.Add(rs as Tab);

            foreach (Tab tab in rsTabsDelete)
            {
                rsCollection.Remove(tab);
                Invalidate(tab.Rectangle);
            }

            // Second, add tabs that aren't in rsCollection.
            foreach (float fTab in value)
            {
                bool bAdd = true;

                foreach (RulerSlider rs in rsCollection)
                    if (rs is Tab && rs.Value == fTab)
                        bAdd = false;

                if (bAdd)
                {
                    Tab tab = new Tab();
                    tab.Value = fTab;
                    tab.X = LeftMargin + InchesToPixels(fTab);
                    rsCollection.Add(tab);
                    Invalidate(tab.Rectangle);
                }
            }
        }
    }
    public int LeftMargin
    {
        get { return iLeftMargin; }
        set
        {
            iLeftMargin = value;
            CalculateDisplayOffsets();
        }
    }

    // For displaying a line when sliders are slid.
    public Control DocumentControl
    {
        get { return ctrlDocument; }
        set { ctrlDocument = value; }
    }

    // These two methods calculate X values for the four types of sliders
    //  (excluding tabs).  If the X values changes, invalidate the rectangle
    //  at the previous position and the new position.
    void CalculateDisplayOffsets()
    {
        CalculateDisplayOffsets2(rsTextWidth, LeftMargin + 
                        InchesToPixels(rsTextWidth.Value));
        CalculateDisplayOffsets2(rsLeftIndent, LeftMargin + 
                        InchesToPixels(rsLeftIndent.Value));
        CalculateDisplayOffsets2(rsRightIndent, LeftMargin + 
                        InchesToPixels(TextWidth - rsRightIndent.Value));
        CalculateDisplayOffsets2(rsFirstIndent, LeftMargin + 
                        InchesToPixels(LeftIndent + rsFirstIndent.Value));
    }
    void CalculateDisplayOffsets2(RulerSlider rs, int xNew)
    {
        if (rs.X != xNew)
        {
            Invalidate(rs.Rectangle);
            rs.X = xNew;
            Invalidate(rs.Rectangle);
        }
    }
    float PixelsToInches(int i)
    {
        return i / fDpi;
    }
    int InchesToPixels(float f)
    {
        return (int)Math.Round(f * fDpi);
    }
}

