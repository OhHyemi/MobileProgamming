using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpawner : MonoBehaviour
{
    private static int nZombies;

    [SerializeField]
    private GameObject zombiePrefab;

    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            spawn();
            timer = 0.0f;
        }
    }

    private void spawn()
    {
        if (nZombies >= 4)
            return;

        Vector3 pos = new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f,10f));

        Instantiate(zombiePrefab, pos, Quaternion.identity);
        nZombies++;
    }

    public void destroyMe(GameObject obj)
    {
        Destroy(obj);
        nZombies--;
    }
}
