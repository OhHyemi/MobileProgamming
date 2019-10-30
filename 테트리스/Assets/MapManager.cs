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

    public bool gameOver;
    public bool active;
    public int score;
    public int combo;
    public int bonusScore;

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
        score = 0;
      
        buildMap();
        gameOver = false;
        isBuilt = true;
        active = true;
        upper = 0.0f;
        combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (upper >= height)
        {
            active = false;
            gameOver = true;

        }

       

        if (active)
            updateMap();
      
    }


    private void updateMap()
    {
        for(int y =0; y <= getIndexY(upper); y++)
        {
            if(isLineFullyOcuppied(y))
            {
                eraseFullyOcuppiedLine(y);
                downLines(y);
                combo++;
                bonusScore = combo * 20;
                score += bonusScore;
                
            }

            //라인 지울 것이 없을 때
            if(!isLineFullyOcuppied(y) && y == getIndexY(upper))
            {
                combo =0;//콤보 초기화
                bonusScore = 0;//보너스점수 초기화
            }
        }
       
    }

    private void eraseFullyOcuppiedLine(int y)
    {
        for (int x = 0; x < 10; x++)
        {
            mapOcuppied[x, y] = false;
            Debug.Log(x + " " + y + "  ");
            Debug.Log(mapOcuppied[x, y]);
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

                if (mapOcuppied[x, i + 1])
                {                   
                    mapOcuppied[x, i ] = mapOcuppied[x, i + 1];
                    mapOcuppied[x, i + 1] = false;
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



