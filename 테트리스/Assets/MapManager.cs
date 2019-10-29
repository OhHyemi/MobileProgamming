using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    private float width;
    private float height;

    private GameObject[,] map;
    public bool[,] mapOcuppied;
    
    public GameObject[,] blockCubes;
    private bool isBuilt = false;
    Material cubeMaterial;
    BlockSpawner blockSpawner;
    public float upper;
   
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
    private void Awake()
    {
        
    }
    void Start()
    {
        map = new GameObject[10, 20];
        mapOcuppied = new bool[10, 20];
        blockCubes = new GameObject[10, 20];
        width = 10.8f;
        height = 22.8f;
        cubeMaterial = new Material(Resources.Load("TranslucentCubeMaterial") as Material);
        blockSpawner = GameObject.Find("Spawner").GetComponent<BlockSpawner>();
      
        buildMap();
        isBuilt = true;
        upper = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        updateMapOcuppied();
      
    }

    //block prefab -> 각각의 cube로 저장
   

    private void updateMapOcuppied()
    {
        for(int y =0; y <= getIndexY(upper); y++)
        {
            if(isLineFullyOcuppied(y))
            {
                eraseFullyOcuppiedLine(y);
                downLines(y);
            }
        }
        
    }

    private void eraseFullyOcuppiedLine(int y)
    {
        for (int x = 0; x < 10; x++)
        {
            mapOcuppied[x, y] = false;
            Destroy(blockCubes[x, y]);
        }
    }
    
    private void downLines(int y)
    {
        for (int i = y; i <= getIndexY(upper); i++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (i == height)
                {
                    mapOcuppied[x, i] = false;
                    Destroy(blockCubes[x, i]);
                    break;
                }

                if (blockCubes[x, i + 1])
                {
                    blockCubes[x, i] = blockCubes[x, i + 1];
                    blockCubes[x, i].transform.position +=  (Vector3.down * 1.2f);
                }
               
            }
        }
    }

    private bool isLineFullyOcuppied(int y)
    {     
        for (int x = 0; x < 10; x++)
        {
            if (!mapOcuppied[x, y])
               return false;      
        }
        Debug.Log(y + ": 꽉참");
        return true;     
    }

    public int getIndexX(float posX)
    {
        float range = -0.6f;
        int index = 0;

        while (range < width + 0.6f)
        {
            if (range < posX && posX < range + 1.2f)
            {
                return index;
            }

            range += 1.2f;
            index++;
        }
        return -1;
    }


    public int getIndexY(float posY)
    {
        float range = -0.6f;
        int index = 0;

        while (range < height + 0.6f)
        {
            if (range < posY && posY < range + 1.2f)
            {
                return index;
            }

            range += 1.2f;
            index++;
        }
        return -1;
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



