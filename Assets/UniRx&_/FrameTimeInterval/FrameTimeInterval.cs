using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class FrameTimeInterval : MonoBehaviour
    {
        void Start()
        {
            //FrameTimeInterval  发射的时间间隔（截至当前帧开始的时间） 和timeInterval的区别  后者携带的是接收时的准确时间

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .FrameTimeInterval()//注意可以设置是否ignore timeScale
                .Subscribe(interval => Debug.LogFormat("{0}:  {1}",interval.Value,interval.Interval));//value是原始携带的数据

           
        }
      
    }
}  
