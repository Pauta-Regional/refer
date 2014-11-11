//---------------------------------------------------
// MdiBrowser.AddrBar.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//---------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

partial class MdiBrowser
{
    ToolStripComboBox comboUrl;

    ToolStrip CreateAddressBar(string strResource)
    {
        // Address Bar for typing in URL.
        ToolStrip addr = new ToolStrip();
        addr.Parent = this;
        addr.GripStyle = ToolStripGripStyle.Hidden;
        addr.Visible = settings.ViewAddressBar;
        addr.SizeChanged += AddressBarOnSizeChanged;

        ToolStripLabel lbl = new ToolStripLabel("Address:");
        addr.Items.Add(lbl);

        // ComboBox for typing or selecting URL.
        comboUrl = new ToolStripComboBox();
        comboUrl.AutoSize = false;
        comboUrl.BeginUpdate();
        foreach (string str in settings.ManualUrls)
            comboUrl.Items.Add(str);
        comboUrl.EndUpdate();
        addr.Items.Add(comboUrl);

        ToolStripButton btn = new ToolStripButton();
        btn.Text = "Go";
        btn.Alignment = ToolStripItemAlignment.Right;
        btn.Click += GoOnClick;
        addr.Items.Add(btn);

        // Initialize size of Combobox.
        AddressBarOnSizeChanged(addr, EventArgs.Empty);

        return addr;
    }
    // AddressBarOnSizeChanged
    void AddressBarOnSizeChanged(object objSrc, EventArgs args)
    {
        ToolStrip tool = objSrc as ToolStrip;
        tool.Items[1].Width = tool.Width - tool.Items[0].Width 
                - tool.Items[2].Width
                - 6 * tool.Items[1].Margin.Horizontal;
    }
    // Event handler for Go button on address bar.
    void GoOnClick(object objSrc, EventArgs args)
    {
        if (comboUrl.Text != null && comboUrl.Text.Length > 0)
            Go(comboUrl.Text, true);
    }
}
