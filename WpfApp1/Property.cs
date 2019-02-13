using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum LuaDataType
    {
        Integer,
        Boolen,
        String,
        RawString,
        ListFunction,
        ListInt,
        Function,
        Skill
    }

    public enum ViewDataType
    {
        TextInput,
        IntInput,
        EnumSelect,
        ListSelectWithArgs,
        EnumSelectWithArgs,
    }

    public enum PropertyType
    {
        Common,
        Buff
    }
    [Serializable]
    class Property
    {
        public Property(string _viewName="None",ViewDataType _viewType=ViewDataType.TextInput,string _luaName="None",LuaDataType _luaDataType=LuaDataType.Boolen,string defaultValue="None",PropertyType type = PropertyType.Common)
        {
            ViewName = _viewName;
            ViewType = _viewType;
            LuaName = _luaName;
            LuaDataType = _luaDataType;
            propValue = defaultValue;
            PropType = type;
            enumDictionary = new Dictionary<string, string>();
            
        }

        public void AddEnumInstance(string key,string value)
        {
            enumDictionary.Add(key, value);
        }

        public string ViewName {
            get;
        }

        public ViewDataType ViewType {
            get;
        }

        public string LuaName
        {
            get;
        }

        public LuaDataType LuaDataType
        {
            get;
        }

        public PropertyType PropType {
            get;
        }
        

        public void SetValue(string input,string args = "")
        {
            switch (ViewType)
            {
                case ViewDataType.IntInput:
                case ViewDataType.TextInput:
                    propValue = input;
                    break;
                case ViewDataType.EnumSelect:
                    propValue = enumDictionary[input];
                    break;
                case ViewDataType.EnumSelectWithArgs:
                    propValue = enumDictionary[input] + args + '|';
                    break;
                case ViewDataType.ListSelectWithArgs:
                    string value = enumDictionary[input];
                    if (propValue.Contains(value))
                    {
                        int index = propValue.IndexOf(value);
                        while (propValue[index] != '|')
                        {
                            propValue.Remove(index, 1);
                        }
                        propValue.Remove(index);
                        
                    }
                    propValue = propValue + value + args + '|';
                    break;
            }
        }

        public string GetValue()
        {
            switch (ViewType)
            {
                case ViewDataType.IntInput:
                case ViewDataType.TextInput:
                    return propValue;
                case ViewDataType.EnumSelect:
                    foreach(var word in enumDictionary)
                    {
                        if (word.Value == propValue) return word.Key;
                    }
                    return null;
            }
            return null;
        }

        public Dictionary<string,string> GetValueWithArgs()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach(var word in enumDictionary)
            {
                if (propValue.Contains(word.Value))
                {
                    string args = "";
                    int index = propValue.IndexOf(word.Value)+word.Value.Length;
                    while (propValue[index] != '|')
                    {
                        args = args + propValue[index];
                        ++index;
                    }
                    res.Add(word.Key, args);
                }
            }
            return res;
        }

        public string GetPropValue()
        {
            return propValue;
        }

        public string GetLuaStream()
        {
            string argName = LuaName;
            string luaValue="";
            switch (LuaDataType)
            {
                case LuaDataType.Boolen:
                case LuaDataType.Integer:
                    luaValue = propValue;
                    break;
                case LuaDataType.String:
                    luaValue = "\"" + propValue + "\"";
                    break;
                case LuaDataType.RawString:
                    luaValue = propValue;
                    break;
                case LuaDataType.ListInt:
                    luaValue = "{" + propValue + "}";
                    break;
                case LuaDataType.ListFunction:
                    luaValue = "{";
                    foreach (var word in GetValueWithArgs())
                    {
                        luaValue = luaValue + enumDictionary[word.Key] + "(" + word.Value + ")" + ",";
                    }
                    luaValue = luaValue + "}";
                    
                    break;
                case LuaDataType.Function:
                    foreach(var word in GetValueWithArgs())
                    {
                        luaValue = enumDictionary[word.Key] + "(" + word.Value + ")";
                    }
                    break;
            }
            return argName + " = " + luaValue;
        }

        string propValue;

        string argsValue;

       
        //ViewEnumSelect;
        public Dictionary<string, string> enumDictionary { get; set; }
        
    }
}
