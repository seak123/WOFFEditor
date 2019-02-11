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
        SkillFile skill;
        public MainWindow()
        {
            InitializeComponent();

            dataMgr = DataManager.GetInstance();

            dataMgr.NewFile();

            Button bt = new Button
            {
                Name = "test_button",
                Content = "test",
                Height = 30,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 10, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Visibility = Visibility.Visible
            };

            NodeView.Children.Add(bt);



        }

        public void AddNode()
        {
           
        }
    }
}
