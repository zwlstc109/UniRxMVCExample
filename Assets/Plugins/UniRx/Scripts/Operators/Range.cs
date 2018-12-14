using System;

namespace UniRx.Operators
{
    internal class RangeObservable : OperatorObservableBase<int>
    {
        readonly int start;
        readonly int count;
        readonly IScheduler scheduler;

        public RangeObservable(int start, int count, IScheduler scheduler)
            : base(scheduler == Scheduler.CurrentThread)
        {
            if (count < 0) throw new ArgumentOutOfRangeException("count < 0");

            this.start = start;
            this.count = count;
            this.scheduler = scheduler;
        }

        protected override IDisposable SubscribeCore(IObserver<int> observer, IDisposable cancel)
        {
            observer = new Range(observer, cancel);//原始observer的代理 重写了那三个接口 使之可以捕获异常 并自动dispose

            if (scheduler == Scheduler.Immediate)
            {
                for (int i = 0; i < count; i++)
                {
                    int v = start + i;
                    observer.OnNext(v);
                }
                observer.OnCompleted();

                return Disposable.Empty;
            }
            else
            {
                var i = 0;
                return scheduler.Schedule((Action self) =>//当执行这个lamda时 会重复执行自己 直到满足一个条件
                {
                    if (i < count)
                    {
                        int v = start + i;
                        observer.OnNext(v);
                        i++;
                        self();
                    }
                    else
                    {
                        observer.OnCompleted();
                    }
                });
            }
        }

        class Range : OperatorObserverBase<int, int> //为什么要有这个range类 是为了包装原始observer 
        {
            public Range(IObserver<int> observer, IDisposable cancel)
                : base(observer, cancel)//缓存的observer就是被代理的对象
            {
            }

            public override void OnNext(int value)//这些就是包装后的重写  这个包装有点像是代理
            {
                try
                {
                    base.observer.OnNext(value);
                }
                catch
                {
                    Dispose();
                    throw;
                }
            }

            public override void OnError(Exception error)
            {
                try { observer.OnError(error); }
                finally { Dispose(); }
            }

            public override void OnCompleted()
            {
                try { observer.OnCompleted(); }
                finally { Dispose(); }
            }
        }
    }
}