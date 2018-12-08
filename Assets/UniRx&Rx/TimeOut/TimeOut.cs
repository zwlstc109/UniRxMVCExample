using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class TimeOut : MonoBehaviour
    {
        void Start()
        {
            //TimeOut   如果一个obs在一定时间内没有进行任何发射 则会异常

            //两次鼠标点击间隔超过3秒则发生异常
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .Timeout(TimeSpan.FromSeconds(3))
                .Subscribe(_ => { }, onError => print(onError));

        }
    }
}  
