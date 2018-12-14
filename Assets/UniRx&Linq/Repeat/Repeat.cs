using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{


    public class Repeat : MonoBehaviour
    {        
        
        void Start()
        {
            //Repeat 创建类

            #region linq
            Enumerable.Repeat(2, 5)./*Select(i => i * i).*/ToList().ForEach(i => print(i));

            #endregion
            #region UniRx
            //创建版
            string repeatStr = "r";
            Observable.Repeat(repeatStr, 5).Subscribe(_ => print(_));//注意给上重复的次数 否则会死循环 只能关编辑器了...
            //很有用的操作符版
            Observable.Timer(TimeSpan.FromSeconds(2f)).Repeat().Subscribe(_ => print("2s elapsed"));
           
            #endregion
        }


    }
}