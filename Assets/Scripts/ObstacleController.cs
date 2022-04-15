using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Rigidbody rb;
    public static float obstacleSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        obstacleSpeed = 0f;
        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, obstacleSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Score"))
        {
            if (other.CompareTag("GameOver"))
            {
              UIManager.M_Instance.Gameover();// new method call game over 
              // new method set score to high score
                if (UIManager.M_Instance.score > UIManager.M_Instance.highScore)
                {
                    UIManager.M_Instance.highScore = UIManager.M_Instance.score;
                }
//new method set score and highscore text
                UIManager.M_Instance.gameoverScore.text = "YOUR SCORE : " + UIManager.M_Instance.score;
               UIManager.M_Instance.highScoreText.text = "HIGH SCORE : " + UIManager.M_Instance.highScore;
               // new method call save score 
                UIManager.M_Instance.SaveScore();
            }
        }

        /*if (gameObject.CompareTag("Enemy"))
        {
            if (other.CompareTag("GameOver"))
            {
                Destroy(gameObject);
            }
        }*/
    }
}