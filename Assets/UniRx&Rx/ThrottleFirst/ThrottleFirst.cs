using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class ThrottleFirst : MonoBehaviour
    {
        void Start()
        {
            //ThrottleFirst  发射 绝对的固定时间间隔点后的第一个事件    （和Throttle完全没关系的赶脚..）

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .ThrottleFirst(TimeSpan.FromSeconds(3f))
                .Subscribe(_ => print("固定3秒间隔内的第一次点击"));
        }
    }
}  
