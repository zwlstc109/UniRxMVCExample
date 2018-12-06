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
        public ReactiveProperty<bool> OnClicked;//点击标记 
        public void SetItemModel(TodoItem model)//提供设置数据的接口 
        {
            mItemModel = model;
            mItemModel.Content.Subscribe(str => mTxtContent.text = str);//ui关联数据
            OnClicked = new ReactiveProperty<bool>(false);//初始化
        }
        public void CloseHighLight() { mHightLight.enabled = false; }//关闭高亮接口
        private void Start()
        {
            mBtnClear.OnClickAsObservable().Subscribe(_ => mItemModel.Completed.Value = true);
            mImgBg.OnPointerClickAsObservable().Subscribe(_ => { OnClicked.Value = true;mHightLight.enabled = true; /*Debug.Log("A" + mItemModel.Id);*/ });
        }
    }
    /// <summary>
    /// UIItem 集合的封装  (Controller的Controller)
    /// </summary>
    public class ItemsListCtl
    {
      
        private Transform mItemsRoot;//scrollView的content节点
        private GameObject mItemPrf;//uiItem预制体
        private TodoItemCollection mItemModelLst;//数据集合
        //private List<UIItem> mUIItemsLst;
        public BoolReactiveProperty Enable;
        private int mCurClickedUiId = -1;//当前选中的Id
        public ItemsListCtl(Transform root)
        {
            mItemPrf = Resources.Load<GameObject>("prfTodoItem");//加载预制体
            mItemsRoot = root;
            Enable = new BoolReactiveProperty(true);
            mItemModelLst =TodoItemCollection.Load();//初始化数据       
            mItemModelLst.ForEach(item =>AddUIItem(item));
       
        }

        private void AddUIItem(TodoItem itemModel)
        {
            var UiItem = Object.Instantiate(mItemPrf).GetComponent<UIItem>();//实例化UIprefab
            UiItem.transform.SetParent(mItemsRoot, false);
            var curId = itemModel.Id; 
            itemModel.Completed.Where(c => c).Subscribe(_ => {//只要这个数据的completed变成了false 就销毁数据和ui
                mItemModelLst.RemoveItem(curId);
                GameObject.Destroy(UiItem.gameObject);
    
            });
            UiItem.SetItemModel(itemModel);
            UiItem.OnClicked.Where(c=>c).Subscribe(c =>
            {
                Enable.Value = !c;//点击即使Scrollview失效 遮罩已订阅此变化 此时遮罩会开启
                UiItem.OnClicked.Value = false;//用完标记后归位  待下次继续触发
                mCurClickedUiId = curId;//锁定选中的item
                //Debug.Log("B" + mCurClickedUiId);
            });
            Enable.Where(e=>e).Subscribe(e => UiItem.CloseHighLight()).AddTo(UiItem);//全暗！管它暗哪个 ps: addTo用来绑定生命周期

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
       




    }
}
