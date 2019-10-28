using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected GameObject block;
    public Transform[] blockChildren;
    private float speed = 2.4f;
    private int randomBlockN;
    private MapManager map;
   

    // Start is called before the first frame update
    void Start()
    {
        //맵 스크립트 가져오기
        map = GameObject.Find("Map").GetComponent<MapManager>();
        //블록 랜덤 생성
        randomBlockN = Random.Range(1, 8);
        block = Instantiate(Resources.Load("Prefabs/Block" + randomBlockN) as GameObject, new Vector3(6,24,0), Quaternion.identity);

        blockChildren = block.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        moveBlock();
        
    }

    private void moveBlock()
    {
        if (isPlaced())
            return;

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            speed *= 2.4f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && isInMapRight())
        {
            block.transform.position += Vector3.right * 1.2f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isInMapLeft())
        {
            block.transform.position += Vector3.left * 1.2f;
          
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isHitOthersRight() && !isHitOthersLeft() && isRotateionOk())
        {
            block.transform.Rotate(new Vector3(0,0,90));
        }
     
        block.transform.position += Vector3.down * speed * Time.deltaTime;

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
                {
                    Debug.Log(rayHit.collider.gameObject.name);
                    return true;
                }

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
                if (rayHit.transform.tag == "Ground" || rayHit.transform.tag == "Block")
                {
                    Debug.Log(rayHit.collider.gameObject.name);
                    return true;
                }

            }
        }
        return false;
    }

    private bool isPlaced()
    {
       foreach(Transform child in blockChildren)
        {
            Ray ray = new Ray(child.position, Vector3.down);
            RaycastHit rayHit;
            Debug.DrawRay(child.position, Vector3.down, Color.red);
            if (Physics.Raycast(ray, out rayHit, 0.6f))
            {
                if (rayHit.transform.tag == "Ground" || rayHit.transform.tag == "Block")
                {
                    Debug.Log(rayHit.collider.gameObject.name);
                    return true;
                }
               
            }
        }
         return false;
    }

    private bool isInMapRight()
    {
        Transform TheFarRightChild = blockChildren[0];
        foreach(Transform child in blockChildren)
        {
            TheFarRightChild = TheFarRightChild.position.x > child.position.x ? TheFarRightChild : child; 
        }
        if (TheFarRightChild.position.x < map.getWidth() - 1)
            return true;
        return false;
    }

    private bool isInMapLeft()
    {
        Transform TheFarLeftChild = blockChildren[0];
        foreach (Transform child in blockChildren)
        {
            TheFarLeftChild = TheFarLeftChild.position.x < child.position.x ? TheFarLeftChild : child;
        }
        if (TheFarLeftChild.position.x > 0)
            return true;
        return false;
    }

    private bool isRotateionOk()
    {
        GameObject rotatedBlock = block;
        rotatedBlock.transform.Rotate(new Vector3(0, 0, 90));
        Transform[] rotatedBlockChildren = rotatedBlock.GetComponentsInChildren<Transform>();
       
        foreach(Transform child in rotatedBlockChildren)
        {
            if (child.position.x >= map.getWidth() - 1 || child.position.x <= 0)
                return false;
        }
        return true;
    }


}
