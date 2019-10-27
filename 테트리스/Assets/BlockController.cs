using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : Block
{
    private float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         MoveBlock();
    }

    void MoveBlock()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            speed *= 2.0f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            block.transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            block.transform.position += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {

        }

        block.transform.position += speed * Vector3.down;
    }

}
