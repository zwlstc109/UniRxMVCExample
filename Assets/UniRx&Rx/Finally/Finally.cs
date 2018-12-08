using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Finally : MonoBehaviour
    {
        void Start()
        {
            //Finally   执行在complete之后 error之前

            var subject = new Subject<int>();
                  //需要卡在里面用...
            subject.Finally(() => print("finally action")).Subscribe(_ => { }, e => print(e), () => print("completed"));  //主题不注册是不会发射的
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnCompleted();
            //subject.OnError(new Exception("error"));
           
        }
    }
}