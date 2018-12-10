using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;
using System.Threading;

namespace UniRxOutLine
{
  
    public class ObserveOnMainThread : MonoBehaviour
    {
       
        void Start()
        {
            //ObserveOnMainThread  强大的操作符  订阅线程的结束 


            print(Time.time);
            Observable.Start(() =>
            {
                Thread.Sleep(2000);
                return -1;
            })
            .ObserveOnMainThread()
            .Do(_ => print(_))//看看返回的是不是这个0呢  由此可见do可以随便插随便用 很方便...
            .Subscribe(_ => print(Time.time));
        }

    }
}  
