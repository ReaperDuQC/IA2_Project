using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prims : Maze
{
    [SerializeField] private int m_startingXPos;
    [SerializeField] private int m_startingZPos;
    public override void Generate()
    {
        int x = m_startingXPos;
        int z = m_startingZPos;

        SetMap(x, z, 0);
        List<MapLocation> walls = new List<MapLocation>();
        AddWalls(x, z, walls);

        int countLoops = 0;
        while(walls.Count > 0)
        {
            int rwall = Random.Range(0, walls.Count);
            x = walls[rwall].m_x;
            z = walls[rwall].m_z;

            walls.RemoveAt(rwall);
            if(CountSquareNeighbours(x, z) == 1)
            {
                SetMap(x, z, 0);
                AddWalls(x, z, walls);
            }

            countLoops++;
        }
    }
    private List<MapLocation> AddWalls(int x, int z, List<MapLocation> walls)
    {
        walls.Add(new MapLocation(x + 1, z));
        walls.Add(new MapLocation(x - 1, z));
        walls.Add(new MapLocation(x, z + 1));
        walls.Add(new MapLocation(x, z - 1));

        return walls;
    }
}
