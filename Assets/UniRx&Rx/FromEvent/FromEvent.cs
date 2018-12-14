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
        event Action keyDown_Action_No;//不带参
        event Action<string> keyDown_Action ;

        Action anyAction;
        void Start()
        {
            anyAction = () => print("anyAction");


            keyDown_Action += str => { };
            //FromEvent   用obs对事件额外增加一个订阅 当obs结束 注销此订阅       

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ => keyDown_Action.Invoke("ss"));//此处其实和obs没有任何关系 单纯的表示按下W键会触发事件KeyW_Action

            //奇特的发射源  subscribe时会执行addHandler 并把参数正确的OnNext传进去
            //发射结束时，会执行removeHandler 解除关联
                                                 
                                                  //这个骚操作第一次见到 居然lamda可以对delegate进行转化....
            Observable.FromEvent<Action<string>,Unit>(h=>(s=>h(Unit.Default)), addHandler, removeHandler)//这个conversion的要求是把subscribe时的无参Action 转换为其他参数类型的Action      
                .Take(2)//两次触发订阅后，结束obs
                .Subscribe(_ => print(_)/*,()=>print("completed")*/);

            //事件触发时 会调用observer的onNext
        }

        void TestConvert(string str)
        {

        }
                       //这个参数是经过转化后的lamda   observer的onNext 转化为 目标action的类型
        void addHandler(Action<string> subscribe) 
        {
            print("add");//然后把这个嚼过的lamda加到你要关联的delegate上
            keyDown_Action += subscribe;
        }

        void removeHandler(Action<string> subscribe)
        {
            print("remove");
            //keyDown_Action -= subscribe;         
        }
    }
}