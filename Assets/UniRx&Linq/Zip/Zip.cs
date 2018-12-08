using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Zip: MonoBehaviour
    {
        void Start()
        {
            //Zip 配对操作...?  属于创建系列
            #region linq  

            #endregion
            #region UniRx
            //左键按过一次后 右键按一下算一次配对 没配对成功会累积
            var left = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0));
            var right = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(1));
            left.Zip(right, (_, __) => "one pair cliked").Subscribe(s => print(s));
            #endregion
        }
    }
}  
