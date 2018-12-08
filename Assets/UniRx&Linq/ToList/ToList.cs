using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class ToList : MonoBehaviour
    {
        void Start()
        {
            //ToList 和ToArray基本一样
            #region linq  

            #endregion
            #region UniRx
            //一次收集前三次单击的时间
            var subject = new Subject<float>();
            subject.ToList().Subscribe(lst =>
            {
                foreach (var item in lst) //toList返回的是ilist 有点蛋疼 只能这么遍历
                {
                    print(item);
                }
            });
            Observable.Timer(TimeSpan.FromSeconds(1f)).Repeat().Take(3).Subscribe(_ => subject.OnNext(Time.time), () => subject.OnCompleted());
            #endregion
        }
    }
}  
