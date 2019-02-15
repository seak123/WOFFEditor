using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class ShieldNode:BaseNode
    {
        public ShieldNode(IFile file)
        {
            nodeName = "shield";
            viewName = nodeName;
            type = NodeType.Shield;
            executePath = "module.battle.data.skill.shield_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();
            SetChildNodeType(ChildNodeType.belongs);
            Property curr;
            //inti buff vo
            curr = new Property("计算公式", ViewDataType.EnumSelectWithArgs, "calc_func", LuaDataType.Function, "calc.make_common_calc0|");
            properties.Add(curr);
            curr.AddEnumInstance("常数值（1值）", "calc.make_common_calc");
            curr.AddEnumInstance("以攻击为基准（1倍率2值）", "calc.make_common_attack");


        }
    }
}
