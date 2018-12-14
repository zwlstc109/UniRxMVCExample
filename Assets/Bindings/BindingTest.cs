using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BindingsRx.Bindings;
using UniRx;
using UnityEngine.UI;
public class BindingTest : MonoBehaviour {
    ReactiveProperty<int> testRpInt = new ReactiveProperty<int>();

    ReactiveProperty<int> rpNum1 = new ReactiveProperty<int>(0);
    ReactiveProperty<int> rpNum2 = new ReactiveProperty<int>(0);
    int num1=0;
    int num2 = 0;

    Text text1;
    Text text2;
	// Use this for initialization
	void Start () {
        Link(testRpInt, rpNum1);
        Link(testRpInt, rpNum2);

        rpNum2.Value = 100;
        Debug.LogFormat("{0} {1} {2}",testRpInt,rpNum1,rpNum2);
        
	}
    static void Link<T>(ReactiveProperty<T> a, ReactiveProperty<T> b)
    {
        a.Subscribe(_ => b.Value = _);
        b.Subscribe(_ => a.Value = _);
    }
	//static void Link<T>
	//// Update is called once per frame
	//void Update () {
		
	//}
}
