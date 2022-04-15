using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float slowmotionTime;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, -5, 0);
    }
   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           slowmotionTime += Time.deltaTime;
         ObstacleController.obstacleSpeed = -3f;
            Debug.Log("Slow");
            Debug.Log("time left : " + slowmotionTime);
            if (slowmotionTime > 30f)
            {
                ObstacleController.obstacleSpeed = -5f;
                Debug.Log("normal");
            }
        }
    }
}
