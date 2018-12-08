using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class PairWise : MonoBehaviour
    {
        void Start()
        {

            //PairWise  当此发射有前一个发射时 打包这两个发射 一起发送
            Observable.Range(0, 10).Pairwise().Subscribe(pair => print(pair.Current + " " + pair.Previous));//range左闭右开
            //Observable.Range(0, 10).Subscribe(_=>print(_));
        }
    }
}