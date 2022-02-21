using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class CreateMazeWindow : EditorWindow
{
    private int _width;
    private int _depth;
    private int _scale;
    private int _startingPosX;
    private int _startingPosZ;
    private int _exitPosX;
    private int _exitPosZ;
    private Transform _ground;
    private Transform _mazeContainer;
    private Prims _maze;
    private List<GameObject> _walls;

    [MenuItem("Window/Maze Generation")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<CreateMazeWindow>("Create Maze");
    }
    private void OnGUI()
    {
        GUILayout.Space(20f);
        GUILayout.Label("Maze Modifiers");
        GUILayout.BeginVertical();
        GUILayout.Label("Width");
        _width = EditorGUILayout.IntSlider(_width, 0, 100);
        GUILayout.Label("Depth");
        _depth = EditorGUILayout.IntSlider(_depth, 0, 100);
        GUILayout.Label("Scale");
        _scale = EditorGUILayout.IntSlider(_scale, 1, 50);
        GUILayout.Label("Starting Position X");
        _startingPosX = EditorGUILayout.IntSlider(_startingPosX, 0, _width - 1);
        GUILayout.Label("Starting Position Z");
        _startingPosZ = EditorGUILayout.IntSlider(_startingPosZ, _startingPosX == 0 ? 1 : 0, _startingPosX == _width - 1 ? _depth - 2 : _depth - 1);
        GUILayout.Label("Exit Position X");
        _exitPosX = EditorGUILayout.IntSlider(_exitPosX, 0, _width - 1);
        GUILayout.Label("Exit Position Z");
        _exitPosZ = EditorGUILayout.IntSlider(_exitPosZ, _exitPosX == 0 ? 1 : 0, _exitPosX == _width - 1 ? _depth - 2 : _depth - 1);
        _ground = (Transform)EditorGUILayout.ObjectField("Ground", _ground, typeof(Transform), true);
        _mazeContainer = (Transform)EditorGUILayout.ObjectField("Maze", _mazeContainer, typeof(Transform), true);
        GUILayout.EndVertical();

        if (GUILayout.Button("Generate New Maze"))
        {
            ClearMaze();
            _maze = new Prims(_startingPosX, _startingPosZ, _exitPosX, _exitPosZ, _ground, _mazeContainer, _width, _depth, _scale);
            _maze.StartGenerating();
            foreach(Transform child in _mazeContainer)
            {
                child.gameObject.isStatic = true;
            }
        }
        if (GUILayout.Button("Bake NavMesh"))
        {
            _ground.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
        if (GUILayout.Button("Clear Maze"))
        {
            ClearMaze();
        }
    }

    private void ClearMaze()
    {
        for(int i = _mazeContainer.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_mazeContainer.GetChild(i).gameObject);
        }
    }
}