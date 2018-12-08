using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class IgnoreElements : MonoBehaviour
    {
        void Start()
        {

            //IgnoreElements  忽略所有发射 只接收结束通知

            Observable.EveryUpdate().Take(100).IgnoreElements().Subscribe(_ => print(_),()=> print("只关注了结束"));//很奇怪的设定，第一个表达式不执行，必须让你写在第二个栏位
        }
    }
}  
