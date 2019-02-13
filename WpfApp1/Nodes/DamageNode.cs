using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class DamageNode:BaseNode
    {
        public DamageNode(IFile file)
        {
            nodeName = "damage";
            viewName = nodeName;
            type = NodeType.Damage;
            executePath = "module.battle.data.skill.damage_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            //inti  vo
            curr = new Property("伤害公式", ViewDataType.EnumSelectWithArgs, "calc_func", LuaDataType.Function, "calc.make_common_attack0,0|");
            properties.Add(curr);
            curr.AddEnumInstance("常用", "calc.make_common_attack");

        }
    }
}
