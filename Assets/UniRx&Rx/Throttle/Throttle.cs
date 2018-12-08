using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Throttle : MonoBehaviour
    {
        void Start()
        {
            //Throttle 时间类操作 对将要来的发射设定一个过滤 此过滤的条件为 一段时间内没有其他发射过来

            Observable.EveryUpdate().Where(_=>Input.GetMouseButtonDown(0))
                .Throttle(TimeSpan.FromSeconds(2))
                .Subscribe(_ => print("上一次点击后2秒内 均未发现到其他点击...\n(离上一次点击已经过去了2秒 并没有再次点击)"));
          //接收发射后，要在规定时间内继续发射 才能 '不' 触发订阅的操作符   有点意思
        }
    }
}  
