using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES

    // PLAYER
    [SerializeField] Transform playerPointer;
    Vector2 playerPointerDirection;
    [SerializeField] Camera playerCamara;
    [SerializeField] Animator playerAnimation;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    CamaraController playerCameraController;

    // PLAYER HEALTH
    [SerializeField] int playerHealth = 5;
    public int PlayerHealth
    {
        get => playerHealth;
        set
        {
            playerHealth = value;
            UIManager.Instance.UpdateUIHealth(playerHealth);
        }
    }

    // MOVE
    public float moveFriction = 4f, moveAcceleration = 2f, moveMaxSpeed = 6f;
    float moveHorizontal, moveVertical;
    Vector3 moveDirection, moveSpeed;

    // BULLET
    bool bulletLoaded = true;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] float bulletRate = 1;

    // INVULNERABLE
    [SerializeField] int invulnerableTime = 3;
    [SerializeField] bool invulnerableState;

    // POWER
    bool powerBulletState;

    //BLINK
    [SerializeField] float blinkRate = 1;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        playerCameraController = FindObjectOfType<CamaraController>();
    }

    void Update()
    {
        // MOVE AND ANIMATION
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        moveDirection.x = moveHorizontal;
        moveDirection.y = moveVertical;
        transform.position += Time.deltaTime * moveSpeed;
        accelerationManager();
        playerAnimation.SetFloat("speedMagnitude", moveSpeed.magnitude);

        // POINTER AND FLIP
        playerPointerDirection = playerCamara.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        playerPointer.position = transform.position + (Vector3)playerPointerDirection.normalized;
        if (playerPointer.position.x < transform.position.x)
        {
            playerSpriteRenderer.flipX = true;
        } else if (playerPointer.position.x > transform.position.x)
        {
            playerSpriteRenderer.flipX = false;
        }

        // BULLET
        if (Input.GetMouseButton(0) && bulletLoaded)
        {
            bulletLoaded = false;
            float bulletAngle = Mathf.Atan2(playerPointerDirection.y, playerPointerDirection.x) * Mathf.Rad2Deg; //find the angle in rad and convert it to deg
            Quaternion bulletRotation = Quaternion.AngleAxis(bulletAngle, Vector3.forward); // convert the deg to rotation information
            Transform bulletClone = Instantiate(bulletPrefab, transform.position, bulletRotation);
            
            if (powerBulletState)
            {
                bulletClone.GetComponent<Bullet>().powerBullet = true;
            }
            
            StartCoroutine(bulletReloader());
        }

    }

    // wait to use the bullet
    IEnumerator bulletReloader()
    {
        yield return new WaitForSeconds(1 / bulletRate);
        bulletLoaded = true;
    }

    //TAKE DAMAGE
    public void TakeDamage()
    {
        if (invulnerableState) // if the player is invulnerable exit
            return;

        PlayerHealth--; //take damage
        invulnerableState = true; //make invulnerable
        playerCameraController.shake(); //Shake camara

        StartCoroutine(turnOffInvulnerability()); //wait tu turn off

        //show the game over
        if (PlayerHealth <= 0)
        {
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOver();
        }
    }

    //make vulnerable
    IEnumerator turnOffInvulnerability()
    {
        StartCoroutine(blinkEffect());//blinks
        yield return new WaitForSeconds(invulnerableTime); //wait
        invulnerableState = false; //turn off
    }

    //BLINK
    IEnumerator blinkEffect()
    {
        int blinkTimes = 10;
        while(blinkTimes > 0)
        {
            playerSpriteRenderer.enabled = false; //turn off
            yield return new WaitForSeconds(blinkTimes * blinkRate); //wait
            playerSpriteRenderer.enabled = true; //turn on
            yield return new WaitForSeconds(blinkTimes * blinkRate); //wait
            blinkTimes--;
        }
    }


    //POWER
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //validate power
        if (collision.CompareTag("PowerUp")) 
        {
            //validate power type
            switch (collision.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.Range: //increment the bullet rate
                    bulletRate++;
                    break;

                case PowerUp.PowerUpType.Shot: //turn on the power bullet state
                    powerBulletState = true;
                    break;
            }

            Destroy(collision.gameObject, 0.1f); //wait 0.1 and destroy de powerUP item
        }
    }

    // MANAGE THE ACCELERATION
    private void accelerationManager() {

        //acceleration in x controller
        if (moveDirection.x == 0)
        {
            if (Mathf.Abs(moveSpeed.x) <= 0.1f)
            {
                moveSpeed.x = 0;
            }
            else
            {
                if (moveSpeed.x > 0)
                {
                    moveSpeed.x -= moveFriction * Time.deltaTime;
                } 
                else
                {
                    moveSpeed.x += moveFriction * Time.deltaTime;
                }
            }
        }
        else 
        {
            if(moveDirection.x * moveSpeed.x < 0)
            {
                moveSpeed.x += moveFriction * Time.deltaTime * moveDirection.x;
            }
            else
            {
                if (Mathf.Abs(moveSpeed.x) >= moveMaxSpeed)
                {
                    moveSpeed.x = moveMaxSpeed * moveDirection.x;
                } 
                else
                {
                    moveSpeed.x += moveAcceleration * Time.deltaTime * moveDirection.x;
                }
            }
        }

        //acceleration in y controller
        if (moveDirection.y == 0)
        {
            if (Mathf.Abs(moveSpeed.y) <= 0.1f)
            {
                moveSpeed.y = 0;
            }
            else
            {
                if (moveSpeed.y > 0)
                {
                    moveSpeed.y -= moveFriction * Time.deltaTime;
                }
                else
                {
                    moveSpeed.y += moveFriction * Time.deltaTime;
                }
            }
        }
        else
        {
            if (moveDirection.y * moveSpeed.x < 0)
            {
                moveSpeed.y += moveFriction * Time.deltaTime * moveDirection.y;
            }
            else
            {
                if (Mathf.Abs(moveSpeed.y) >= moveMaxSpeed)
                {
                    moveSpeed.y = moveMaxSpeed * moveDirection.y;
                }
                else
                {
                    moveSpeed.y += moveAcceleration * Time.deltaTime * moveDirection.y;
                }
            }
        }
    }
}