using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
  
    public class TakeUntilDestory : MonoBehaviour
    {
        void Start()
        {
            //TakeUntilDestory  字面意思..

            Observable.EveryUpdate().TakeUntilDestroy(this)//这个this 指定的虽然是这个脚本 但是UniRx的意思是让你指定gameObject
                .Subscribe(_ => print(_));

            Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => Destroy(gameObject)).AddTo(this);//所以这里只有销毁gameobject才有用 写this没有用

        }

    }
}  
