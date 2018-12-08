using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class CombineLatest : MonoBehaviour
    {
        void Start()
        {

            //CombineLatest  和zip有点像  首先两个obs都发射过后才会发射第一个数据，发射逻辑为当一个流发射时，都会结合这个发射和另一个流最近的发射 进行发射
            int a=0, b = 0;
            var leftStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Select(_ => ++a);
            var rightStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(1)).Select(_ => (++b).ToString());//可以结合不同类型的源
                                                     //可自定义结合函数
            leftStream.CombineLatest(rightStream, (left, right) => (left + " " + right)).Subscribe(result => print(result));


        }
    }
}  
