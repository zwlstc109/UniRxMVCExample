using System;
using System.Collections;

namespace UnityRx_InitialCommit
{
    // TODO:Subject? Scheduler?

    // Select, Where, SelectMany, Zip, Merge, CombineLatest, Switch, ObserveOn, Retry, Defer, Etc...


    public static class Observable
    {
        // TODO:need scheduler
		/// <summary>
		/// 已经讲完
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="count">Count.</param>
        public static IObservable<int> Range(int start, int count)
        {
            return AnonymousObservable.Create<int>(observer =>
            {
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        observer.OnNext(start++);
                    }
                    observer.OnCompleted();
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }

                // TODO:Cancellable!
				return Disposable.Empty;
            });
        }

		/// <summary>
		/// 已经学习
		/// </summary>
		/// <param name="source"> randomRange </param>
		/// <param name="selector"> 变换函数 </param>
		/// <typeparam name="T">source 发射过来的数据类型</typeparam>
		/// <typeparam name="TR">selector 中的返回值类型</typeparam>
        public static IObservable<TR> Select<T, TR>(this IObservable<T> source, Func<T, TR> selector)
        {
			return new Observable<TR>(observer =>
            {
                
                //这种写法的意思就是 先执行这段代码 然后返回一个原本source注册时应该返回的disposable(定义在原来的observable里)
                return source.Subscribe(new Observer<T>(x =>//把原来的回调包装进一个新的observer里（旧的指lamda传进来的参数）
                {                                            //对这个obserble进行注册时 传进来的observer自动转换成新的observer
                    var v = selector(x);
                    observer.OnNext(v);
                }, observer.OnError, observer.OnCompleted));
            });
        }

		/// <summary>
		/// 已经学习
		/// </summary>
		/// <param name="source">Source.</param>
		/// <param name="predicate">Predicate.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
        public static IObservable<T> Where<T>(this IObservable<T> previousObservable, Func<T, bool> predicate)
        {
            return AnonymousObservable.Create<T>(nextObserver =>//看上去不一样 神经病啊 明明一样 为什么要这么写？
            {
				return previousObservable.Subscribe(AnonymousObserver.Create<T>(x =>//关键字 同样是 新的观察者
                {                                                   //也就是说对这个obs注册的观察者 自动转换成一个新的观察者 !!! 
                    if (predicate(x))
                    {
						nextObserver.OnNext(x);
                    }
				}, nextObserver.OnError, nextObserver.OnCompleted));
            });
        }

        public static IObservable<TR> SelectMany<T, TR>(this IObservable<T> source, Func<T, IObservable<TR>> selector)
        {
            return source.Select(selector).Merge();
        }

        public static IObservable<TR> SelectMany<T, TC, TR>(this IObservable<T> source, Func<T, IObservable<TC>> collectionSelector, Func<T, TC, TR> selector)
        {
            return source.SelectMany(x => collectionSelector(x).Select(y => selector(x, y)));
        }

        public static IObservable<T> Merge<T>(this IObservable<IObservable<T>> sources)
        {
            return AnonymousObservable.Create<T>(observer =>
            {
                var group = new CompositeDisposable();

                var first = sources.Subscribe(innerSource =>
                {
                    var d = innerSource.Subscribe(observer.OnNext);
                    group.Add(d);
                }, observer.OnError, observer.OnCompleted);

                group.Add(first);

                return group;
            });
        }

        public static IObservable<T> Delay<T>(this IObservable<T> source, TimeSpan dueTime)
        {
            return source.Delay(dueTime, Scheduler.GameLoop);
        }

        public static IObservable<T> Delay<T>(this IObservable<T> source, TimeSpan dueTime, IScheduler scheduler)
        {
            return AnonymousObservable.Create<T>(observer =>
            {
                var group = new CompositeDisposable();

                var first = source.Subscribe(x =>
                {
                    var d = scheduler.Schedule(() => observer.OnNext(x), dueTime);
                    group.Add(d);
                }, observer.OnError, observer.OnCompleted);

                group.Add(first);

                return group;
            });
        }

        public static IObservable<T> ObserveOn<T>(this IObservable<T> source, IScheduler scheduler)
        {
            return AnonymousObservable.Create<T>(observer =>
            {
                var group = new CompositeDisposable();

                var first = source.Subscribe(x =>
                {
                    var d = scheduler.Schedule(() => observer.OnNext(x));
                    group.Add(d);
                }, observer.OnError, observer.OnCompleted);

                group.Add(first);

                return group;
            });
        }

        public static IObservable<T> DistinctUntilChanged<T>(this IObservable<T> source)
        {
            return source;
            // throw new NotImplementedException();
        }
    }
}