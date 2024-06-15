using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShapeShifting
{
    public static class PoissonDiscSampling
    {
        public static List<Vector2> GeneratePoints(int i_PointCount, float i_Radius, Vector2 i_SampleRegionSize, int i_SamplesBeforeRejectionCount = 30)
        {
            List<Vector2> points = GeneratePoints(i_Radius, i_SampleRegionSize, i_SamplesBeforeRejectionCount);
            points.Shuffle();
            return points.Take(i_PointCount).ToList();
        }
        public static List<Vector2> GeneratePoints(float i_Radius, Vector2 i_SampleRegionSize, int i_SamplesBeforeRejectionCount = 30)
        {
            float cellSize = i_Radius / Mathf.Sqrt(2);

            int[,] grid = new int[Mathf.CeilToInt(i_SampleRegionSize.x / cellSize), Mathf.CeilToInt(i_SampleRegionSize.y / cellSize)];
            List<Vector2> points = new List<Vector2>();
            List<Vector2> spawnPoints = new List<Vector2>();

            spawnPoints.Add(i_SampleRegionSize / 2);
            while (spawnPoints.Count > 0)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Count);
                Vector2 spawnCentre = spawnPoints[spawnIndex];
                bool candidateAccepted = false;

                for (int i = 0; i < i_SamplesBeforeRejectionCount; i++)
                {
                    float angle = Random.value * Mathf.PI * 2;
                    Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                    Vector2 candidate = spawnCentre + dir * Random.Range(i_Radius, 2 * i_Radius);
                    if (isValid(candidate, i_SampleRegionSize, cellSize, i_Radius, points, grid))
                    {
                        points.Add(candidate);
                        spawnPoints.Add(candidate);
                        grid[(int)(candidate.x / cellSize), (int)(candidate.y / cellSize)] = points.Count;
                        candidateAccepted = true;
                        break;
                    }
                }
                if (!candidateAccepted)
                {
                    spawnPoints.RemoveAt(spawnIndex);
                }

            }

            return points;
        }
        static bool isValid(Vector2 i_Candidate, Vector2 i_SampleRegionSize, float i_CellSize, float i_Radius, List<Vector2> i_Points, int[,] i_Grid)
        {
            if (i_Candidate.x >= 0 && i_Candidate.x < i_SampleRegionSize.x && i_Candidate.y >= 0 && i_Candidate.y < i_SampleRegionSize.y)
            {
                int cellX = (int)(i_Candidate.x / i_CellSize);
                int cellY = (int)(i_Candidate.y / i_CellSize);
                int searchStartX = Mathf.Max(0, cellX - 2);
                int searchEndX = Mathf.Min(cellX + 2, i_Grid.GetLength(0) - 1);
                int searchStartY = Mathf.Max(0, cellY - 2);
                int searchEndY = Mathf.Min(cellY + 2, i_Grid.GetLength(1) - 1);

                for (int x = searchStartX; x <= searchEndX; x++)
                {
                    for (int y = searchStartY; y <= searchEndY; y++)
                    {
                        int pointIndex = i_Grid[x, y] - 1;
                        if (pointIndex != -1)
                        {
                            float sqrDst = (i_Candidate - i_Points[pointIndex]).sqrMagnitude;
                            if (sqrDst < i_Radius * i_Radius)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}

