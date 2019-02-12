using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    public enum ChildNodeType
    {
        belongs = 0,
        subs = 1
    }

    [Serializable]
    public enum NodeType
    {
        Action = 0,
    }

    [Serializable]
    class BaseNode
    {
        public string nodeName="BaseNode";
        public string executePath = "BasePath";

        protected List<Property> properties;

        protected BaseNode parentNode;
        protected List<BaseNode> childNodes;
        protected ChildNodeType childNodeType=ChildNodeType.subs;

        protected int uid;
        protected IFile sourceFile;


        public string ExportLuaStream() {
            string stream = "";
            string argName = nodeName + uid;
            stream = stream + argName + " = " + nodeName + ".new()\n";
            foreach (var prop in properties)
            {
                stream = stream + argName + "." + prop.GetLuaStream() + "\n";
            }
            if (childNodes.Count != 0)
            {
                switch (childNodeType)
                {
                    case ChildNodeType.subs:
                        stream = stream + argName + ":append(" + "\"subs\"";
                        foreach (var child in childNodes)
                        {
                            stream = stream + "," + child.nodeName + child.GetUid();
                        }
                        stream = stream + ")\n";
                        break;
                    case ChildNodeType.belongs:
                        stream = stream + argName + ":append(" + "\"belongs\"";
                        foreach (var child in childNodes)
                        {
                            stream = stream + "," + child.nodeName + child.GetUid();
                        }
                        stream = stream + ")\n";
                        break;
                }
            }
            stream = stream + "\n";
            foreach (var child in childNodes)
            {
                stream = stream + child.ExportLuaStream();
            }
            return stream;
        }

        protected BaseNode()
        {
            properties = new List<Property>();
            childNodes = new List<BaseNode>();
            uid = 0;
            //base property
            Property curr;
           
            curr = new Property("触发时机", ViewDataType.EnumSelect, "buff_occasion", LuaDataType.String, "None");
            properties.Add(curr);
            curr.AddEnumInstance("无", "none");
            curr.AddEnumInstance("Add时", "on_buff_add");
            curr.AddEnumInstance("Tick时", "on_tick");

            curr = new Property("触发条件", ViewDataType.ListSelect, "checkers", LuaDataType.List, "s");
    
        }
        private void SetParent(BaseNode parent)
        {
            parentNode = parent;
            //parent.AddChildNode(this);
        }

        public BaseNode GetParent()
        {
            return parentNode;
        }

        public int GetUid()
        {
            return uid;
        }

        public List<BaseNode> GetChilds()
        {
            return childNodes;
        }

        public void SetChildNodeType(ChildNodeType type)
        {
            childNodeType = type;
        }

        public void AddChildNode(BaseNode child,int index = -1)
        {
            if (index == -1)
            {
                child.SetParent(this);
                childNodes.Add(child);
            }
            else
            {
                child.SetParent(this);
                childNodes.Insert(index, child);
            }
        }

        public void AddBrotherAbove(BaseNode node)
        {
            List<BaseNode> brothers = parentNode.GetChilds();
            int index = brothers.FindIndex(_n => _n == this);
            parentNode.AddChildNode(node, index);
        }

        public void AddBrotherBelow(BaseNode node)
        {
            List<BaseNode> brothers = parentNode.GetChilds();
            int index = brothers.FindIndex(_n => _n == this);
            parentNode.AddChildNode(node, index+1);
        }

        public void ShiftUp()
        {
            List<BaseNode> brothers = parentNode.GetChilds();
            int index = brothers.FindIndex(_n => _n == this);
            if (index == 0) return;
            brothers.RemoveAt(index);
            brothers.Insert(index - 1, this);
        }

        public void ShiftDown()
        {
            List<BaseNode> brothers = parentNode.GetChilds();
            int index = brothers.FindIndex(_n => _n == this);
            if (index == brothers.Count-1) return;
            brothers.RemoveAt(index);
            brothers.Insert(index + 1, this);
        }
    }
}
