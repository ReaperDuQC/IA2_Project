using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
public class Maze
{
    [SerializeField] private Transform m_maze;
    [SerializeField] private int m_width;
    [SerializeField] private int m_depth;

    [SerializeField] byte[,] m_map;
    [SerializeField] private int m_scale = 1;
    private List<GameObject> m_walls = new List<GameObject>();
    private List<Vector3> m_availablePositions = new List<Vector3>();
    public Maze( Transform maze, int width, int depth, int scale)
    {
        m_maze = maze;
        m_width = width;
        m_depth = depth;
        m_scale = scale;
    }
    public void StartGenerating()
    {
        InitialiseMap(); // met tout les valeur a 1 
        Generate(); // on envoie le crawwler faire les tests
        DrawMap(); // dessine la map. 
    }

    public virtual void Generate() // Genere une grille de de grandeur X Y 
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
    private void InitialiseMap() //initialise la grille a 1
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
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.transform.parent = m_maze.transform;
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(m_width * m_scale, 1f, m_depth * m_scale);
        ground.transform.localPosition += new Vector3(((m_width * m_scale) * 0.5f ) - m_scale * 0.5f, 0f, ((m_depth * m_scale) * 0.5f) - m_scale * 0.5f);
        for (int z = 0; z < m_depth; z++)
        {
            for (int x = 0; x < m_width; x++)
            {
                Vector3 pos = new Vector3(x * m_scale, m_scale * 0.5f, z * m_scale);
                if (m_map[x, z] == 1)
                {
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.name = "Wall";
                    wall.transform.parent = m_maze.transform;
                    Vector3 newScale = m_maze.transform.localScale * m_scale;
                    wall.transform.localScale = newScale;
                    wall.transform.localPosition = pos;
                    m_walls.Add(wall);
                }
                else if(m_map[x, z] == 0)
                {
                    m_availablePositions.Add(pos);
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
    public int CountSquareNeighbours(int x, int z)  // compte le nombre de case voisin libre
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
    public List<Vector3> GetAvailablePositions()
    {
        return m_availablePositions;
    }
}
