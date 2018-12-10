using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class TimeoutFrame : MonoBehaviour
    {
        void Start()
        {
            //TimeoutFrame  指定帧数内没有收到发射 则会提醒

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .TimeoutFrame(100)
                .Subscribe(_ => print("clicked"),e=>print(e), ()=>print("completed"));

        }

    }
}  
