using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityRx_InitialCommit
{
    
    public class UniRxTest : MonoBehaviour
    {
        outTest<object> fun()
        {
            outTest<string> e = new OutClass2();
            return e;
        }


        // Use this for initialization
        void Start()
        {
            int i = 5;
            //MsgDispathcer dispathcer = new MsgDispathcer();
            //dispathcer.Subscirbe(_ => print("1：" + _.content)).Dispose();
            //dispathcer.Subscirbe(_ => print("2：" + _.content))/*.Dispose()*/;

            //dispathcer.send("...");
            Action<Action> a= (action) => { if (i > 0) {
                    i--;
                    print("1");
                    action();
                }

            };

            //var obs = gameObject.AddComponent<ObservableMonoBehaviour>(); 
            //var click1=obs.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
            //var click2 = obs.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
            //click1./*SelectMany(_ => click2).*/Subscribe(_ => print("double clicked"));


        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public class Msg
    {
        public int id;
        public string content;
    }

    public class MsgDispathcer
    {
        List<IObserver<Msg>> observers;
        IObservable<Msg> mMsgDispather;
        public MsgDispathcer()
        {
            observers = new List<IObserver<Msg>>();
            mMsgDispather = AnonymousObservable.Create<Msg>(observer =>
            {
                observers.Add(observer);//对观察者自定义的注册行为

                

                return Disposable.Create(() => observers.Remove(observer));//对观察者自定义的注销行为
            });
        }


        public IDisposable Subscirbe(Action<Msg> msgHandler)
        {
            return mMsgDispather.Subscribe(AnonymousObserver.Create<Msg>(msgHandler, e => { }, () => { }));//注册需要observer
        }
        //但是这里又有一个Send 和发射数据一样
        public void send(string content)
        {
            Msg m = new Msg() { id = 0, content = content };
            observers.ForEach(_ => _.OnNext(m));

        }
    }
    public interface outTest<out T>
    {

    }
    public class OutClass : outTest<object> { }
    public class OutClass2 : outTest<string> { }
}