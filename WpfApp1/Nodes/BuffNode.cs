using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class BuffNode:BaseNode
    {
        public BuffNode(IFile file)
        {
            nodeName = "buff";
            viewName = nodeName;
            executePath = "module.battle.data.skill.buff_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();
            SetChildNodeType(ChildNodeType.belongs);
            Property curr;
            //inti buff vo
            curr = new Property("Buff ID", ViewDataType.IntInput, "buff_id", LuaDataType.Integer, "0");
            properties.Add(curr);

            curr = new Property("持续回合", ViewDataType.IntInput, "duration", LuaDataType.Integer, "0");
            properties.Add(curr);

            curr = new Property("层数", ViewDataType.IntInput, "stack_num", LuaDataType.Integer, "1");
            properties.Add(curr);


        }
    }
}
