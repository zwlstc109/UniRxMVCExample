using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class SkipWhile : MonoBehaviour
    {
        void Start()
        {
            //SkipWhile 一直忽略直到不满足xxx条件
            #region linq  
            List<int> strLst = new List<int>() { 1, 2, 2, 3, 3, 3, 2, 1, 5, 4 };
            strLst.SkipWhile(i => i < 5).ToList().ForEach(i => print(i));
            #endregion
            #region UniRx

            ReactiveProperty<int> num = new ReactiveProperty<int>(0);
            num.SkipWhile((n, c) => n < 5 && c < 3).Subscribe(_ => print(num));//忽略 3次数字小于5 的变化  然后一直捕获
            num.Value = 1;
            num.Value = 1;
            num.Value = 2;
            num.Value = 3;
            num.Value = 4;
            num.Value = 5;
            #endregion
        }
    }
}  
