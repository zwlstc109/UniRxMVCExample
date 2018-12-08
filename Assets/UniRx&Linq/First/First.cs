using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class First: MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //first

            #region linq
            List<Student> stuLst = new List<Student>() {
                new Student{id=0,name="张三",Age=10},
                new Student{id=1,name="李四",Age=20},
                new Student{id=2,name="王五",Age=30}
            };
            var query = from Student in stuLst select Student;
            print(query.First(stu=>stu.Age>10).name);//可带条件
            
            #endregion
            #region UniRx
            var query2 = from updateEvent in Observable.EveryUpdate()
                         where Input.GetMouseButtonDown(0)
                         select "leftMouseClicked"; 
            query2.First().Subscribe(str => print(str));//只处理第一次点击
            #endregion
        }


    }
}