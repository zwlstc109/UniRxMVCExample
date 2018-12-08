using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Return : MonoBehaviour
    {
        void Start()
        {
            //Return   创建类的  直接返回

            Observable.Return(Unit.Default)//可以像这样开启一个序列  不是很有意义...
                .Delay(TimeSpan.FromSeconds(1f))
                .Repeat()
                .Subscribe(_ => print("after 1s"));
        }
    }
}  
