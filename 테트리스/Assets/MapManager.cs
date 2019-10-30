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

    //맵 생성
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
        //게임 오버 조건 블록이 맵보다 높이 있을 때
        if (upper > height)
        {
            //맵 비활성화
            active = false;
            gameOver = true;

        }

        //맵이 활성화되어있을 때만 업데이트하기.
        if (active)
            updateMap();
      
    }


    private void updateMap()
    {
        //라인이 꽉찼는지 확인하고 차있으면 지우고 윗라인을 아랫라인으로 내림. 
        for(int y =0; y <= getIndexY(upper); y++)
        {
            if(isLineFullyOcuppied(y))
            {
                eraseFullyOcuppiedLine(y);
                downLines(y);
                //한줄을 깨면 콤보, 한꺼번에 많은 줄을 깨면 콤보레벨 증가, 
                combo++;
                bonusScore = combo * 20;//콤보레벨이 클 수 록 얻는 보너스 포인트도 커짐(20,40,80..)
                score += bonusScore;//스코어 업데이트

                break;
            }

            //라인 지울 것이 없을 때
            if(!isLineFullyOcuppied(y) && y == getIndexY(upper))
            {
                combo =0;//콤보 초기화
                bonusScore = 0;//보너스점수 초기화
            }
        }
       
    }
    
    //라인 지우기
    private void eraseFullyOcuppiedLine(int y)
    {
        for (int x = 0; x < 10; x++)
        {
            mapOcuppied[x, y] = false;
            Destroy(blockCubes[x, y]);
        }
    }
    
    //윗라인들을 한칸씩 내리기
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

    //라인이 꽉 찼는지 확인하기
    private bool isLineFullyOcuppied(int y)
    {     
        for (int x = 0; x < 10; x++)
        {
            if (!mapOcuppied[x, y])
               return false;      
        }
        
        return true;     
    }

    //소숫점 연산 오차를 보완하기 위해 만든 함수(mapOcuppied, blockCubes에 쓰일 인덱스를 얻기 위함)
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



