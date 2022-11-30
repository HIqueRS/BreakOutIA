using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raquete : MonoBehaviour
{
    public float distance = 10f;
    public GameObject boula;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(distance = Vector2.Distance(this.transform.position, boula.transform.position));

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
