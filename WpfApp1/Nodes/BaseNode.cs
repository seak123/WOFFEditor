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
        Buff = 1,
        Caster = 2,
        Chain = 3,
        Damage = 4,
        Move = 5,
        Property = 6,
        Shield = 7,

        //misc node
        Ap = 50,
        Delay = 51,
        Wait = 52,
        Hide = 53,
        Queue = 54,
        Terminal = 55,
        ReAct = 56,
        RetargetAll = 57,
        RetargetMain = 58,
      
        Interrupt = 60,
        State = 61,
        Steady = 62,
        KeepAlive = 63,
        MirageDie = 64,
        Undying = 65,
        Death = 66,
        Relief = 67,
        Phase = 68,
        Yield = 69,
       
        CommonBuff = 72,
        
        SceneReset = 75,
        Camera = 76,
        Audio = 77,
       

        //root Node
        Skill = 100,
        Trigger = 101,
    }

    [Serializable]
    class BaseNode
    {
        public string nodeName="BaseNode";

        public string viewName = "BaseNode";

        public string executePath = "BasePath";

        protected List<Property> properties;

        protected BaseNode parentNode;
        protected List<BaseNode> childNodes;
        protected ChildNodeType childNodeType=ChildNodeType.subs;
        protected NodeType type;

        protected int uid;
        protected IFile sourceFile;

        public NodeType GetNodeType()
        {
            return type;
        }

        public virtual string ExportLuaStream() {
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
                stream = child.ExportLuaStream() + stream;
            }
            return stream;
        }

        protected BaseNode()
        {
            properties = new List<Property>();
            childNodes = new List<BaseNode>();
            uid = 0;
            
           
        }

        public void RebuildNode(BaseNode oldNode)
        {
            nodeName = oldNode.nodeName;
            viewName = oldNode.viewName;
            executePath = oldNode.executePath;
            for(int index = 0;index<properties.Count;++index)
            {
                string key = properties[index].ViewName;
                foreach(Property oldprop in oldNode.GetProperties())
                {
                    if (oldprop.ViewName == key) { properties[index].RebuildProp(oldprop); break; }
                }
            }
            parentNode = oldNode.GetParent();
            foreach(var node in oldNode.GetChilds())
            {
                AddChildNode(node);
            }
        }

        private bool ContainSpecialProp(PropertyType type)
        {
            foreach (var prop in properties)
            {
                if (prop.PropType == type)
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveSpecialProp(PropertyType type)
        {
            for(int index = properties.Count; index >= 0; --index)
            {
                if(properties[index].PropType == type)
                {
                    properties.RemoveAt(index);
                }
            }
        }

        private void SetParent(BaseNode parent)
        {
            parentNode = parent;
           
            //reset special property
            if(ContainSpecialProp(PropertyType.Buff))
            {
                if(parent.nodeName != "buff")
                {
                    RemoveSpecialProp(PropertyType.Buff);
                }
            }
            else
            {
                if(parent.nodeName == "buff")
                {
                    Property curr;

                    curr = new Property("触发时机", ViewDataType.EnumSelect, "buff_occasion", LuaDataType.String, "on_buff_add",PropertyType.Buff);
                    properties.Add(curr);
                    curr.AddEnumInstance("无", "none");
                    curr.AddEnumInstance("Add时", "on_buff_add");
                    curr.AddEnumInstance("Tick时", "on_tick");
                    curr.AddEnumInstance("受到伤害前", "prev_damaged");

                    curr = new Property("触发条件", ViewDataType.ListSelectWithArgs, "checkers", LuaDataType.ListFunction, "",PropertyType.Buff);
                    properties.Add(curr);
                    curr.AddEnumInstance("触发几率", "check_chance");
                    curr.AddEnumInstance("固定属性", "check_attr");
                    curr.AddEnumInstance("触发层数", "check_stack");
                    curr.AddEnumInstance("击杀目标", "check_death");
                    curr.AddEnumInstance("AP值触发", "check_ap");
                }
            }
        }

        public BaseNode GetParent()
        {
            return parentNode;
        }

        public List<Property> GetProperties()
        {
            return properties;
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

        public void DeleteChildNode(int uid)
        {
            for(int i = 0; i < childNodes.Count; ++i)
            {
                if (childNodes[i].GetUid() == uid)
                {
                    childNodes.RemoveAt(i);
                    return;
                }
            }
        }

        public void DeleteAllChildNode()
        {
            childNodes.Clear();
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

        public static BaseNode NodeCreator(NodeType type,IFile file)
        {
            BaseNode node = null;
            switch (type)
            {
                case NodeType.Action:
                    node = new ActionNode(file);
                    break;
                case NodeType.Buff:
                    node = new BuffNode(file);
                    break;
                case NodeType.Caster:
                    node = new CasterNode(file);
                    break;
                case NodeType.Chain:
                    node = new ChainNode(file);
                    break;
                case NodeType.Move:
                    node = new MoveNode(file);
                    break;
                case NodeType.Damage:
                    node = new DamageNode(file);
                    break;
                case NodeType.Property:
                    node = new PropertyNode(file);
                    break;
                case NodeType.Shield:
                    node = new ShieldNode(file);
                    break;
                //misc Node
                case NodeType.Ap:
                    node = new MiscNode(file, MiscType.Ap);
                    break;
                case NodeType.Audio:
                    node = new MiscNode(file, MiscType.Audio);
                    break;
                case NodeType.Camera:
                    node = new MiscNode(file, MiscType.Camera);
                    break;
                case NodeType.CommonBuff:
                    node = new MiscNode(file, MiscType.CommonBuff);
                    break;
                case NodeType.Death:
                    node = new MiscNode(file, MiscType.Death);
                    break;
                case NodeType.Delay:
                    node = new MiscNode(file, MiscType.Delay);
                    break;
                case NodeType.Hide:
                    node = new MiscNode(file, MiscType.Hide);
                    break;
                case NodeType.Interrupt:
                    node = new MiscNode(file, MiscType.Interrupt);
                    break;
                case NodeType.KeepAlive:
                    node = new MiscNode(file, MiscType.KeepAlive);
                    break;
                case NodeType.MirageDie:
                    node = new MiscNode(file, MiscType.MirageDie);
                    break;
                case NodeType.Phase:
                    node = new MiscNode(file, MiscType.Phase);
                    break;
                case NodeType.ReAct:
                    node = new MiscNode(file, MiscType.ReAct);
                    break;
                case NodeType.Relief:
                    node = new MiscNode(file, MiscType.Relief);
                    break;
                case NodeType.RetargetAll:
                    node = new MiscNode(file, MiscType.RetargetAll);
                    break;
                case NodeType.RetargetMain:
                    node = new MiscNode(file, MiscType.RetargetMain);
                    break;
                case NodeType.SceneReset:
                    node = new MiscNode(file, MiscType.SceneReset);
                    break;
                case NodeType.State:
                    node = new MiscNode(file, MiscType.State);
                    break;
                case NodeType.Steady:
                    node = new MiscNode(file, MiscType.Steady);
                    break;
                case NodeType.Undying:
                    node = new MiscNode(file, MiscType.Undying);
                    break;
                case NodeType.Wait:
                    node = new MiscNode(file, MiscType.Wait);
                    break;
                case NodeType.Yield:
                    node = new MiscNode(file, MiscType.Yield);
                    break;
                case NodeType.Queue:
                    node = new MiscNode(file, MiscType.Queue);
                    break;
                case NodeType.Terminal:
                    node = new MiscNode(file, MiscType.Terminal);
                    break;
                case NodeType.Skill:
                    node = new SkillNode(file);
                    break;

            }
            return node;
        }
    }
}
