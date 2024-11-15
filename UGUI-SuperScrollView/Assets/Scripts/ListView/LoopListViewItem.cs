using UnityEngine;

public class LoopListViewItem : MonoBehaviour
{
    object mUserObjectData = null;
    public object UserObjectData
    {
        get { return mUserObjectData; }
        set { mUserObjectData = value; }
    }

    int mUserInitData1 = 0;
    public int UserInitData1
    {
        get { return mUserInitData1; }
        set { mUserInitData1 = value; }
    }

    int mUserInitData2 = 0;
    public int UserInitData2
    {
        get { return mUserInitData2; }
        set { mUserInitData2 = value; }
    }

    string mUserStringData1 = null;
    public string UserStringData1
    {
        get { return mUserStringData1; }
        set { mUserStringData1 = value; }
    }

    string mUserStringData2 = null;
    public string UserStringData2
    {
        get { return mUserStringData2; }
        set { mUserStringData2 = value; }
    }

    float mDistanceWithViewportSnapCenter;
    public float DistanceWithViewportSnapCenter
    {
        get { return mDistanceWithViewportSnapCenter; }
        set { mDistanceWithViewportSnapCenter = value; }
    }

    float mStartPosOffset;
    public float StartPosOffset
    {
        get { return mStartPosOffset; }
        set { mStartPosOffset = value; }
    }

    int mItemCreatedCheckFrameCount;
    public int ItemCreatedCheckFrameCount
    {
        get { return mItemCreatedCheckFrameCount; }
        set { mItemCreatedCheckFrameCount = value; }
    }

    float mPadding;
    public float Padding
    {
        get { return mPadding; }
        set { mPadding = value; }
    }

    RectTransform mCachedRectTransform;
    public RectTransform CachedRectTransform
    {
        get
        {
            if (!mCachedRectTransform)
            {
                mCachedRectTransform = gameObject.GetComponent<RectTransform>();
            }
            return mCachedRectTransform;
        }
    }

    string mItemPrefabName;
    public string ItemPrefabName
    {
        get { return mItemPrefabName; }
        set { mItemPrefabName = value; }
    }

    int mItemIndex = -1;
    public int ItemIndex
    {
        get { return mItemIndex; }
        set { mItemIndex = value; }
    }

    int mItemId;
    public int ItemId
    {
        get { return mItemId; }
        set { mItemId = value; }
    }

    bool mIsInitHandlerCalled;
    public bool IsInitHandlerCalled
    {
        get { return mIsInitHandlerCalled; }
        set { mIsInitHandlerCalled = value; }
    }

    LoopListView mParentListView = null;
    public LoopListView ParentListView
    {
        get { return mParentListView; }
        set { mParentListView = value; }
    }

    public float TopY
    {
        get
        {
            //todo
            return 0;
        }
    }

    public float BottomY
    {
        get
        {
            //todo
            return 0;
        }
    }

    public float LeftX
    {
        get
        {
            //todo
            return 0;
        }
    }

    public float RightX
    {
        get
        {
            //todo
            return 0;
        }
    }

    public float ItemSize
    {
        get
        {
            if (ParentListView.IsVertList)
                return CachedRectTransform.rect.height;
            else
                return CachedRectTransform.rect.width;
        }
    }

    public float ItemSizeWithPadding
    {
        get { return ItemSize + mPadding; }
    }
}