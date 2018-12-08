using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Amb : MonoBehaviour
    {
        void Start()
        {

            //Amb  并行系列操作符  舍弃 除了第一个进行发射的obs 以外的所有obs

            Observable.Amb(
                    Observable.Timer(TimeSpan.FromSeconds(1)),
                    Observable.Timer(TimeSpan.FromMinutes(1)),
                    Observable.Timer(TimeSpan.FromHours(1)),
                    Observable.Timer(TimeSpan.FromDays(1))
                ).Subscribe(_ => print("..."));
        }
    }
}