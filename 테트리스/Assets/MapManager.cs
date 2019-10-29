using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private float width;
    private float height;
    protected GameObject[,] map;
    public bool[,] mapOcuppied; 
    private bool isBuilt = false;
    Material cubeMaterial;
    BlockSpawner blockSpawner;
   
    private void buildMap()
    {
        for(int x = 0; x < 10; x++)
        {
            for(int y =0; y <20; y++)
            {
              
                var mapBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                mapBox.transform.position = new Vector3(x * 1.2f , y * 1.2f, 0);
                Renderer rend = mapBox.GetComponent<Renderer>();
                rend.material = cubeMaterial;
                map[x, y] = mapBox;
                mapOcuppied[x, y] = false;
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject[10, 20];
        width = 10.8f;
        height = 22.8f;
        cubeMaterial = new Material(Resources.Load("TranslucentCubeMaterial") as Material);
        blockSpawner = GameObject.Find("Spawner").GetComponent<BlockSpawner>();
        buildMap();
        isBuilt = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updatMapOcuppied()
    {
        GameObject block = blockSpawner.getBlock();
    }

    public bool isMapBuilt()
    {
        return isBuilt;
    }

    public float getWidth()
    {
        return width;
    }
    public float getHeight()
    {
        return height;
    }
}



