using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Sample : MonoBehaviour
    {
        void Start()
        {
            //Sample  定期 发射 该时间点前 最靠近的一个发射

            //每隔2秒发射一次  每隔3秒接收最近的一次
            int index = 0;
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(2f)).Select(_ => index++)
                .Sample(TimeSpan.FromSeconds(3f))
                .Subscribe(_ => print(index));
        }
    }
}  
