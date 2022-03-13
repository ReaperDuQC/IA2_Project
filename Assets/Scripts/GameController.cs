using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField] [Range(5, 100)] private int _width;
    [SerializeField] [Range(5, 100)] private int _depth;
    [SerializeField] private int _scale;

    private int _startingPosX;
    [SerializeField] private int _startingPosZ;
    private int _exitPosX;
    [SerializeField] private int _exitPosZ;
    [SerializeField] private Transform _mazeContainer;
    private Prims _maze;
    private List<GameObject> _walls;
    private NavMeshSurface _ground;
    [SerializeField] GameObject _finishLinePrefab;
    [SerializeField] GameObject _startingLinePrefab;
    [SerializeField] Transform _player;

    bool _isPathAvailable;
    NavMeshPath _navMeshPath;
    [SerializeField] NavMeshAgent _agent;
    private void Awake()
    {
        PlayerPrefs.GetInt("Difficulty");
        _startingPosX = 0;
        _startingPosZ = _startingPosZ > _depth - 1 ? (_depth - 1) / 2 : _startingPosZ;
        _exitPosX = _width - 1;
        _exitPosZ = _exitPosZ > _depth - 1 ? (_depth - 1) / 2 : _exitPosZ;
        int loopCount = 0;
        do
        {
            ClearMaze();
            _maze = new Prims(_startingPosX, _startingPosZ, _exitPosX, _exitPosZ, _mazeContainer, _width, _depth, _scale);

            CreateWall(GetStartingPos() - new Vector3(1f * _scale, -0.5f * _scale,0f));
            CreateWall(GetEndingPos() + new Vector3(1f * _scale, 0.5f * _scale, 0f));

            _maze.StartGenerating();
            MakeNavMeshReady();
            BakeNavMesh();

            _navMeshPath = new NavMeshPath();

            NavMesh.CalculatePath(GetStartingPos(),GetEndingPos(), NavMesh.AllAreas, _navMeshPath);
            
            if(_navMeshPath.status != NavMeshPathStatus.PathComplete)
            {
                _isPathAvailable = false;
            }
            loopCount++;
        }
        while (!_isPathAvailable && loopCount < 2);
        PlaceStartingLine();
        PlaceFinishLine();
        SetPlayerInitialPosition();
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
        return new Vector3( _startingPosX * _scale, 0f, _startingPosZ * _scale);
    }
    public Vector3 GetEndingPos()
    {
        return new Vector3(_exitPosX * _scale, 0f, _scale * _exitPosZ);
    }
    private void PlaceStartingLine()
    {
        if (_startingLinePrefab != null)
        {
            Transform finishLine = Instantiate(_startingLinePrefab, GetStartingPos() + new Vector3(0f, _scale * 0.5f, 0f), Quaternion.identity).transform;
            finishLine.localRotation = Quaternion.Euler(0f, -90f, 0f);
        }
    }
    private void PlaceFinishLine()
    {
        if (_finishLinePrefab != null)
        {
            Transform finishLine = Instantiate(_finishLinePrefab, GetEndingPos() + new Vector3(0f, _scale * 0.5f, 0f) , Quaternion.identity).transform;
            finishLine.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
    private void SetPlayerInitialPosition()
    {
        if(_player != null)
        {
            _player.position = GetStartingPos() + new Vector3(0f, _scale * 0.5f, 0f);
        }
    }
    private void SetAgentInitialPosition()
    {
        if (_agent != null)
        {
            _agent.transform.position = GetStartingPos() + new Vector3(0f, _scale * 0.5f, 0f);
        }
    }
    private void ClearMaze()
    {
        int wallCount = _mazeContainer.childCount;
        for (int i = wallCount - 1; i >= 0;i--)
        {
            Destroy(_mazeContainer.GetChild(i).gameObject);
        }
    }
    private void CreateWall(Vector3 pos)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = "Wall";
        wall.transform.parent = _mazeContainer.transform;
        Vector3 newScale = _mazeContainer.transform.localScale * _scale;
        wall.transform.localScale = newScale;
        wall.transform.position = pos;
    }

}
