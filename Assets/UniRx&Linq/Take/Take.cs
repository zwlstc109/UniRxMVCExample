using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class Take : MonoBehaviour
    {
        void Start()
        {
            //Take 和first last属于同一类  拿取前n个元素
            #region linq  
            List<string> strLst = new List<string>() { "abc", "def", "ghi", "jkl" };
            strLst.Take(2).ToList().ForEach(s => print(s));
            #endregion
            #region UniRx
            //一种双击实现
            //用了一个selectMany的技巧把传来的无效参数重新转换成一个事件源
            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .SelectMany(_ =>( Observable.EveryUpdate().Take(30).Where(___ => Input.GetMouseButtonDown(0)))/*.Take(1)*/)
                .Subscribe(_ => print("double clicked")/*, error => { }*/);
            //还是有bug的 拿取到的30个update都会进行单击判断 正常的话 应该判断一次就结束，但是用first不管加哪里都不对，等后面学了更多的操作符再来改
            //sequence is null这个异常原因有可能是First的时候序列为空，取不到第一个就会异常
            #endregion
        }
    }
}  
