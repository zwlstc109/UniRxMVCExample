using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class DelayFrame : MonoBehaviour
    {
        void Start()
        {
            //DelayFrame 延迟n帧执行


            print(Time.frameCount);//时间戳（帧）
            Observable.ReturnUnit()
                .Do(_ => print(Time.frameCount))
                .DelayFrame(10)
                .Do(_ => print(Time.frameCount))
                .Subscribe(_ => print(Time.frameCount));

            //确实有点问题 写了10帧 他却延迟了11帧  

        }
      
    }
}  
