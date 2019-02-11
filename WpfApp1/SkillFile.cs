using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfApp1
{
    public enum ManualTarget
    {
        SelfOnly = 1,
        EnemySingle = 2,
        FriendSingle = 3,
        EnemyAll = 4,
        FriendAll = 5,
        EnemyHpMax = 6,
        FriendHpMax = 7,
        EnemyHpMin = 8,
        FriendHpMin = 9,
        RandomEnemy = 10
    }

    public enum PitchType
    {
        Self = 1,
        Enemy = 2,
        Friend = 3
    }

    interface IFile
    {
        string ExportLuaFile();
        int RequestNodeUid();
        BaseNode GetRoot();
        int GetUid();
    }
    [Serializable]
    class SkillFile:BaseFile
    {
        public SkillFile(int uid)
        {
            nodeUid = 0;
            fildUid = uid;
            treeRoot = new SkillNode(this);
            treeRoot.SetChildNodeType(ChildNodeType.subs);
        }

        public int Coold
        {
            set;
            get;
        }

        public int InitCoold
        {
            set;
            get;
        }

        public string UnitName
        {
            set;
            get;
        }

        public string SkillName
        {
            set;
            get;
        }

        public ManualTarget ManualTarget
        {
            set;
            get;
        }

        public PitchType PitchType
        {
            set;
            get;
        }

        public override void SaveFile()
        {
            string DicrectoryPath = DataManager.GetInstance().rootPath+@"\SkillOut\"+UnitName+@"\Skill";
            if (System.IO.Directory.Exists(DicrectoryPath) == false)
            {
                System.IO.Directory.CreateDirectory(DicrectoryPath);
            }
            string filePath = DicrectoryPath + @"\" + SkillName + ".dat";
            Stream fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();

            binFormat.Serialize(fStream, this);
        }

        public override string ExportLuaFile()
        {
            string stream = "";
            //insert require path
            Dictionary<string, string> requireCache = new Dictionary<string, string>();
            List<BaseNode> queue = new List<BaseNode>();
            List<BaseNode> allNodes = new List<BaseNode>();
            queue.Add(treeRoot);
            while (queue.Count != 0)
            {
                BaseNode currNode = queue[0];
                if (!requireCache.ContainsKey(currNode.nodeName))
                {
                    requireCache.Add(currNode.nodeName, currNode.executePath);
                }
                foreach(var child in currNode.GetChilds())
                {
                    queue.Add(child);
                }
                allNodes.Add(currNode);
                queue.RemoveAt(0);
            }
            foreach (var content in requireCache)
            {
                stream = stream + "local " + content.Key + " = require(\"" + content.Value + "\")\n";
            }
            stream = stream + "\n";


            //insert all node definition
            stream = stream + "local ";
            stream = stream + allNodes[0].nodeName + allNodes[0].GetUid();
            for(int i = 1;i<allNodes.Count;++i)
            {
                stream = stream + "," + allNodes[i].nodeName + allNodes[i].GetUid();
            }
            stream = stream + "\n\n";

            //insert all node export
            stream = stream + treeRoot.ExportLuaStream();

            stream = stream + "return " + treeRoot.nodeName + treeRoot.GetUid() +"\n";

            return stream;
        }

        
    }
}
