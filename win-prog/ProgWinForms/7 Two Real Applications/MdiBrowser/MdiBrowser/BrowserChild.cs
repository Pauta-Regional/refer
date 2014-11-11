//---------------------------------------------
// BrowserChild.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//---------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class BrowserChild : Form
{
    WebBrowser wb;
    ToolStripStatusLabel statlbl;
    ToolStripProgressBar statprog;

    // Constructor.
    public BrowserChild()
    {
        wb = new WebBrowser();
        wb.Parent = this;
        wb.Dock = DockStyle.Fill;

        StatusStrip status = new StatusStrip();
        status.Parent = this;

        statlbl = new ToolStripStatusLabel();
        statlbl.TextAlign = ContentAlignment.MiddleLeft;
        statlbl.Spring = true;
        status.Items.Add(statlbl);

        statprog = new ToolStripProgressBar();
        statprog.Visible = false;
        status.Items.Add(statprog);

        // Now that the StatusStrip is in place, it's safe to install
        //  event handlers for the WebBrowser.
        wb.DocumentTitleChanged += OnDocumentTitleChanged;
        wb.StatusTextChanged += OnStatusTextChanged;
        wb.ProgressChanged += OnProgressChanged;
    }
    // Public property.
    public WebBrowser WebBrowser
    {
        get { return wb; }
    }
    // Event handlers for caption bar and status bar.
    void OnDocumentTitleChanged(object objSrc, EventArgs args)
    {
        WebBrowser wb = objSrc as WebBrowser;
        Text = wb.DocumentTitle;

        if (wb.Url != null && wb.Url.ToString().Length > 0)
            Text += " \x2014 " + wb.Url.ToString();
    }
    void OnStatusTextChanged(object objSrc, EventArgs args)
    {
        WebBrowser wb = objSrc as WebBrowser;
        statlbl.Text = wb.StatusText;
    }
    void OnProgressChanged(object obj, WebBrowserProgressChangedEventArgs args)
    {
        if (statprog.Visible = (args.CurrentProgress != args.MaximumProgress))
            statprog.Value = (int)(100 * args.CurrentProgress / 
                                         args.MaximumProgress);
    }
    protected override void OnClosed(EventArgs args)
    {
        base.OnClosed(args);
        WebBrowser.Dispose();
    }
}
