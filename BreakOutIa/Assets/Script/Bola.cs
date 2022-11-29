using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{

    public Rigidbody2D rgb2d;
    float angle;
    Vector3 dir;

    public float time;

    // Start is called before the first frame update
    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        InitBall();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > 25)
        {
            InitBall();
            time = 0f;
        }
    }

    public void InitBall(float force = 200)
    {
        angle = Random.Range(-60f, 60f);

        dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);

        rgb2d.AddForce(dir * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Bot"))
        {
            time = 0;
        }
    }
}
