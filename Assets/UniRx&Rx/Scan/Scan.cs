using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Scan : MonoBehaviour
    {
        void Start()
        {
            //Scan  Aggregate的翻版 区别是Scan不需要完整序列 （不需要序列结束） 对每个发射执行一个函数 函数的结果会缓存到下一个发射

            //发射一共的点击次数
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .Scan(0, (count, _) => ++count)
                .Subscribe(count => print(count + " times clicked"));

        }
    }
}  
