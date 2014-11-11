//----------------------------------------------------
// MdiBrowser.ViewMenu.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

partial class MdiBrowser : Form
{
    ToolStripMenuItem itemBack, itemForward, itemStop;
    ToolStripMenuItem itemRefresh, itemHome, itemSource;

    ToolStripMenuItem ViewMenu()
    {
        ToolStripMenuItem itemView = new ToolStripMenuItem("&View");
        itemView.DropDownOpening += ViewOnDropDownOpening;

        ToolStripMenuItem item = new ToolStripMenuItem("&Tool Bar");
        item.Checked = settings.ViewToolBar;
        item.CheckOnClick = true;
        item.Click += ViewToolBarOnClick;
        itemView.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Address Bar");
        item.Checked = settings.ViewAddressBar;
        item.CheckOnClick = true;
        item.Click += ViewAddressBarOnClick;
        itemView.DropDownItems.Add(item);

        itemView.DropDownItems.Add(new ToolStripSeparator());

        itemBack = new ToolStripMenuItem("&Back");
        itemBack.Click += BackOnClick;
        itemView.DropDownItems.Add(itemBack);

        itemForward = new ToolStripMenuItem("&Forward");
        itemForward.Click += ForwardOnClick;
        itemView.DropDownItems.Add(itemForward);

        itemStop = new ToolStripMenuItem("Sto&p");
        itemStop.Click += StopOnClick;
        itemView.DropDownItems.Add(itemStop);

        itemRefresh = new ToolStripMenuItem("&Refresh");
        itemRefresh.Click += RefreshOnClick;
        itemView.DropDownItems.Add(itemRefresh);

        itemHome = new ToolStripMenuItem("&Home");
        itemHome.Click += HomeOnClick;
        itemView.DropDownItems.Add(itemHome);

        itemView.DropDownItems.Add(new ToolStripSeparator());

        itemSource = new ToolStripMenuItem("Sour&ce");
        itemSource.Click += ViewSourceOnClick;
        itemView.DropDownItems.Add(itemSource);

        return itemView;
    }
    // Event handler for View menu dropping down.
    void ViewOnDropDownOpening(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;
        bool bActiveChild = bc != null;

        // Disable all these items if there's no active MDI child.
        itemBack.Enabled = bActiveChild;
        itemForward.Enabled = bActiveChild;
        itemStop.Enabled = bActiveChild;
        itemRefresh.Enabled = bActiveChild;
        itemHome.Enabled = bActiveChild;
        itemSource.Enabled = bActiveChild;

        // If there's an active child, enable these items based on properties.
        if (bActiveChild)
        {
            itemBack.Enabled = bc.WebBrowser.CanGoBack;
            itemForward.Enabled = bc.WebBrowser.CanGoForward;
        }
    }
    // Make toolbar and address bar visible or invisible.
    void ViewToolBarOnClick(object objSrc, EventArgs args)
    {
        ToolStripMenuItem item = objSrc as ToolStripMenuItem;
        tool.Visible = settings.ViewToolBar = item.Checked;
    }
    void ViewAddressBarOnClick(object objSrc, EventArgs args)
    {
        ToolStripMenuItem item = objSrc as ToolStripMenuItem;
        addr.Visible = settings.ViewAddressBar = item.Checked;
    }
    // Implement Back, Forward, Stop, Refresh, and Home.
    void BackOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
            bc.WebBrowser.GoBack();
    }
    void ForwardOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
            bc.WebBrowser.GoForward();
    }
    void StopOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
            bc.WebBrowser.Stop();
    }
    void RefreshOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
            bc.WebBrowser.Refresh();
    }
    void HomeOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
            bc.WebBrowser.Url = new Uri(settings.Home);
    }
    // View Source menu item.
    void ViewSourceOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
        {
            Form frm = new Form();
            frm.Text = bc.WebBrowser.DocumentTitle;

            TextBox txtbox = new TextBox();
            txtbox.Parent = frm;
            txtbox.Multiline = true;
            txtbox.WordWrap = false;
            txtbox.ScrollBars = ScrollBars.Both;
            txtbox.Dock = DockStyle.Fill;
            txtbox.Text = bc.WebBrowser.DocumentText;
            txtbox.Select(0, 0);

            frm.Show();
        }
    }
}
