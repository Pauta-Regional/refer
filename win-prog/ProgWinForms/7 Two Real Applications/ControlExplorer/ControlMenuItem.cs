//------------------------------------------------
// ControlMenuItem.cs (c) 2005 by Charles Petzold
// From ControlExplorer program
//------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

class ControlMenuItem : ToolStripMenuItem
{
    public ControlMenuItem(EventHandler evtClick)
    {
        // Obtain the assembly in which the Control class is defined.
        Assembly asbly = Assembly.GetAssembly(typeof(Control));

        // This is an array of all the types in that class
        Type[] atype = asbly.GetTypes();

        // We're going to store descendents of Control in a sorted list.
        SortedList<string, ToolStripMenuItem> sortlst = 
            new SortedList<string, ToolStripMenuItem>();

        Text = "Control";
        Tag = typeof(Control);
        sortlst.Add("Control", this);

        // Enumerate all the types in the array.
        // For Control and its descendents, create menu items and
        //  add to the SortedList object.
        // Notice the menu item Tag property is the associated Type object.
        foreach (Type typ in atype)
        {
            if (typ.IsPublic && (typ.IsSubclassOf(typeof(Control))))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(typ.Name);
                item.Click += evtClick;
                item.Tag = typ;
                sortlst.Add(typ.Name, item);
            }
        }

        // Go through the sorted list and set menu item parents.
        foreach (KeyValuePair<string, ToolStripMenuItem> kvp in sortlst)
        {
            if (kvp.Key != "Control")
            {
                string strParent = ((Type)kvp.Value.Tag).BaseType.Name;
                ToolStripMenuItem itemParent = sortlst[strParent];
                itemParent.DropDownItems.Add(kvp.Value);

                // itemParent shouldn't have event handler!
                itemParent.Click -= evtClick;
            }
        }

        // Scan through again:
        //  If abstract and selectable, disable.
        //  If not abstract and not selectable, add a new item.
        foreach (KeyValuePair<string, ToolStripMenuItem> kvp in sortlst)
        {
            Type typ = (Type) kvp.Value.Tag;

            if (typ.IsAbstract && kvp.Value.DropDownItems.Count == 0)
                kvp.Value.Enabled = false;

            if (!typ.IsAbstract && kvp.Value.DropDownItems.Count > 0)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(kvp.Value.Text);
                item.Click += evtClick;
                item.Tag = typ;
                kvp.Value.DropDownItems.Insert(0, item);
            }
        }
    }
}