using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityRx_InitialCommit
{
    public static class ObservableWWW
    {
        static readonly Hashtable defaultHeaders = new Hashtable()
        {
            {"User-Agent", "HogeHoge"}
        };

        public static IEnumerator GetWWW(string url, Action<string> onSuccess, Action<string> onError)
        {
            using (var www = new WWW(url))
            {
                yield return www;
                if (www.isDone)
                {
                    if (www.error != null)
                    {
                        onError(www.error);
                    }
                    else
                    {
                        onSuccess(www.text);
                    }
                }
            }
        }

        //static IEnumerator PostWWW(string url, WWWForm form, Action<string> onSuccess, Action<string> onError)
        //{
            
        //    using (var www = new WWW(url, form))
        //    {
                
        //    }
        //}

        //public static IObservable<string> Post(string url)
        //{

        //}

        public static IObservable<string> Get(string url)
        {
            return AnonymousObservable.Create<string>(observer =>
            {
                var e = GetWWW(url, x =>  //断点1
                {
                    try
                    {
                        observer.OnNext(x);//断点2
                        observer.OnCompleted();
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                    }
                }, x => observer.OnError(new Exception(x)));
                 //断点3
                GameLoopDispatcher.StartCoroutineRx(e);//开启一个协程 上面这个e直接替换这个e也是可以的
                //如上所示三个断点 经测试 执行顺序是1 3 2 也就是说 很正常的注册流程 开启一个协程就结束了 并没有花里胡哨的操作
                return Disposable.Empty;
            });
        }
    }
}