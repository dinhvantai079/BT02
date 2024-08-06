//Bài toán Nén dữ liệu mã hóa Huffman
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Bai04
{
    // Lớp để đại diện cho một nút trong cây Huffman
    public class Node
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(char character, int frequency)
        {
            Character = character;
            Frequency = frequency;
            Left = null;
            Right = null;
        }

        // Để dễ sắp xếp trong SortedSet
        public override string ToString()
        {
            return $"{Character}:{Frequency}";
        }
    }

    static void Main()
    {
        string input = "ABABBCBBDEEEABABBAEEDDCCABBBCDEEDCBCCCCDBBBCAAA";

        // Tạo bảng tần suất
        var frequency = BuildFrequencyTable(input);

        // Xây dựng cây Huffman
        var huffmanTree = BuildHuffmanTree(frequency);

        // Tính toán độ dài có trọng số
        int weightedLength = CalculateWeightedLength(huffmanTree, 0);
        Console.WriteLine($"Tong do dai co trong so: {weightedLength}");

        // Tạo mã Huffman
        var huffmanCodes = GenerateHuffmanCodes(huffmanTree);

        // Hiển thị mã Huffman
        Console.WriteLine("Ma Huffman:");
        foreach (var code in huffmanCodes)
        {
            Console.WriteLine($"{code.Key}: {code.Value}");
        }
    }

    // Xây dựng bảng tần suất từ dữ liệu đầu vào
    static Dictionary<char, int> BuildFrequencyTable(string input)
    {
        var frequency = new Dictionary<char, int>();
        foreach (var character in input)
        {
            if (frequency.ContainsKey(character))
                frequency[character]++;
            else
                frequency[character] = 1;
        }
        return frequency;
    }

    // Xây dựng cây Huffman từ bảng tần suất
    static Node BuildHuffmanTree(Dictionary<char, int> frequency)
    {
        var priorityQueue = new SortedSet<Node>(Comparer<Node>.Create((x, y) =>
        {
            int result = x.Frequency.CompareTo(y.Frequency);
            if (result == 0)
                result = x.Character.CompareTo(y.Character);
            return result;
        }));

        foreach (var kvp in frequency)
        {
            priorityQueue.Add(new Node(kvp.Key, kvp.Value));
        }

        while (priorityQueue.Count > 1)
        {
            var left = priorityQueue.Min;
            priorityQueue.Remove(left);
            var right = priorityQueue.Min;
            priorityQueue.Remove(right);

            var newNode = new Node('\0', left.Frequency + right.Frequency)
            {
                Left = left,
                Right = right
            };

            priorityQueue.Add(newNode);
        }

        return priorityQueue.First();
    }

    // Tạo mã Huffman từ cây Huffman
    static Dictionary<char, string> GenerateHuffmanCodes(Node root)
    {
        var codes = new Dictionary<char, string>();
        GenerateHuffmanCodesRecursive(root, "", codes);
        return codes;
    }

    static void GenerateHuffmanCodesRecursive(Node node, string code, Dictionary<char, string> codes)
    {
        if (node.Left == null && node.Right == null)
        {
            codes[node.Character] = code;
            return;
        }

        if (node.Left != null)
            GenerateHuffmanCodesRecursive(node.Left, code + "0", codes);
        if (node.Right != null)
            GenerateHuffmanCodesRecursive(node.Right, code + "1", codes);
    }

    // Tính toán tổng độ dài có trọng số của cây Huffman
    static int CalculateWeightedLength(Node root, int depth)
    {
        if (root.Left == null && root.Right == null)
        {
            return root.Frequency * depth;
        }
        int weightedLength = 0;
        if (root.Left != null)
            weightedLength += CalculateWeightedLength(root.Left, depth + 1);
        if (root.Right != null)
            weightedLength += CalculateWeightedLength(root.Right, depth + 1);

        return weightedLength;
    }
}
