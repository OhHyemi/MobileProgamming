using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    // protected GameObject block;
    public Transform[] blockChildren;
    private float speed = 3.6f;
    float yPosition; //y위치를 정확하게 하기 위해서.

    private MapManager map;
    public BlockSpawner blockSpawner;

    bool active = true;


    // Start is called before the first frame update
    void Start()
    {
        //맵 스크립트 가져오기
        map = GameObject.Find("Map").GetComponent<MapManager>();
        //블록 랜덤 생성
        blockSpawner = GameObject.Find("Spawner").GetComponent<BlockSpawner>();
        blockChildren = blockSpawner.block.GetComponentsInChildren<Transform>();

        if (active)
        {
            foreach (Transform blcok in blockChildren)
            {
                blcok.transform.tag = "ActiveBlock";
            }
        }

        return;

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
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            speed *= 2.0f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && isInMapRight())
        {
            blockSpawner.block.transform.position += Vector3.right * 1.2f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isInMapLeft())
        {
            blockSpawner.block.transform.position += Vector3.left * 1.2f;

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotateBlock();
        }

        if (isPlaced())
        {
            active = false;
            blockSpawner.needBlockSpawn = true;
            foreach (Transform blcok in blockChildren)
            {
                blcok.transform.tag = "Block";
                
            }
            if (transform.position.y % 1.2 != 0)
            {
                transform.position = new Vector3(transform.position.x, yPosition + 1.2f, 0);

            }
            return;
        }

        transform.position += Vector3.down * speed * Time.deltaTime;

        return;
    }
    private bool isHitOthersRight()
    {
        foreach (Transform child in blockChildren)
        {
            Ray ray = new Ray(child.position, Vector3.right);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, 0.6f))
            {
                if (rayHit.transform.tag == "Block")
                    return true;
            }
        }
        return false;
    }

    private bool isHitOthersLeft()
    {
        foreach (Transform child in blockChildren)
        {
            Ray ray = new Ray(child.position, Vector3.left);
            RaycastHit rayHit;


            if (Physics.Raycast(ray, out rayHit, 0.6f))
            {
                if (rayHit.transform.tag == "Block")
                    return true;
            }
        }
        return false;
    }



    private bool isPlaced()
    {
        foreach (Transform child in blockChildren)
        {
            Ray ray = new Ray(child.position, Vector3.down);
            RaycastHit rayHit;
            Debug.DrawRay(child.position, Vector3.down, Color.red);
            if (Physics.Raycast(ray, out rayHit, 0.6f))
            {
                if (rayHit.transform.tag == "Ground" || rayHit.transform.tag == "Block")
                {
                    yPosition = rayHit.transform.position.y;
                     return true;
                }
                    
            }
        }
        return false;
    }

    private bool isInMapRight()
    {
        foreach (Transform child in blockChildren)
        {
            if (child.transform.position.x >= map.getWidth() - 1.2f)
                return false;
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
        blockSpawner.block.transform.Rotate(new Vector3(0, 0, 90));

        if (!isInMapLeft() || !isInMapRight() || isHitOthersLeft() || isHitOthersRight())
            blockSpawner.block.transform.Rotate(new Vector3(0, 0, -90));
        return;
    }

    public GameObject getBlock()
    {
        return gameObject;
    }


}
