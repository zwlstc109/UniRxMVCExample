using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class StartWith : MonoBehaviour
    {
        void Start()
        {

            //StartWith  在obs之前手动添加一些参数...或者添加一个可枚举序列...不会用...
            var s1 = Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0));
          

            s1.StartWith(100).Subscribe(_ => print(_));//提前发射 似乎不受where影响


        }
    }
}  
