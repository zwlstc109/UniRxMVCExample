using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Interval : MonoBehaviour
    {
        void Start()
        {
            //Interval 每隔一段时间发送一次
            //传递的参数是发送过的次数 很有意思的参数
            Observable.Interval(TimeSpan.FromSeconds(1f)).Subscribe(times=>print(times));
           
        }
    }
}  
