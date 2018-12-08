using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class TimeInterval : MonoBehaviour
    {
        void Start()
        {
            //TimeInterval  发射 当前发射距离上个发射的时间间隔

            //获取每次单击的间隔时间
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .TimeInterval()
                .Subscribe(Interval => Debug.LogFormat("interval:{0} value:{1}", Interval.Interval, Interval.Value));
                                                          //interval即间隔  value是原始发射的携带的数据
        }
    }
}  
