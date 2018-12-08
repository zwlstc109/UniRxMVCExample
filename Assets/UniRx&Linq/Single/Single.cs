using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Single : MonoBehaviour
    {
        void Start()
        {
            //Single 返回序列中符合条件的一个元素 但是如果有多个元素 则会异常...  用法不明
            #region linq  
            List<int> intLst = new List<int>() { 1, 2, 3, 4, 6 };
            print(intLst.Single(i => i > 5));
            #endregion
            #region UniRx
           
            #endregion
        }
    }
}  
