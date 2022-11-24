using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{

    public Rigidbody2D rgb2d;
    float angle;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        InitBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitBall(float force = 300f)
    {
        angle = Random.Range(-60f, 60f);

        dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);

        rgb2d.AddForce(dir * 300f);
    }
}
