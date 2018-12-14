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
            //阅读源码后 发现deferObservable相当于缓存了 创建observable的回调  每次对其进行的订阅其实是对缓存的obs的订阅
            var random = new System.Random();
            var randomObservable = Observable.Defer(() => Observable.Start(random.Next));
            //三个对同一个主题的订阅 接收到的发射不同
            randomObservable.Subscribe(_ => print(_));
            randomObservable.Subscribe(_ => print(_));
            randomObservable.Subscribe(_ => print(_));


        }
    }
}  
