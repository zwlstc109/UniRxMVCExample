using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Timer : MonoBehaviour
    {
        void Start()
        {
            //Timer

            //1秒后 每隔0.1s 发射
            Observable.Timer(TimeSpan.FromSeconds(1f),TimeSpan.FromSeconds(0.1f)).Subscribe(_ => print("..."));
        }
    }
}  
