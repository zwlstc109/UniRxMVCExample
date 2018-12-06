using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace MyTodoList
{
    /// <summary>
    /// 待办事项界面控制  
    /// </summary>
    /// 可以认为这就已经是MVC中的controller了！！！ 根本不需要再弄个其他类当作controller  
    /// 编辑器内编辑的UI界面可以认为就是View 界面更新这种简单工作可以交给UniRx的reactiveProperty  wtf! 优雅的mvc？
    public class UITodoList : MonoBehaviour
    {
        [SerializeField] private GameObject mItemPrefab;//uiItem的预制体
        [SerializeField] private Button mBtnSure;//确定按钮
        [SerializeField] private InputField mIptContent;//输入框
        [SerializeField] private Transform mItemsRoot;//scrollview的content节点
        [SerializeField] private Image mImgEventMask;//遮罩 用来 在点击item进入修改模式时盖住scrollview

        private ItemsListCtl mItemsLstCtl;//items列表的控制器
        private void Awake()
        {
            mItemsLstCtl = new ItemsListCtl(mItemsRoot);
        }
        private void Start()
        {      
            //讲输入框传来的值转化成一个bool值 关联 确认按钮 的interactable
            mIptContent.OnValueChangedAsObservable().Select(str => !string.IsNullOrEmpty(str)).SubscribeToInteractable(mBtnSure);
            //确认按钮点击事件源
            var btnSure = mBtnSure.OnClickAsObservable();
            //btnSure事件源 将被此条件[遮罩enable==false] 过滤      非修改模式下 添加待办事项
            btnSure.Where(_=> !mImgEventMask.enabled).Subscribe(_ => mItemsLstCtl.AddUIItem(mIptContent.text));
            //btnSure事件源 将被此条件[遮罩enable==true] 过滤       修改模式下 修改当前选中的待办事项 （当前选中逻辑由列表控制器负责）
            btnSure.Where(_ => mImgEventMask.enabled).Subscribe(_ => mItemsLstCtl.ModifyUIItem(mIptContent.text));
            //btnSure只要按过 清空输入框 去除遮罩
            btnSure.Subscribe(_ => { mIptContent.text = "";mItemsLstCtl.Enable.Value = true; });
            //列表控制器的一个reactive属性 可以订阅此属性的变化 该值表达了当前scrollview能否被选中
            var itemsLstEnable = mItemsLstCtl.Enable;
            //让遮罩订阅这个值，跟随一起变化
            itemsLstEnable.Subscribe(e => mImgEventMask.enabled = !e);
            //当遮罩开启时的遮罩点击事件
            mImgEventMask.OnPointerClickAsObservable().Where(_ =>mImgEventMask.enabled).Subscribe(_=>mItemsLstCtl.Enable.Value = true);
            
        }

    }
}