using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Defer : MonoBehaviour
    {
        void Start()
        {
            //Defer 创建系列  他只在被订阅时执行创建操作 所以每次创建出的observable是不一样的  延迟初始化

            var random = new System.Random();
            var randomObservable = Observable.Defer(() => Observable.Start(random.Next));
            //三个对同一个主题的订阅 接收到的发射不同
            randomObservable.Subscribe(_ => print(_));
            randomObservable.Subscribe(_ => print(_));
            randomObservable.Subscribe(_ => print(_));


        }
    }
}  
