using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class NextFrame : MonoBehaviour
    {
        void Start()
        {
            //NextFrame 下一帧执行

            //例子展现一个nextFrame 和 协程等待下一帧的对比  发现好像nextFrame慢一帧 ...说明是不是这个无法做精确的事情？
            print(Time.frameCount);//时间戳（帧）
            Observable.NextFrame().Subscribe(_ => print("NextFrame: " + Time.frameCount));
            StartCoroutine(coroutine());
           
        }
        IEnumerator coroutine()
        {
            yield return null;
            print("coroutine:"+Time.frameCount);
        }
    }
}  
