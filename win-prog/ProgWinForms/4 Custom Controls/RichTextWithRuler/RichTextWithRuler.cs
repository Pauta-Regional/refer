//--------------------------------------------------
// RichTextWithRuler.cs (c) 2005 by Charles Petzold
//--------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class RichTextWithRuler : Form
{
    DocumentRuler ruler;
    RichTextBox txtbox;
    float fDpi;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new RichTextWithRuler());
    }
    public RichTextWithRuler()
    {
        Text = "RichText with Ruler";

        Graphics grfx = CreateGraphics();
        fDpi = grfx.DpiX;
        grfx.Dispose();

        txtbox = new RichTextBox();
        txtbox.Parent = this;
        txtbox.AcceptsTab = true;
        txtbox.Dock = DockStyle.Fill;
        txtbox.RightMargin = InchesToPixels(6);
        txtbox.ShowSelectionMargin = true;
        txtbox.SelectionChanged += TextBoxOnSelectionChanged;

        ruler = new DocumentRuler();
        ruler.Parent = this;
        ruler.LeftMargin = 10; 
        ruler.TextWidth = PixelsToInches(txtbox.RightMargin);
        ruler.DocumentControl = txtbox;
        ruler.Change += RulerOnChange;

        // Initialize the ruler with text box values.
        TextBoxOnSelectionChanged(txtbox, EventArgs.Empty);
    }
    void TextBoxOnSelectionChanged(object objSrc, EventArgs args)
    {
        ruler.LeftIndent = PixelsToInches(txtbox.SelectionIndent + 
                                          txtbox.SelectionHangingIndent);
        ruler.RightIndent = PixelsToInches(txtbox.SelectionRightIndent);
        ruler.FirstLineIndent = PixelsToInches(-txtbox.SelectionHangingIndent);

        float[] fTabs = new float[txtbox.SelectionTabs.Length];

        for (int i = 0; i < txtbox.SelectionTabs.Length; i++)
            fTabs[i] = PixelsToInches(txtbox.SelectionTabs[i]);

        ruler.Tabs = fTabs;
    }
    void RulerOnChange(object objSrc, RulerEventArgs args)
    {
        switch (args.RulerChange)
        {
            case RulerProperty.TextWidth:
                txtbox.RightMargin = InchesToPixels(ruler.TextWidth);
                break;

            case RulerProperty.LeftIndent:
            case RulerProperty.FirstLineIndent:
                txtbox.SelectionIndent = InchesToPixels(ruler.LeftIndent +
                                                        ruler.FirstLineIndent);
                txtbox.SelectionHangingIndent =
                    InchesToPixels(-ruler.FirstLineIndent);
                break;

            case RulerProperty.RightIndent:
                txtbox.SelectionRightIndent = InchesToPixels(ruler.RightIndent);
                break;

            case RulerProperty.Tabs:
                int[] iTabs = new int[ruler.Tabs.Length];

                for (int i = 0; i < ruler.Tabs.Length; i++)
                    iTabs[i] = InchesToPixels(ruler.Tabs[i]);

                txtbox.SelectionTabs = iTabs;
                break;
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
