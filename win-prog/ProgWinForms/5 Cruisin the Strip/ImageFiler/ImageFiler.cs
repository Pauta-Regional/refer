//-------------------------------------------
// ImageFiler.cs (c) 2005 by Charles Petzold
//-------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

class ImageFiler : Form
{
    PictureBox picbox;
    ToolStripMenuItem itemSaveAs, itemCut, itemCopy, itemPaste, itemDelete;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ImageFiler());
    }
    public ImageFiler()
    {
        Text = "Image Filer";

        // Create picture box.
        picbox = new PictureBox();
        picbox.Parent = this;
        picbox.Dock = DockStyle.Fill;

        // Load bitmaps.
        Bitmap bmOpen = new Bitmap(GetType(), "ImageFiler.Open.bmp");
        Bitmap bmCut = new Bitmap(GetType(), "ImageFiler.Cut.bmp");
        Bitmap bmCopy = new Bitmap(GetType(), "ImageFiler.Copy.bmp");
        Bitmap bmPaste = new Bitmap(GetType(), "ImageFiler.Paste.bmp");
        Bitmap bmDelete = new Bitmap(GetType(), "ImageFiler.Delete.bmp");

        // Create menu.
        MenuStrip menu = new MenuStrip();
        menu.Parent = this;

        // Assemble File menu.
        ToolStripMenuItem itemFile = new ToolStripMenuItem("&File");
        itemFile.DropDownOpening += FileOnDropDown;
        menu.Items.Add(itemFile);

        ToolStripMenuItem item = new ToolStripMenuItem("&Open...");
        item.Image = bmOpen;
        item.ImageTransparentColor = Color.Magenta;
        item.ShortcutKeys = Keys.Control | Keys.O;
        item.Click += OpenOnClick;
        itemFile.DropDownItems.Add(item);

        itemSaveAs = new ToolStripMenuItem("Save &As...");
        itemSaveAs.Click += SaveAsOnClick;
        itemFile.DropDownItems.Add(itemSaveAs);

        itemFile.DropDownItems.Add(new ToolStripSeparator());

        item = new ToolStripMenuItem("E&xit");
        item.Click += ExitOnClick;
        itemFile.DropDownItems.Add(item);
        
        // Assemble Edit menu.
        ToolStripMenuItem itemEdit = new ToolStripMenuItem("&Edit");
        itemEdit.DropDownOpening += EditOnDropDown;
        menu.Items.Add(itemEdit);

        itemCut = new ToolStripMenuItem("Cu&t");
        itemCut.Image = bmCut;
        itemCut.ImageTransparentColor = Color.Magenta;
        itemCut.ShortcutKeys = Keys.Control | Keys.X;
        itemCut.Click += CutOnClick;
        itemEdit.DropDownItems.Add(itemCut);

        itemCopy = new ToolStripMenuItem("&Copy");
        itemCopy.Image = bmCopy; 
        itemCopy.ImageTransparentColor = Color.Magenta;
        itemCopy.ShortcutKeys = Keys.Control | Keys.C;
        itemCopy.Click += CopyOnClick;
        itemEdit.DropDownItems.Add(itemCopy);

        itemPaste = new ToolStripMenuItem("&Paste");
        itemPaste.Image = bmPaste;
        itemPaste.ImageTransparentColor = Color.Magenta;
        itemPaste.ShortcutKeys = Keys.Control | Keys.V;
        itemPaste.Click += PasteOnClick;
        itemEdit.DropDownItems.Add(itemPaste);

        itemDelete = new ToolStripMenuItem("&Delete");
        itemDelete.Image = bmDelete;
        itemDelete.ImageTransparentColor = Color.Magenta;
        itemDelete.ShortcutKeys = Keys.Delete;
        itemDelete.Click += DeleteOnClick;
        itemEdit.DropDownItems.Add(itemDelete);

        // Create and assemble View menu items.
        ToolStripMenuItem itemView = new ToolStripMenuItem("&View");
        itemView.DropDownOpening += ViewOnDropDown;
        menu.Items.Add(itemView);

        item = new ToolStripMenuItem("&Normal"); 
        item.Tag = PictureBoxSizeMode.Normal;
        item.Click += ViewItemOnClick;        
        itemView.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Center"); 
        item.Tag = PictureBoxSizeMode.CenterImage;
        item.Click += ViewItemOnClick;
        itemView.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Stretch"); 
        item.Tag = PictureBoxSizeMode.StretchImage;
        item.Click += ViewItemOnClick;
        itemView.DropDownItems.Add(item);

        item = new ToolStripMenuItem("&Zoom"); 
        item.Tag = PictureBoxSizeMode.Zoom;
        item.Click += ViewItemOnClick;
        itemView.DropDownItems.Add(item);
    }
    void FileOnDropDown(object obj, EventArgs args)
    {
        itemSaveAs.Enabled = picbox.Image != null;
    }
    void EditOnDropDown(object obj, EventArgs args)
    {
        itemCut.Enabled = itemCopy.Enabled = itemDelete.Enabled =
            picbox.Image != null;

        IDataObject data = Clipboard.GetDataObject();

        itemPaste.Enabled = data.GetDataPresent(typeof(Bitmap)) ||
                            data.GetDataPresent(typeof(Metafile));
    }
    void ViewOnDropDown(object obj, EventArgs args)
    {
        ToolStripMenuItem itemView = (ToolStripMenuItem)obj;

        foreach (ToolStripItem item in itemView.DropDownItems)
        {
            ToolStripMenuItem mitem = (ToolStripMenuItem)item;
            mitem.Checked = (PictureBoxSizeMode)mitem.Tag == picbox.SizeMode;
        }
    }
    void OpenOnClick(object obj, EventArgs args)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;" +
                         "*.jfif;*.png;*.tif;*.tiff;*.wmf;*.emf|" +
                     "Windows Bitmap (*.bmp)|*.bmp|" +
                     "Windows Icon (*.ico)|*.ico|" +
                     "Graphics Interchange Format (*.gif)|*.gif|" +
                     "JPEG File Interchange Format (*.jpg)|" +
                         "*.jpg;*.jpeg;*.jfif|" +
                     "Portable Network Graphics (*.png)|*.png|" +
                     "Tag Image File Format (*.tif)|*.tif;*.tiff|" +
                     "Windows Metafile (*.wmf)|*.wmf|" +
                     "Enhanced Metafile (*.emf)|*.emf|" +
                     "All Files (*.*)|*.*";

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            try
            {
                picbox.Image = Image.FromFile(dlg.FileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Text);
                return;
            }
        }
    }
    void SaveAsOnClick(object obj, EventArgs args)
    {
        SaveFileDialog savedlg = new SaveFileDialog();
        savedlg.AddExtension = true;
        savedlg.Filter = "Windows Bitmap (*.bmp)|*.bmp|" +
                         "Graphics Interchange Format (*.gif)|*.gif|" +
                         "JPEG File Interchange Format (*.jpg)|" +
                            "*.jpg;*.jpeg;*.jfif|" +
                         "Portable Network Graphics (*.png)|*.png|" +
                         "Tagged Imaged File Format (*.tif)|*.tif;*.tiff";

        ImageFormat[] aif = { ImageFormat.Bmp,  ImageFormat.Gif,
                              ImageFormat.Jpeg, ImageFormat.Png,
                              ImageFormat.Tiff };

        if (savedlg.ShowDialog() == DialogResult.OK)
        {
            try
            {
                picbox.Image.Save(savedlg.FileName, aif[savedlg.FilterIndex - 1]);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Text);
                return;
            }
        }
    }
    void CutOnClick(object obj, EventArgs args)
    {
        CopyOnClick(obj, args);
        DeleteOnClick(obj, args);
    }
    void CopyOnClick(object obj, EventArgs args)
    {
        Clipboard.SetDataObject(picbox.Image, true);
    }
    void PasteOnClick(object obj, EventArgs args)
    {
        IDataObject data = Clipboard.GetDataObject();

        if (data.GetDataPresent(typeof(Metafile)))
            picbox.Image = (Image)data.GetData(typeof(Metafile));

        else if (data.GetDataPresent(typeof(Bitmap)))
            picbox.Image = (Image)data.GetData(typeof(Bitmap));
    }
    void DeleteOnClick(object obj, EventArgs args)
    {
        picbox.Image = null;
    }
    void ViewItemOnClick(object obj, EventArgs args)
    {
        ToolStripMenuItem item = (ToolStripMenuItem)obj;
        picbox.SizeMode = (PictureBoxSizeMode) item.Tag;
    }
    void ExitOnClick(object obj, EventArgs args)
    {
        Close();
    }
}