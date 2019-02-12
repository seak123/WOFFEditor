using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class MoveNode:BaseNode
    {
        public MoveNode(IFile file)
        {
            nodeName = "move";
            viewName = nodeName;
            executePath = "module.battle.data.skill.move_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            //inti buff vo
            curr = new Property("方向", ViewDataType.EnumSelect, "direction", LuaDataType.Integer, "0");
            properties.Add(curr);
            curr.AddEnumInstance("向前", "1");
            curr.AddEnumInstance("向后", "0");

            curr = new Property("轨迹类型", ViewDataType.EnumSelect, "move_type", LuaDataType.Integer, "0");
            properties.Add(curr);
            curr.AddEnumInstance("直线", "1");

            curr = new Property("时长", ViewDataType.IntInput, "duration", LuaDataType.Integer, "1");
            properties.Add(curr);

        }
    }
}
