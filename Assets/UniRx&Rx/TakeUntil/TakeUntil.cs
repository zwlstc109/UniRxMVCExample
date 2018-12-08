using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class TakeUntil : MonoBehaviour
    {
        void Start()
        {
            //TakeUntil  乍看之下和takeWhile相似(都是获取前半段序列) 但是不同之处在于 只能以一个Observable的第一个有效发射（或直接停止） 当作predicate 

            Observable.EveryUpdate().TakeUntil(Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)))
                 .Subscribe(_ => print("i will stop until click..."));
           
        }
    }
}  
