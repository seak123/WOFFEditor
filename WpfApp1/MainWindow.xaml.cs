using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DataManager dataMgr;
        IFile skill;

        ContextMenu nodeViewMenu;

        Dictionary<int, Button> nodeDic;

        int currentSelectNodeUID = 0;

        int height = 0;


        public MainWindow()
        {
            InitializeComponent();

            #region InitData
            dataMgr = DataManager.GetInstance();

            dataMgr.NewFile();
            skill = dataMgr.GetCurrFile();

            nodeDic = new Dictionary<int, Button>();

            #endregion

            CreateContextMenu();

            CreateRootNode();

        }

        public void AddNode(NodeType type,int parentUid)
        {
            Button bt = new Button
            {
                Name = "test_button",
                Content = type.ToString(),
                Height = 30,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 10, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Visibility = Visibility.Visible
            };

            NodeCanvas.Children.Add(bt);


            int id = skill.AddChildNode(type, currentSelectNodeUID);

            nodeDic.Add(id, bt);

            bt.ContextMenu = nodeViewMenu;
            bt.MouseRightButtonUp += (s, e) => {
                currentSelectNodeUID = id;
            };
            bt.Click += (e, a) => {
                NodeName.Text = id.ToString();
                currentSelectNodeUID = id;
            };

            height = 0;
            UpdateNodeView(skill.GetRoot(),0);

        }

        public void DeleteNode(int uid)
        {

        }

        void CreateRootNode()
        {
            Button btRoot = new Button
            {
                Name = "root_button",
                Content = "root",
                Height = 30,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 10, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Visibility = Visibility.Visible
            };

            NodeCanvas.Children.Add(btRoot);

            nodeDic.Add(skill.GetRoot().GetUid(), btRoot);

            btRoot.ContextMenu = nodeViewMenu;
            btRoot.MouseRightButtonUp += (s, e) => { currentSelectNodeUID = skill.GetRoot().GetUid(); };
            btRoot.Click += (e, a) => {
                NodeName.Text = skill.GetRoot().GetUid().ToString();
                currentSelectNodeUID = skill.GetRoot().GetUid();
            };
        }

        void CreateContextMenu()
        {
            nodeViewMenu = new ContextMenu();
            foreach (var item in Enum.GetValues(typeof(NodeType)))
            {
                MenuItem mitem = new MenuItem();
                mitem.Header = item.ToString();
                mitem.Click += (e, a) => AddNode((NodeType)item, currentSelectNodeUID);
                nodeViewMenu.Items.Add(mitem);
            }
            MenuItem deleteItem = new MenuItem();
            deleteItem.Header = "删除";
            deleteItem.Click += (e, a) => { };
            nodeViewMenu.Items.Add(deleteItem);
        }

        
       
        void UpdateNodeView(BaseNode root,int depth)
        {
            nodeDic[root.GetUid()].Margin = new Thickness(110 * depth, height, 0, 0);

            if (root.GetChilds().Count == 0)
            {
                height += 40;
            }
            else
            {
                depth++;
                foreach (BaseNode child in root.GetChilds())
                {
                    UpdateNodeView(child, depth);
                }
            }

        }
    }
}
