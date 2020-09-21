using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGeneration : MonoBehaviour
{
    /*This script generates a wall around the terrain. It will place the wallBlock
     * at a distance 0.5 unit from the edge of the terrain and the each wallBlock will be placed
     * 1 unit apart. This requires the wallBlock to be of size 1x1 on the x-z plane.
     * Also, this script assumes that the lower-left edge of the terrain is at (0,0).
     */
    public GameObject wallBlock; //1xheightx1 object to be placed at the edge of the terrain.
    // Start is called before the first frame update
    void Start()
    {
        Terrain attachedTerrain = GetComponent<Terrain>();
        Vector3 terrainSize = attachedTerrain.terrainData.size; //get the size of the terrain.

        float y = wallBlock.transform.localScale.y/2; //y value for all the wallBlocks to be placed, which should be half of the height of the wallBlock.

        for(float i=0.5f;i<terrainSize.x;i+=1) 
        {
            Instantiate(wallBlock, new Vector3(i, y, 0.5f),Quaternion.identity); //instantiate wallBlocks along the lower x border.
            Instantiate(wallBlock, new Vector3(i, y, terrainSize.z - 0.5f), Quaternion.identity); //instantiate wallBlocks along the upper x border.
        }

        for(float j=1.5f;j<terrainSize.z;j+=1) //instantiate wallBlocks along the left z border.
        {
            Instantiate(wallBlock, new Vector3(0.5f, y, j), Quaternion.identity); //instantiate wallBlocks along the left z border.
            Instantiate(wallBlock, new Vector3(terrainSize.x - 0.5f, y, j), Quaternion.identity); //instantiate wallBlocks along the right z border.
        }





    }

}
