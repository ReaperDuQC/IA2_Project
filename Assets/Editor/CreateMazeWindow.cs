using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CreateMazeWindow : EditorWindow
{
    private int _width;
    private int _depth;
    private int _scale;
    private int _startingPosX;
    private int _startingPosZ;
    private Transform _ground;
    private Transform _mazeContainer;
    private Prims _maze;

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
        _width = EditorGUILayout.IntSlider(_width, 1, 100);
        GUILayout.Label("Depth");
        _depth = EditorGUILayout.IntSlider(_depth, 1, 100);
        GUILayout.Label("Scale");
        _scale = EditorGUILayout.IntSlider(_scale, 1, 50);
        GUILayout.Label("Starting Position X");
        _startingPosX = EditorGUILayout.IntSlider(_startingPosX, 0, _width);
        GUILayout.Label("Starting Position Z");
        _startingPosZ = EditorGUILayout.IntSlider(_startingPosZ, 0, _depth);
        _ground = (Transform)EditorGUILayout.ObjectField("Ground", _ground, typeof(Transform), true);
        _mazeContainer = (Transform)EditorGUILayout.ObjectField("Maze", _mazeContainer, typeof(Transform), true);
        GUILayout.EndVertical();

        if (GUILayout.Button("Generate New Maze"))
        {
            if(_maze != null)
            {
                _maze.ClearMaze();
            }
            _maze = new Prims(_startingPosX, _startingPosZ, _ground, _mazeContainer, _width, _depth, _scale);
            _maze.StartGenerating();

        }
    }
}