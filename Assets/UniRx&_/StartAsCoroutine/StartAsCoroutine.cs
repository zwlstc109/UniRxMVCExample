using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class StartAsCoroutine : MonoBehaviour {

	
	void Start () {

        StartCoroutine(fun());
      
         
    }
	IEnumerator fun()
    {
        int i = 0;
        //发射源结束后 才会通过  有点意思的功能....但是仔细一想 onComplete不是一样吗...
        yield return Observable.EveryUpdate().Where(_=>Input.GetMouseButtonDown(0)).Take(3). StartAsCoroutine( );
        Debug.Log(i);
       
    }
	
}
