using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Merge : MonoBehaviour
    {
        void Start()
        {

            //Merge  合并源 似乎只能用来让人少写一点订阅.. 本来要分别写两个相同的订阅

            var leftStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Select(_ => "left");
            var rightStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(1)).Select(_ => "right");

            Observable.Merge(leftStream, rightStream).Subscribe(s => print(s));


        }
    }
}  
