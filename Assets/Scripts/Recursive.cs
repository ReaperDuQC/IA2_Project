using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursive : Maze
{
    public override void Generate()
    {
        Generate(Random.Range(1, GetWidth()), Random.Range(1, GetDepth()));
    }
    void Generate(int x, int z)
    {
        if(CountSquareNeighbours(x,z) >= 2)
        {
            return;
        }
        SetMap(x, z, 0);

        Generate(x + 1, z);
    }
}
