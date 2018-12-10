using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class ThrottleFirstFrame : MonoBehaviour
    {
        void Start()
        {
            //ThrottleFirstFrame  发射 绝对时间间隔的间隔点后的第一个发射
            print(Time.frameCount);
            Observable.EveryUpdate().ThrottleFirst(TimeSpan.FromSeconds(2f))
                .Subscribe(_ => print(":"+Time.frameCount));//第一个接收是:3  感觉UniRx对帧的操作不精确呀  很魔性
        }

    }
}  
