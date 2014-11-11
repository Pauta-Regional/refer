//---------------------------------------------------------
// MdiBrowser.FavoritesMenu.cs (c) 2005 by Charles Petzold
// From MdiBrowser Program
//---------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

partial class MdiBrowser : Form
{
    ToolStripMenuItem itemAdd, itemSetHome;

    ToolStripMenuItem FavoritesMenu()
    {
        ToolStripMenuItem itemFavorites = new ToolStripMenuItem("F&avorites");
        itemFavorites.DropDownOpening += FavoritesOnDropDownOpening;

        return itemFavorites;
    }
    void FavoritesOnDropDownOpening(object objSrc, EventArgs args)
    {
        ToolStripMenuItem itemFavorites = objSrc as ToolStripMenuItem;
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        // Remove everything from the drop-down menu.
        itemFavorites.DropDownItems.Clear();

        itemAdd = new ToolStripMenuItem("&Add to favorites");
        itemAdd.Enabled = (bc != null);
        itemAdd.Click += AddOnClick;
        itemFavorites.DropDownItems.Add(itemAdd);

        itemSetHome = new ToolStripMenuItem("&Make this your home");
        itemSetHome.Enabled = (bc != null);
        itemSetHome.Click += SetHomeOnClick;
        itemFavorites.DropDownItems.Add(itemSetHome);

        itemFavorites.DropDownItems.Add(new ToolStripSeparator());

        // Add favorites to the menu;            
        foreach (Favorite fav in settings.Favorites)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = fav.Title;
            item.Tag = fav.Url;
            item.Click += FavoriteOnClick;
            itemFavorites.DropDownItems.Add(item);
        }
    }
    void AddOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
        {
            Favorite fav = new Favorite(bc.WebBrowser.DocumentTitle,
                                        bc.WebBrowser.Url.ToString());
            settings.Favorites.Add(fav);
            settings.Favorites.Sort();
            settings.Save(strAppData);
        }
    }
    void SetHomeOnClick(object objSrc, EventArgs args)
    {
        BrowserChild bc = ActiveMdiChild as BrowserChild;

        if (bc != null)
        {
            settings.Home = bc.WebBrowser.Url.ToString();
        }
    }
    void FavoriteOnClick(object objSrc, EventArgs args)
    {
        ToolStripMenuItem item = objSrc as ToolStripMenuItem;
        Go((string)item.Tag, false);
    }
}
