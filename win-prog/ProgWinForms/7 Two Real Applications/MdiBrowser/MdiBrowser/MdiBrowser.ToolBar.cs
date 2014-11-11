//---------------------------------------------------
// MdiBrowser.ToolBar.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//---------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

partial class MdiBrowser
{
    ToolStripButton btnBack, btnForward;
    ToolStripButton btnStop, btnRefresh, btnHome, btnPrint;
    BrowserChild bcLastActive;

    ToolStrip CreateToolBar(string strResource)
    {
        // ImageList for ToolStrip.
        ImageList imglst = new ImageList();
        imglst.ImageSize = SystemInformation.IconSize;  // 32x32, probably.
        imglst.Images.Add("back", new Icon(GetType(), "MdiBrowser.hotplug.ico"));
        imglst.Images.Add("stop", new Icon(GetType(), "MdiBrowser.error.ico"));
        imglst.Images.Add("rfsh", new Icon(GetType(), "MdiBrowser.idr_dll.ico"));
        imglst.Images.Add("home", new Icon(GetType(), "MdiBrowser.homenet.ico"));
        imglst.Images.Add("prnt", new Icon(GetType(), "MdiBrowser.printer.ico"));

        // Create Forward image from Back image.
        Image img = imglst.Images["back"];
        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
        imglst.Images.Add("frwd", img);

        // ToolStrip with buttons.
        ToolStrip tool = new ToolStrip();
        tool.Parent = this;
        tool.Visible = settings.ViewToolBar;
        tool.ImageList = imglst;
        tool.ImageScalingSize = imglst.ImageSize;

        // The event handlers for these five buttons 
        //  are located in MdiBrowser.ViewMenu.cs.
        btnBack = new ToolStripButton();
        btnBack.Text = "Back";
        btnBack.ImageKey = "back";
        btnBack.Click += BackOnClick;
        tool.Items.Add(btnBack);

        btnForward = new ToolStripButton();
        btnForward.Text = "Forward";
        btnForward.ImageKey = "frwd";
        btnForward.Click += ForwardOnClick;
        tool.Items.Add(btnForward);

        btnStop = new ToolStripButton();
        btnStop.ImageKey = "stop";
        btnStop.ToolTipText = "Stop";
        btnStop.Click += StopOnClick;
        tool.Items.Add(btnStop);

        btnRefresh = new ToolStripButton();
        btnRefresh.ImageKey = "rfsh";
        btnRefresh.ToolTipText = "Refresh";
        btnRefresh.Click += RefreshOnClick;
        tool.Items.Add(btnRefresh);

        btnHome = new ToolStripButton();
        btnHome.ImageKey = "home";
        btnHome.ToolTipText = "Home";
        btnHome.Click += HomeOnClick;
        tool.Items.Add(btnHome);

        tool.Items.Add(new ToolStripSeparator());

        btnPrint = new ToolStripButton();
        btnPrint.ImageKey = "prnt";
        btnPrint.ToolTipText = "Printer";
        btnPrint.Click += PrintOnClick;
        tool.Items.Add(btnPrint);

        return tool;
    }
    // When active MDI Child changes, alter event handlers and ToolStrip.
    protected override void OnMdiChildActivate(EventArgs args)
    {
        base.OnMdiChildActivate(args);

        if (bcLastActive != null)
        {
            bcLastActive.WebBrowser.CanGoBackChanged -= OnCanGoBackChanged;
            bcLastActive.WebBrowser.CanGoForwardChanged -= OnCanGoForwardChanged;
        }

        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
        {
            bc.WebBrowser.CanGoBackChanged += OnCanGoBackChanged;
            bc.WebBrowser.CanGoForwardChanged += OnCanGoForwardChanged;

            btnBack.Enabled = bc.WebBrowser.CanGoBack;
            btnForward.Enabled = bc.WebBrowser.CanGoForward;
        }
        else
        {
            btnBack.Enabled = false;
            btnForward.Enabled = false;
        }

        btnStop.Enabled = (bc != null);
        btnRefresh.Enabled = (bc != null);
        btnHome.Enabled = (bc != null);
        btnPrint.Enabled = (bc != null);

        bcLastActive = bc;
    }
    // Event handlers for enabling Back and Forward buttons.
    void OnCanGoBackChanged(object objSrc, EventArgs args)
    {
        WebBrowser wb = objSrc as WebBrowser;
        btnBack.Enabled = wb.CanGoBack;
    }
    void OnCanGoForwardChanged(object objSrc, EventArgs args)
    {
        WebBrowser wb = objSrc as WebBrowser;
        btnForward.Enabled = wb.CanGoForward;
    }
    void PrintOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bcActive = ActiveMdiChild as BrowserChild;

        if (bcActive != null)
            bcActive.WebBrowser.Print();
    }
}

