using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;
using UnityEngine.UI;
namespace UniRxOutLine
{
    public class Distinct : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //Distinct  剔除重复的  

          
            #region linq
            List<Student> stuLst = new List<Student>() {
                new Student{id=0,name="张三",Age=20},
                new Student{id=1,name="张三",Age=20},
                new Student{id=2,name="王五",Age=30}
            };
            var query = from Student in stuLst select Student.name;//剔除重名
            query.Distinct().ToList().ForEach(name => print(name));
            #endregion
            #region UniRx
            InputField ipt = GameObject.Find("ipt").GetComponent<InputField>(); ipt.Select();
            Text txt = GameObject.Find("txt").GetComponent<Text>();
            //一个过滤相同输入的输入框  （优雅！！）
            ipt.onEndEdit.AsObservable().Distinct().Subscribe(str => {     //从这里可以发现IObserable是UniRx对可迭代对象的扩展， 上下两种distinct重载数不同，UniRx的显然更灵活丰富       
                txt.text += str + "\n";
                ipt.text = "";
                ipt.Select();//但不是很懂为什么这句代码没效果 本想输完继续输
            });
            #endregion

        }
       
    }
}