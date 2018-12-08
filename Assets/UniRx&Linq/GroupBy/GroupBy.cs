using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

namespace UniRxOutLine
{
 public class G
    {
        public int id;
        public int group;
    }

    public class GroupBy : MonoBehaviour
    {        
        
        void Start()
        {
            //GroupBy 分组

            #region linq
            List<G> gLst = new List<G>() {
                new G() { id = 0, group = 0 },
                new G() { id = 1, group = 1 },
                new G() { id = 2, group = 2 },
                new G() { id = 3, group = 0 },
                new G() { id = 4, group = 2 },
                new G() { id = 5, group = 1 }};
            //group后 原序列被重组成一个一个的分组
            gLst.GroupBy(g => g.group).ToList().ForEach(group=>group.ToList().ForEach(g=>Debug.LogFormat("id:{0} gourp:{1}",g.id,g.group)));
            #endregion
            #region UniRx
            ReactiveProperty<int> rpInt = new ReactiveProperty<int>(0);
            rpInt.GroupBy(i => (i & 1) == 1 ? "奇数" : "偶数")//groupby 需要一个selector作为groupKey 这个操作可以理解为把每个接受到的发射都有一个自定义标记 （不同于linq版需要完整序列）
                .Subscribe(group=>group.Subscribe(i=>Debug.LogFormat("{0}:{1}",group.Key,i)));
            rpInt.Value = 1;
            rpInt.Value = 2;
            rpInt.Value = 3;
            rpInt.Value = 4;
            rpInt.Value = 5;
            #endregion
        }

       
    }
}