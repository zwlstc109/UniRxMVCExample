using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Learn
{
    //首先简述一下IEnumerable 和 IEnumerator这两个接口
    //实现了前者 就可以获取一个 后者的对象
    //调用后者对象的moveNext 和current的方法 就可以在不用知道对象内部结构的情况下 一个一个的拿取这个对象给你提供的返回


    //yield 是一种语法糖 让程序员可以不显示实现enumerable和enumerator接口中的方法
    //不妨把 含有yield的方法 想象成一个 返回对应接口对象的 构造方法
    //由函数表现生成可迭代对象 

    public class LearnYield : MonoBehaviour
    {
        IEnumerable enumerableTest()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return "ble";    //一次yield return 相当于MoveOnNext和get current的组合操作...
            }
        }
        IEnumerator enumeratorTest()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return "tor";
            }
        } 
        IEnumerator yieldTest()
        {
            yield return new WaitForSeconds(1);
            print("after 1");
            yield return 1;
        }
        //下面做一个用协程实现自己的协程的实验
        //自定义的等待指令对象,只支持秒数等待
        class MyYieldInstruction
        {
            public MyYieldInstruction(float second) { time = second; }
            public float time;
            //复制接口
            public void copy(MyYieldInstruction other) { time=other.time;}
            
        }
        //协程调度器
        class MyCorouineManager
        {
            //一个字典 key是被管理的协程，value是这个协程当前的等待指令对象(里面存着还剩多少时间)
            Dictionary<IEnumerator, MyYieldInstruction> mIterDic = new Dictionary<IEnumerator, MyYieldInstruction>();
            //提供给外界开启协程的接口
            public IEnumerator StartCorouine(IEnumerator iter)
            {   
                //先运行到第一个yield处
                iter.MoveNext();
                //得到yield 返回的 指令对象
                MyYieldInstruction operation = (MyYieldInstruction)iter.Current;
                //加入字典 进行管理
                mIterDic.Add(iter, operation);
                return iter;
            }
            public void Update()
            {   //每帧进行遍历
                GoOnForeach(mIterDic.GetEnumerator());
            }
            //可以删除元素的遍历
            void GoOnForeach(IEnumerator dicIter)
            {
                while (dicIter.MoveNext())
                {
                    var dicIterItem =(KeyValuePair<IEnumerator,MyYieldInstruction>) dicIter.Current;
                    //持续减少指令对象的剩余时间
                    dicIterItem.Value.time -= Time.deltaTime;
                    //时间到了
                    if (dicIterItem.Value.time < 0)
                    {
                        //如果这个协程后面还有yield  就替换新的指令对象(用copy的方式)
                        if (dicIterItem.Key.MoveNext())
                        {                         
                            MyYieldInstruction tempOperation = null;
                            if(mIterDic.TryGetValue(dicIterItem.Key, out tempOperation))
                            { 
                                tempOperation.copy((MyYieldInstruction)dicIterItem.Key.Current) ;                               
                            }
                        }
                        //否则 删除这个协程
                        else
                        {
                            var tempIterItem = dicIterItem;
                            GoOnForeach(dicIter);
                            mIterDic.Remove(tempIterItem.Key);
                            break;                          
                        }
                    }
                }
            }
        }
        MyCorouineManager coroutineManger = new MyCorouineManager();
        void Start()
        {
            coroutineManger.StartCorouine(testWaitFun("1"));
            coroutineManger.StartCorouine(testWaitFun("2"));
            //coroutineManger.StartCorouine(testWaitFun("1"));
            //coroutineManger.StartCorouine(testWaitFun("2"));
            //coroutineManger.StartCorouine(testWaitFun("1"));
            //coroutineManger.StartCorouine(testWaitFun("2"));
            //coroutineManger.StartCorouine(testWaitFun("1"));
            //coroutineManger.StartCorouine(testWaitFun("2"));
            //coroutineManger.StartCorouine(testWaitFun("1"));
            //coroutineManger.StartCorouine(testWaitFun("2"));
            //coroutineManger.StartCorouine(testWaitFun("1"));
            //coroutineManger.StartCorouine(testWaitFun("2"));
            //coroutineManger.StartCorouine(testWaitFun("1"));
            //coroutineManger.StartCorouine(testWaitFun("2"));


        }
        void Update()
        {
            coroutineManger.Update();
        }
      
        IEnumerator testWaitFun(string index)
        {
            print(index+" start wait...");
            yield return new MyYieldInstruction(2) ;
            print(index+" 2s after");
            yield return new MyYieldInstruction(2);
            print(index+" 4s after");
            print(index+" end");

        }
       
    }
    
   
    class SomeClass
    {

        //public IEnumerator GetEnumerator()
        //{
        //    yield return 1;
        //    yield return 4;
        //    yield return 2;
        //    yield return 7;
        //}
        //不需要显示继承IEnmerable 只要实现这个方法 就可以被foreach认识
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(0);
        }
         class Enumerator : IEnumerator<object>, IEnumerator, IDisposable
        {
            private object mCurrent;
            private int mState;

            public object Current
            {
                get
                {
                    return mCurrent;
                }
            }

            public void Dispose()
            {
               
            }

         
          public  Enumerator(int state)
            {
                mState = state;
            }
            public bool MoveNext()
            {
                switch (mState)
                {
                    case 0:
                        mCurrent = 1;
                        mState++;
                        return true;
                    case 1:
                        mCurrent = 4;
                        mState++;
                        return true;
                    case 2:
                        mCurrent = 2;
                        mState++;
                        return true;
                    case 3:
                        mCurrent = 7;
                        mState++;
                        return true;
                    default:
                        return false;

                }
            }

            public void Reset()
            {
                mState = 0;
            }

            
        }
    }

}



