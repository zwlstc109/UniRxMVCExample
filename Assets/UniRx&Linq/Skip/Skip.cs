using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Skip : MonoBehaviour
    {
        void Start()
        {
            //Skip 和Take是同一系列的  忽略前n个 UniRx中可以忽略一段时间
            #region linq  
            List<string> strLst = new List<string>() { "abc", "def", "ghi", "jkl" };
            strLst.Skip(2).ToList().ForEach(s => print(s));
            #endregion
            #region UniRx
            //3秒钟后才能单击判定
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
               .Skip(TimeSpan.FromSeconds(3f))
               .Subscribe(_=>print("clicked"));

            ////对仗的Take
            ////3秒钟内才能单击判定
            //Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
            //   .Take(TimeSpan.FromSeconds(3f))
            //   .Subscribe(_ => print("clicked"));
            #endregion
        }
    }
}  
