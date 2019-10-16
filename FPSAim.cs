using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSAim : MonoBehaviour
{
    [SerializeField]
    private float speed ;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.mousePosition.x - Screen.width / 2;
        //float y = Input.mousePosition.y - Screen.height / 2;

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(y * speed * -1, x * speed ,0);

    }
}
