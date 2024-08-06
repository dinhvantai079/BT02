//Bài toán đi đường của người giao hàng.
using System;
using System.Collections.Generic;
using System.Linq;

class Bai03
{
    public class Graph
    {
        public Dictionary<string, List<(string, double)>> AdjacencyList { get; } = new Dictionary<string, List<(string, double)>>();

        public void AddEdge(string from, string to, double weight)
        {
            if (!AdjacencyList.ContainsKey(from))
                AdjacencyList[from] = new List<(string, double)>();

            if (!AdjacencyList.ContainsKey(to))
                AdjacencyList[to] = new List<(string, double)>();

            AdjacencyList[from].Add((to, weight));
            AdjacencyList[to].Add((from, weight));
        }
    }

    static void Main()
    {
        // Khởi tạo đồ thị với tọa độ
        var points = new Dictionary<string, (double x, double y)>
        {
            {"a", (0, 0)},
            {"b", (4, 3)},
            {"c", (1, 7)},
            {"d", (15, 7)},
            {"e", (15, 4)},
            {"f", (18, 0)}
        };

        var graph = new Graph();

        // Thêm tất cả các cạnh với trọng số (khoảng cách)
        foreach (var p1 in points)
        {
            foreach (var p2 in points)
            {
                if (p1.Key != p2.Key)
                {
                    double distance = CalculateDistance(p1.Value, p2.Value);
                    graph.AddEdge(p1.Key, p2.Key, distance);
                }
            }
        }

        // Khởi chạy thuật toán TSP tham lam
        var (tour, totalCost) = TSPGreedy(graph, "a");

        // Hiển thị kết quả
        Console.WriteLine("Chuyen du lich:");
        foreach (var edge in tour)
        {
            Console.WriteLine($"{edge.Item1} -> {edge.Item2}");
        }
        Console.WriteLine($"Tong chi phi: {totalCost}");

        // Tính tổng độ dài chu trình tối ưu đã cho
        var optimalTour = new List<string> { "a", "c", "d", "e", "f", "b", "a" };
        double optimalCost = CalculateTourCost(optimalTour, points);
        Console.WriteLine($"Chi toi uu: {optimalCost}");
    }

    public static double CalculateDistance((double x, double y) p1, (double x, double y) p2)
    {
        return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
    }

    public static double CalculateTourCost(List<string> tour, Dictionary<string, (double x, double y)> points)
    {
        double totalCost = 0.0;
        for (int i = 0; i < tour.Count - 1; i++)
        {
            totalCost += CalculateDistance(points[tour[i]], points[tour[i + 1]]);
        }
        return totalCost;
    }

    public static (List<(string, string)>, double) TSPGreedy(Graph graph, string start)
    {
        var visited = new HashSet<string>();
        var tour = new List<(string, string)>();
        double totalCost = 0;
        string current = start;

        visited.Add(current);

        while (visited.Count < graph.AdjacencyList.Count)
        {
            var edges = graph.AdjacencyList[current];
            var nextEdge = edges
                .Where(edge => !visited.Contains(edge.Item1))
                .OrderBy(edge => edge.Item2)
                .FirstOrDefault();

            if (nextEdge.Item1 == null)
                break;

            tour.Add((current, nextEdge.Item1));
            totalCost += nextEdge.Item2;
            visited.Add(nextEdge.Item1);
            current = nextEdge.Item1;
        }

        // Quay lại đỉnh xuất phát
        var returnEdge = graph.AdjacencyList[current]
            .FirstOrDefault(edge => edge.Item1 == start);

        if (returnEdge.Item1 != null)
        {
            tour.Add((current, start));
            totalCost += returnEdge.Item2;
        }

        return (tour, totalCost);
    }
}