using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
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
       
        CommonBuff = 22,
       
        SceneReset = 25,
        Camera = 26,
        Audio = 27,

    }

    [Serializable]
    class MiscNode:BaseNode
    {
        public MiscNode(IFile file,MiscType _type = MiscType.Queue)
        {
            nodeName = "misc";
            viewName = nodeName;
            type = NodeType.Queue;
            executePath = "module.battle.data.skill.misc_vos";
            sourceFile = file;
            uid = sourceFile.RequestNodeUid();

            Property curr;
            switch (_type)
            {
                case MiscType.Ap:
                    viewName = "ap";
                    type = NodeType.Ap;
                    curr = new Property("ap值", ViewDataType.IntInput, "rate", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    break;
                case MiscType.Delay:
                    viewName = "delay";
                    type = NodeType.Delay;
                    curr = new Property("延迟", ViewDataType.IntInput, "delay", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    break;
                case MiscType.Wait:
                    viewName = "wait";
                    type = NodeType.Wait;
                    curr = new Property("事件名", ViewDataType.TextInput, "event", LuaDataType.String, "None");
                    properties.Add(curr);
                    
                    break;
                case MiscType.Hide:
                    viewName = "hide";
                    type = NodeType.Hide;
                    curr = new Property("隐藏部件", ViewDataType.EnumSelect, "hide_type", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr.enumDictionary.Add("血条", "0");
                    curr.enumDictionary.Add("模型", "1");
                    curr.enumDictionary.Add("阴影", "2");
                    curr.enumDictionary.Add("溶解", "3");

                    curr = new Property("是否隐藏", ViewDataType.EnumSelect, "hide_state", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr.enumDictionary.Add("隐藏", "0");
                    curr.enumDictionary.Add("显示", "1");

                    curr = new Property("隐藏单位", ViewDataType.EnumSelect, "unit_type", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr.enumDictionary.Add("召唤兽", "0");
                    curr.enumDictionary.Add("迷影兽", "1");

                    break;
                case MiscType.Queue:
                    viewName = "queue";
                    type = NodeType.Queue;
                    break;
                case MiscType.Terminal:
                    type = NodeType.Terminal;
                    viewName = "terminal";
                    break;
                case MiscType.ReAct:
                    viewName = "react";
                    type = NodeType.ReAct;
                    break;
                case MiscType.RetargetAll:
                    viewName = "retargetAll";
                    type = NodeType.RetargetAll;
                    curr = new Property("转变为", ViewDataType.EnumSelect, "method", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr.AddEnumInstance("自己", "0");
                    curr.AddEnumInstance("单个敌方", "1");
                    curr.AddEnumInstance("单个友方", "2");
                    curr.AddEnumInstance("所有敌方", "11");
                    curr.AddEnumInstance("所有友方", "12");
                    curr.AddEnumInstance("血量递减敌方", "13");
                    curr.AddEnumInstance("血量递减友方", "14");
                    curr.AddEnumInstance("血量递增敌方", "15");
                    curr.AddEnumInstance("血量递增友方", "16");
                    curr.AddEnumInstance("随机敌方", "17");

                    curr = new Property("目标个数", ViewDataType.IntInput, "target_num", LuaDataType.Integer, "1");
                    properties.Add(curr);
                    break;
                case MiscType.RetargetMain:
                    viewName = "retargetMain";
                    type = NodeType.RetargetMain;
                    curr = new Property("转变为", ViewDataType.IntInput, "turn_to", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    break;
                case MiscType.Interrupt:
                    viewName = "interrupt";
                    type = NodeType.Interrupt;
                    break;
                case MiscType.State:
                    viewName = "state";
                    type = NodeType.State;
                    curr = new Property("特殊状态", ViewDataType.EnumSelect, "state_name", LuaDataType.String, "stun");
                    properties.Add(curr);
                    curr.AddEnumInstance("眩晕", "stun");
                    break;
                case MiscType.Steady:
                    viewName = "steady";
                    type = NodeType.Steady;
                    curr = new Property("稳固值", ViewDataType.IntInput, "delta", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    break;
                case MiscType.MirageDie:
                    viewName = "mirageDie";
                    type = NodeType.MirageDie;
                    break;
                case MiscType.Undying:
                    viewName = "undying";
                    type = NodeType.Undying;
                    curr = new Property("次数", ViewDataType.IntInput, "times", LuaDataType.Integer, "1");
                    properties.Add(curr);
                    break;
                case MiscType.Death:
                    viewName = "death";
                    type = NodeType.Death;
                    break;
                case MiscType.Relief:
                    viewName = "relief";
                    type = NodeType.Relief;
                    curr = new Property("重生类型", ViewDataType.EnumSelect, "fixed_id", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr.AddEnumInstance("复活", "1");
                    curr.AddEnumInstance("召唤", "0");
                    curr = new Property("召唤单位ID", ViewDataType.IntInput, "target_id", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    break;
                case MiscType.Phase:
                    viewName = "phase";
                    type = NodeType.Phase;
                    break;
                case MiscType.Yield:
                    viewName = "yield";
                    type = NodeType.Yield;
                    curr = new Property("有效回合数(0为永久)", ViewDataType.IntInput, "repeats", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    break;
                case MiscType.CommonBuff:
                    viewName = "commonBuff";
                    type = NodeType.CommonBuff;
                    curr = new Property("通用buff类型", ViewDataType.EnumSelect, "buff_type", LuaDataType.String, "0");
                    properties.Add(curr);
                    curr.AddEnumInstance("冰冻", "frozen_state");
                    curr.AddEnumInstance("眩晕", "stun_state");
                    curr.AddEnumInstance("嘲讽", "taunt_state");
                    curr.AddEnumInstance("持续伤害", "sustained_damage_debuff");
                    curr.AddEnumInstance("持续治疗", "sustained_heal_buff");
                    curr.AddEnumInstance("暴击率+", "crit_rate_buff");
                    curr.AddEnumInstance("攻击+", "attack_rate_buff");
                    curr.AddEnumInstance("速度+", "speed_rate_buff");
                    curr.AddEnumInstance("速度-", "speed_rate_debuff");
                    curr.AddEnumInstance("防御+", "defence_rate_buff");
                    curr.AddEnumInstance("防御-", "defence_rate_buff");
                    curr.AddEnumInstance("造成伤害+", "damage_rate_debuff");
                   
                    break;
                case MiscType.SceneReset:
                    viewName = "sceneReset";
                    type = NodeType.SceneReset;
                    curr = new Property("新场景镜头ID", ViewDataType.IntInput, "camera_id", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr = new Property("新场景站位", ViewDataType.TextInput, "seat_ids", LuaDataType.ListInt, "");
                    properties.Add(curr);
                    break;
                case MiscType.Camera:
                    viewName = "camera";
                    type = NodeType.Camera;
                    
                    curr = new Property("镜头类型", ViewDataType.EnumSelect, "camera_type", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr.AddEnumInstance("场景镜头", "1");
                    curr.AddEnumInstance("迷影兽聚焦", "2");
                    curr.AddEnumInstance("镜头震动", "3");
                    curr = new Property("镜头ID[场景镜头]", ViewDataType.IntInput, "camera_id", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr = new Property("镜头运动[迷影兽镜头]", ViewDataType.IntInput, "turn", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    curr = new Property("震动类型[震动]", ViewDataType.IntInput, "shake_type", LuaDataType.Integer, "0");
                    properties.Add(curr);
                    break;
                case MiscType.Audio:
                    viewName = "audio";
                    type = NodeType.Audio;
                    curr = new Property("文件名", ViewDataType.TextInput, "name", LuaDataType.String, "");
                    properties.Add(curr);
                   
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
                stream = child.ExportLuaStream() + stream;
            }
            return stream;
        }
    }
}
