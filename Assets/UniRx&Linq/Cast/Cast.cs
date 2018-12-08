using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
 

    public class Cast : MonoBehaviour
    {        
        
        void Start()
        {
            //Cast  类型转换

            #region linq
            //...
            #endregion
            #region UniRx
            var subject = new Subject<object>();
            subject.Cast<object,A>().Subscribe(_ => { },onError=> print("cast error"));

            subject.OnNext(new A1());
            subject.OnNext(new A3());
            subject.OnNext("...");
            subject.OnNext(new A2());
            subject.OnNext(new A1());

            #endregion
        }

       
    }
}