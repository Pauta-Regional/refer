//----------------------------------------------------
// MdiBrowser.FileMenu.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

partial class MdiBrowser: Form
{
    ToolStripMenuItem itemSaveAs, itemPrint, itemPreview, itemProps;

    ToolStripMenuItem FileMenu()
    {
        ToolStripMenuItem itemFile = new ToolStripMenuItem("&File");
        itemFile.DropDownOpening += FileOnDropDownOpening;

        ToolStripMenuItem item = new ToolStripMenuItem("&New");
        item.ShortcutKeys = Keys.Control | Keys.N;
        item.Click += NewOnClick;
        itemFile.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Open...");
        item.ShortcutKeys = Keys.Control | Keys.O;
        item.Click += OpenOnClick;
        itemFile.DropDownItems.Add(item);

        itemSaveAs = new ToolStripMenuItem("Save &As...");
        itemSaveAs.Click += SaveAsOnClick;
        itemFile.DropDownItems.Add(itemSaveAs);

        itemFile.DropDownItems.Add(new ToolStripSeparator());

        item = new ToolStripMenuItem("Page Set&up...");
        item.Click += PageSetupOnClick;
        itemFile.DropDownItems.Add(item);

        itemPrint = new ToolStripMenuItem("&Print...");
        itemPrint.ShortcutKeys = Keys.Control | Keys.P;
        itemPrint.Click += PrintDialogOnClick;
        itemFile.DropDownItems.Add(itemPrint);

        itemPreview = new ToolStripMenuItem("Print Pre&view...");
        itemPreview.Click += PreviewOnClick;
        itemFile.DropDownItems.Add(itemPreview);

        itemFile.DropDownItems.Add(new ToolStripSeparator());

        itemProps = new ToolStripMenuItem("P&roperties");
        itemProps.Click += PropertiesOnClick;
        itemFile.DropDownItems.Add(itemProps);

        itemFile.DropDownItems.Add(new ToolStripSeparator());
        
        item = new ToolStripMenuItem("E&xit");
        item.Click += ExitOnClick;
        itemFile.DropDownItems.Add(item);

        return itemFile;
    }
    void FileOnDropDownOpening(object objSrc, EventArgs args)
    {
        BrowserChild bcActive = ActiveMdiChild as BrowserChild;

        itemSaveAs.Enabled = (bcActive != null);
        itemPrint.Enabled = (bcActive != null);
        itemPreview.Enabled = (bcActive != null);
        itemProps.Enabled = (bcActive != null);
    }
    void NewOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bcActive = ActiveMdiChild as BrowserChild;
        string strUrl;

        if (bcActive != null)
            strUrl = bcActive.WebBrowser.Url.ToString();
        else
            strUrl = settings.Home;

        Go(strUrl, false);
    }
    void OpenOnClick(object objSrc, EventArgs args)
    {
        OpenDialog dlg = new OpenDialog(settings);

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            Go(dlg.Url, true);
        }
    }
    void SaveAsOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bcActive = ActiveMdiChild as BrowserChild;

        if (bcActive != null)
            bcActive.WebBrowser.ShowSaveAsDialog();
    }
    void PageSetupOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bcActive = ActiveMdiChild as BrowserChild;

        if (bcActive != null)
            bcActive.WebBrowser.ShowPageSetupDialog();
    }
    void PrintDialogOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bcActive = ActiveMdiChild as BrowserChild;

        if (bcActive != null)
            bcActive.WebBrowser.ShowPrintDialog();
    }
    void PreviewOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bcActive = ActiveMdiChild as BrowserChild;

        if (bcActive != null)
            bcActive.WebBrowser.ShowPrintPreviewDialog();
    }
    void PropertiesOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bcActive = ActiveMdiChild as BrowserChild;

        if (bcActive != null)
            bcActive.WebBrowser.ShowPropertiesDialog();
    }
    void ExitOnClick(object objSrc, EventArgs args)
    {
        Close();
    }
}
