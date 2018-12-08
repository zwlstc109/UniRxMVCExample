using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class TakeWhile : MonoBehaviour
    {
        void Start()
        {
            //TakeWhere 一直拿取直到不满足xxx条件
            #region linq  
            List<int> strLst = new List<int>() { 1, 2, 2, 3, 3, 3, 2, 1, 5, 4 };
            //strLst.TakeWhile(i => i < 5).ToList().ForEach(i => print(i));
            #endregion
            #region UniRx
           
            ReactiveProperty<int> num = new ReactiveProperty<int>(0);
            num.TakeWhile((n,c)=>n<5&&c<3).Subscribe(_ => print(num));//捕获 3次数字小于5 的变化  这里可以看出初始化也算一次变化 但赋相同的值不算
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
