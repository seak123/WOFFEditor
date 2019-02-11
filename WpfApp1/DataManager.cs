using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfApp1
{
    class DataManager
    {
        private DataManager()
        {
            allFiles = new List<IFile>();
        }

        public static DataManager GetInstance()
        {
            if (instance != null) return instance;
            else
            {
                instance = new DataManager();
            }
            return instance;
        }

        public void NewFile()
        {
            currFile = new SkillFile();
            allFiles.Add(currFile);
        }

        public IFile GetCurrFile()
        {
            return currFile;
        }

        public void ExportLuaFile()
        {
            currFile.ExportLuaFile();
        }

        public void ReadFile(string filePath)
        {
            //string filePath = @"D:\Ifrit1.dat";
            Stream fStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();
            SkillFile newFile = (SkillFile)binFormat.Deserialize(fStream);
            if(newFile == null)
            {
                //alert
            }
            allFiles.Add(newFile);
            currFile = newFile;
        }

        public string rootPath = @"D:\";

        private static DataManager instance;
        private IFile currFile;
        private List<IFile> allFiles;
    }
}
