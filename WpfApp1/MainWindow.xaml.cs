﻿using System;
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

        List<Path> paths;

        public MainWindow()
        {
            InitializeComponent();

            #region InitData
            dataMgr = DataManager.GetInstance();

            dataMgr.NewFile();
            skill = dataMgr.GetCurrFile();

            nodeDic = new Dictionary<int, Button>();

            paths = new List<Path>();
            #endregion

            CreateContextMenu();

            CreateRootNode();

            
        }

        public void AddNode(NodeType type,int parentUid)
        {
 
            int id = skill.AddChildNode(type, currentSelectNodeUID);

            nodeDic.Add(id, CreateNode(id));

            height = 0;
            UpdateNodeView(skill.GetRoot(),0);
            UpdateNodeLine();
        }

        Button CreateNode(int uid)
        {
            Button bt = new Button
            {
                Name = "test_button",
                Height = 30,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 10, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Visibility = Visibility.Visible
            };

            bt.ContextMenu = nodeViewMenu;
            bt.MouseRightButtonUp += (s, e) =>
            {
                currentSelectNodeUID = uid;
            };
            bt.Click += (e, a) =>
            {
                currentSelectNodeUID = uid;
                SetPropertyView(currentSelectNodeUID);
            };
            NodeCanvas.Children.Add(bt);

            return bt;
        }

        public void DeleteNode(int uid)
        {

        }

        void CreateRootNode()
        {
            nodeDic.Add(skill.GetRoot().GetUid(), CreateNode(skill.GetRoot().GetUid()));
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
            if (nodeDic.ContainsKey(root.GetUid()) == false)
            {
                nodeDic.Add(root.GetUid(), CreateNode(root.GetUid()));
            }

            nodeDic[root.GetUid()].Content = root.viewName;

            nodeDic[root.GetUid()].Margin = new Thickness(10+110 * depth, 20+height, 0, 0);

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

        void UpdateNodeLine()
        {
            foreach (var pt in paths)
            {
                NodeCanvas.Children.Remove(pt);
            }
            paths.Clear();

            foreach (var key in nodeDic.Keys)
            {
                int parentUID = 0;

                if (skill.FindNode(key).GetParent() != null)
                {
                    parentUID = skill.FindNode(key).GetParent().GetUid();
                    Drawline(new Point(nodeDic[parentUID].Margin.Left + 50, nodeDic[parentUID].Margin.Top + 15),
                       new Point(nodeDic[key].Margin.Left + 50, nodeDic[key].Margin.Top + 15));
                }

            }
        }

        void SetPropertyView(int uid)
        {
            PropertyGrid.Children.Clear();
            PropertyGrid.RowDefinitions.Clear();
            PropertyGrid.ColumnDefinitions.Clear();

            BaseNode node =  skill.FindNode(uid);

            PropertyGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            PropertyGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            int rowindex = 0;
            foreach (var property in node.GetProperties())
            {
                PropertyGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                Label label = new Label
                {
                    Content = property.ViewName,
                };

                PropertyGrid.Children.Add(label);
                label.SetValue(Grid.RowProperty, rowindex);
                

                switch (property.ViewType) {
                    case ViewDataType.EnumSelect:
                        ComboBox cb = new ComboBox
                        {
                            Width = 100,
                            Height = 30,
                            
                            ItemsSource = property.enumDictionary,
                            
                        };
                        
                        cb.SelectedValuePath = "Key";
                        cb.DisplayMemberPath = "Key";

                        cb.SelectedValue = property.GetValue();
                        
                        cb.SelectionChanged += (e, a) => { property.SetValue(cb.SelectedValue.ToString()); };
                        PropertyGrid.Children.Add(cb);
                        cb.SetValue(Grid.RowProperty,rowindex);
                        cb.SetValue(Grid.ColumnProperty, 1);
                        break;
                    case ViewDataType.IntInput:
                        TextBox int_tb = new TextBox
                        {
                            MinWidth = 100,
                            MinHeight = 30
                        };
                        int_tb.Text = property.GetValue();
                        int_tb.TextChanged += (e, a) => { property.SetValue(int_tb.Text); };
                        PropertyGrid.Children.Add(int_tb);
                        int_tb.SetValue(Grid.RowProperty, rowindex);
                        int_tb.SetValue(Grid.ColumnProperty, 1);
                        break;
                    case ViewDataType.ListSelectWithArgs:
                        TextBox temp = new TextBox
                        {
                            MinWidth = 100,
                            MinHeight = 30
                        };

                        PropertyGrid.Children.Add(temp);
                        temp.SetValue(Grid.RowProperty, rowindex);
                        temp.SetValue(Grid.ColumnProperty, 1);
                        break;
                    case ViewDataType.TextInput:
                        TextBox txt_tb = new TextBox
                        {
                            MinWidth = 100,
                            MinHeight = 30
                        };
                        txt_tb.Text = property.GetValue();
                        txt_tb.TextChanged += (e, a) => { property.SetValue(txt_tb.Text); };
                        PropertyGrid.Children.Add(txt_tb);
                        txt_tb.SetValue(Grid.RowProperty, rowindex);
                        txt_tb.SetValue(Grid.ColumnProperty, 1);
                        break;
                        
                }

                rowindex++;
            }
        }

 
        void Drawline(Point start,Point end)
        {
            Point turn = new Point(start.X, end.Y);
            if (start.Y != end.Y)
            {
                LineGeometry line1 = new LineGeometry();
                line1.StartPoint = start;
                line1.EndPoint = turn;

                Path pt1 = new Path();
                pt1.Stroke = Brushes.Black;
                pt1.StrokeThickness = 1;
                pt1.Data = line1;
                
                NodeCanvas.Children.Add(pt1);
                Canvas.SetZIndex(pt1, -1);
                paths.Add(pt1);
            }
            
            LineGeometry line2 = new LineGeometry();
            line2.StartPoint = turn;
            line2.EndPoint = end;

            Path pt2 = new Path();
            pt2.Stroke = Brushes.Black;
            pt2.StrokeThickness = 1;
            pt2.Data = line2;

            NodeCanvas.Children.Add(pt2);
            Canvas.SetZIndex(pt2, -1);
            paths.Add(pt2);
        }

        private void MenuItem_Click_New(object sender, RoutedEventArgs e)
        {
            dataMgr.NewFile();
        }
        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".dat";
            dlg.Filter = "技能文件(*.dat)|*dat";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                dataMgr.ReadFile(filename);
                skill = dataMgr.GetCurrFile();

                NodeCanvas.Children.Clear();
                nodeDic.Clear();
                UpdateNodeView(dataMgr.GetCurrFile().GetRoot(), 0);
                UpdateNodeLine();
            }

            
        }
        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            dataMgr.SaveFile();
        }

        private void MenuItem_Click_SaveAll(object sender, RoutedEventArgs e)
        {
            dataMgr.SaveAllFile();
        }

        private void MenuItem_Click_Export(object sender, RoutedEventArgs e)
        {
            //skill.ExportLuaFile();
            dataMgr.ExportLuaFile();
        }
    }
}
