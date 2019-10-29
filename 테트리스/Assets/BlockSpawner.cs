using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject block;
    
    public bool needBlockSpawn = true;

    // Start is called before the first frame update
    void Awake()
    {
        spawnBlock();
    }

    // Update is called once per frame
    
    void Update()
    {
        if (needBlockSpawn)
        {
            spawnBlock();
        }
    }

    private void spawnBlock()
    {
      
       int randomBlockN = Random.Range(1, 8);
       block = Instantiate(Resources.Load("Prefabs/Block" + randomBlockN) as GameObject, new Vector3(6, 22.8f, 0), Quaternion.identity);
       block.AddComponent<BlockController>();

       needBlockSpawn = false;
        
    }

    public GameObject getBlock()
    {
        return block;
    }
}
