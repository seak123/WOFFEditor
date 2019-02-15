using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class TriggherNode:BaseNode
    {
        public TriggherNode(IFile file)
        {
            nodeName = "trigger";
            viewName = nodeName;
            type = NodeType.Trigger;
            executePath = "module.battle.data.skill.trigger_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            //inti aciton data

            //skill
            curr = new Property("被动技能名", ViewDataType.TextInput, "name", LuaDataType.String, "None_0");
            properties.Add(curr);

            curr = new Property("触发类型", ViewDataType.EnumSelect, "manual_target", LuaDataType.Integer, "0");
            properties.Add(curr);
            curr.AddEnumInstance("自己", "0");
            curr.AddEnumInstance("单个敌方", "1");
            curr.AddEnumInstance("单个友方", "2");
            curr.AddEnumInstance("所有敌方", "11");
            curr.AddEnumInstance("所有友方", "12");
            curr.AddEnumInstance("血量递减敌方", "13");
            curr.AddEnumInstance("血量递减友方", "14");
            curr.AddEnumInstance("血量递增敌方", "15");
            curr.AddEnumInstance("血量递增友方", "16");
            curr.AddEnumInstance("随机敌方", "17");

            curr = new Property("点击目标", ViewDataType.EnumSelect, "pitch_type", LuaDataType.Integer, "1");
            properties.Add(curr);
            curr.AddEnumInstance("自己", "1");
            curr.AddEnumInstance("敌方", "2");
            curr.AddEnumInstance("友方", "3");

            curr = new Property("初始CD", ViewDataType.IntInput, "ini_coold", LuaDataType.Integer, "0");
            properties.Add(curr);

            curr = new Property("技能CD", ViewDataType.IntInput, "coold", LuaDataType.Integer, "1");
            properties.Add(curr);





        }
    }
}
