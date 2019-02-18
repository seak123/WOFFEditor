using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class MoveNode:BaseNode
    {
        public MoveNode(IFile file)
        {
            nodeName = "move";
            viewName = nodeName;
            type = NodeType.Move;
            executePath = "module.battle.data.skill.move_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            //inti buff vo
            curr = new Property("方向", ViewDataType.EnumSelect, "direction", LuaDataType.Integer, "0");
            properties.Add(curr);
            curr.AddEnumInstance("向前", "0");
            curr.AddEnumInstance("向后", "1");

            curr = new Property("轨迹类型", ViewDataType.EnumSelect, "move_type", LuaDataType.Integer, "1");
            properties.Add(curr);
            curr.AddEnumInstance("直线", "0");
            curr.AddEnumInstance("定点抛物线", "1");
            curr.AddEnumInstance("闪烁", "2");
            curr.AddEnumInstance("定高抛物线", "3");
            curr.AddEnumInstance("渐变跟踪", "4");

            curr = new Property("目的点偏移量(1高度2距离)", ViewDataType.TextInput, "offset", LuaDataType.Vector2, "0,0");
            properties.Add(curr);

            curr = new Property("时长（可选）", ViewDataType.IntInput, "duration", LuaDataType.Integer, "0");
            properties.Add(curr);

            curr = new Property("速度（可选）", ViewDataType.IntInput, "speed", LuaDataType.Integer, "0");
            properties.Add(curr);

        }
    }
}
