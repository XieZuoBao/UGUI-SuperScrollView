using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    //todo
}

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
}