using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum MiscType
    {
        Ap = 0,
        Delay = 1,
        Wait = 2,
        Hide = 3,
        Queue = 4,
        Terminal = 5,
        ReAct = 6,
        RetargetAll = 7,
        RetargetMain = 8,
        Revive = 9,
        Interrupt = 10,
        State = 11,
        Steady = 12,
        KeepAlive = 13,
        MirageDie = 14,
        Undying = 15,
        Death = 16,
        Relief = 17,
        Phase = 18,
        Yield = 19,
        Sync = 20,
        Hurt = 21,
        CommonBuff = 22,
        Rotate = 23,
        Split = 24,
        SceneReset = 25,
        Camera = 26,
        Audio = 27,
        TapWord = 28,
    }

    class MiscNode:BaseNode
    {
        public MiscNode(IFile file,MiscType type = MiscType.Ap)
        {
            nodeName = "misc";
            viewName = nodeName;
            executePath = "module.battle.data.skill.misc_vos";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            switch (type)
            {
                case MiscType.Wait:
                    viewName = "wait";
                    curr = new Property("事件名", ViewDataType.TextInput, "event", LuaDataType.String, "None");
                    properties.Add(curr);
                    
                    break;
                case MiscType.Queue:
                    viewName = "queue";
                    break;
                case MiscType.Terminal:
                    viewName = "terminal";
                    break;
            }

        }

        public override string ExportLuaStream()
        {
            string stream = "";
            string argName = nodeName + uid;
            stream = stream + argName + " = " + "misc." + viewName + "()\n";
            foreach (var prop in properties)
            {
                stream = stream + argName + "." + prop.GetLuaStream() + "\n";
            }
            if (childNodes.Count != 0)
            {
                switch (childNodeType)
                {
                    case ChildNodeType.subs:
                        stream = stream + argName + ":append(" + "\"subs\"";
                        foreach (var child in childNodes)
                        {
                            stream = stream + "," + child.nodeName + child.GetUid();
                        }
                        stream = stream + ")\n";
                        break;
                    case ChildNodeType.belongs:
                        stream = stream + argName + ":append(" + "\"belongs\"";
                        foreach (var child in childNodes)
                        {
                            stream = stream + "," + child.nodeName + child.GetUid();
                        }
                        stream = stream + ")\n";
                        break;
                }
            }
            stream = stream + "\n";
            foreach (var child in childNodes)
            {
                stream = stream + child.ExportLuaStream();
            }
            return stream;
        }
    }
}
