using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class DelayFrameSubscription : MonoBehaviour
    {
        void Start()
        {
            //DelayFrameSubscription 不知道和delayFramne有什么区别


            print(Time.frameCount);//时间戳（帧）
            Observable.ReturnUnit()
                .Do(_ => print(Time.frameCount))
                .DelayFrameSubscription(10)
                .Do(_ => print(Time.frameCount))
                .Subscribe(_ => print(Time.frameCount));
          
         //确实有点问题 写了10帧 他却延迟了11帧  
        }
      
    }
}  
