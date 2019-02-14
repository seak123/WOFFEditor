using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfApp1
{
    [Serializable]
    class EditorSettings
    {
        //export path
        public string rootPath = @"D:\WOFFEditor";
    }

    class DataManager
    {
        private DataManager()
        {
            allFiles = new List<IFile>();
            fileUid = 0;
            string FilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Settings.dat";
           
            BinaryFormatter binFormat = new BinaryFormatter();

            
            if (System.IO.File.Exists(FilePath) == false)
            {
                Stream _Stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                binFormat.Serialize(_Stream, new EditorSettings());
            }
            Stream fStream = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite);
            EditorSettings settings = (EditorSettings)binFormat.Deserialize(fStream);
            if (settings == null)
            {
                //alert
            }
            //init settings
            rootPath = settings.rootPath;
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
            currFile = new SkillFile(fileUid++);
            
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

        public void ExportLuaAllFile()
        {
            foreach(var file in allFiles)
            {
                file.ExportLuaFile();
            }
        }

        public void SaveFile()
        {
            currFile.SaveFile();
        }

        public void SaveAllFile()
        {
            foreach(var file in allFiles)
            {
                file.SaveFile();
            }
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
            newFile.ResetUid(fileUid++);
            newFile.RefreshNodes();
            allFiles.Add(newFile);
            currFile = newFile;
        }

        public IFile FindFile(int uid)
        {
            foreach(var file in allFiles)
            {
                if (file.GetUid() == uid) return file;
            }
            return null;
        }

        public string rootPath;

        private static DataManager instance;
        private IFile currFile;
        private List<IFile> allFiles;
        private int fileUid;
    }
}
