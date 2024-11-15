using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemPrefabConfData
{
    public GameObject mItemPrefab = null;
    /// <summary>
    /// 条目间距
    /// </summary>
    public float mPadding = 0;
    public int mInitCreateCount = 0;
    public float mStartPosOffset = 0;
}

public class LoopListViewInitParam
{
    /// <summary>
    /// 数值大小:mDistanceForRecycle0 > mDistanceForNew0
    /// </summary>
    public float mDistanceForRecycle0 = 300;
    public float mDistanceForNew0 = 200;
    /// <summary>
    /// 数值大小:mDistanceForRecycle1 > mDistanceForRecycle1
    /// </summary>
    public float mDistanceForRecycle1 = 300;
    public float mDistanceForNew1 = 200;
    public float mSmoothDumpRate = 0.3f;
    public float mSnapFinishThreshold = 0.01f;
    public float mSnapVecThreshold = 145;
    /// <summary>
    /// 条目默认尺寸(包括padding)
    /// </summary>
    public float mItemDefaultWithPaddingSize = 100;

    public static LoopListViewInitParam CopyDefaultInitParam()
    {
        return new LoopListViewInitParam();
    }
}

/// <summary>
/// 注意:
///     1.所有条目的缩放比例必须是Vector3.one
/// </summary>
public class LoopListView : MonoBehaviour
{
    #region Inspector面板上显示的属性
    /// <summary>
    /// 条目Item列表
    /// </summary>
    [SerializeField]
    List<ItemPrefabConfData> mItemPrefabDataList = new List<ItemPrefabConfData>();
    /// <summary>
    /// 条目排列类型
    /// </summary>
    [SerializeField]
    private ListItemArrangeType mArrangeType = ListItemArrangeType.TopToBottom;
    public ListItemArrangeType ArrangeType
    {
        get { return mArrangeType; }
        set { mArrangeType = value; }
    }
    /// <summary>
    /// 滑动条是否显示标志位
    /// </summary>
    [SerializeField]
    private bool mSupportScrollBar;
    /// <summary>
    /// 瞄点(Item和Viewport)是否可重设标志位
    /// </summary>
    [SerializeField]
    private bool mItemSnapEnable;
    /// <summary>
    /// 条目的瞄点
    /// </summary>
    [SerializeField]
    private Vector2 mItemSnapPivot = Vector2.zero;
    /// <summary>
    /// Viewport的瞄点
    /// </summary>
    [SerializeField]
    private Vector2 mViewportSnapPivot = Vector2.zero;
    #endregion

    private LoopListViewInitParam mInitParam;
    private ScrollRect mScrollRect;
    RectTransform mScrollRectTransform = null;
    RectTransform mViewportRectTransform = null;
    RectTransform mContainerTrans;
    ItemPosMgr mItemPosMgr = null;
    bool mIsVertList;
    public bool IsVertList
    {
        get { return mIsVertList; }
    }
    Func<LoopListView, int, LoopListViewItem> mOnGetItemByIndex;
    /// <summary>
    /// 条目索引[0, itemTotalCount - 1]
    /// </summary>
    private int mItemIndex = -1;
    /// <summary>
    /// 条目id
    /// </summary>
    private int mItemId = -1;

    /// <summary>
    /// 初始化列表
    /// </summary>
    /// <param name="itemTotalCount"></param>
    /// <param name="onGetItemByIndex">委托参数:LoopListView,int;委托返回值:LoopListView</param>
    /// <param name="initParam"></param>
    public void InitListView(int itemTotalCount, Func<LoopListView, int, LoopListView> onGetItemByIndex,
        LoopListViewInitParam initParam = null)
    {
        if (initParam != null)
            mInitParam = initParam;
        mScrollRect = gameObject.GetComponent<ScrollRect>();
        if (!mScrollRect)
        {
            Debug.LogError("ListView Init Failed! ScrollRect component not found");
            return;
        }
        if (mInitParam.mDistanceForRecycle0 <= mInitParam.mDistanceForNew0)
        {
            Debug.LogError("mDistanceForRecycle0 should be bigger than mDistanceForNew0");
            return;
        }
        if (mInitParam.mDistanceForRecycle1 <= mInitParam.mDistanceForNew1)
        {
            Debug.LogError("mDistanceForRecycle1 should be bigger than mDistanceForNew1");
            return;
        }
        //todo
        mItemPosMgr = new ItemPosMgr(mInitParam.mItemDefaultWithPaddingSize);
        mScrollRectTransform = mScrollRect.GetComponent<RectTransform>();
        mViewportRectTransform = mScrollRect.viewport;
        mContainerTrans = mScrollRect.content;
        if (mViewportRectTransform == null)
        {
            mViewportRectTransform = mScrollRectTransform;
        }
        if (mScrollRect.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport && mScrollRect.horizontalScrollbar)
        {
            Debug.LogError("ScrollRect.horizontalScrollbarVisibility cannot be set to AutoHideAndExpandViewport");
        }
        if (mScrollRect.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport && mScrollRect.verticalScrollbar)
        {
            Debug.LogError("ScrollRect.verticalScrollbarVisibility cannot be set to AutoHideAndExpandViewport");
        }
        mIsVertList = (mArrangeType == ListItemArrangeType.TopToBottom || mArrangeType == ListItemArrangeType.BottomToTop);
        mScrollRect.horizontal = !mIsVertList;
        mScrollRect.vertical = mIsVertList;
        SetScrollbarListener();
        AdjustPivot(mViewportRectTransform);
        AdjustAnchor(mContainerTrans);
        AdjustContainerPivot(mContainerTrans);
        InitItemPool();
    }

    public LoopListView NewListViewItem(string itemPrefabName)
    {
        //todo
        return null;
    }

    public void SetListItemCount(int itemCount, bool resetPos = true)
    {
        //todo
    }

    /// <summary>
    /// 在???中查找可见条目
    /// </summary>
    /// <param name="itemIndex">取值范围[0, mItemTotalCount - 1]</param>
    /// <returns>如果itemIndex对应的条目不可见,则返回null</returns>
    public LoopListView GetShownItemByItemIndex(int itemIndex)
    {
        //todo
        return null;
    }

    /// <summary>
    /// 在mItemList中查找可见条目
    /// </summary>
    /// <param name="index">[0, mItemList.Count - 1]</param>
    /// <returns></returns>
    public LoopListView GetShownItemByIndex(int index)
    {
        //todo
        return null;
    }

    public int ShowItemCount
    {
        get
        {
            //todo
            return 0;
        }
    }

    public void RefreshItemByItemIndex(int itemIndex)
    {
        //todo
    }

    public void RefreshAllShownItem()
    {
        //todo
    }

    public void MovePanelToItemIndex(int itemIndex, float offset)
    {
        //todo
    }

    public void OnItemSizeChanged(int itemIndex)
    {
        //todo
    }

    public void FinishSnapImmediately()
    {
        //todo
    }

    public int CurSnapNearestItemIndex
    {
        get
        {
            //todo
            return 0;
        }
    }

    public void SetSnapTargetItemIndex(int itemIndex)
    {
        //todo
    }

    public void ClearSnapData()
    {
        //todo
    }

    //==========================================================================================
    void SetScrollbarListener()
    {
        //todo
    }

    void AdjustPivot(RectTransform rectTransform)
    {
        //todo
    }

    private void AdjustAnchor(RectTransform rectTransform)
    {
        //todo
    }

    private void AdjustContainerPivot(RectTransform rectTransform)
    {
        //todo
    }

    private void InitItemPool()
    {
        //todo
    }
    //==========================================================================================
}