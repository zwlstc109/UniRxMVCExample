using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Buffer : MonoBehaviour
    {
        void Start()
        {
            //Buffer  对发射来的数据进行打包 
            //和batchFrame的区别 batchFrame 的收集条件（和UpdateFrameType耦合）是在一定的帧数范围内,  而buffer的收集条件重载有很多 比如n个发射打一个包或者 、 一段时间内的发射打一个包 等等（显得更普适）
            //另外batchFrame只在第一次收到发射时才开启收集(?) 而buffer会一直按照收集条件进行收集，即使收集到的是空集（比如按时间间隔收集时就会收到空集）
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
              //经测试 这里如果填写两个时间，前一个表示整体延迟多少秒 后一个时间则时真正的buffer收集策略
               .Buffer(TimeSpan.FromSeconds(3),TimeSpan.FromSeconds(1))//和BatchFrame类似的效果(一个是流逝的时间，一个是流逝的游戏帧)，但buffer会以绝对固定的格子间隔分割时间进行收集，一直
               .Subscribe(buffer =>                        
               {
                   foreach (var item in buffer)
                   {
                       print("a collection in buffer:"+item);//这边的item就是收集到的发射参数，和想象中的一样，但不知为何BatchFrame的不是， 原因不明
                   }
                   print("a buffer packed");
               });

        }
    }
}  
