using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class TakeLast : MonoBehaviour
    {
        void Start()
        {
            //TakeLast
            #region linq  

            #endregion
            #region UniRx
            //最后两秒能点几下...
            var subject = new Subject<float>();
            subject.TakeLast(TimeSpan.FromSeconds(2.0f)).Subscribe(_ => print(Time.time));//订阅最后两秒的主题

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))//点击时迭代主题
                .Subscribe(_ => subject.OnNext(1));
            Observable.Timer(TimeSpan.FromSeconds(5.0f))
                .Subscribe(_ => subject.OnCompleted());//设置主题结束时间
            
            #endregion
        }
    }
}  
