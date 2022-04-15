using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

enum Cars
{
    Blue,
    Red
}

public class CarController: MonoBehaviour
{
    
    private float changePointDuration = .35f;
    private float rotateSpeed = 2f;
    [SerializeField] private List<Transform> carPositions;
    [SerializeField] private Cars type;
    [SerializeField] private float speed;
    [SerializeField] private float smoth = 1f;
    private Vector3 startPosition, targetPoint;
    private Quaternion targetRotation;
    private Quaternion startRotation;
    private Transform tr;
    private void Awake()
    {
        tr = transform;


    }

    private void Start()
    {

        startRotation = tr.rotation;
    }

    private void Update()
    {
        //UIManager.M_Instance.HighScoreText.text = "Best Score : " + UIManager.M_Instance.HighScore.ToString();

    }

    public void SetTarget()//name best change to : car detector
    {
        if (type == Cars.Blue) 
        {
            // position detector
            if (transform.position.x > 0 && transform.position.x < 1.4)
            {
                // set position
                targetPoint = carPositions[0].position;
                targetRotation = Quaternion.Euler(new Vector3(0, 0, -45));
                ChangePointBlue(targetPoint,targetRotation);
            }
            // position detector

            if (transform.position.x > 1.4 && transform.position.x < 6.75)
            {
                // set position
                targetPoint = carPositions[1].position;
                targetRotation = Quaternion.Euler(new Vector3(0, 0, 45));

                ChangePointBlue(targetPoint,targetRotation);
            }

        
        }


        if (type == Cars.Red)// car detector
        {
            // position detector

            if (transform.position.x < 0 && transform.position.x > -1.4)
            {
                // set position

                Debug.Log("Red");

                targetPoint = carPositions[3].position;
                targetRotation = Quaternion.Euler(new Vector3(0, 0, 45));

                ChangePointRed(targetPoint,targetRotation);
            }
            // position detector

            if (transform.position.x < -1.4 && transform.position.x > -6.75)
            {
                // set position

                targetPoint = carPositions[2].position;
                targetRotation = Quaternion.Euler(new Vector3(0, 0, -45));

                ChangePointRed(targetPoint,targetRotation);
            }

          
        }
    }

    private void ChangePointBlue(Vector3 targetPoint, Quaternion targetRotation)
    {
        //use destination instead point
        StartCoroutine(ChangePointCorotuneBlue(targetPoint,targetRotation));
    }
    private void ChangePointRed(Vector3 targetPoint,Quaternion targetRotation)
    {
        StartCoroutine(ChangePointCorotuneRed(targetPoint,targetRotation));
    }
    private IEnumerator ChangePointCorotuneBlue(Vector3 targetPoint, Quaternion targetRotation)// change name to master name include small scopes
    {
        float timer = 0;
        Vector3 startPoint = tr.position;

        while (timer <= .5f)
        {
            timer += Time.deltaTime;// time passed from move is better instead timer
            // lerping position
            tr.position = Vector3.Lerp(startPoint, targetPoint, timer / changePointDuration);
            // set gap , change gaps name to "distance from target"
            Vector3 gap = targetPoint - tr.position; 
// close while
            
//start new while from .5 with time += time.deltatime 
            if (timer > .7f)// lerping rotation, this scope never run for a bug that I hope will solve with new comments
            {
                tr.rotation = Quaternion.Lerp(tr.rotation, targetRotation,
                    rotateSpeed * Time.deltaTime);
                if (gap == Vector3.zero)// is in position is better for name
                {
                    transform.rotation = Quaternion.identity;
                }
            }
            else
            {// has duplicated!better is in above code .7 chnage to .5 and then remove below code
                tr.rotation = Quaternion.Lerp(tr.rotation, targetRotation,
                    rotateSpeed * Time.deltaTime);
                if (gap == Vector3.zero)
                {
                    transform.rotation = Quaternion.identity;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        tr.position = targetPoint;// must ba in if gap part in above code
    }
    private IEnumerator ChangePointCorotuneRed(Vector3 targetPoint, Quaternion targetRotation)
    {
        float timer = 0;
        Vector3 startPoint = tr.position;

        while (timer <= 0.5f )
        {
            timer += Time.deltaTime;
            tr.position = Vector3.Lerp(startPoint, targetPoint, timer / changePointDuration);
            Vector3 gap = targetPoint - tr.position;

           

            if (timer > .7f)
            {
                tr.rotation = Quaternion.Lerp(tr.rotation, targetRotation,
                    rotateSpeed * Time.deltaTime);
                if (gap == Vector3.zero)
                {
                    transform.rotation = Quaternion.identity;
                }
            }
            else
            {
                tr.rotation = Quaternion.Lerp(tr.rotation, targetRotation,
                    rotateSpeed * Time.deltaTime);
                if (gap == Vector3.zero)
                {
                    transform.rotation = Quaternion.identity;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        tr.position = targetPoint;
    }
    // add new class named collide manager for belove scope
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //UIManager.M_Instance.mainMenuOn = true;
           
            UIManager.M_Instance.Gameover();
            // add method set score to high score
            if (UIManager.M_Instance.score > UIManager.M_Instance.highScore)
            {
                UIManager.M_Instance.highScore = UIManager.M_Instance.score;
            }
            // add method update score text
            UIManager.M_Instance.gameoverScore.text = "YOUR SCORE : " + UIManager.M_Instance.score;
            // add method update high score text

          UIManager.M_Instance.highScoreText.text = "HIGH SCORE : " + UIManager.M_Instance.highScore;
         
            UIManager.M_Instance.SaveScore();
        }

        if (other.CompareTag("Score"))
        {
            
            UIManager.M_Instance.AddScore();        
            Destroy(other.gameObject);
         //   ObjPool.M_instance.AddToPool = gameObject;
         // call update hight score text method
            UIManager.M_Instance.scoreText.text = "" + UIManager.M_Instance.score.ToString();
        }
    }
}