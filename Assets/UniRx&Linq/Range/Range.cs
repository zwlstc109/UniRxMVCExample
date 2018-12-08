using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{


    public class Range : MonoBehaviour
    {        
        
        void Start()
        {
            //Range 创建类的操作符

            #region linq
            Enumerable.Range(1, 5).Select(i => i * i).ToList().ForEach(i => print(i)); 
            
            #endregion
            #region UniRx
            Observable.Range(1, 5).Select(i => i * i).Subscribe(i => print(i));     
            //上下对比可以发现,UniRx中的subscribe和Linq中的forEach是兄弟  但是通常 linq的foreach发生在同一帧内 而UniRx的‘forEach’发生在不同帧上 多个流并发，轻松实现异步逻辑
            #endregion
        }


    }
}