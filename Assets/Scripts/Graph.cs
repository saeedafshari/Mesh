using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Graph
{
    readonly static Random Random = new Random();
    public readonly List<int> Values;

    public Graph(int count, bool allowDefective)
    {
        //Algorithm:
        //1. Generate a random graph with no loops (?)
        //1a. For each edge, make a connection with another edge
        //1b. At most we have N-1 edges otherwise the graph is not valid (?)
        //2. Label edges as even numbers
        //3. Divide numbers between connecting nodes

        //Defective doesn't work for 2
        if (count == 2) allowDefective = false;

        //create graph
        var edges = new List<int>();
        do
        {
            //Connect edges
            bool lastNodeConnected = false;
            for (int i = 0; i < count - 1; i++)
            {
                do
                {
                    int endPoint = Random.Range(0, count);
                    if (endPoint == i) continue;
                    if (endPoint < i && edges[endPoint] == i) continue;
                    edges.Add(endPoint);
                    if (endPoint == count - 1) lastNodeConnected = true;
                    break;
                } while (true);
            }
            
            if (allowDefective) //connect the Nth node
            {
                int i = count - 1;
                do
                {
                    int endPoint = Random.Range(0, count);
                    if (endPoint == i) continue;
                    if (endPoint < i && edges[endPoint] == i) continue;
                    edges.Add(endPoint);
                    lastNodeConnected = true;
                    break;
                } while (true);
            }

            if (!lastNodeConnected && !allowDefective)
            {
                //invalid graph because last node does not have any connections
                edges.Clear();
                continue;
            }
            break;
        } while (true);


        //Label edges
        var edgeLabels = new List<int>();
        for (int i = 0; i < count - 1; i++)
            edgeLabels.Add(2 * (Random.Range(0, 3) + 1));
        if (allowDefective) edgeLabels.Add(2 * (Random.Range(0, 3) + 1));

        //Label nodes
        var result = new List<int>();
        for (int n = 0; n < count; n++)
        {
            int sum = (n < count - 1 ? edgeLabels[n] : 0);
            if (allowDefective) sum = edgeLabels[n];

            for (int e = 0; e < count - 1; e++)
            {
                if (n == e) continue;
                if (edges[e] == n) sum += edgeLabels[e];
            }
            if (allowDefective && n != count - 1 && edges[count - 1] == n) sum += edgeLabels[count - 1];
            result.Add(sum / 2);
        }

        //shuffle nodes
        result = result.OrderBy(x => Random.value).ToList();


        //additional step, if all the numbers are divisible by the smallest,
        //TODO

        //log
        Debug.Log($"Nodes: {string.Join(", ", result)}");

        Values = result;
    }
}
