using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine;


public delegate void modifyGraphDelegate(Graph g);
public class Graph //represents a graph in a 2d cartesian plane. Each node in this graph can only have edges to its four neighbors.
{
    private int xNodeNum; //number of nodes along x-axis
    private int zNodeNum; //number of nodes along z-axis
    private Node[,] nodeTable;
    enum directions {NORTH,SOUTH,WEST,EAST};
public class Node //represnts a node in the graph. .
    {
        public int xInd; //x-index of the node
        public int zInd; //z-index of the node
        public Boolean used, rightEdge, upEdge; //right edge and up edge are optionally used. I use them for Aldous-Broder only.
        public Node(int x, int z)
        {
            xInd = x;
            zInd = z;
            used = rightEdge = upEdge = false;
        }
    }

    public Graph(int x, int z) //constructor of a graph of size x * z
    {
        xNodeNum = x;
        zNodeNum = z;
        nodeTable = new Node[x, z];
        for (int i = 0; i < nodeTable.GetLength(0); i++)
        {
            for (int j = 0; j < nodeTable.GetLength(1); j++)
            {
                nodeTable[i, j] = new Node(i, j);
            }
        }
    }
    /* The Algorithm:
     *     MAKESTACK
     *     PUSH startNode

           while (stack.TOP != endNode)
	        stack.TOP.used = true
	        if stack.TOP has an unused neighbor
		        push a random neighbor of stack.TOP
	        else
		        pop

        The path will be the stack.
     */
    public Stack<Node> makeUnicursal(int xStart, int zStart, int xEnd, int zEnd) //returns a stack of nodes that represent a unicursal path from (xStart,zStart) to (xEnd,zEnd)
    {
        if (xStart < 0 || xStart >= xNodeNum || zStart < 0 || zStart > zNodeNum)
        {
            throw new ArgumentException("Start node index out of bound");
        }
        if (xEnd < 0 || xEnd >= xNodeNum || zEnd < 0 || zEnd > zNodeNum)
        {
            throw new ArgumentException("End node inde out of bound");
        }
        var rand = new System.Random();
        Stack<Node> path = new Stack<Node>();
        path.Push(nodeTable[xStart, zStart]);

        while (!(path.Peek().xInd == xEnd && path.Peek().zInd == zEnd))
        {
            Node cur = path.Peek(); //the node currently being considered, i.e. the top of the stack
            cur.used = true;
            List<Node> neighbors = new List<Node>();
            if (cur.xInd > 0 && !nodeTable[cur.xInd - 1, cur.zInd].used) //if the current node is not in the leftmost column and the node on its left has not been used
            {
                neighbors.Add(nodeTable[cur.xInd - 1, cur.zInd]); //add this unused neighbor to the list
            }
            if (cur.xInd < xNodeNum - 1 && !nodeTable[cur.xInd + 1, cur.zInd].used) //if the current node is not in the rightmost column (xNodeNum-1 th column) and the node on its right has not been used
            {
                neighbors.Add(nodeTable[cur.xInd + 1, cur.zInd]);
            }
            if (cur.zInd > 0 && !nodeTable[cur.xInd, cur.zInd - 1].used) //same consideration for the node to the south
            {
                neighbors.Add(nodeTable[cur.xInd, cur.zInd - 1]);
            }
            if (cur.zInd < zNodeNum - 1 && !nodeTable[cur.xInd, cur.zInd + 1].used) //idem for the node to the north
            {
                neighbors.Add(nodeTable[cur.xInd, cur.zInd + 1]);
            }

            if (neighbors.Count > 0)
            {
                int index = rand.Next(neighbors.Count);
                path.Push(neighbors[index]);
            }
            else
            {
                path.Pop();
            }
        }
        return path;
    }

    public void resetGraph() //resets the edges and "used" flagged for the nodes in the graph
    {
        foreach (Node i in nodeTable)
        {
            i.used = false;
            i.upEdge = false;
            i.rightEdge = false;
        }
    }
    public Node[,] makeUST() //returns an UST represented as an array of nodes. Uses Aldous-Broder. Appropriate for small mazes like 5x5.
    {
        System.Random random = new System.Random();
        int startIndexX = random.Next(nodeTable.GetLength(0));
        int starIndexZ = random.Next(nodeTable.GetLength(1));

        Node cur = nodeTable[startIndexX, starIndexZ];
        cur.used = true;
        int remaining = xNodeNum * zNodeNum - 1;

        while (remaining > 0)
        {

            List<Node> neighbors = new List<Node>();
            List<directions> directionsFlag = new List<directions>(); //record the direction of the next node relative to the present node
            if (cur.xInd > 0) //if the current node is not in the leftmost column
            {
                neighbors.Add(nodeTable[cur.xInd - 1, cur.zInd]); //add this neighbor to the list
                directionsFlag.Add(directions.WEST);
            }
            if (cur.xInd < xNodeNum - 1) //if the current node is not in the rightmost column (xNodeNum-1 th column) 
            {
                neighbors.Add(nodeTable[cur.xInd + 1, cur.zInd]);
                directionsFlag.Add(directions.EAST);
            }
            if (cur.zInd > 0) //same consideration for the node to the south
            {
                neighbors.Add(nodeTable[cur.xInd, cur.zInd - 1]);
                directionsFlag.Add(directions.SOUTH);
            }
            if (cur.zInd < zNodeNum - 1) //idem for the node to the north
            {
                neighbors.Add(nodeTable[cur.xInd, cur.zInd + 1]);
                directionsFlag.Add(directions.NORTH);
            }
            int index = random.Next(neighbors.Count);
            Node next = neighbors[index];
            directions nextDirection = directionsFlag[index];

            switch (nextDirection) //add the appropriate edge. If the edge is already there, nothing will change.
            {
                case directions.EAST:
                    if (!cur.rightEdge && !next.used)
                    { cur.rightEdge = true; }
                      
                    break;
                case directions.NORTH:
                    if (!cur.upEdge && !next.used)
                    { cur.upEdge = true; }
                    break;
                case directions.WEST:
                    if (!next.rightEdge && !next.used)
                    { next.rightEdge = true; }
                    break;
                case directions.SOUTH:
                    if (!next.upEdge && !next.used)
                    { next.upEdge = true; }
                    break;
            }
            //Debug.Log("Going from (" + cur.xInd + "," + cur.zInd + ") to (" + next.xInd + "," + next.zInd + ")");
            if (!next.used)
            { next.used = true; remaining--; }
            cur = next; //travel to the next node
        }
        return this.nodeTable; //returning an mutable object. The next use of any method on Graph should be preceded by a resetGraph();
    }






}
