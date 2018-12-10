using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

namespace UniRxOutLine
{

    public class RefCount : MonoBehaviour
    {

        void Start()
        {

            //RefCount 和connect同一系列(作用也相似)   另外对可连接obs的观察者数量 做计数???
            int index = 0;
          var obs=  Observable.Interval(TimeSpan.FromSeconds(1)).Publish();

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Do(_ => print(index++))
                .Subscribe(_ => obs.Subscribe(l =>
                {
                    //int temp = index++;
                    print(index+":"+l);
                }));

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(1))
                .Subscribe(_ => obs.RefCount().Subscribe(__ => print(__)));
        }
    }
}