//----------------------------------------------------
// CrazyMenuAssemblage.cs (c) 2005 by Charles Petzold
//----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class CrazyMenuAssemblage : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new CrazyMenuAssemblage());
    }
    public CrazyMenuAssemblage()
    {
        Text = "Crazy Menu Assemblage";

        MenuStrip menu = new MenuStrip();
        menu.Parent = this;

        menu.Items.AddRange(new ToolStripMenuItem[]
            {
                new ToolStripMenuItem("&File", null, new ToolStripMenuItem[]
                {
                    new ToolStripMenuItem("&Open...", null, DefaultOnClick,
                                          Keys.Control | Keys.O),
                    new ToolStripMenuItem("&Save", null, DefaultOnClick,
                                          Keys.Control | Keys.S),
                    new ToolStripMenuItem("&Close", null, DefaultOnClick)
                }),
                new ToolStripMenuItem("&Edit", null, new ToolStripMenuItem[]
                {
                    new ToolStripMenuItem("Cu&t", null, DefaultOnClick,
                                          Keys.Control | Keys.X),
                    new ToolStripMenuItem("&Copy", null, DefaultOnClick,
                                          Keys.Control | Keys.C),
                    new ToolStripMenuItem("&Paste", null, DefaultOnClick,
                                          Keys.Control | Keys.V)
                }),
                new ToolStripMenuItem("&Help", null, new ToolStripMenuItem[]
                {
                    new ToolStripMenuItem("&Help", null, DefaultOnClick,
                                          Keys.F1),
                    new ToolStripMenuItem("&About...", null, DefaultOnClick)
                })
            });
    }
    void DefaultOnClick(object obj, EventArgs args)
    {
        MessageBox.Show("Menu item not yet implemented", Text);
    }
}