using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Select : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //Select 只接受查询目标的某个属性 或是干脆转换成其他属性  我更愿意把他理解成'转换器'


            //#region linq
            //List<Student> stuLst = new List<Student>() {
            //    new Student{id=0 },
            //    new Student{id=1,name="张三"},
            //    new Student{id=2,name="李四",Age=100}
            //};
            //var query = from Student in stuLst where Student.Age == 100 select Student.name;
            //query.ToList().ForEach(name => Debug.Log(name));
            //#endregion
            //#region UniRx
            //var query2 = from updateEvent in Observable.EveryUpdate()
            //             where Input.GetMouseButtonDown(0)
            //             select "leftMouseClicked";  //对流的全新理解
            //query2.Subscribe(str => print(str));
            //#endregion

            //阅读源码后 发现另一个版本 index就是一个从0开始一直++的数字 初步想法可以用统计发射总数?
            Observable.EveryUpdate().Where(_=>Input.GetMouseButtonDown(0)). Select((_, index) =>index).Subscribe(_ => print("点击了"+_+"次"));
            
        }


    }
}