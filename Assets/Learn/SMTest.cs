using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;


public class HappySubjectArgs : SubjectArgs
{
    public static readonly int ID = typeof(HappySubjectArgs).GetHashCode();
    public override int SubjectId { get { return ID; } }
    public int HappyDegree = 0;
}

public class SMTest : MonoBehaviour {

    void Start () {
        //只接收开心程度大于10的发射...
        SubjectManager.GetSubject<HappySubjectArgs>().Where(e => e.HappyDegree > 10)
            .Subscribe(e => print("开心程度:"+e.HappyDegree+"...看起来很开心"));

        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
            .Select((_,count)=>count)
            .Subscribe(count => SubjectManager.Fire(new HappySubjectArgs() { HappyDegree = count++ }));

    }
}
