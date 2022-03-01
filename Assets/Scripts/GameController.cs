using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField] [Range(3, 100)] private int _width;
    [SerializeField] [Range(3, 100)] private int _depth;
    [SerializeField] private int _scale;

    [SerializeField] private int _startingPosX;
    [SerializeField] private int _startingPosZ;
    [SerializeField] private int _exitPosX;
    [SerializeField] private int _exitPosZ;
    [SerializeField] private Transform _ground;
    [SerializeField] private Transform _mazeContainer;
    private Prims _maze;
    private List<GameObject> _walls;
    private void Awake()
    {
        PlayerPrefs.GetInt("Difficulty");
        _maze = new Prims(_startingPosX, _startingPosZ, _exitPosX, _exitPosZ, _ground, _mazeContainer, _width, _depth, _scale);
        _maze.StartGenerating();
        MakeNavMeshReady();
        BakeNavMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void BakeNavMesh()
    {
        _ground.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
    private void MakeNavMeshReady()
    {
        foreach (Transform child in _mazeContainer)
        {
            child.gameObject.isStatic = true;

            NavMeshModifier modifier = child.gameObject.AddComponent<NavMeshModifier>();
            modifier.overrideArea = true;
            modifier.area = 1;
        }
    }
}
