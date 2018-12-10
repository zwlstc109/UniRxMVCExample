using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Threading;
using System.Linq;

namespace UniRxOutLine
{
  
    public class RepeatUntilDestroy : MonoBehaviour
    {
        void Start()
        {
            //RepeatUntilDestroy   重复直到被销毁  和addTo差不多

            Observable.Timer(TimeSpan.FromSeconds(1f))
                .RepeatUntilDestroy(this)
                .Subscribe(_ => print("ticked"), () => print("completed"));//物体销毁时  结束
        }

    }
}  
