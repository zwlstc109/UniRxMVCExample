using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class FromCoroutine : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //和猜想中一样 有一次yield 发射一次
        Observable.FromCoroutine(testCoroutine,true).Subscribe(_=>print(_));
        //Observable.from
	}

    IEnumerator testFun()
    {
        yield return testCoroutine();
    }

	IEnumerator testCoroutine()
    {
        yield return new WaitForSeconds(2f);
        callback();
    }

    IEnumerator testCoroutine2()
    {
      
        yield return null;
        
    }

  public  void callback()
    {
        print("call back");
    }
}
