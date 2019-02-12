using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class SkillNode:BaseNode
    {
        public SkillNode(IFile file)
        {
            nodeName = "skill";
            viewName = nodeName;
            executePath = "module.battle.data.skill.skill_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            //inti aciton data

            //skill
            curr = new Property("技能名", ViewDataType.TextInput, "name", LuaDataType.String, "None");
            properties.Add(curr);

            curr = new Property("目标类型", ViewDataType.EnumSelect, "manual_target", LuaDataType.Integer, "0");
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

            curr = new Property("补间以外时间", ViewDataType.IntInput, "besides_tween_time", LuaDataType.Integer, "0");
            properties.Add(curr);



        }
    }
}
