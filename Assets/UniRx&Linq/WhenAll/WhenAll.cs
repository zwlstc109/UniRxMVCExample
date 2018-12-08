using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class WhenAll : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //WhenAll （合并类操作符）   好像这个操作符的原版和UniRx版 意思不太一样 理解的还不深刻


            #region linq
            List<int> intLst1 = new List<int>() { 0, 1, 2,20,30 };
            print( intLst1.All(i => i < 40));//是否都小于40     参数是一个predicate表达式   目前可以想到的联系:当所有的元素都通过xxx判断后，返回true WhenAll有一丢丢这个意思的影子
            #endregion
            #region UniRx
            //4个事件全结束后 complete
            var everyUpdate = Observable.EveryUpdate();
            var a = everyUpdate.Where(_ => Input.GetMouseButtonDown(0)).Take(2).Select(_ => {
                Debug.Log("leftMouse clicked");
                return Unit.Default; });//返回Unit为了和下面协程的返回值保持一致 才能合起来
            var b = everyUpdate.Where(_ => Input.GetMouseButtonDown(1)).Take(2).Select(_ => {
                Debug.Log("RightMouse clicked");
                return Unit.Default;
            });
            var c = Observable.FromCoroutine(C);
            var d = Observable.FromCoroutine(D);
            Observable.WhenAll(a, b, c, d).Subscribe(_=>print("all completed"));

            #endregion
        }
        IEnumerator C()
        {
            yield return new WaitForSeconds(1);
            Debug.Log("C");
        }
        IEnumerator D()
        {
            yield return new WaitForSeconds(1);
            Debug.Log("D");
        }


    }
}