using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Concat : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //Concat


            #region linq
            List<int> intLst1 = new List<int>() { 0, 1, 2 }; List<int> intLst2 = new List<int>() { 3, 4, 5 };
            intLst1.Concat(intLst2).ToList()/*.ForEach(i=>print(i))*/;
            #endregion
            #region UniRx
            //先点两次左键后才能点两次右键
            var everyUpdate = Observable.EveryUpdate();
            var a = everyUpdate.Where(_ => Input.GetMouseButtonDown(0)).Take(2).Select(_ => "A");//感觉这个select操作很精髓...
            var b = everyUpdate.Where(_ => Input.GetMouseButtonDown(1)).Take(2).Select(_ => "B");
            Observable.Concat(a, b).Subscribe(s => print(s));
            //和用selectMany连接不同 concat中的每个接收时都会触发订阅 而selectMany是在结束前又转成了另一个事件源 结果只有当所有事件源都结束才会触发订阅
            #endregion
        }


    }
}