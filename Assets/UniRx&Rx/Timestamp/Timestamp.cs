using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Timestamp : MonoBehaviour
    {
        void Start()
        {
            //Timestamp 时间戳  给每个发射打上一个时间戳 

            //发射每次单击时的时间戳
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .Timestamp()
                .Subscribe(timestamped =>print(timestamped.Timestamp.ToLocalTime()));//注意timestamp这个参数  他的类型是DataTimeOffset 支持时区转换
        }
    }
}  
