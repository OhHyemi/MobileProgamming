using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    private GameObject player;

    private ZombiSpawner zombiSpawner;

    float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        zombiSpawner = GameObject.Find("Main Camera").GetComponent<ZombiSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            return;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist < 1.0f)
        {
            zombiSpawner.destroyMe(this.gameObject);
            return;
            
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);

        
    }

   
}
