using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

namespace UniRxOutLine
{
   
    public class FromEvent : MonoBehaviour
    {
        event Action keyW_Action;
        void Start()
        {

            //FromEvent   用obs对事件额外增加一个订阅 当obs结束 注销此订阅       

            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.W)).Subscribe(_ => keyW_Action.Invoke());//此处其实和obs没有任何关系 单纯的表示按下W键会触发事件KeyW_Action
           
            //运行时 先调用了一次addHandler  参数即subscribe的内容 
            //注册完后，这个observable并没有结束
            //当这个observable结束的时候 就会调用注销
            Observable.FromEvent(addHandler, removeHandler)
                .Take(2)//两次触发订阅后，结束obs
                .Subscribe(_ => print("...")/*,()=>print("completed")*/);

        }

        void addHandler(Action subscribe) 
        {
            print("add");
            keyW_Action += subscribe;
        }

        void removeHandler(Action subscribe)
        {
            print("remove");
            keyW_Action -= subscribe;         
        }
    }
}