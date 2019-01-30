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
        public MainWindow()
        {
            InitializeComponent();
            /*
            string rootPath = @"D:";

            string filePath = @"D:\Ifrit1.dat";
            Stream fStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();
            SkillFile newFile = (SkillFile)binFormat.Deserialize(fStream);
            Console.Write(newFile.ExportLuaFile());*/

            /*
            SkillFile newFile = new SkillFile();
            newFile.Coold = 1;
            newFile.InitCoold = 0;
            newFile.SkillName = "fire_paw";
            newFile.ManualTarget = ManualTarget.EnemySingle;
            newFile.PitchType = PitchType.Enemy;

            ActionNode node = new ActionNode(newFile);
            node.SetChildNodeType(ChildNodeType.subs);
            ActionNode node2 = new ActionNode(newFile);
            node.AddChildNode(node2);
            ActionNode node3 = new ActionNode(newFile);
            node2.AddBrotherAbove(node3);

            newFile.SetRoot(node);
            Console.Write(newFile.ExportLuaFile());
            string filePath = @"D:\Ifrit1.dat";
            Stream fStream = new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();
            
            binFormat.Serialize(fStream, newFile);*/

            SkillFile newFile = new SkillFile();
            newFile.Coold = 1;
            newFile.InitCoold = 0;
            newFile.SkillName = "fire_paw";
            newFile.UnitName = "Ifrit";
            newFile.ManualTarget = ManualTarget.EnemySingle;
            newFile.PitchType = PitchType.Enemy;

            ActionNode node = new ActionNode(newFile);
            node.SetChildNodeType(ChildNodeType.subs);
            ActionNode node2 = new ActionNode(newFile);
            node.AddChildNode(node2);
            ActionNode node3 = new ActionNode(newFile);
            node2.AddBrotherAbove(node3);

            newFile.SetRoot(node);
            newFile.SaveFile();

        }
    }
}
