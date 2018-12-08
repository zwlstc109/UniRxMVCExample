using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Do : MonoBehaviour
    {
        void Start()
        {

            //Do 可以随便插入 执行一些东西

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .Do(_ => print("do"))
                .Subscribe(_ => print("..."));


        }
    }
}  
