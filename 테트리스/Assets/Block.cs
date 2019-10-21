using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    GameObject block;
    int randomBlockN;

    // Start is called before the first frame update
    void Start()
    {
        randomBlockN = Random.Range(1, 8);
        block = Instantiate(Resources.Load("Prefabs/Block" + randomBlockN) as GameObject, new Vector3(5,19,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
