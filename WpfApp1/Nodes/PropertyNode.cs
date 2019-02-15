using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class PropertyNode:BaseNode
    {
        public PropertyNode(IFile file)
        {
            nodeName = "property";
            viewName = nodeName;
            type = NodeType.Property;
            executePath = "module.battle.data.skill.property_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            //inti vo

            curr = new Property("属性", ViewDataType.EnumSelect, "prop_type", LuaDataType.String, "2");
            properties.Add(curr);
            curr.AddEnumInstance("血量", "hp_add");
            curr.AddEnumInstance("血量比", "hp_rate");
            curr.AddEnumInstance("攻击", "attack_add");
            curr.AddEnumInstance("攻击比", "attack_rate");
            curr.AddEnumInstance("防御", "defence_add");
            curr.AddEnumInstance("防御比", "defence_rate");
            curr.AddEnumInstance("速度", "speed_add");
            curr.AddEnumInstance("速度比", "speed_rate");
            curr.AddEnumInstance("暴击率", "crit_rate");
            curr.AddEnumInstance("暴击伤害率", "crit_rate_rate");
            curr.AddEnumInstance("效果命中", "sp_hit_rate");
            curr.AddEnumInstance("效果抵抗", "resist_rate");
            curr.AddEnumInstance("减暴率", "tough_rate");
            curr.AddEnumInstance("格挡", "block_rate");
            curr.AddEnumInstance("格挡率", "block_rate_rate");
            curr.AddEnumInstance("伤害加成", "damage_rate_rate");
            curr.AddEnumInstance("受伤加成", "suffer_rate_rate");
            curr.AddEnumInstance("治疗增益", "heal_rate_rate");
            curr.AddEnumInstance("治疗减扣", "heal_sub_rate");
            curr.AddEnumInstance("吸血", "suckblood_rate");

            curr = new Property("改值方式", ViewDataType.EnumSelect, "change_type", LuaDataType.Integer, "2");
            properties.Add(curr);
            curr.AddEnumInstance("累计", "1");
            curr.AddEnumInstance("重载", "2");

            curr = new Property("计算公式", ViewDataType.EnumSelectWithArgs, "calc_func", LuaDataType.Function, "calc.make_common_calc0|");
            properties.Add(curr);
            curr.AddEnumInstance("常数值（1值）", "calc.make_common_calc");
            curr.AddEnumInstance("以攻击为基准（1倍率2值）", "calc.make_common_attack");

        }
    }
}
