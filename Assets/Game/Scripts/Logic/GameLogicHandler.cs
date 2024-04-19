using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameLogicHandler
{
    public static bool DetectOverlapAbove(int layer, int slot, Dictionary<int, List<int>> map, int nRow, int nCol)
    {
        foreach (int key in map.Keys)
        {
            if (key < layer && map[key] != null)
            {
                var list = map[key];
                var (row, col) = FromIndexToRowCol(slot, nCol);
                int[] bounds = { -1, 0, 1 };
                for (int i = 0; i < bounds.Length; i++)
                {
                    for (int j = 0; j < bounds.Length; j++)
                    {
                        int detectRow = row + bounds[i];
                        int detectCol = col + bounds[j];
                        if (detectRow > nRow - 1 || detectRow < 0 || detectCol > nCol - 1 || detectCol < 0)
                            continue;
                        if (list.Contains(detectRow * nCol + detectCol))
                            return true;
                    }
                }
            }
        }
        return false;
    }
    public static List<(int, int)> GetOverlapBelow(int layer, int slot, Dictionary<int, List<int>> map, int nRow, int nCol)
    {
        List<(int, int)> listOverlap = new List<(int, int)>();
        foreach (int key in map.Keys)
        {
            if (key > layer && map[key] != null)
            {
                var list = map[key];
                var (row, col) = FromIndexToRowCol(slot, nCol);
                int[] bounds = { -1, 0, 1 };
                for (int i = 0; i < bounds.Length; i++)
                {
                    for (int j = 0; j < bounds.Length; j++)
                    {
                        int detectRow = row + bounds[i];
                        int detectCol = col + bounds[j];
                        if (detectRow > nRow - 1 || detectRow < 0 || detectCol > nCol - 1 || detectCol < 0)
                            continue;
                        if (list.Contains(detectRow * nCol + detectCol))
                        {
                            listOverlap.Add((key, detectRow * nCol + detectCol));
                        }
                    }
                }
            }
        }
        return listOverlap;
    }
    public static int DetectSameFruitOnQueue(int type, List<int> queue, int maxQueueCount)
    {
        int indexEnqueue = queue.LastIndexOf(type);
        if (indexEnqueue == -1)
            return queue.Count;
        if (indexEnqueue >= maxQueueCount - 1)
            return -1;
        return indexEnqueue + 1;
    }
    public static (int, int) FromIndexToRowCol(int index, int nCol)
    {
        int row = index / nCol;
        int col = index % nCol;
        return (row, col);
    }
}
