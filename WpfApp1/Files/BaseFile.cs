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

        //view node change
        private BaseNode NodeCreator(NodeType type)
        {
            BaseNode node = null;
            switch (type)
            {
                case NodeType.Action:
                    node = new ActionNode(this);
                    break;
                case NodeType.Buff:
                    node = new BuffNode(this);
                    break;
                case NodeType.Caster:
                    node = new CasterNode(this);
                    break;
                case NodeType.Chain:
                    node = new ChainNode(this);
                    break;
                //misc Node
                case NodeType.Queue:
                    node = new MiscNode(this, MiscType.Queue);
                    break;
                case NodeType.Wait:
                    node = new MiscNode(this, MiscType.Wait);
                    break;
                case NodeType.Terminal:
                    node = new MiscNode(this, MiscType.Terminal);
                    break;
               
            }
            return node;
        }
        // add child
        public int AddChildNode(NodeType type, int parentUid)
        {
            BaseNode node = NodeCreator(type);
            FindNode(parentUid).AddChildNode(node);
            return node.GetUid();
        }
        // add brother
        public int AddBrotherAbove(NodeType type, int parentUid)
        {
            BaseNode node = NodeCreator(type);
            FindNode(parentUid).AddBrotherAbove(node);
            return node.GetUid();
        }

        public int AddBrotherBelow(NodeType type,int parentUid)
        {
            BaseNode node = NodeCreator(type);
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


        protected BaseNode treeRoot;
        protected int nodeUid;
        protected int fildUid;
    }
}
