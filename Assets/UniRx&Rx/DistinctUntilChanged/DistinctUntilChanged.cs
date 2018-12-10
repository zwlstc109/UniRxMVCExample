using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class DistinctUntilChanged : MonoBehaviour
    {
        void Start()
        {

            //DistinctUntilChanged  过滤相同的发射 直到一个不同的发射发射 相比Distinct会记录所有发射不同 DistinctUntilChanged只会记录当前的发射 

            var stream1 = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Alpha1)).Select(_=>"1");
            var stream2 = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Alpha2)).Select(_ => "2");
            var stream3 = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Alpha3)).Select(_ => "3");
            var stream4 = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Alpha4)).Select(_ => "4");

            Observable.Merge(stream1, stream2, stream3, stream4).DistinctUntilChanged(). Subscribe(_ => print(_));
        }
    }
}