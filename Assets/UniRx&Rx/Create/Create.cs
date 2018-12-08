using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Create : MonoBehaviour
    {
        void Start()
        {

            //Create  observable的工厂 用一个函数创建一个obs 此函数内部的执行就像是一个流 且必须有complete或者error在结尾处

            Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnNext(2);
                observer.OnNext(3);
                observer.OnCompleted();
                return Disposable.Create(() => print("dispose action"));
            }).Subscribe(_ => print(_));


        }
    }
}