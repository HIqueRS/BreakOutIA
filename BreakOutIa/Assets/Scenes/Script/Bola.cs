using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEditor.UIElements;
using TMPro;

public class Bola : MonoBehaviour
{
    public TextMeshProUGUI inter;
    public TextMeshProUGUI gremio;
 
    public int golsInter = 0;
    public int golsGremio = 0;
    public Rigidbody2D rgb2d;
    float angle;
    Vector3 dir;

    public float time;
    public Transform middle;

    public Vector2 posAntiga;
    public Vector2 posAtual;

    #region TIMERS
    public float countX = 0;
    public float countY = 0;
    #endregion

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
        posAtual = this.transform.position;

        Debug.Log(rgb2d.velocity);
        time += Time.deltaTime;

        if (rgb2d.velocity.magnitude < 4f)
        {
            rgb2d.velocity = Vector2.zero;
            Vector2 dir = posAtual - posAntiga;

            dir = dir.normalized;
            rgb2d.AddForce(dir * 241.01f);
        }

        if (posAtual.x == posAntiga.x)
        {
            countX += Time.deltaTime;
        } 
        if (posAtual.y == posAntiga.y)
        {
            countY += Time.deltaTime;
        }

        if (countX > 2f || countY > 2f)
        {
            ResetBall();
        }

        posAntiga = this.transform.position;

        //if (time > 5)
        //{
        //    ResetBall();
        //}
    }

    public void InitBall(float force = 241.1f)
    {
        angle = Random.Range(60f, -60f);
        //angle = 60;
        dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0);

        rgb2d.AddForce(dir * force);
    }


    private void UpdateScore()
    {
        inter.text = golsInter.ToString();
        gremio.text = golsGremio.ToString();
    }

    private void ResetBall()
    {
        countY = countX = 0f;
        rgb2d.velocity = Vector2.zero;
        this.transform.position = middle.transform.position;
        angle = 0.0f;
        dir = new Vector3(0, 0, 0);

        time = 0f;
        InitBall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "GoleiraInter")
        {
            time = 0;
            golsGremio += 1;
            UpdateScore();
            ResetBall();

        }else if (collision.tag == "GoleiraGremio"){
            time = 0;
            golsInter += 1;
            UpdateScore();
            ResetBall();
        }
    }
}
