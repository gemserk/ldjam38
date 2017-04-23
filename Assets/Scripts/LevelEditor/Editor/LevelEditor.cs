
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Gemserk.LD38.Game.World;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class LevelEditor : Editor {

    static LevelEditor()
    {
        SuscribeToEvents();
    }

    public static void SuscribeToEvents()
    {
        //Debug.LogFormat ("Level Editor Suscribing to events", theInt);

        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    static void UnsuscribeFromEvents()
    {
        //Debug.LogFormat ("Level Editor UnSuscribing to events", theInt);
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }



    static bool ShouldRun()
    {
        var shouldRunFirstPass = !EditorApplication.isPlaying && IsEditorScene();

        return shouldRunFirstPass;


    }

    private static bool IsEditorScene()
    {
        return SceneManager.GetActiveScene().name.StartsWith("Editor");
    }


    static void OnSceneGUI(SceneView sceneView)
    {

        if (!ShouldRun())
            return;



        CheckEvents(sceneView);
    }


    public static void CheckEvents(SceneView sceneView)
    {
        Debug.Log("EventHandler");
        var currentEvent = Event.current;

        if( currentEvent.type == EventType.keyDown &&
            currentEvent.keyCode == KeyCode.I )
        {
            Debug.Log("SelectColumn");
            var selectedGOs = Selection.gameObjects.Select(o => o.GetComponentInChildren<WorldCube>()).Where(worldCube => worldCube != null);

            var toSelect = new HashSet<GameObject>();

            foreach (var cubeSelected in selectedGOs)
            {
                var axis = cubeSelected.transform.up * -1;
                Ray ray = new Ray(cubeSelected.transform.position - axis * 1000, axis);

                var hits = Physics.RaycastAll(ray);

                toSelect.UnionWith(hits.Select(hit =>
                    {
                        return hit.collider.gameObject.GetComponentInParent<WorldCube>();
                    })
                    .Where(worldCube => worldCube != null)
                    .Select(worldCube => worldCube.gameObject));
            }

            Selection.objects = toSelect.ToArray();

            currentEvent.Use();
        }

        if( currentEvent.type == EventType.keyDown &&
            currentEvent.keyCode == KeyCode.P )
        {

            var gos = Selection.gameObjects.ToList();

            var firstCubes = gos.Select(go => GetWorldCubesInColumn(go).FirstOrDefault())
                .Where(o => o != null)
                .Distinct()
                .ToList();

            var selection = new List<GameObject>();

            foreach(var firstCube in firstCubes){

                var prefab = PrefabUtility.GetPrefabParent(firstCube);
                var newGO = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                Undo.RegisterCreatedObjectUndo(newGO, "Create new Cube");
                newGO.transform.SetParent(firstCube.transform.parent);
                newGO.transform.localPosition = firstCube.transform.localPosition;
                newGO.transform.eulerAngles = firstCube.transform.eulerAngles;
                newGO.transform.Translate(firstCube.transform.up);
                selection.Add(newGO);
            }

            Selection.objects = selection.ToArray();

            currentEvent.Use();
        }

        if( currentEvent.type == EventType.keyDown &&
            currentEvent.keyCode == KeyCode.O )
        {
            var gos = Selection.gameObjects;

            var firstCubes = gos.Select(go => GetWorldCubesInColumn(go).FirstOrDefault()).Where(o => o != null).Distinct();

            var selection = new List<GameObject>();

            foreach(var firstCube in firstCubes)
            {
                var thisCubes = GetWorldCubesInColumn(firstCube);

                var nextCube = thisCubes.FirstOrDefault(o => o != firstCube);

                GameObject.DestroyImmediate(firstCube);
                if (nextCube != null)
                {
                    selection.Add(nextCube);
                }
            }

            Selection.objects = selection.ToArray();

            currentEvent.Use();
        }
    }

    public static IEnumerable<GameObject> GetWorldCubesInColumn(GameObject go)
    {
        var cube = go.GetComponent<WorldCube>();

        if (cube != null)
        {
            var axis = cube.transform.up * -1;
            Ray ray = new Ray(cube.transform.position - axis * 1000, axis);

            var hits = Physics.RaycastAll(ray);

            return hits.OrderBy(hit => hit.distance)
                .Select(hit => hit.collider.gameObject.GetComponentInParent<WorldCube>())
                .Where(worldCube => worldCube != null)
                .Select(worldCube => worldCube.gameObject);
        }
        else
        {
            return new List<GameObject>();
        }
    }
}
