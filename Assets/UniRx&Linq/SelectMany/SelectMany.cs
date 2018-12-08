using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class SelectMany : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //SelectMany  把元素打散的转换器  ， 他和select同样是转换器 但是他的要求是转换后的对象仍然可迭代 

            //#region linq
            //List<string> strLst = new List<string>() { "abc", "def", "ghi", "jkl" };
            //strLst.SelectMany(_ => _+"1").ToList().ForEach(ch => print(ch));
            //#endregion
            #region UniRx
            var a = Observable.FromCoroutine(A);
            var b = Observable.FromCoroutine(B);
            var c = Observable.FromCoroutine(C);

            //对协程执行顺序进行任意组合  
            //原理是协程(可观察的)默认只会在结束后被观察到一次，A结束后被转成了B,B结束后又转成了C ,全部观察完后 输出end
            a.SelectMany(b.SelectMany(c)).Subscribe(_ => print("end"));

            //a.SelectMany(sa=> {
            //    print("a convered to b");
            //    return b;
            //}).Subscribe(_=>print("end"));

            //todo怎么组合带参数的协程?
            #endregion
        }

        IEnumerator A()
        {
            yield return new WaitForSeconds(1.0f);
            print("A");
        }
        IEnumerator B()
        {
            yield return new WaitForSeconds(1.0f);
            print("B");
        }
        IEnumerator C()
        {
            yield return new WaitForSeconds(1.0f);
            print("C");
        }

    }
}