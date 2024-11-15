using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LoopListView))]
public class LoopListViewEditor : Editor
{
    //图文信息
    private GUIContent mItemPrefabListContent = new GUIContent("ItemPrefabList");
    //与LoopListView中的mItemPrefabDataList对应(映射不成功会报错)
    private SerializedProperty mItemPrefabDataList;

    private GUIContent mSupportScorllBarContent = new GUIContent("SupportScrollBar");
    private SerializedProperty mSupportScrollBar;

    private GUIContent mItemSnapEnableContent = new GUIContent("ItemSnapEnable");
    private SerializedProperty mItemSnapEnable;

    private GUIContent mArrangeTypeContent = new GUIContent("ArrangeType");
    private SerializedProperty mArrangeType;

    private GUIContent mItemSnapPivotContent = new GUIContent("ItemSnapPivot");
    private SerializedProperty mItemSnapPivot;

    private GUIContent mViewportSnapPivotContent = new GUIContent("ViewportSnapPivot");
    private SerializedProperty mViewportSnapPivot;
    
    protected virtual void OnEnable()
    {
        mItemPrefabDataList = serializedObject.FindProperty("mItemPrefabDataList");
        mSupportScrollBar = serializedObject.FindProperty("mSupportScrollBar");
        mItemSnapEnable = serializedObject.FindProperty("mItemSnapEnable");
        mArrangeType = serializedObject.FindProperty("mArrangeType");
        mItemSnapPivot = serializedObject.FindProperty("mItemSnapPivot");
        mViewportSnapPivot = serializedObject.FindProperty("mViewportSnapPivot");
    }
    
    public override void OnInspectorGUI()
    {
        //推荐写法:serializedObject.Update()放在第一行,serializedObject.ApplyModifiedProperties()放在最后一行
        serializedObject.Update();
        LoopListView view = serializedObject.targetObject as LoopListView;
        if (!view)
            return;
        ShowItemPrefabDataList(view);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(mSupportScrollBar, mSupportScorllBarContent);
        EditorGUILayout.PropertyField(mItemSnapEnable, mItemSnapEnableContent);
        if (mItemSnapEnable.boolValue)
        {
            EditorGUI.indentLevel += 1; //缩进显示
            EditorGUILayout.PropertyField(mItemSnapPivot, mItemSnapPivotContent);
            EditorGUILayout.PropertyField(mViewportSnapPivot, mViewportSnapPivotContent);
            EditorGUI.indentLevel -= 1; 
        }
        EditorGUILayout.PropertyField(mArrangeType, mArrangeTypeContent);
        serializedObject.ApplyModifiedProperties();
    }
    
    void ShowItemPrefabDataList(LoopListView view)
    {
        EditorGUILayout.PropertyField(mItemPrefabDataList, mItemPrefabListContent, false);
        EditorGUI.indentLevel += 1; //缩进显示
        //ItemPrefabList新增按钮:复制最后一个Item的数据,并置空
        if (GUILayout.Button("Add New"))
        {
            mItemPrefabDataList.InsertArrayElementAtIndex(mItemPrefabDataList.arraySize);
            if (mItemPrefabDataList.arraySize > 0)
            {
                SerializedProperty itemData = mItemPrefabDataList.GetArrayElementAtIndex(mItemPrefabDataList.arraySize - 1);
                SerializedProperty mItemPrefab = itemData.FindPropertyRelative("mItemPrefab");
                mItemPrefab.objectReferenceValue = null;
            }
        }
        //绘制ItemPrefabList的内容
        int removeIndex = -1;
        EditorGUILayout.PropertyField(mItemPrefabDataList.FindPropertyRelative("Array.size"));
        for (int i = 0; i < mItemPrefabDataList?.arraySize; i++)
        {
            SerializedProperty itemData = mItemPrefabDataList.GetArrayElementAtIndex(i);
            //以下四个属性要与ItemPrefabConfData中定义的字段相互映射
            SerializedProperty mItemPrefab = itemData.FindPropertyRelative("mItemPrefab");
            SerializedProperty mPadding = itemData.FindPropertyRelative("mPadding");
            SerializedProperty mInitCreateCount = itemData.FindPropertyRelative(("mInitCreateCount"));
            SerializedProperty mStartPosOffset = itemData.FindPropertyRelative("mStartPosOffset");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(itemData, false);
            //移除按钮
            if (GUILayout.Button("Remove"))
                removeIndex = i;
            EditorGUILayout.EndHorizontal();
            if (!itemData.isExpanded)
                continue;
            mItemPrefab.objectReferenceValue = EditorGUILayout.ObjectField("mItemPrefab", mItemPrefab.objectReferenceValue, typeof(GameObject), true);
            mPadding.floatValue = EditorGUILayout.FloatField("ItemPadding", mPadding.floatValue);
            mInitCreateCount.intValue = EditorGUILayout.IntField("InitCreateCount", mInitCreateCount.intValue);
            if (view.ArrangeType == ListItemArrangeType.TopToBottom || view.ArrangeType == ListItemArrangeType.BottomToTop)
                mStartPosOffset.floatValue = EditorGUILayout.FloatField("XPosOffset", mStartPosOffset.floatValue);
            else
                mStartPosOffset.floatValue = EditorGUILayout.FloatField("YPosOffset", mStartPosOffset.floatValue);
        }
        //移除回调
        if (removeIndex >= 0)
            mItemPrefabDataList.DeleteArrayElementAtIndex(removeIndex);
        EditorGUI.indentLevel -= 1;
    }
}