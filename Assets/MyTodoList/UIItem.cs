using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace MyTodoList
{
    /// <summary>
    /// 单个itemModel的Controller
    /// </summary>
    public class UIItem : MonoBehaviour
    {
        [SerializeField] private Text mTxtContent;
        [SerializeField] private Button mBtnClear;
        [SerializeField] private Image mImgBg;
        [SerializeField] public Image mHightLight;

        public TodoItem mItemModel; //再强调一遍, UI类已经被当作Controller了 持有数据一点也没有问题     
        public IObservable<int> UIItemBgOnClicked;
        public IObservable<int> UIItemClearOnClicked;
        public void SetItemModel(TodoItem model)//提供设置数据的接口 
        {
            mItemModel = model;                                                             
            mItemModel.Content.SubscribeToText(mTxtContent); //ui关联数据   
        }
       
        private void Awake()
        {
            UIItemClearOnClicked = mBtnClear.OnClickAsObservable().Select(_ => mItemModel.Id);
            UIItemBgOnClicked = mImgBg.OnPointerClickAsObservable().Select(_ =>mItemModel.Id);
            UIItemBgOnClicked.Subscribe(_ => 
            {
                RPLink.GetRp(RPLink.EventMaskEnable).Value = true;//修改全局rp，以通知他处
                mHightLight.enabled = true;
            });                                    //变false
            RPLink.GetRp(RPLink.EventMaskEnable).Where(e => !e).Subscribe(e =>mHightLight.enabled=false).AddTo(this);//订阅全局rp
        }
    }
    /// <summary>
    /// UIItems 的封装  (Controller的Controller)
    /// </summary>
    public class ItemsListCtl
    {    
        private Transform mItemsRoot;//scrollView的content节点
        private GameObject mItemPrf;//uiItem预制体
        private TodoItemCollection mItemModelLst;//数据集合         
        public int mCurClickedUiId=-1;//当前选中的Id
        public ItemsListCtl(Transform root)
        {
            mItemPrf = Resources.Load<GameObject>("prfTodoItem");//加载预制体文件
            mItemsRoot = root;
            mItemModelLst =TodoItemCollection.Load();//初始化数据       
            mItemModelLst.ForEach(item =>AddUIItem(item));      
        }

        private void AddUIItem(TodoItem itemModel)
        {
            var UiItem = Object.Instantiate(mItemPrf).GetComponent<UIItem>();//实例化UIprefab
            UiItem.transform.SetParent(mItemsRoot, false);
            UiItem.SetItemModel(itemModel);//给uiItem数据
            UiItem.UIItemClearOnClicked.Subscribe(id => {
                mItemModelLst.RemoveItem(id);
                Object.Destroy(UiItem.gameObject);
            });
            UiItem.UIItemBgOnClicked.Subscribe(id => mCurClickedUiId = id);       
        }
        /// <summary>
        /// 给高层调用的添加待办事项接口
        /// </summary>
        /// <param name="content"></param>
        public void AddUIItem(string content)
        {
            AddUIItem(mItemModelLst.AddItem(content));
        }
        /// <summary>
        /// 修改待办事项
        /// </summary>
        /// <param name="content"></param>
        public void ModifyUIItem(string content)
        {
            mItemModelLst.ModifyItem(mCurClickedUiId, content);
        }
        /// <summary>
        /// 拿取uiItem的内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCurUIContent()
        {
            return mItemModelLst.GetItemContent(mCurClickedUiId);
        }
       




    }
}
