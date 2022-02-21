using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wilsons : Maze
{
    List<MapLocation> m_directions = new List<MapLocation>()
    {
        new MapLocation (1, 0),
        new MapLocation (0, 1),
        new MapLocation (-1, 0),
        new MapLocation (0, -1)
    };
    List<MapLocation> m_availables = new List<MapLocation>();

    [SerializeField] private int m_startX;
    [SerializeField] private int m_startZ;

    public override void Generate()
    {
        int x = Random.Range(2, GetWidth() - 1);
        int z = Random.Range(2, GetDepth() - 1);
        m_startX = x;
        m_startZ = z;
        SetMap(x, z, 2);

        while (GetAvailableCells() > 1)
        {
            RandomWalk();
        }
    }
    int GetAvailableCells()
    {
        m_availables.Clear();
        for(int z = 1; z< GetDepth() - 1; z++)
        {
            for(int x = 1; x < GetWidth() - 1; x++)
            {
                if(CountSquareMazeNeighbours(x,z) == 0)
                {
                    m_availables.Add(new MapLocation(x, z));
                }
            }
        }
        return m_availables.Count;
    }
    private int CountSquareMazeNeighbours(int x, int z)
    {
        int count = 0;
        for(int d = 0; d < m_directions.Count; d++)
        {
            int nextX = x + m_directions[d].m_x;
            int nextZ = z + m_directions[d].m_z;
            if(GetMap(nextX, nextZ) == 2)
            {
                count++;
            }
        } 
        return count;
    }
    private void RandomWalk()
    {
        int randomStartIndex = Random.Range(0, m_availables.Count);
        int x = m_availables[randomStartIndex].m_x;
        int z = m_availables[randomStartIndex].m_z;

        List<MapLocation> inWalk = new List<MapLocation>();

        inWalk.Add(new MapLocation(x, z));

        int loopCount = 0;
        bool isValidPath = false;
        while (x > 0 && x < GetWidth() - 1 && z > 0 && z < GetDepth() - 1 && loopCount < 5000 && !isValidPath)
        {
            SetMap(x, z, 0);
            if(CountSquareMazeNeighbours(x, z) > 1)
            {
                break;
            }

            int randomDirection = Random.Range(0, m_directions.Count);
            int nextX = x + m_directions[randomDirection].m_x;
            int nextZ = z + m_directions[randomDirection].m_z;
            if (CountSquareNeighbours(nextX, nextZ) < 2)
            {
                x = nextX;
                z = nextZ;
                inWalk.Add(new MapLocation(x, z));
            }
            isValidPath = CountSquareMazeNeighbours(x, z) == 1;
            loopCount++;
        }
        if (isValidPath)
        {
            SetMap(x, z, 0);
            Debug.Log("Path found");
            foreach(MapLocation m in inWalk)
            {
                SetMap(m.m_x, m.m_z, 2);
            }
            inWalk.Clear();
        }
        else
        {
            Debug.Log("path not found");
            foreach (MapLocation m in inWalk)
            {
                SetMap(m.m_x, m.m_z, 1);
            }
            inWalk.Clear();
        }
    }
}
