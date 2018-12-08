using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class A { }
    public class A1 : A { }
    public class A2 : A { }
    public class A3 : A { }

    public class OfType : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //OfType

            #region linq
            List<A> aLst = new List<A>() { new A1(), new A2(), new A3(), new A1(), new A3() };
            print(aLst.OfType<A1>().ToList().Count);
            #endregion
            #region UniRx
            //涉及到UniRx原理的内容了！
            var subject = new Subject<A>();

            subject.OfType(new A1()).Subscribe(_=>print("a A1 receive"));

            subject.OnNext(new A1());
            subject.OnNext(new A3());
            subject.OnNext(new A2());
            subject.OnNext(new A1());

            #endregion
        }
        //IEnumerator C()
        //{
        //    yield return new WaitForSeconds(1);
        //    Debug.Log("C");
        //}
        //IEnumerator D()
        //{
        //    yield return new WaitForSeconds(1);
        //    Debug.Log("D");
        //}


    }
}