using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    class TriggerFile:BaseFile
    {
        public TriggerFile(int uid)
        {
            nodeUid = 0;
            fildUid = uid;
            treeRoot = new SkillNode(this);
            treeRoot.SetChildNodeType(ChildNodeType.subs);

            //baseRequirePath = new Dictionary<string, string>();
            //baseRequirePath.Add("calc", "module.battle.data.skill.calculate");
        }
    }
}
