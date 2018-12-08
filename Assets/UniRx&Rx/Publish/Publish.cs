using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Publish : MonoBehaviour
    {
        void Start()
        {

            //Publish

            var stream = Observable.Range(0, 3).Publish();//用了publish以后 只有connect以后才会开始发射
            stream.Subscribe(_ => print(_));//如果一个不是订阅一个publish的源 ， 订阅后 马上就会开始发射
            stream.Subscribe(_ => print(_));
            stream.Connect();//类似开了一个开关 stream开始发射


        }
    }
}