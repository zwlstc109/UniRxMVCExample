using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class ThrottleFrame : MonoBehaviour
    {
        void Start()
        {
            //ThrottleFrame 等待帧数内没有收到发射则发射（发射后又继续计时） 反之憋着不发射（类似于无限重新计时）            和ThrottleFirstFrame完全不一样...  
          

            //60帧内接收不到鼠标点击则会输出 输出后又开始计数  一直点会 一直不输出
            Observable.EveryUpdate().Where(_=>Input.GetMouseButtonDown(0)). ThrottleFrame(90)
                .Subscribe(_ => print("end"));

            //类似于监督你发射 你不发射的时候 鞭挞你一下 以你下一次发射为监督点 继续监督
        }

    }
}  
