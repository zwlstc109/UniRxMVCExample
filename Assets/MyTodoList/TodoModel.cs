using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace  MyTodoList
{
    [Serializable]
    public class TodoItem
    {
        public StringReactiveProperty Content;
        public BoolReactiveProperty Completed;
        public int Id;//数据的标识 是必须的 否则当它处在一个集合中时怎么找到它呢...
        public TodoItem() { }

        public TodoItem(string content,int id)
        {
            Content = new StringReactiveProperty(content);
            Completed = new BoolReactiveProperty(false);
            Id = id;
        }
    }
   /// <summary>
   /// 数据集合类，有本地持久化
   /// </summary>
   [Serializable]
    public class TodoItemCollection
    {
        [NonSerialized] public static readonly string JSONKEY = "TodoItems";//playerPref的key
        [SerializeField] private int NewItemId = 0;//新item的唯一标识     
        [SerializeField]private List<TodoItem> mItemsLst = new List<TodoItem>();//UIItem的集合

        private TodoItemCollection() { }//强制类外部用load获取对象
        
        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        public static TodoItemCollection Load()
        {
            string json = PlayerPrefs.GetString(JSONKEY);
            if (string.IsNullOrEmpty(json))
                return new TodoItemCollection();
            else
                return JsonUtility.FromJson<TodoItemCollection>(json);
            //return new TodoItemCollection();
        }
        
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="item"></param>
        public TodoItem AddItem(string content)
        {
            var item = new TodoItem(content, NewItemId++);
            mItemsLst.Add(item);
            SaveToJson();
            return item;
        }
      
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="id"></param>
        public void RemoveItem(int id)
        {
            foreach (var item in mItemsLst)
            {
                if (item.Id == id)
                {
                    mItemsLst.Remove(item);
                    SaveToJson();
                    break;
                }
            }
        }
        
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        public void ModifyItem(int id, string content)
        {
            foreach (var item in mItemsLst)
            {
                if (item.Id == id)
                {
                    item.Content.Value = content;
                    SaveToJson();
                    break;
                }
            }

        }
        /// <summary>
        /// 查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetItemContent(int id)
        {
            foreach (var item in mItemsLst)
            {
                if (item.Id == id)
                {
                    return item.Content.Value;
                }
            }
            return null;
        }
        /// <summary>
        /// 逐个访问
        /// </summary>
        /// <param name="handler"></param>
        public void ForEach(Action<TodoItem> handler)
        {
            if (handler == null) return;
            foreach (var item in mItemsLst)
            {
                handler.Invoke(item);
            }
        }

      /// <summary>
      /// 保存本地
      /// </summary>
        public void SaveToJson()
        {
            PlayerPrefs.SetString(JSONKEY, JsonUtility.ToJson(this));
            //Debug.Log(PlayerPrefs.GetString(JSONKEY));
        }
    }
}

