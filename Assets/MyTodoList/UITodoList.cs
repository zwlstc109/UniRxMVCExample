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
       
        [SerializeField] private Button mBtnSure;//确定按钮
        [SerializeField] private InputField mIptContent;//输入框
        [SerializeField] private Button mBtnCancel;//输入框取消按钮
        [SerializeField] private Transform mItemsRoot;//scrollview的content节点
        [SerializeField] private Image mImgEventMask;//遮罩 用来 在点击item进入修改模式时盖住scrollview


        private ItemsListCtl mItemsLstCtl;//items列表的控制器
        private void Awake()
        {
            mItemsLstCtl = new ItemsListCtl(mItemsRoot);
            RPLink.GetRp(RPLink.EventMaskEnable).Value = false;//一个全局的reactiveProperty 用来表示遮罩的enable
        }
        private void Start()
        {
            //把输入框是否为空转化成一个bool值 关联 按钮 的禁用显示
            mIptContent.OnValueChangedAsObservable().Select(str => string.IsNullOrEmpty(str)).Subscribe(
                emptyIpt =>
                {
                    mBtnSure.interactable = !emptyIpt;
                    if (!emptyIpt)
                        mBtnCancel.image.enabled = true;
                    else if (!mImgEventMask.enabled)
                        mBtnCancel.image.enabled = false;
                });

            //确认按钮点击
            mBtnSure.OnClickAsObservable().Subscribe(
                _ =>
                {
                    if (!mImgEventMask.enabled) //非修改模式下 添加待办事项（仅用遮罩得enable来代表是否修改模式）
                        mItemsLstCtl.AddUIItem(mIptContent.text);
                    else // 修改模式下 修改当前选中的待办事项 （当前选中逻辑由列表控制器负责）
                       mItemsLstCtl.ModifyUIItem(mIptContent.text);
                    //重置一切
                    mIptContent.text = "";
                    mBtnCancel.image.enabled = false;
                    RPLink.GetRp(RPLink.EventMaskEnable).Value = false;
                });
             //订阅一个全局的reaciveProperty(全局可修改可订阅) 他在此处用来关联遮罩的开启与否
             RPLink.GetRp(RPLink.EventMaskEnable).Subscribe(
                enabled =>
                {   
                    mImgEventMask.enabled = enabled;//直接关联遮罩的enable
                    if (enabled)//如果从false变成了true
                    {
                        mIptContent.Select();
                        mIptContent.text = mItemsLstCtl.GetCurUIContent();
                    }
                });
            
            //遮罩开启时才有的点击事件
            mImgEventMask.OnPointerClickAsObservable().Subscribe(_ => RPLink.GetRp(RPLink.EventMaskEnable).Value = false);
            //取消输入按钮
            mBtnCancel.OnClickAsObservable().Subscribe(_ =>
            {
                if (mIptContent.text == "")
                {
                    RPLink.GetRp(RPLink.EventMaskEnable).Value = false;
                    mBtnCancel.image.enabled = false;
                }
                else
                    mIptContent.text = "";
            });

        }
    }
}