using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class SampleFrame : MonoBehaviour
    {
        void Start()
        {
            //SampleFrame  每隔多少帧（这个不算 接下去的第5个） 取样一个发射

            Observable.EveryUpdate().SampleFrame(5).Subscribe(_ => print(_));
        }
    }
}  
