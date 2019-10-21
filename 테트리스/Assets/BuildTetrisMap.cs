using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTetrisMap : MonoBehaviour
{
    protected GameObject[,] map = new GameObject[12, 22];
    private bool isBuilt = false;
   
    private void buildMap()
    {
        for(int x = 0; x < 12; x++)
        {
            for(int y =0; y <22; y++)
            {
                if (x == 0 || x == 11 || y == 0 || y == 21)
                {
                    var mapBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    mapBox.transform.position = new Vector3(x - 1, y - 1, 0);

                    map[x, y] = mapBox;
                }
                else
                    map[x, y] = null;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buildMap();
        isBuilt = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isMapBuilt()
    {
        return isBuilt;
    }
}



