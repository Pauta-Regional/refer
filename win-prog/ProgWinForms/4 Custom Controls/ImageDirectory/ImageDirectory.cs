//-----------------------------------------------
// ImageDirectory.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ImageDirectory: Form
{
    PictureBox picbox;
    ImageScan imgscan;
    Label lblDirectory;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ImageDirectory());
    }
    public ImageDirectory()
    {
        Text = "Image Directory";

        picbox = new PictureBox();
        picbox.Parent = this;
        picbox.Dock = DockStyle.Fill;
        picbox.SizeMode = PictureBoxSizeMode.Zoom;

        imgscan = new ImageScan();
        imgscan.Parent = this;
        imgscan.Dock = DockStyle.Top;
        imgscan.Click += ImageScanOnClick;

        FlowLayoutPanel pnl = new FlowLayoutPanel();
        pnl.Parent = this;
        pnl.AutoSize = true;
        pnl.Dock = DockStyle.Top;

        Button btn = new Button();
        btn.Parent = pnl;
        btn.AutoSize = true;
        btn.Anchor = AnchorStyles.Left;
        btn.Text = "Directory...";
        btn.Click += ButtonOnClick;

        lblDirectory = new Label();
        lblDirectory.Parent = pnl;
        lblDirectory.AutoSize = true;
        lblDirectory.Anchor = AnchorStyles.Right;

        // Initialize.
        imgscan.Directory = lblDirectory.Text =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }
    void ButtonOnClick(object objSrc, EventArgs args)
    {
        FolderBrowserDialog dlg = new FolderBrowserDialog();
        dlg.SelectedPath = lblDirectory.Text;
        dlg.ShowNewFolderButton = false;

        if (dlg.ShowDialog() == DialogResult.OK)
            imgscan.Directory = lblDirectory.Text = dlg.SelectedPath;
    }
    void ImageScanOnClick(object objSrc, EventArgs args)
    {
        picbox.ImageLocation = imgscan.SelectedImageFile;
    }
}
