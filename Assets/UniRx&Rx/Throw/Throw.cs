using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Throw : MonoBehaviour
    {
        void Start()
        {
            //Throw  直接发射一个异常...

            Observable.Throw<string>(new Exception("error"))
                .Subscribe(_ => { }, error => print(error));//前一个栏位不会执行 只执行error那个

        }
    }
}  
