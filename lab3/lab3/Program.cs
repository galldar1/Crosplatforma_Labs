using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string[] inputLines = File.ReadAllLines("INPUT.TXT");
        string[] input = inputLines[0].Split();
        int n = int.Parse(input[0]);
        int m = int.Parse(input[1]);

        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

        for (int i = 1; i <= m; i++)
        {
            string[] road = inputLines[i].Split();
            int u = int.Parse(road[0]);
            int v = int.Parse(road[1]);

            if (!graph.ContainsKey(u))
                graph[u] = new List<int>();

            if (!graph.ContainsKey(v))
                graph[v] = new List<int>();

            graph[u].Add(v);
            graph[v].Add(u);
        }

        bool result = CanOrganizeCircularTrack(graph, n, m);

        using (StreamWriter sw = new StreamWriter("OUTPUT.TXT"))
        {
            sw.WriteLine(result ? "YES" : "NO");
        }
    }

    static bool CanOrganizeCircularTrack(Dictionary<int, List<int>> graph, int n, int m)
    {
        if (m == 0)
            return false;

        if (m % 2 == 1)
            return false;

        if (!IsConnected(graph, n))
            return false;

        int oddDegreeCount = 0;
        foreach (var node in graph.Keys)
        {
            int degree = graph[node].Count;
            if (degree % 2 != 0)
            {
                oddDegreeCount++;
                if (oddDegreeCount > 2)
                    return false;
            }
        }

        return oddDegreeCount == 0 || oddDegreeCount == 2;
    }

    static bool IsConnected(Dictionary<int, List<int>> graph, int n)
    {
        bool[] visited = new bool[n + 1];

        foreach (var node in graph.Keys)
        {
            if (graph[node].Count > 0)
            {
                DFS(graph, visited, node);
                break;
            }
        }

        for (int i = 1; i <= n; i++)
        {
            if (graph.ContainsKey(i) && !visited[i])
                return false;
        }

        return true;
    }

    static void DFS(Dictionary<int, List<int>> graph, bool[] visited, int node)
    {
        visited[node] = true;
        foreach (var neighbor in graph[node])
        {
            if (!visited[neighbor])
                DFS(graph, visited, neighbor);
        }
    }
}
