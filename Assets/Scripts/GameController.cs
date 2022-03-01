using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField] [Range(5, 100)] private int _width;
    [SerializeField] [Range(5, 100)] private int _depth;
    [SerializeField] private int _scale;

    [SerializeField] private int _startingPosX;
    [SerializeField] private int _startingPosZ;
    [SerializeField] private int _exitPosX;
    [SerializeField] private int _exitPosZ;
    [SerializeField] private Transform _mazeContainer;
    private Prims _maze;
    private List<GameObject> _walls;
    private NavMeshSurface _ground;
    private void Awake()
    {
        PlayerPrefs.GetInt("Difficulty");
        _maze = new Prims(_startingPosX, _startingPosZ, _exitPosX, _exitPosZ, _mazeContainer, _width, _depth, _scale);
        _maze.StartGenerating();
        MakeNavMeshReady();
        BakeNavMesh();
    }
    private void BakeNavMesh()
    {
        if (_ground != null)
        {
            _ground.BuildNavMesh();
        }
    }
    private void MakeNavMeshReady()
    {
        foreach (Transform child in _mazeContainer)
        {
            child.gameObject.isStatic = true;

            NavMeshModifier modifier = child.gameObject.AddComponent<NavMeshModifier>();
            if (child.name == "Ground")
            {
                _ground = child.gameObject.AddComponent<NavMeshSurface>();
                continue;
            }
            modifier.overrideArea = true;
            modifier.area = 1;
        }
    }

    public Vector3 GetStartingPos()
    {
        Vector3 pos = transform.position;
        return new Vector3(pos.x + _startingPosX, 0f, pos.z + _startingPosZ);
    }
    public Vector3 GetEndingPos()
    {
        Vector3 pos = transform.position;
        return new Vector3(_exitPosX * _scale, 0f, _scale * _exitPosZ);
    }
}
