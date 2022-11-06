using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIABLES
    Vector2 playerPointerDirection;
    [SerializeField] Camera playerCamara;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    CamaraController playerCameraController;
    [SerializeField] int playerCrash = 0;
    public int PlayerCrash
    {
        get => playerCrash;
        set
        {
            playerCrash = value;
            UIManager.Instance.UpdateUICrash(playerCrash);
        }
    }
    float moveAcceleration = 4f, moveMaxSpeed = 6f;
    float moveHorizontal, moveVertical;
    Vector3 moveDirection;
     private Vector3 moveSpeed;
    public float MoveSpeedY
    {
        get => moveSpeed.y;
        set
        {
            moveSpeed.y = value;
            UIManager.Instance.UpdateUISpeedY(Mathf.Round(Mathf.Abs(moveSpeed.y)));
        }
    }
    public float MoveSpeedX
    {
        get => moveSpeed.x;
        set
        {
            moveSpeed.x = value;
            UIManager.Instance.UpdateUISpeedX(Mathf.Round(Mathf.Abs(moveSpeed.x)));
        }
    }
    [SerializeField] int invulnerableTime = 3;
    [SerializeField] bool invulnerableState;
    [SerializeField] float blinkRate = 1;
    //MANAGE INITIAL POSITION AND CAMERA
    void Start()
    {
        transform.position = new Vector3((float)-1.5, -3, 0);
        playerCameraController = FindObjectOfType<CamaraController>();
    }
    //MANAGE MOVEMENT
    void Update()
    {
        transform.position += Time.deltaTime * moveSpeed;
        accelerationManager();
    }
    //MANAGE DAMAGE
    public void TakeDamage()
    {
        if (invulnerableState)
            return;
        PlayerCrash++;
        invulnerableState = true;
        playerCameraController.shake(); 
        StartCoroutine(turnOffInvulnerability());
    }
    //MANAGE INVULNERABILITY
    IEnumerator turnOffInvulnerability()
    {
        StartCoroutine(blinkEffect());
        yield return new WaitForSeconds(invulnerableTime);
        invulnerableState = false;
    }
    //MANAGE BLINK EFFECT
    private IEnumerator blinkEffect()
    {
        int blinkTimes = 10;
        while(blinkTimes > 0)
        {
            playerSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkTimes * blinkRate);
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkTimes * blinkRate);
            blinkTimes--;
        }
    }
    //MANAGE SLOW EFFECT
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slow")){
            StartCoroutine(blinkEffect());
            playerCameraController.shake();
        }
    }

    float numTec = 0;
    //ROTATE CAR
    private IEnumerator RotateCar(float rotateTimes, float rotateAngle)
    {
        for (int i = 0; i < rotateTimes; i++)
        {
            transform.RotateAround(transform.position, Vector3.back * Time.deltaTime, rotateAngle);
            yield return new WaitForSeconds(0.01f);
        }
    }
    //ACCELERATION MANAGER
    private void accelerationManager() {
        //MANAGE ACCELERATION IN X 
        if(  transform.position.x <= 2 && transform.position.x >= -8)
        {
            if (Input.GetKeyDown("right") && numTec == 0) {
                MoveSpeedX = -3;
                StartCoroutine(RotateCar(10, 1f));
                numTec = 1;
            } 
            if (Input.GetKeyUp("right") && numTec == 1)
            {
                MoveSpeedX = 0;
                StartCoroutine(RotateCar(10, -1f));
                numTec = 0;
            } 
            if (Input.GetKeyDown("left") && numTec == 0)
            {
                MoveSpeedX = 3;
                StartCoroutine(RotateCar(10, -1f));
                numTec = 2;
            }
            if (Input.GetKeyUp("left") && numTec == 2)
            {
                MoveSpeedX = 0;
                StartCoroutine(RotateCar(10, 1f));
                numTec = 0;
            }
            /*
            if (Input.GetMouseButtonDown(0))
            {
                if(Input.mousePosition.x < Screen.width / 2 && numTec == 0)
                {
                    MoveSpeedX = 3;
                    StartCoroutine(RotateCar(10, -1f));
                    numTec = Input.mousePosition.x;
                }
                if(Input.mousePosition.x > Screen.width / 2 && numTec == 0)
                {
                    MoveSpeedX = -3;
                    StartCoroutine(RotateCar(10, 1f));
                    numTec = Input.mousePosition.x;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if(numTec <  Screen.width / 2)
                {
                    MoveSpeedX = 0;
                    StartCoroutine(RotateCar(10, 1f));
                    numTec = 0;
                }
                if (numTec >  Screen.width / 2)
                {
                    MoveSpeedX = 0;
                    StartCoroutine(RotateCar(10, -1f));
                    numTec = 0;
                }
            }*/
        } else {
            if(transform.position.x > 2)
            {
                MoveSpeedX = -1;
                StartCoroutine(RotateCar(10, 36f));
                
            }
            if(transform.position.x < -8)
            {
                MoveSpeedX = 1;
                StartCoroutine(RotateCar(10, -36f));
            }
            playerCameraController.shake();
            MoveSpeedY = 0;
        }
        //MANAGE ACCELERATION IN Y
        if (Mathf.Abs(MoveSpeedY) >= moveMaxSpeed)
        {
            MoveSpeedY = moveMaxSpeed * (-1);
        }
        else
        {
            MoveSpeedY -= moveAcceleration * Time.deltaTime;
        }
    }
}