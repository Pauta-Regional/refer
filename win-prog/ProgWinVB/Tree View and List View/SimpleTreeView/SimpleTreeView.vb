'-----------------------------------------------
' SimpleTreeView.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SimpleTreeView
    Inherits Form

    Shared Sub Main()
        Application.Run(New SimpleTreeView())
    End Sub

    Sub New()
        Text = "Simple Tree View"

        Dim treevu As New TreeView()
        treevu.Parent = Me
        treevu.Dock = DockStyle.Fill

        treevu.Nodes.Add("Animal")
        treevu.Nodes(0).Nodes.Add("Dog")
        treevu.Nodes(0).Nodes(0).Nodes.Add("Poodle")
        treevu.Nodes(0).Nodes(0).Nodes.Add("Irish Setter")
        treevu.Nodes(0).Nodes(0).Nodes.Add("German Shepherd")
        treevu.Nodes(0).Nodes.Add("Cat")
        treevu.Nodes(0).Nodes(1).Nodes.Add("Calico")
        treevu.Nodes(0).Nodes(1).Nodes.Add("Siamese")
        treevu.Nodes(0).Nodes.Add("Primate")
        treevu.Nodes(0).Nodes(2).Nodes.Add("Chimpanzee")
        treevu.Nodes(0).Nodes(2).Nodes.Add("Ape")
        treevu.Nodes(0).Nodes(2).Nodes.Add("Human")
        treevu.Nodes.Add("Mineral")
        treevu.Nodes(1).Nodes.Add("Calcium")
        treevu.Nodes(1).Nodes.Add("Zinc")
        treevu.Nodes(1).Nodes.Add("Iron")
        treevu.Nodes.Add("Vegetable")
        treevu.Nodes(2).Nodes.Add("Carrot")
        treevu.Nodes(2).Nodes.Add("Asparagus")
        treevu.Nodes(2).Nodes.Add("Broccoli")
    End Sub
End Class
