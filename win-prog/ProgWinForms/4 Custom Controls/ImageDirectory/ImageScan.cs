//------------------------------------------
// ImageScan.cs (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.ComponentModel;    // for AsyncCompletedEventArgs
using System.Drawing;
using System.IO;
using System.Windows.Forms;

class ImageScan : FlowLayoutPanel
{
    Size szImage;
    string strImageLocation;
    ToolTip tips = new ToolTip();

    public ImageScan()
    {
        FlowDirection = FlowDirection.LeftToRight;
        WrapContents = false;
        AutoScroll = true;

        // Create Size object of one square inch.
        Graphics grfx = CreateGraphics();
        szImage = new Size((int)grfx.DpiX, (int)grfx.DpiY); // 1" square
        grfx.Dispose();

        Height = szImage.Height + Font.Height + 
                        SystemInformation.HorizontalScrollBarHeight;
    }
    public string Directory
    {
        set 
        {
            Controls.Clear();
            tips.RemoveAll();

            string[] astrFiles = System.IO.Directory.GetFiles(value, "*.*");

            foreach (string strFile in astrFiles)
            {
                PictureBox picbox = new SelectablePictureBox();
                picbox.Parent = this;
                picbox.Size = szImage;
                picbox.SizeMode = PictureBoxSizeMode.Zoom;
                picbox.Click += PictureBoxOnClick;
                picbox.LoadCompleted += PictureBoxOnLoadCompleted;
                picbox.LoadAsync(strFile);
            }
        }
    }
    public string SelectedImageFile
    {
        get 
        {
            return strImageLocation;
        }
    }
    void PictureBoxOnClick(object objSrc, EventArgs args)
    {
        PictureBox picbox = objSrc as PictureBox;
        strImageLocation = picbox.ImageLocation;

        OnClick(args);
    }

    // Don't generate Click events when user clicks the panel.
    protected override void OnMouseDown(MouseEventArgs args)
    {
    }
    void PictureBoxOnLoadCompleted(object objSrc, AsyncCompletedEventArgs args)
    {
        PictureBox picbox = objSrc as PictureBox;

        if (args.Error == null)
            tips.SetToolTip(picbox, Path.GetFileName(picbox.ImageLocation));
        else
            Controls.Remove(picbox);
    }
}
