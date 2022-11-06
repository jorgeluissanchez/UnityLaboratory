using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    //VARIABLES
    float moveAcceleration = 4f, moveMaxSpeed = 6f;
    [SerializeField] SpriteRenderer robotSpriteRenderer;
    Vector3 moveSpeed;
    //MANAGE INITIAL POSITION
    void Start()
    {
        transform.position = new Vector3((float)0.5, -3, 0);
    }
    //MANAGE MOVEMENT
    void Update()
    {
        transform.position += Time.deltaTime * moveSpeed;
        accelerationManager();
    }
    //MANAGE ACCELERATION AND Direction
    int rotateState = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slow")
        {
            moveSpeed.y /= 4;
            StartCoroutine(blinkEffect());
        }
        if (collision.gameObject.tag == "Derecha")
        {
            moveSpeed.x = -3;
            if (rotateState == 0)
            {
                StartCoroutine(RotateCar(10, 1f));
                rotateState = 1;
            }
            else if (rotateState == 2)
            {
                StartCoroutine(RotateCar(10, 2f));
                rotateState = 1;
            }
        }
        if (collision.gameObject.tag == "Izquierda")
        {
            moveSpeed.x = 3;
            if (rotateState == 0)
            {
                StartCoroutine(RotateCar(10, -1f));
                rotateState = 2;
            }
            else if (rotateState == 1)
            {
                StartCoroutine(RotateCar(10, -2f));
                rotateState = 2;
            }
        }
        if (collision.gameObject.tag == "Centro")
        {
            moveSpeed.x = 0;
            if (rotateState == 1)
            {
                StartCoroutine(RotateCar(10, -1f));
                rotateState = 0;
            }
            else if (rotateState == 2)
            {
                StartCoroutine(RotateCar(10, 1f));
                rotateState = 0;
            }
        }
    }
    //MANAGE Blink Effect
    private IEnumerator blinkEffect()
    {
        int blinkTimes = 10;
        while(blinkTimes > 0)
        {
            robotSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkTimes * (float)0.01);
            robotSpriteRenderer.enabled = true; //turn on
            yield return new WaitForSeconds(blinkTimes * (float)0.01);
            blinkTimes--;
        }
    }
    //MANAGE ROTATION
    private IEnumerator RotateCar(float rotateTimes, float rotateAngle)
    {
        for (int i = 0; i < rotateTimes; i++)
        {
            transform.RotateAround(transform.position, Vector3.back * Time.deltaTime, rotateAngle);
            yield return new WaitForSeconds(0.01f);
        }
    }
    //MANAGE ACCELERATION
    private void accelerationManager() {
        if (Mathf.Abs(moveSpeed.y) >= moveMaxSpeed)
        {
            moveSpeed.y = moveMaxSpeed * (-1);
        }
        else
        {
            moveSpeed.y -= moveAcceleration * Time.deltaTime;
        }
    }
}
