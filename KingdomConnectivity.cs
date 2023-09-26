using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{
    private static int mod = 1000000000;
    public static void countPaths(int n, List<List<int>> edges)
    {
        //cria a lista de adjacencia
        List<List<int>> adj = new List<List<int>>();
        //adiciona as listas vazias
        for (int i = 0; i < n; i++)
        {
            adj.Add(new List<int>());
        }
        //adiciona as arestas
        foreach (List<int> edge in edges)
        {
            adj[edge[0] - 1].Add(edge[1] - 1);
        }
        //chama a função de busca
        long paths = SearchCountPaths(0, n - 1, adj, new bool[n], new Dictionary<int, long>());
        if (paths == -1)
        {
            Console.WriteLine("INFINITE PATHS");
        }
        else
        {
            Console.WriteLine(paths);
        }

    }
    private static long SearchCountPaths(int source, int destination, List<List<int>> adj, bool[] visited, Dictionary<int, long> notes)
    {
        //se o source for igual ao destino, retorna 1
        if (source == destination)
        {
            return 1;
        }
        //se o source já foi visitado, retorna -2
        else if (visited[source])
        {
            return -2;
        }
        else if (notes.ContainsKey(source))
        {
            return notes[source];
        }
        //marca o source como visitado

        visited[source] = true;
        long paths = 0;
        bool loop = false;
        //para cada vizinho do source
        foreach (int neighbor in adj[source])
        {
            //chama a função de busca
            long neighborPaths = SearchCountPaths(neighbor, destination, adj, visited, notes);
            if (neighborPaths == -1)
            {
                return -1;
            }
            //se o vizinho for igual a -2, marca o loop como true
            else if (neighborPaths == -2)
            {
                loop = true;
            }
            //se não, soma o numero de caminhos
            else
            {
                paths = (neighborPaths + paths) % mod;
            }
            //se o loop for true e o numero de caminhos for maior que 0, retorna -1
            if (loop && paths > 0)
            {
                return -1;
            }

        }
        //marca o source como não visitado
        visited[source] = false;
        //adiciona o source e o numero de caminhos no dicionario
        notes[source] = paths;
        //retorna o numero de caminhos
        return paths;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int nodes = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<List<int>> edges = new List<List<int>>();

        for (int i = 0; i < m; i++)
        {
            edges.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(edgesTemp => Convert.ToInt32(edgesTemp)).ToList());
        }

        Result.countPaths(nodes, edges);
    }
}
