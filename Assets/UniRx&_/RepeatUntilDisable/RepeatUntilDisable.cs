using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class RepeatUntilDisable : MonoBehaviour
    {
        void Start()
        {
            //RepeatUntilDisable  字面意思..

            Observable.Timer(TimeSpan.FromSeconds(1f)).RepeatUntilDisable(this)//指的是gameobject的隐藏
                .Subscribe(_ => print(_));

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ =>gameObject.SetActive(false));

        }

    }
}  
