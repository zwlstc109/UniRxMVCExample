using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Never : MonoBehaviour
    {
        void Start()
        {
            //Never 创建一个永远不结束 也不发射的Observable ....干嘛呢..

            Observable.Never<string>().Subscribe(_ => print("never"));


        }
    }
}  
