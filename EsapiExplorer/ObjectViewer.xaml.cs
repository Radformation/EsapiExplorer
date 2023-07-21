using System.Windows;
using System.Windows.Controls;

namespace EsapiExplorer
{
    public partial class ObjectViewer : UserControl
    {
        public ObjectViewer()
        {
            InitializeComponent();
        }

        public void Load(object obj, string name)
        {
            var o = new Composite(obj, name);
            var treeViewItem = CreateTreeViewItem(o);
            ObjectTreeView.Items.Add(treeViewItem);
            treeViewItem.IsExpanded = true;
        }

        private TreeViewItem CreateTreeViewItem(IObject o)
        {
            if (o is Composite composite)
            {
                var treeViewItem = new TreeViewItem {Header = composite};
                treeViewItem.Items.Add("Loading...");
                treeViewItem.Expanded += ItemOnExpanded;
                return treeViewItem;
            }
            else if (o is Simple simple)
            {
                return new TreeViewItem {Header = simple};
            }

            return null;
        }

        private void ItemOnExpanded(object sender, RoutedEventArgs e)
        {
            var tvItem = (TreeViewItem)sender;
            if (tvItem.Items.Count == 1 && tvItem.Items[0] is string)
            {
                tvItem.Items.Clear();

                if (tvItem.Header is Composite composite)
                {
                    foreach (var item in composite.Items)
                        tvItem.Items.Add(CreateTreeViewItem(item));
                }
            }
        }
    }
}
