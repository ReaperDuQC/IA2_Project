using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prims : Maze
{
    [SerializeField] private int m_startingPosX;
    [SerializeField] private int m_startingPosZ;
    [SerializeField] private int m_exitPosX;
    [SerializeField] private int m_exitPosZ;
    public Prims(int startingX, int startingZ, int exitX, int exitZ, Transform ground, Transform maze, int width, int depth, int scale) : base(ground, maze, width, depth, scale)
    {
        m_startingPosX = startingX;
        m_startingPosZ = startingZ;
        m_exitPosX = exitX;
        m_exitPosZ = exitZ;
    }
    public override void Generate()
    {
        int x = m_startingPosX;
        int z = m_startingPosZ;

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
        SetMap(m_exitPosX - 1, m_exitPosZ, 0);
        SetMap(m_exitPosX, m_exitPosZ, 0);
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
