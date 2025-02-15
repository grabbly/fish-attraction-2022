using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ReferenceFinderOnScene : EditorWindow
{
    Object SearchPlace;
    Object Source;
    Vector2 ScrollPosition;
    List<ReferenceData> ReferencedBy = new List<ReferenceData>();

    [MenuItem("Window/Tools/Reference finder on scene")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ReferenceFinderOnScene));
    }

    void OnGUI()
    {

        EditorGUILayout.BeginHorizontal();
        Source = EditorGUILayout.ObjectField(Source, typeof(Object), true);
//        EditorGUILayout.LabelField("Object");
        EditorGUILayout.EndHorizontal();
//        EditorGUILayout.BeginHorizontal();
//        SearchPlace = EditorGUILayout.ObjectField(SearchPlace, typeof(Object), true);
//        EditorGUILayout.LabelField("(Out of order) Conteiner (leave empty for scene)");
//        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Find!"))
        {
            if (Source == null)
                ShowNotification(new GUIContent("No object selected for searching"));
            else
            {
                ReferencedBy = FindReferencesTo(Source);
                if (ReferencedBy.Count == 0)
                    ShowNotification(new GUIContent("No references have found"));
            }
        }

        ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);
        for (int i = 0; i < ReferencedBy.Count; i++)
        {
            EditorGUILayout.ObjectField(ReferencedBy[i].ReferenceObject, typeof(Object), true);
            EditorGUILayout.LabelField("Script : " + ReferencedBy[i].ReferenceScript);
            EditorGUILayout.LabelField("Field : " + ReferencedBy[i].ReferenceField);
            EditorGUILayout.LabelField("Reference to : " + ReferencedBy[i].ReferenceTo);
        }
        EditorGUILayout.EndScrollView();
    }

    private List<ReferenceData> FindReferencesTo(Object to)
    {
        var referencedBy = new List<ReferenceData>();
        var allObjects = Object.FindObjectsOfType<GameObject>();
        var referenceTo = new List<Object>();
        referenceTo.Add(to);

        //if gameobject, find references to all components of it
        var toGO = to as GameObject;
        if (toGO)
        {
            var components = toGO.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                referenceTo.Add(components[i]);
            }
        }

        IEnumerable<GameObject> allObjcts = SearchPlace != null ? AllObjectsInPlace() : AllSceneObjects() ;
        foreach (var go in allObjcts)
        {
            if (PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
            {
                if (PrefabUtility.GetCorrespondingObjectFromSource(go) == to)
                {
                    referencedBy.Add(new ReferenceData(go, "Prefab instance", "", ""));
                }
            }

            var components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                var c = components[i];
                if (!c) continue;

                var so = new SerializedObject(c);
                var sp = so.GetIterator();

                while (sp.NextVisible(true))
                    if (sp.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        for (int k = 0; k < referenceTo.Count; k++)
                        {
                            if (sp.objectReferenceValue == referenceTo[k])
                            {
                                referencedBy.Add(new ReferenceData(c.gameObject, c.GetType().ToString(), 
                                    sp.propertyPath, referenceTo[k].GetType().ToString()));
                            }
                        }
                    }
            }
        }

        return referencedBy;
    }

    
    public IEnumerable<GameObject> AllObjectsInPlace()
    {
        var queue = new Queue<GameObject>();
//        Debug.Log((SearchPlace is GameObject));
        GameObject sp = SearchPlace as GameObject;
        foreach (var root in sp.GetComponentsInChildren<GameObject>(true))
        {
            queue.Enqueue(root);
        }

        while (queue.Count > 0)
        {
            var curGO = queue.Dequeue();
            for (int i = 0; i < curGO.transform.childCount; i++)
            {
                queue.Enqueue(curGO.transform.GetChild(i).gameObject);
            }

            yield return curGO;
        }
    }
    
    public static IEnumerable<GameObject> AllSceneObjects()
    {
        var queue = new Queue<GameObject>();

        foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            queue.Enqueue(root);
        }

        while (queue.Count > 0)
        {
            var curGO = queue.Dequeue();
            for (int i = 0; i < curGO.transform.childCount; i++)
            {
                queue.Enqueue(curGO.transform.GetChild(i).gameObject);
            }

            yield return curGO;
        }
    }

    class ReferenceData
    {
        public ReferenceData(GameObject ro, string rs, string rf, string rt)
        {
            ReferenceObject = ro;
            ReferenceScript = rs;
            ReferenceField = rf;
            ReferenceTo = rt;
        }

        public GameObject ReferenceObject;
        public string ReferenceScript;
        public string ReferenceField;
        public string ReferenceTo;
    }
}