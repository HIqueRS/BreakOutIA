using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raquete : MonoBehaviour
{

    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = Vector2.left;
            transform.position += dir * Time.deltaTime * 4;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            dir = Vector2.right;
            transform.position += dir * Time.deltaTime * 4;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
