﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Aggregate : MonoBehaviour
    {
        void Start()
        { 
            //Aggregate 累加器  可以理解为带缓存的遍历器  UniRx中这种操作一整个序列的操作都必须等待序列元素完整时才能触发订阅 当你的订阅不触发时看看是不是序列没有如你所想的那样结束
            #region linq  
            //选出最大值
            List<int> intLst = new List<int>() { 1, 2, 3, 9, 4, 5 };
            print(intLst.Aggregate((max, num) => num > max ? num : max));//可以看出max相当于是这次遍历的一个缓存变量 而具体对每一个元素进行的处理可以自定义 不是必须'累加' 
            #endregion
            #region UniRx
            //记录 每个鼠标抬起 之间的间隔时间长度 （本来想做成获取鼠标pressing时间，但是出了点问题，只要加上注释那段话，takewhile就不再可以结束everyUpdate了 原因不明...）
            float totalCache=0f;//累加器缓存类型和事件源传递参数不同的话，需要外部传参
            Observable.EveryUpdate()/*.Where(_=>Input.GetMouseButton(0))*/.TakeWhile(_ => !Input.GetMouseButtonUp(0))
                .Aggregate<long, float>( totalCache, (total, _) => total + Time.deltaTime).Repeat().Subscribe(total =>print(total));
            #endregion
            //只需要一个结果的时候 就可以订阅这种对整个序列的操作 不用写在onComplete栏位了 
           
        }
    }
}  
