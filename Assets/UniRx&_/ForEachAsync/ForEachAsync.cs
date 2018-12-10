using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class ForEachAsync : MonoBehaviour
    {
        void Start()
        {
            //ForEachAsync   异步遍历
            Observable.Range(0, 1000).ForEachAsync(_ => print(Time.frameCount))
                .Subscribe(_ =>print(_), () => print("completed at:"+Time.frameCount));//这个订阅的第一个栏位只在结束时收到一个unit，只执行结束栏位

        }

    }
}  
