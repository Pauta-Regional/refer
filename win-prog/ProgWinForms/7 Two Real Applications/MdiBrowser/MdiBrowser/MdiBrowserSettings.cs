//---------------------------------------------------
// MdiBrowserSettings.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//---------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

public class MdiBrowserSettings
{
    // Default settings.
    public Rectangle WindowBounds = new Rectangle(0, 0, 800, 600);
    public FormWindowState WindowState = FormWindowState.Normal;

    public Rectangle ChildWindowBounds = new Rectangle(0, 0, 660, 400);
    public FormWindowState ChildWindowState = FormWindowState.Normal;

    public string Home = "http://www.charlespetzold.com";
    public bool ViewToolBar = true;
    public bool ViewAddressBar = true;

    // Lists of favorites and typed-in URLs.
    public List<Favorite> Favorites = new List<Favorite>();
    public List<string> ManualUrls = new List<string>();

    // Load settings from file.
    public static MdiBrowserSettings Load(string strAppData)
    {
        StreamReader sr;
        MdiBrowserSettings settings;
        XmlSerializer xmlser = 
            new XmlSerializer(typeof(MdiBrowserSettings));

        try
        {
            sr = new StreamReader(strAppData);
            settings = (MdiBrowserSettings)xmlser.Deserialize(sr);
            sr.Close();
        }
        catch
        {
            settings = new MdiBrowserSettings();
        }
        return settings;
    }
    // Save settings to file.
    public void Save(string strAppData)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(strAppData));
        StreamWriter sw = new StreamWriter(strAppData);
        XmlSerializer xmlser = new XmlSerializer(GetType());
        xmlser.Serialize(sw, this);
        sw.Close();
    }
}
