using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class ToArray : MonoBehaviour
    {
        void Start()
        {
            //ToArray 打包序列生成数组 时间异步的逻辑中，需等待序列结束后，才能打包 似乎蛮有用的操作符
            #region linq  

            #endregion
            #region UniRx
            //一次收集前三次单击的时间
            var subject = new Subject<float>();
            subject.ToArray().Subscribe(array => Array.ForEach(array, t => print(t)));//数组似乎只能用这个方法遍历
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Take(3)//说明take可以停止序列
                .Subscribe(_ => subject.OnNext(Time.time), () => subject.OnCompleted());
            //Observable.EveryUpdate()
            #endregion
        }
    }
}  
