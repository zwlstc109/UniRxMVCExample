using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

namespace UniRxOutLine
{

    public class DelaySubscription : MonoBehaviour
    {

        void Start()
        {

            //DelaySubscription  延迟订阅
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(3f)).Subscribe(_ => print(_));

        }
    }
}