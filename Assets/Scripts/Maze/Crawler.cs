using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Maze
{
    [SerializeField] private int m_nbsVerticalCrawler;
    [SerializeField] private int m_nbsHorizontalCrawler;

    public Crawler(int verticalCrawlers, int horizontalCrawlers,  Transform maze, int width, int depth, int scale) : base( maze, width, depth, scale)
    {
        verticalCrawlers = m_nbsVerticalCrawler;
        horizontalCrawlers = m_nbsHorizontalCrawler;
    }
    public override void Generate()
    {
        for(int i = 0; i < m_nbsVerticalCrawler; i++)
        {
            CrawlVertical();
        }
        for (int i = 0; i < m_nbsHorizontalCrawler; i++)
        {
            CrawlHorizontal();
        }
    }
    private void CrawlVertical()
    {
        int x = Random.Range(1, GetWidth() - 1);
        int z = 1;
        int minX = -1;
        int minZ = 0;
        Crawl(x, z , minX, minZ);
    }
    private void CrawlHorizontal()
    {

        int x = 1;
        int z = Random.Range(1, GetDepth() - 1);
        int minX = 0;
        int minZ = -1;

        Crawl(x, z , minX, minZ);
    }
    private void Crawl(int x, int z , int minX, int minZ)
    {
        bool isDone = false;

        while (!isDone)
        {
            SetMap(x, z, 0);
            if (Random.Range(0, 100) < 50)
            {
                x += Random.Range(minX, 2);
            }
            else
            {
                z += Random.Range(minZ, 2);
            }
            isDone |= (x < 1 || x >= GetWidth() - 1 || z < 1 || z >= GetDepth() - 1);
        }
    }
}
