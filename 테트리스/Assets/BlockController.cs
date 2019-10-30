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
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            timerCount /= 4;
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D)))
            && isInMapRight()
            && !isHitOthersRight())
        {
            blockSpawner.block.transform.position += Vector3.right * 1.2f;
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            && isInMapLeft()
            && !isHitOthersLeft())
        {
            blockSpawner.block.transform.position += Vector3.left * 1.2f;

        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            rotateBlock();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            while (!isPlaced())
            {
                transform.position += Vector3.down * 1.2f;
            }
            timer = timerCount;

        }

        if (timer > timerCount)
        {
            if (isPlaced())
            {
                active = false;
                blockSpawner.needBlockSpawn = true;

                pushInMap();
       
                return;
            }
            transform.position += Vector3.down * 1.2f;
            timer = 0;
        }

        timer++;
        return;
    }



    private bool isHitOthersRight()
    {
        foreach (Transform child in blockChildren)
        {
            int x = map.getIndexX(child.position.x);
            int y = map.getIndexY(child.position.y);

            if (x == 9)
                return true;

            if (map.mapOcuppied[x + 1, y])
            {
                return true;
            }
        }
        return false;
    }

    private bool isHitOthersLeft()
    {
        foreach (Transform child in blockChildren)
        {
            int x = map.getIndexX(child.position.x);
            int y = map.getIndexY(child.position.y);

            if (x == 0)
            {
                return true;
            }

            if (map.mapOcuppied[x - 1, y])
            {
                return true;
            }

        }
        return false;
    }



    private bool isPlaced()
    {
        foreach (Transform child in blockChildren)
        {

            if (child.position.y > map.getHeight())
                return false;

            int x = map.getIndexX(child.position.x);
            int y = map.getIndexY(child.position.y);

            if (y == 0)
                return true;

            if (map.mapOcuppied[x, y - 1])
                return true;

        }
        return false;
    }

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

    private bool isInMapLeft()
    {

        foreach (Transform child in blockChildren)
        {
            if (child.transform.position.x <= 0)
                return false;
        }

        return true;
    }

    private void rotateBlock()
    {
        if (transform.name == "Block5(Clone)")
            return;
        transform.Rotate(new Vector3(0, 0, 90));


        foreach (Transform child in blockChildren)
        {
            if (child.transform.position.x > map.getWidth() + 0.5f || child.transform.position.x < -0.5f)
            {
                transform.Rotate(new Vector3(0, 0, -90));
                return;
            }
        }

    }

    private void pushInMap()
    {
        foreach (Transform block in blockChildren)
        {
            block.transform.tag = "Block";
            if (block == blockChildren[0])
                continue;

            map.upper = map.upper > block.position.y ? map.upper : block.position.y;

            if (map.upper >= map.getHeight() )
            {
                blockSpawner.needBlockSpawn = false;
                return;
            }

            int x = map.getIndexX(block.position.x);
            int y = map.getIndexY(block.position.y);

            map.mapOcuppied[x, y] = true;
            map.blockCubes[x, y] = block.gameObject;
            map.score++;

        }

    }

    public GameObject getBlock()
    {
        return gameObject;
    }


}
