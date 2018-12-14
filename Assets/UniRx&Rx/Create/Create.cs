using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{ //有个想法就是 可以把工厂方法返回对象和这个结合
    abstract class Monster { public string Name; }
    class MonsterA: Monster { public string Weapon; }
    class MonsterFactoryParameter { public string Name; }
    class MonsterAFacotryParameter : MonsterFactoryParameter { public string Weapon; }
    abstract class MonsterFactory
    {
       
        public IObservable<Monster> FactoryObservable;
        public void SetFactory(MonsterFactoryParameter param) { mParam = param; }
        private MonsterFactoryParameter mParam = new MonsterFactoryParameter() { Name="defaultName"};
        protected MonsterFactory()
        {
            FactoryObservable = Observable.Create<Monster>(factoryMethod);
        }
        private IDisposable factoryMethod(IObserver<Monster> observer)
        {
            observer.OnNext(createMonster(mParam));
            return Disposable.Empty;
        }
        
        protected abstract Monster createMonster(MonsterFactoryParameter param);
    }
    class MonsterA_Facotry : MonsterFactory
    {
        protected override Monster createMonster(MonsterFactoryParameter param)
        {   //...此处省略可能的初始化工作
            MonsterAFacotryParameter A_param = param as MonsterAFacotryParameter;
            return new MonsterA()
            {
                Weapon = A_param.Weapon,
                Name = A_param.Name
            };
        }
    }
    static class FactoryManager
    {
        private static Dictionary<Type, MonsterFactory> mMonsterFactoryDic = new Dictionary<Type, MonsterFactory>();
        static FactoryManager()
        {
            mMonsterFactoryDic.Add(typeof(MonsterA), new MonsterA_Facotry());
        }
        public static IObservable<T> GetMonsterFactory<T>(MonsterFactoryParameter param) where T : Monster
        {
            mMonsterFactoryDic[typeof(T)].SetFactory(param);
            return mMonsterFactoryDic[typeof(T)].FactoryObservable.Select(m => m as T);
        }
        public static IObservable<T> GetMonsterFactory<T>() where T : Monster
        {
            return mMonsterFactoryDic[typeof(T)].FactoryObservable.Select(m => m as T);
        }
    }

    public class Create : MonoBehaviour
    {
        void Start()
        {

            //Create  observable的工厂 用一个函数创建一个obs 此函数内部的执行就像是一个流 且必须有complete或者error在结尾处
            MonsterAFacotryParameter param = new MonsterAFacotryParameter() { Name = "guaishou", Weapon = "yanshen" };
            var factory=  FactoryManager.GetMonsterFactory<MonsterA>(param);
            factory.Subscribe(MonsterA => print(MonsterA.Name + " " + MonsterA.Weapon));

        }
    }
    //但是我感觉并没有什么卵用。。。这个封装
    //我本来以为可以隔一段时间创建一个对象之类的 感觉好像不对...
}