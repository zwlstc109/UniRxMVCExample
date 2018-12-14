using System;
using System.Collections;

namespace UnityRx_InitialCommit
{
    public interface IObservable<out T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }

    public interface IObserver<in T>
    {
        void OnCompleted();
        void OnError(Exception error);
        void OnNext(T value);
    }

    /// <summary>
    ///深藏功与名的事件源创建者 负责构造事件源（被观察对象）
    /// </summary>
    public static class AnonymousObservable
    {
        public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> subscribe)//被观察者创建时需要指定 他的注册方法 
        {
            if (subscribe == null) throw new ArgumentNullException("subscribe");

            return new Observable<T>(subscribe);
        }
    }
    /// <summary>
    /// 事件源（可被观察对象）
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class Observable<T> : IObservable<T>//还没有发现枚举体现在哪里...
	{
		readonly Func<IObserver<T>, IDisposable> subscribe;//注册方法缓存
      
		public Observable(Func<IObserver<T>, IDisposable> subscribe)//事件源在创建时 就被指定了 注册方法 注册方法需要一个观察者 返回一个注销器
		{
			this.subscribe = subscribe;
		}
        /// <summary>
        /// 有观察者要注册时 就调用设定好的注册方法 
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
		public IDisposable Subscribe(IObserver<T> observer)
		{
			return subscribe(observer);
		}
	}

    /// <summary>
    /// 深藏功与名的观察者的创建者 负责构造观察者
    /// </summary>
    public static class AnonymousObserver
    {
        public static IObserver<T> Create<T>(Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            if (onNext == null) throw new ArgumentNullException("onNext");
            if (onError == null) throw new ArgumentNullException("onError");
            if (onCompleted == null) throw new ArgumentNullException("onCompleted");

            return new Observer<T>(onNext, onError, onCompleted);
        }
    }
    /// <summary>
    /// 观察者（订阅者）
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class Observer<T> : IObserver<T>
	{
		readonly Action<T> onNext;
		readonly Action<Exception> onError;
		readonly Action onCompleted;

		public Observer(Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			this.onNext = onNext;
			this.onError = onError;
			this.onCompleted = onCompleted;
		}

		public void OnCompleted()
		{
			onCompleted();
		}

		public void OnError(Exception error)
		{
			onError(error);
		}

		public void OnNext(T value)
		{
			onNext(value);
		}
	}
    /// <summary>
    /// 传回调 帮你构造observer 给observable
    /// </summary>
    public static partial class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> source)
        {
            return source.Subscribe(AnonymousObserver.Create<T>(_ => { }, _ => { }, () => { }));
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)
        {
            return source.Subscribe(AnonymousObserver.Create(onNext, _ => { }, () => { }));
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError)
        {
            return source.Subscribe(AnonymousObserver.Create(onNext, onError, () => { }));
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action onCompleted)
        {
            return source.Subscribe(AnonymousObserver.Create(onNext, _ => { }, onCompleted));
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            return source.Subscribe(AnonymousObserver.Create(onNext, onError, onCompleted));
        }
    }
}