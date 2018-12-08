using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Student
    {
       public int id; public string name; public int Age;
    }

    public class Where : MonoBehaviour
    {
        void Start()
        {
            #region linq  
         
            List<Student> stuLst = new List<Student>() {
                new Student{id=0 },
                new Student{id=1,name="张三"},
                new Student{id=2,name="李四",Age=100}
            };
            var query = from Student in stuLst where Student.Age == 100 select Student;
            query.ToList().ForEach(s => Debug.Log(s.name));
            #endregion
            #region UniRx
            var query2 = from updateEvent in Observable.EveryUpdate()
                         where Input.GetMouseButtonDown(0)
                         select updateEvent;
            query2.Subscribe(_ => print("leftMouseClicked"));
            #endregion
        }
    }
}   //能写成表达式的会写成表达式 后面有些不能的 用链式展示
