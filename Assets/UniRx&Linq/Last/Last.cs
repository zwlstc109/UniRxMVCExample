using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
    public class Last : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //Last

            #region linq
            List<Student> stuLst = new List<Student>() {
                new Student{id=0,name="张三",Age=10},
                new Student{id=1,name="李四",Age=20},
                new Student{id=2,name="王五",Age=30}
            };
            var query = from Student in stuLst select Student;
            print(query.Last(stu=>stu.Age<30).name);
            
            #endregion
            #region UniRx
 //todo    
            #endregion
        }


    }
}