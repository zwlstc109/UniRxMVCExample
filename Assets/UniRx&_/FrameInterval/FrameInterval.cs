using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class FrameInterval : MonoBehaviour
    {
        void Start()
        {
            //FrameInterval  发射的帧间隔

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .FrameInterval()
                .Subscribe(interval => Debug.LogFormat("{0}:{1}",interval.Value,interval.Interval));//value是原始携带的数据

           
        }
      
    }
}  
