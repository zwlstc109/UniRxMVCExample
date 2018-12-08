using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Catch : MonoBehaviour
    {
        void Start()
        {
            //Catch   捕捉发射中抛出的异常 然后返回一个新的序列

            var subject = new Subject<long>();
            subject.Catch<long, Exception>((ex => { print("some handler"); return Observable.EveryUpdate(); }))//前后obs的泛型需要相同
                .Subscribe(_=>print(_));
            subject.OnNext(999);
            subject.OnNext(999);
            subject.OnError(new Exception("exception"));
            //如猜测那样 不是只能捕获由throw抛出的异常
        }
    }
}  
