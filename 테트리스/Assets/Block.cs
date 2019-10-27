using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected GameObject block;
    private float speed = 1.0f;
    private int randomBlockN;
    private MapManager mapManager;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        //맵 스크립트 가져오기
        mapManager = GameObject.Find("Map").GetComponent<MapManager>();
        //블록 랜덤 생성
        randomBlockN = Random.Range(1, 8);
        block = Instantiate(Resources.Load("Prefabs/Block" + randomBlockN) as GameObject, new Vector3(5,19,0), Quaternion.identity);

        rend = block.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveBlock();
        
    }

    private void moveBlock()
    {
        if (isBlockInMap() == false)
            return;
       
        if(Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            speed *= 2.0f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true 
            && block.transform.position.x <= mapManager.getWidth() - 1)
        {
            block.transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true
            && block.transform.position.x >= 0.0f)
        {
            block.transform.position += Vector3.left;
          
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            block.transform.Rotate(new Vector3(0,0,90));
        }

        block.transform.position += Vector3.down * speed * Time.deltaTime;

        return;
    }

    private bool isBlockInMap()
    {
        if (block.transform.position.y <= 0.0f)
            return false;

        return true;
    }

   

    

    
}
