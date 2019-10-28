using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private int width;
    private int height;
    protected GameObject[,] map; 
    private bool isBuilt = false;
    Material cubeMaterial;
   
    private void buildMap()
    {
        for(int x = 0; x < 10; x++)
        {
            for(int y =0; y <20; y++)
            {
              
                var mapBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                mapBox.transform.position = new Vector3(x * 1.2f , y * 1.2f, 0);
                Renderer rend = mapBox.GetComponent<Renderer>();
                rend.material = cubeMaterial;
                map[x, y] = mapBox;
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject[10, 20];
        width = 12;
        height = 24;
        cubeMaterial = new Material(Resources.Load("TranslucentCubeMaterial") as Material);
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

    public int getWidth()
    {
        return width;
    }
    public int getHeight()
    {
        return height;
    }
}



