using System;
using System.Collections;
using System.Collections.Generic;


public delegate void modifyGraphDelegate(Graph g);
public class Graph //represents a graph in a 2d cartesian plane. Each node in this graph can only have edges to its four neighbors.
{
    private int xNodeNum; //number of nodes along x-axis
    private int zNodeNum; //number of nodes along z-axis
    private Node[,] nodeTable;
    public class Node //represnts a node in the graph. .
    {
        public int xInd; //x-index of the node
        public int zInd; //z-index of the node
        public Boolean used, rightEdge, upEdge;
        public Node(int x,int z)
        {
            xInd = x; 
            zInd = z;
            used = rightEdge = upEdge =false;
        }
    }

    public Graph(int x, int z) //constructor of a graph of size x * z
    {
        xNodeNum = x;
        zNodeNum = z;
        nodeTable = new Node[x, z];
        for(int i=0;i<nodeTable.GetLength(0);i++)
        {
            for(int j=0;j<nodeTable.GetLength(1);j++)
            {
                nodeTable[i, j] = new Node(i,j);
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
        if(xStart<0 || xStart>=xNodeNum || zStart<0 || zStart>zNodeNum)
        {
            throw new ArgumentException("Start node index out of bound");
        }
        if (xEnd < 0 || xEnd >= xNodeNum || zEnd < 0 || zEnd > zNodeNum)
        {
            throw new ArgumentException("End node inde out of bound");
        }
        var rand = new Random();
        Stack<Node> path = new Stack<Node>();
        path.Push(nodeTable[xStart, zStart]);

        while(!(path.Peek().xInd==xEnd && path.Peek().zInd==zEnd))
        {
            Node cur = path.Peek(); //the node currently being considered, i.e. the top of the stack
            cur.used = true;
            List<Node> neighbors = new List<Node>();
            if(cur.xInd>0 && !nodeTable[cur.xInd-1,cur.zInd].used) //if the current node is not in the leftmost column and the node on its left has not been used
            {
                neighbors.Add(nodeTable[cur.xInd - 1, cur.zInd]); //add this unused neighbor to the list
            }
            if (cur.xInd < xNodeNum - 1 && !nodeTable[cur.xInd + 1, cur.zInd].used) //if the current node is not in the rightmost column (xNodeNum-1 th column) and the node on its right has not been used
            {
                neighbors.Add(nodeTable[cur.xInd + 1, cur.zInd]);
            }
            if(cur.zInd>0 && !nodeTable[cur.xInd,cur.zInd-1].used) //same consideration for the node to the south
            {
                neighbors.Add(nodeTable[cur.xInd, cur.zInd - 1]);
            }
            if(cur.zInd<zNodeNum-1 && !nodeTable[cur.xInd, cur.zInd+1].used) //idem for the node to the north
            {
                neighbors.Add(nodeTable[cur.xInd, cur.zInd + 1]); 
            }

            if(neighbors.Count>0)
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






}
