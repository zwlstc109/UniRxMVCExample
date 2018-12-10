using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

namespace UniRxOutLine
{

    public class Replay : MonoBehaviour
    {

        void Start()
        {

            //Replay    订阅后  收到的发射就是这个obs从他本身开始时的所有发射   请联想 回放 

            //这样的一个obs 原先在中间订阅 只会从中间开始接收发射 而如果replay 将从头接收这个obs的所有发射
            var obs =  Observable.Interval(TimeSpan.FromSeconds(1)).Publish().Connect();

           
        }
    }
}