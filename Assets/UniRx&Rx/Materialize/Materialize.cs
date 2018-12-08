using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Materialize : MonoBehaviour
    {
        void Start()
        {

            //Materialize  将发射转换成Notification  DeMaterialize 逆操作
            //这个操作符看起来可以获得一个obs在nofication层面上的一些状态，并在必要的时候逆操作变回原身
            var subject = new Subject<int>();
            var OnlyException = subject.Materialize().Where(notification => notification.Exception != null)
                //.Subscribe(notification => print(notification.Value));
                .Dematerialize().Subscribe(_ => { }, e => print(e));//和上一句注释掉的代码有点像

            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnError(new Exception("error"));
            subject.OnNext(1);
        }
    }
}  
