//--------------------------------------------------
// DirectoryListView.cs (c) 2005 by Charles Petzold
//--------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

class DirectoryListView : ListView
{
    string strDirectory;

    public DirectoryListView()
    {
        // Create the ListView columns.
        Columns.Add("Name", 150, HorizontalAlignment.Left);
        Columns.Add("Size", 100, HorizontalAlignment.Right);
        Columns.Add("Type", 100, HorizontalAlignment.Left);
        Columns.Add("Date Modified", 150, HorizontalAlignment.Left);

        // Create the ListView image lists.
        SmallImageList = new ImageList();
        LargeImageList = new ImageList();
        LargeImageList.ImageSize = new Size(32, 32);
    }

    public string Directory
    {
        get { return strDirectory; }
        set
        {
            // Clear all items and the image lists.
            Items.Clear();
            SmallImageList.Images.Clear();
            LargeImageList.Images.Clear();

            // Load the Folder icon.
            Icon icn = new Icon(GetType(), "Resource.Folder.ico");
            SmallImageList.Images.Add(icn);
            LargeImageList.Images.Add(icn);

            // Create DirectoryInfo object based on requested directory.
            DirectoryInfo dirinfo = new DirectoryInfo(strDirectory = value);

            // Obtain all the subdirectories and add them to the ListView.
            foreach (DirectoryInfo dir in dirinfo.GetDirectories())
            {
                ListViewItem item = new ListViewItem(dir.Name);
                item.ImageIndex = 0;
                item.Tag = "dir";
                item.SubItems.Add("");
                item.SubItems.Add("File Folder");
                item.SubItems.Add(dir.LastAccessTime.ToString());

                Items.Add(item);
            }

            int iImage = 1;

            // Obtain all the files and add them to the ListView.
            foreach (FileInfo file in dirinfo.GetFiles())
            {
                ListViewItem item = new ListViewItem(file.Name);

                icn = Icon.ExtractAssociatedIcon(
                                Path.Combine(file.DirectoryName, file.Name));

                SmallImageList.Images.Add(icn);
                LargeImageList.Images.Add(icn);
                item.ImageIndex = iImage++;

                item.SubItems.Add(file.Length.ToString("N0"));
                item.SubItems.Add(
                    Path.GetExtension(file.Name).ToUpper() == ".EXE" ?
                        "Executable" : "Document");
                item.SubItems.Add(file.LastWriteTime.ToString());

                Items.Add(item);
            }
        }
    }
    protected override void OnMouseDown(MouseEventArgs args)
    {
        base.OnMouseDown(args);

        // Change the view on a right button click.
        if (args.Button == MouseButtons.Right)
            View = (View)(((int)View + 1) % 5);
    }
    protected override void OnItemActivate(EventArgs args)
    {
        base.OnItemActivate(args);

        // If a directory being clicked, change to that directory.
        if ((string)SelectedItems[0].Tag == "dir")
        {
            Directory = Path.Combine(Directory, SelectedItems[0].Text);
        }
        else
        {
            // Otherwise, launch the programs associated with the file(s).
            foreach (ListViewItem item in SelectedItems)
            {
                try
                {
                    Process.Start(Path.Combine(Directory, item.Text));
                }
                catch
                {
                }
            }
        }
    }
}