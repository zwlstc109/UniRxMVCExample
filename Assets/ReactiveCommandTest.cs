using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class ReactiveCommandTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Subject<bool> subject = new Subject<bool>();

        ReactiveCommand<string> reactiveCommand = new ReactiveCommand<string>(subject);
        reactiveCommand.Execute("1");
        reactiveCommand.Subscribe(s => print(s));
        reactiveCommand.Execute("2");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
