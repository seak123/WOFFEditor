using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class ChainNode:BaseNode
    {
        public ChainNode(IFile file)
        {
            nodeName = "chain";
            viewName = nodeName;
            type = NodeType.Chain;
            executePath = "module.battle.data.skill.chain_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            //inti chain vo
            curr = new Property("特效 ID", ViewDataType.IntInput, "fx_id", LuaDataType.Integer, "0");
            properties.Add(curr);

            curr = new Property("速度（列表）", ViewDataType.TextInput, "speeds", LuaDataType.ListInt, "");
            properties.Add(curr);

            curr = new Property("轨迹", ViewDataType.EnumSelect, "track", LuaDataType.RawString, "chain.TRACE.CURVE");
            properties.Add(curr);
            curr.AddEnumInstance("抛物线", "chain.TRACE.CURVE");

            curr = new Property("缩放比（列表）", ViewDataType.TextInput, "scales", LuaDataType.ListInt, "");
            properties.Add(curr);


        }
    }
}
