using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    // protected GameObject block;
    public Transform[] blockChildren;
    private MapManager map;
    public BlockSpawner blockSpawner;

    bool active = true;
    private int timer;
    private int timerCount;
    // Start is called before the first frame update
    void Start()
    {
        //맵 스크립트 가져오기
        map = GameObject.Find("Map").GetComponent<MapManager>();
        //블록 랜덤 생성
        blockSpawner = GameObject.Find("Spawner").GetComponent<BlockSpawner>();
        blockChildren = transform.GetComponentsInChildren<Transform>();

        //조작하는 블록의 태그는 ActiveBlock을 사용
        if (active)
        {
            foreach (Transform blcok in blockChildren)
            {
                blcok.transform.tag = "ActiveBlock";
            }
        }

        timer = 50;
        timerCount = 50;

    }

    // Update is called once per frame
    void Update()
    {
        moveBlock();

    }

    private void moveBlock()
    {
        if (!active)
            return;
        //속도빠르게하기
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            timerCount /= 4;
        }
        //오른쪽이동
        if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D)))
            && isInMapRight()
            && !isHitOthersRight())
        {
            blockSpawner.block.transform.position += Vector3.right * 1.2f;
        }
        //왼쪽이동
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            && isInMapLeft()
            && !isHitOthersLeft())
        {
            blockSpawner.block.transform.position += Vector3.left * 1.2f;

        }
        //블록회전
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            rotateBlock();
        }
        //블록한번에 내리기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            while (!isPlaced())
            {
                transform.position += Vector3.down * 1.2f;
            }
            timer = timerCount;
        }

        //일정시간에 한번씩 1.2만큼 이동하기(맵의 큐브들의 간격이 1.2임 칸에 맞춰 이동하기 위함)
        if (timer > timerCount)
        {
            //블록이 place되었을 때
            if (isPlaced())
            {
                active = false;//블록비활성화
                blockSpawner.needBlockSpawn = true;//새로운 블록이 필요하다고 알려줌

                //블록들의 태그를 Block으로 바꾸기 (이제 조작이 끝남)
                foreach (Transform block in blockChildren) 
                {
                    block.transform.tag = "Block";
                    if (block == blockChildren[0])
                        continue;
                }

                pushInMap(); //맵에 정보 자신의 정보 입력
       
                return;
            }
            transform.position += Vector3.down * 1.2f;
            timer = 0;
        }

        timer++;
        return;
    }

    //오른쪽에 다른 블록이 있는가 확인
    private bool isHitOthersRight()
    {
        foreach (Transform child in blockChildren)
        {
            int x = map.getIndexX(child.position.x);
            int y = map.getIndexY(child.position.y);

            if (x == 9)
                return false;

            if (map.mapOcuppied[x + 1, y])
            {
                return true;
            }
        }
        return false;
    }

    //왼쪽에 다른 블록이 있는가 확인
    private bool isHitOthersLeft()
    {
        foreach (Transform child in blockChildren)
        {
            int x = map.getIndexX(child.position.x);
            int y = map.getIndexY(child.position.y);

            if (x == 0)
            {
                return false;
            }

            if (map.mapOcuppied[x - 1, y])
            {
                return true;
            }

        }
        return false;
    }


    //블록이 place했는지 확인
    private bool isPlaced()
    {
        foreach (Transform child in blockChildren)
        {
            if (child.position.y > map.getHeight()) 
                return false;

            int x = map.getIndexX(child.position.x);
            int y = map.getIndexY(child.position.y);

            if (y == 0)//ground까지 내려오면 place
                return true;

            if (map.mapOcuppied[x, y - 1])//바로 밑에 블록이 있을 때 palce
                return true;
        }
        return false;
    }

    //맵의 오른쪽으로 벗어났는지 확인
    private bool isInMapRight()
    {
        foreach (Transform child in blockChildren)
        {

            if (child.transform.position.x + 1.2f > map.getWidth())
            {
                return false;
            }
        }

        return true;
    }

    //맵의 왼쪽으로 벗어났는지 확인
    private bool isInMapLeft()
    {
        foreach (Transform child in blockChildren)
        {
            if (child.transform.position.x <= 0)
                return false;
        }

        return true;
    }

    //블록 회전
    private void rotateBlock()
    {
        if (transform.name == "Block5(Clone)") //회전이 필요없는 블록
            return;

        transform.Rotate(new Vector3(0, 0, 90));

        foreach (Transform child in blockChildren)//돌린 블록들의 자식들이 범위를 벗어나면 원래대로 되돌림.
        {
            if (child.transform.position.x > map.getWidth() + 0.5f || child.transform.position.x < -0.5f)//오차범위
            {
                transform.Rotate(new Vector3(0, 0, -90));
                return;
            }
        }

    }

    //맵에 자신의 정보 입력
    private void pushInMap()
    {
        foreach (Transform block in blockChildren)
        {
            if (block == blockChildren[0])
                continue;

            map.upper = map.upper > block.position.y ? map.upper : block.position.y;//가장높은 y값 수정

            if (map.upper > map.getHeight())//게임 오버일 시 맵에 입력하지 않음, 더 이상 새로운 블록을 생성하지 않도록 함.
            {
                blockSpawner.needBlockSpawn = false;
                return;
            }

            int x = map.getIndexX(block.position.x);
            int y = map.getIndexY(block.position.y);

            //자기 자신 등록
            map.mapOcuppied[x, y] = true;
            map.blockCubes[x, y] = block.gameObject;
            map.score++;//점수 업데이트

        }

    }

    public GameObject getBlock()
    {
        return gameObject;
    }


}
