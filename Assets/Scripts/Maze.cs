using UnityEngine;
using Random = UnityEngine.Random;

public class MapLocation
{
    public int m_x;
    public int m_z;
    public MapLocation(int x, int z)
    {
        m_x = x;
        m_z = z;
    }
}
public class Maze : MonoBehaviour
{
    [SerializeField] private Transform m_ground;
    [SerializeField] private int m_width;
    [SerializeField] private int m_depth;

    [SerializeField] byte[,] m_map;
    [SerializeField] private int m_scale = 1;

    void Start()
    {
        InitialiseMap();
        Generate();
        DrawMap();
        //m_ground.position -= new Vector3(0, m_scale / 2 ,0);
    }

    public virtual void Generate()
    {
        for (int z = 0; z < m_depth; z++)
        {
            for (int x = 0; x < m_width; x++)
            {
                if (Random.Range(0, 100) < 50)
                {
                    SetMap(x, z, 0);
                }
            }
        }
    }
    private void InitialiseMap()
    {
        m_map = new byte[m_width, m_depth];

        for (int z = 0; z < m_depth; z++)
        {
            for (int x = 0; x < m_width; x++)
            {
                SetMap(x,z,1);
            }
        }
    }
    private void DrawMap()
    {
        for (int z = 0; z < m_depth; z++)
        {
            for (int x = 0; x < m_width; x++)
            {
                if (m_map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * m_scale, m_scale / 2, z * m_scale);
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.parent = transform;
                    Vector3 newScale = transform.localScale * m_scale;
                    wall.transform.localScale = newScale;
                    wall.transform.position = pos;
                }
            }
        }
    }
    public int GetWidth()
    {
        return m_width;
    }
    public int GetDepth()
    {
        return m_depth;
    }
    public int GetMap(int x, int z)
    {
        return m_map[x, z];
    }
    public void SetMap(int x, int z, byte value)
    {
        m_map[x, z] = value;
    }
    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;
        if( x <= 0 || x >= m_width - 1 || z <= 0 || z >= m_depth - 1)
        {
            count = 5;
            return count;
        }
        if(m_map[x - 1, z] == 0)
        {
            count++;
        }
        if (m_map[x + 1, z] == 0)
        {
            count++;
        }
        if (m_map[x, z - 1] == 0)
        {
            count++;
        }
        if (m_map[x, z + 1] == 0)
        {
            count++;
        }
        return count;
    }
    public int CountDiagonalNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= m_width - 1 || z <= 0 || z >= m_depth - 1)
        {
            count = 5;
            return count;
        }
        if (m_map[x - 1, z + 1] == 0)
        {
            count++;
        }
        if (m_map[x - 1, z - 1] == 0)
        {
            count++;
        }
        if (m_map[x + 1, z - 1] == 0)
        {
            count++;
        }
        if (m_map[x + 1, z + 1] == 0)
        {
            count++;
        }
        return count;
    }
    public int CountAllNeighbours(int x, int z)
    {
        return CountSquareNeighbours(x, z) + CountDiagonalNeighbours(x, z);
    }

}
