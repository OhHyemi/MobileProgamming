using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Material blueMaterial;
    public Transform[] zombiTr = new Transform[10];
    int closestZombiIndex;

    // Start is called before the first frame update
    void Start()
    {
         
        for (int i = 0; i < 10; i++)
        {
            zombiTr[i] = GameObject.Find("Zombi" + (i+1)).transform;
        }
        closestZombiIndex = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
       for(int i = 1; i < 10; i++)
        {
            closestZombiIndex = GetDistance(zombiTr[closestZombiIndex]) < GetDistance(zombiTr[i])
                ? closestZombiIndex : i;
        }
        zombiTr[closestZombiIndex].GetComponent<Renderer>().material = blueMaterial;

        print("Zombi" + (closestZombiIndex+1) + " is the closest to you");
    }
    
    float GetDistance(Transform zombi)
    {
        Vector3 temp;
        
        temp = (gameObject.transform.localPosition - zombi.localPosition);
        
        return (temp.x * temp.x) + (temp.y * temp.y) + (temp.z * temp.z);
    }

   
}
