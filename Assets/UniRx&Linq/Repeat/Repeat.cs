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
            //Repeat 创建类的操作符

            #region linq
            Enumerable.Repeat(2, 5)./*Select(i => i * i).*/ToList().ForEach(i => print(i));

            #endregion
            #region UniRx
            //很有用的重复操作符
            Observable.Timer(TimeSpan.FromSeconds(2f)).Repeat().Subscribe(_ => print("2s elapsed"));
            #endregion
        }


    }
}