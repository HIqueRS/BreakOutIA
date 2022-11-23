using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{

    Rigidbody2D rgb2d;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();

        float angle;

        angle = Random.Range(-60f, 60f);





        dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);

        //Debug.Log(dir);

        rgb2d.AddForce(dir*300f); //ser uma força random entre -60 e 60 graus 

        //rb2d.velocity = (dir * 35f);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
