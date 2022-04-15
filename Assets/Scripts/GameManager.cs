using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float turn= 0;
    private bool changing = false;

    void ChangeLane(Transform car , float angle ,float time )
    {
        float t;
        float bank;
     
        if (changing) return;
        changing = true;
        for (t = 0; t < 1;)
        {
            t += 2 * Time.deltaTime / time;
            turn = Mathf.Lerp(turn, angle, t);
            bank = 0.5f* turn;
            car.localEulerAngles =new Vector3(0, turn, bank);
            break;
            
        }

        for (t = 0; t < 1;)
        {
            t += 2 * Time.deltaTime / time;
            turn = Mathf.Lerp(turn, 0, t);
            bank = 0.5f * turn;
            car.localEulerAngles =new Vector3(0, turn, bank);
           break;
           
        }

        changing = false;
    }

// This is just for testing purposes - you must call ChangeLane in your script // when changing lanes. Attach this script to the car to test it.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("change");
            ChangeLane(transform, 5, 1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(transform, -5, 1);
        }
    }
}