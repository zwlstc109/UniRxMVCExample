using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{


    public class Empty : MonoBehaviour
    {        
        
        void Start()
        {
            //Empty 创建类的操作符 创建一个空序列 对于UniRx来说 相当于创建了一个立即结束的Observable 用法不明...

            #region linq


            #endregion
            #region UniRx
            Observable.Empty<string>().Subscribe(_ => { }, () =>print("OnCompleted"));
            #endregion
        }


    }
}