using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBars : MonoBehaviour
{
    private GameObject[,,] bars = new GameObject[10, 10, 10];

    private void buildBars()
    {
        for (int x = 0; x < 10; ++x)
        {
            for (int z = 0; z < 10; ++z)
            { 
                for (int y = 0; y < 10; ++y)
                {
                    if (bars[x, y, z] != null)
                    {
                        //게임 엔진 내에서 게임오브젝트를 따로 관리함 ? 그래서 관리안할거라고 이 함수를 써줘야함.
                        Destroy(bars[x, y, z]);
                        bars[x, y, z] = null;
                        
                    }
                }
            }
        }
        for (int x = 0; x < 10; ++x)
        {
            for(int z = 0; z < 10; ++z)
            {
                int maxY = Random.Range(1, 10);
                for (int y = 0; y < maxY; ++y)
                {
                    var bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bar.transform.position = new Vector3(x, y, z);

                    bars[x, y, z] = bar;
                }
            }
        }
    }

    private float timer = 0.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        buildBars();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 3.0f)
        {
            buildBars();
            timer = 0.0f;
        }
    }
}
