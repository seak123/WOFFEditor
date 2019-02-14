using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class ActionNode:BaseNode
    {
        public ActionNode(IFile file)
        {
            nodeName = "action";
            viewName = nodeName;
            type = NodeType.Action;
            executePath = "module.battle.data.skill.action_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();
            
            Property curr;
            //inti aciton data

            //action
            curr = new Property("动作文件名", ViewDataType.TextInput, "anim_name", LuaDataType.String, "");
            properties.Add(curr);

            curr = new Property("动作", ViewDataType.EnumSelect, "action", LuaDataType.String,"IsAppear");
            properties.Add(curr);
            curr.AddEnumInstance("出场", "IsAppear");
            curr.AddEnumInstance("死亡", "IsDeath");
            curr.AddEnumInstance("受击", "IsHurted");
            curr.AddEnumInstance("撤回", "IsMoveBack");
            curr.AddEnumInstance("技能1", "Skill_1");
            curr.AddEnumInstance("技能2", "Skill_2");
            curr.AddEnumInstance("技能3", "Skill_3");
            curr.AddEnumInstance("技能4", "Skill_4");
            curr.AddEnumInstance("技能5", "Skill_5");
            curr.AddEnumInstance("技能6", "Skill_6");

            curr = new Property("动作阶段", ViewDataType.EnumSelect, "stage", LuaDataType.Integer, "1");
            properties.Add(curr);
            curr.AddEnumInstance("1", "1");
            curr.AddEnumInstance("2", "2");
            curr.AddEnumInstance("3", "3");

            curr = new Property("播放单位", ViewDataType.EnumSelect, "perform_unit", LuaDataType.Integer, "0");
            properties.Add(curr);
            curr.AddEnumInstance("施法者", "0");
            curr.AddEnumInstance("被施法者", "1");


            curr = new Property("补间以外时间", ViewDataType.IntInput, "besides_tween_time", LuaDataType.Integer, "0");
            properties.Add(curr);




        }
        /*
        public override string ExportLuaStream()
        {
            string stream = "";
            string argName = nodeName + uid;
            stream = stream + argName + " = " + nodeName + ".new()\n";
            foreach(var prop in properties)
            {
                stream = stream + argName + "." + prop.GetLuaStream() + "\n";
            }
            switch (childNodeType)
            {
                case ChildNodeType.subs:
                    stream = stream + argName + ":append(" + "\"subs\"";
                    foreach(var child in childNodes)
                    {
                        stream = stream + "," + child.nodeName + child.GetUid();
                    }
                    break;
                case ChildNodeType.belongs:
                    stream = stream + argName + ":append(" + "\"belongs\"";
                    foreach (var child in childNodes)
                    {
                        stream = stream + "," + child.nodeName + child.GetUid();
                    }
                    break;
            }
            stream = stream + "\n";
            foreach(var child in childNodes)
            {
                stream = stream + child.ExportLuaStream();
            }
            return stream;
        }*/

    }
}
