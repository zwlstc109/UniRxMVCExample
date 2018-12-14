using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public static class RPLink 
{
    private static Dictionary<string, BoolReactiveProperty> mBoolRpDic = new Dictionary<string, BoolReactiveProperty>();

    public static BoolReactiveProperty GetRp(string name,bool initialValueIfEmpty=true)
    {
        BoolReactiveProperty boolrp = null;
        if (!mBoolRpDic.TryGetValue(name,out boolrp))
        {
            boolrp = new BoolReactiveProperty(initialValueIfEmpty);
            mBoolRpDic.Add(name, boolrp);
        }
        return boolrp;
    }
    public static readonly string EventMaskEnable = "EventMaskEnable_todoList";
}
