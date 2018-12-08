using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class SkipUntil : MonoBehaviour
    {
        void Start()
        {
            //SkipUntil 对仗TakeUntil 拿取后半段序列 注意 由于predicate是一个observable 所以注意是否需要构造'停止'

            Observable.EveryUpdate().SkipUntil(Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)))
                .Take(300).Repeat()//observable要repeat先要保证他会结束
                .Subscribe(_ => print("a frame after clicked"));
        }
    }
}  
