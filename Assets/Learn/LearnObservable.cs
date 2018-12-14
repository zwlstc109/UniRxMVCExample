using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UniRx;
namespace UniRxOutLine
{
    public class LearnObservable : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //请仔细体会下面两段代码

            new List<int>() { 0, 1, 2, 3, 4 }
            .Select(i => i * 2)
            .Where(i => i > 3)
            .ToList()
            .ForEach(i => print(i));

            Observable.Range(0,5)
            .Select(i => i * 2)
            .Where(i => i > 3)
            .Subscribe(i=>print(i));

            /* IEnumerable接口定义的GetEnumerator方法是为了主动遍历对象
             * IObservable接口定义的subscribe方法是为了被动'处理'遍历到的对象
             * subscribe本身并没有对事件源（序列）的枚举权力, 只是被动的订阅操作
            */

        }

    }


}