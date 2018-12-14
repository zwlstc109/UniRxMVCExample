using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Delay : MonoBehaviour
    {
        void Start()
        {
            //Delay  对每个发射延迟接收 
            //阅读源码后 的理解是 每个onNext的回调执行都被调度器延迟发送了

            //单击后 延迟1秒 接收  
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                //.Select(_=> {                 //随便插哪里都行的Select
                //    print("clicked...");
                //    return _;
                //})
                .Delay(TimeSpan.FromSeconds(1f))
                .Select(_ =>"... 1s later")
                .Subscribe(_=>print(_));
          
        }
    }
}  
