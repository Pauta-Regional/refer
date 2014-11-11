//-------------------------------------------
// MdiBrowser.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//-------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

[assembly: AssemblyTitle("MDI Browser")]
[assembly: AssemblyDescription("Multiple Document Interface Web Browser")]
[assembly: AssemblyCompany("www.charlespetzold.com")]
[assembly: AssemblyProduct("MDI Browser")]
[assembly: AssemblyCopyright("(c) 2005 by Charles Petzold")]
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]

partial class MdiBrowser : Form
{
    static string strAppData = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData),
                "Petzold\\MdiBrowser\\MdiBrowser.Settings.xml");

    MdiBrowserSettings settings;
    MenuStrip menu;
    ToolStrip addr, tool;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MdiBrowser());
    }
    public MdiBrowser()
    {
        // Set basic Form properties.
        Text = "MDI Browser";
        Icon = new Icon(GetType(), "MdiBrowser.MdiBrowser.ico");
        IsMdiContainer = true;

        // Load settings.
        settings = MdiBrowserSettings.Load(strAppData);

        // Set window size and state.
        Bounds = settings.WindowBounds;
        WindowState = settings.WindowState;

        // Create Address bar ToolStrip
        addr = CreateAddressBar("MdiBrowser");  // in MdiBrowser.AddrBar.cs

        // Create Toolbar ToolStrip
        tool = CreateToolBar("MdiBrowser");  // in MdiBrowser.ToolBar.cs

        // Create MenuStrip
        menu = new MenuStrip();
        menu.Parent = this;
        MainMenuStrip = menu;           // This is good for MDI.

        menu.Items.Add(FileMenu());     // in MdiBrowser.FileMenu.cs        
        menu.Items.Add(ViewMenu());     // in MdiBrowser.ViewMenu.cs
        menu.Items.Add(FavoritesMenu());// in MdiBrowser.FavoritesMenu.cs
        menu.Items.Add(WindowMenu());   // in MdiBrowser.WindowMenu.cs
        menu.Items.Add(HelpMenu());     // in MdiBrowser.HelpMenu.cs

        // Load a child window with the Home page.
        Go(settings.Home, false);
    }
    // The Go method goes to a URL and optionally adds it to the list.
    void Go(string strUrl, bool bAddToList)
    {
        BrowserChild bcNew = new BrowserChild();
        bcNew.MdiParent = this;
        bcNew.Icon = Icon;
        bcNew.WebBrowser.Navigate(strUrl);

        // We're setting these from the settings object, but not
        //  updating them. That would require installing event
        //  handlers for Resize and saving the user's last
        //  preference.
        bcNew.Bounds = settings.ChildWindowBounds;
        bcNew.WindowState = settings.ChildWindowState;
        bcNew.Show();

        // If the URL was typed in manually, add it to the list
        //  to be displayed by the combo boxes.
        if (bAddToList)
        {
            if (settings.ManualUrls.IndexOf(strUrl) == -1)
            {
                settings.ManualUrls.Add(strUrl);
                settings.ManualUrls.Sort();
                settings.Save(strAppData);
            }
        }
    }
    // Save settings when the form is closing.
    protected override void OnClosed(EventArgs args)
    {
        settings.WindowState = WindowState;
        settings.WindowBounds = WindowState == 
            FormWindowState.Normal ? Bounds : RestoreBounds;
        settings.Save(strAppData);

        base.OnClosed(args);
    }
}
