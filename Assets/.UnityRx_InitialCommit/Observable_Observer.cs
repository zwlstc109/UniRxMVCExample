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
	/// 匿名的 Observable
	/// </summary>
    public static class AnonymousObservable
    {
        public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> subscribe)
        {
            if (subscribe == null) throw new ArgumentNullException("subscribe");

            return new Observable<T>(subscribe);
        }
    }

	public class Observable<T> : IObservable<T>
	{
		readonly Func<IObserver<T>, IDisposable> subscribe;

		public Observable(Func<IObserver<T>, IDisposable> subscribe)
		{
			this.subscribe = subscribe;
		}

		public IDisposable Subscribe(IObserver<T> observer)
		{
			return subscribe(observer);
		}
	}

	/// <summary>
	/// 匿名的观察者、也就是默认的
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