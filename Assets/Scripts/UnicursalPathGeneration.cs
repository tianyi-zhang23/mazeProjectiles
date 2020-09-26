using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnicursalPathGeneration : MonoBehaviour
{
    //This script generates a unicursal path leading from the starting point to the ending point.
    // Start is called before the first frame update
    public float xLimit; //left boundary of the area to create unicursal path
    public float yOffset = 0; //offset in the y direction when creating tiles
    public float zLimit; //lower boundary of the area to create unicursal path
    public int xNumOfNodes; //number of nodes along the x-axis
    public int zNumOfNodes; //number of nodes along the z-axis
    public float sideLengthOfTile = 2; //side length of each tile.
    public GameObject projectileSourceController; //will need to pass information to this object to inform it of the total number of projectiles created along the path
    //The width of the area to generate unicursal path will be 2*sideLengthOfTile*xNumOfNodes+1, and similarly for the height.
    public GameObject tile;
    public GameObject projectileSource;

    private float desiredProjectileSourceProba; //the desired probability that a projectile source appears on a tile
    private int numOfProjectileSourceCreated = 0;
    private int numberOfAvailableNodeTiles; //number of node tiles that we have not created
    private System.Random random = new System.Random();

    Vector3 paintNode(Graph.Node aNode) //paint the node on the terrain, returns the position of the painted node
    { 

        float size = 2 * sideLengthOfTile;
        float xPos = xLimit + size * aNode.xInd;
        float zPos = zLimit + size * aNode.zInd;
        Vector3 position = new Vector3(xPos, yOffset, zPos);
        Instantiate(tile, position, tile.transform.rotation);

        //if the expected number of projectile sources that we will be able to create is smaller than the number we need
        if(numberOfAvailableNodeTiles*desiredProjectileSourceProba < (8-numOfProjectileSourceCreated))
        {
            Instantiate(projectileSource, new Vector3(xPos, yOffset + 1, zPos), Quaternion.identity); //instantiate a projectile source here.
            numOfProjectileSourceCreated++;
        }
        else if (numOfProjectileSourceCreated<15 && random.NextDouble()<desiredProjectileSourceProba) //instantiate a projectile source at the specified probability, but never make more than 15 projectile sources.
        {
            Instantiate(projectileSource, new Vector3(xPos, yOffset + 1, zPos), Quaternion.identity); //instantiate a projectile source here.
            numOfProjectileSourceCreated++;
        }
        numberOfAvailableNodeTiles--;
        return position;
    }
    void paintEdge(Vector3 pos1, Vector3 pos2) //instantiate a tile at the midpoint of pos1 and pos2
    {
        Vector3 midPoint = new Vector3((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2, (pos1.z + pos2.z) / 2);
        Instantiate(tile, midPoint, tile.transform.rotation);
    }
    void Start()
    {
        Graph upperTerrain = new Graph(xNumOfNodes, zNumOfNodes); //models the part of terrain where we are going to generate an unicursal path.
        Stack<Graph.Node> path = upperTerrain.makeUnicursal(xNumOfNodes/2,0,xNumOfNodes/2,zNumOfNodes-1); //make a path that starts and end at middle in x-direction
        numberOfAvailableNodeTiles = path.Count;
        desiredProjectileSourceProba = (8f / path.Count)*1.25f; //we want at least 8 projectile sources to appear, so the probability should be a bit over 8/totalTiles.
        Vector3 prevTilePos = new Vector3(0,0,0) ; //needed to paint the link tile between two nodes
        Boolean firstPass = true;
        while(path.Count>0) //visit the nodes in the path stack one by one
        {
            Graph.Node cur = path.Pop(); //pop the top node
            Vector3 currentPosition = paintNode(cur); //paint it in the terrain
            if(firstPass)
            {
                firstPass = false;
            }
            else 
            {
                paintEdge(prevTilePos,currentPosition); //paint the link between the previous and current node
            }
            prevTilePos = currentPosition;
        }
        projectileSourceController.GetComponent<ProjectileSourceCtrl>().setTotalNumberOfProjSourceUncollected(numOfProjectileSourceCreated);
        //let the projectile source controller know about the total number of projectile sources that we have created.
    }

}
