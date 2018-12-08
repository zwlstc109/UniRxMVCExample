using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Switch : MonoBehaviour
    {
        void Start()
        {
       
            //Switch 连接类  对 两个 observable进行连接  当其中一个发射一次后（不接收），切换另一个进行观察，发射一次后 又会观察最开始的那个

      
            //该操作符本来用于两个obs之间的连接，下面这个例子展现了一个不同的用法，达到了顺序连接的效果，比之selectMany语法上更直观,比之concat 只在最后一次进行了接收
            var stream1 = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Alpha1));
            var stream2 = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Alpha2));
            var stream3 = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Alpha3));
            var stream4 = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Alpha4));
            //？？？似乎对这个操作符的理解出了点问题  这个例子中 switch的真实效果类似开关 而不是状态的切换 当1发射后 开启了2的观察 此时 1和2 居然可以同时发射 switch到底是什么意思？？？
            stream1.Select(_ => { print("1"); return stream2; })
                .Switch()
                //.Switch().Select(_ => stream3)//注意这个select 本来当s2发射一次后会返回s1但是这里s1被selector强转成了s3
                //.Switch().Select(_ => stream4)//同理当s3发射一次后，本该返回s2，而这里又被selctor强转成了s4
             
                .Subscribe(_ =>print(/*"1-->2-->3-->4 done"*/"2"));
       

            //似乎不支持响应式属性
            //ReactiveProperty<int> num1 = new ReactiveProperty<int>(0);
            //ReactiveProperty<int> num2 = new ReactiveProperty<int>(0);
            //ReactiveProperty<int> num3 = new ReactiveProperty<int>(0);


        }
    }
}  
