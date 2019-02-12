using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class CasterNode:BaseNode
    {
        public CasterNode(IFile file)
        {
            nodeName = "caster";
            viewName = nodeName;
            executePath = "module.battle.data.skill.caster_vo";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            //inti buff vo
            curr = new Property("技能名称", ViewDataType.TextInput, "skill", LuaDataType.Skill, "");
            properties.Add(curr);

            curr = new Property("触发时机", ViewDataType.EnumSelect, "timing", LuaDataType.RawString, "caster.INSTANT");
            properties.Add(curr);
            curr.AddEnumInstance("立即触发", "caster.INSTANT");
            curr.AddEnumInstance("队列触发", "caster.QUEUED");

            curr = new Property("触发次数", ViewDataType.IntInput, "limit", LuaDataType.Integer, "1");
            properties.Add(curr);

            curr = new Property("目标选择", ViewDataType.EnumSelect, "target", LuaDataType.String, "common");
            properties.Add(curr);
            curr.AddEnumInstance("保持不变", "common");
            curr.AddEnumInstance("追击", "counter");


        }
    }
}
