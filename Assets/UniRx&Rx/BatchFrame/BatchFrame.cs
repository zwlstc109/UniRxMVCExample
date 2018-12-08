using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class BatchFrame : MonoBehaviour
    {
        void Start()
        {
            //BatchFrame 收集一段帧数内的发射 比之于toArray 他不会要求序列结束

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
               .BatchFrame(100, FrameCountType.EndOfFrame)//收集模式 猜测是这样的 : 当接受到第一个发射时，才开始收集发射一直到预定帧数，触发订阅后 等待下一次收集开始
               .Subscribe(batch =>                        //所以连续按两次是不会触发两次收集的 有点意思
               {
                   foreach (var item in batch)
                   {
                       print("a collection in batch:"+item);//这边按照理解应该输出的是连续发射中传来的long 但是实际console输出的是同一个数字 原因不明
                   }
                   print("a batch collected");
               });

        }
    }
}  
