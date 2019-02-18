using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class BaseFile:IFile
    {
        public int RequestNodeUid()
        {
            return ++nodeUid;
        }

        public void ResetUid(int uid)
        {
            fildUid = uid;
        }

        public virtual void SaveFile() { }

        public virtual string ExportLuaFile() {
            return null;
        }

        public int GetUid()
        {
            return fildUid;
        }

        public BaseNode GetRoot()
        {
            return treeRoot;
        }

        public BaseNode FindNode(int uid)
        {
            List<BaseNode> searchQue = new List<BaseNode>();
            searchQue.Add(treeRoot);
            while (searchQue.Count > 0)
            {
                if (searchQue[0].GetUid() == uid) return searchQue[0];
                else
                {
                    foreach (var childNode in searchQue[0].GetChilds())
                    {
                        searchQue.Add(childNode);
                    }
                    searchQue.RemoveAt(0);
                }
            }
            return null;
        }

        
        //delete node
        public void DeleteNode(int uid)
        {
            BaseNode node = FindNode(uid);
            if (node != null)
            {
                node.GetParent().DeleteChildNode(uid);
            }
            
        }
        //replace node
        public void ReplaceNode(NodeType type,int uid)
        {
            BaseNode newNode = BaseNode.NodeCreator(type,this);
            BaseNode oldNode = FindNode(uid);
            BaseNode parentNode = oldNode.GetParent();
            if(oldNode != null)
            {
                List<BaseNode> list = oldNode.GetChilds();
                for(int i = 0; i < list.Count; ++i)
                {
                    newNode.AddChildNode(list[i]);
                }
            }
            for(int i = 0; i < parentNode.GetChilds().Count; ++i)
            {
                if (parentNode.GetChilds()[i].GetUid() == uid)
                {
                    parentNode.GetChilds()[i] = newNode;
                    return;
                }
            }
            
        }
        // add child
        public int AddChildNode(NodeType type, int parentUid)
        {
            BaseNode node = BaseNode.NodeCreator(type, this);
            FindNode(parentUid).AddChildNode(node);
            return node.GetUid();
        }
        // add brother
        public int AddBrotherAbove(NodeType type, int parentUid)
        {
            BaseNode node = BaseNode.NodeCreator(type, this);
            FindNode(parentUid).AddBrotherAbove(node);
            return node.GetUid();
        }

        public int AddBrotherBelow(NodeType type,int parentUid)
        {
            BaseNode node = BaseNode.NodeCreator(type, this);
            FindNode(parentUid).AddBrotherBelow(node);
            return node.GetUid();
        }

        public void NodeShiftUp(int nodeUid) {
            BaseNode node = FindNode(nodeUid);
            if (node!=null)
            {
                node.ShiftUp();
            }
        }

        public void NodeShiftDown(int nodeUid)
        {
            BaseNode node = FindNode(nodeUid);
            if (node != null)
            {
                node.ShiftDown();
            }
        }

        //refresh node
        public void RefreshNodes()
        {
            List<BaseNode> cacheQue = new List<BaseNode>();

            BaseNode newRoot = BaseNode.NodeCreator(treeRoot.GetNodeType(), this);
            newRoot.RebuildNode(treeRoot);
            treeRoot = newRoot;

            cacheQue.Add(treeRoot);

            while (cacheQue.Count > 0)
            {
                BaseNode currNode = cacheQue[0];
                List<BaseNode> oldChilds = new List<BaseNode>();
                foreach(var oldNode in currNode.GetChilds())
                {
                    oldChilds.Add(oldNode);
                }
                currNode.DeleteAllChildNode();
                foreach(var child in oldChilds)
                {
                    BaseNode node = BaseNode.NodeCreator(child.GetNodeType(), this);
                    currNode.AddChildNode(node);
                    cacheQue.Add(node);
                    node.RebuildNode(child);
                }
                cacheQue.RemoveAt(0);
            }
        }

        //protected Dictionary<string, string> baseRequirePath;

        protected BaseNode treeRoot;
        protected int nodeUid;
        protected int fildUid;
    }
}
