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
        List,
        Function,
        Skill
    }

    public enum ViewDataType
    {
        TextInput,
        IntInput,
        EnumSelect,
        ListSelect
    }
    [Serializable]
    class Property
    {
        public Property(string _viewName="None",ViewDataType _viewType=ViewDataType.TextInput,string _luaName="None",LuaDataType _luaDataType=LuaDataType.Boolen,string defaultValue="None")
        {
            ViewName = _viewName;
            ViewType = _viewType;
            LuaName = _luaName;
            LuaDataType = _luaDataType;
            propValue = defaultValue;
            if(ViewType == ViewDataType.EnumSelect)
            {
                enumDictionary = new Dictionary<string, string>();
            }
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

        public void SetValue(string input)
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
            }
            return argName + " = " + luaValue;
        }

        string propValue;

        string argsValue;

        //ViewEnumSelect;
        Dictionary<string, string> enumDictionary;
        
    }
}
