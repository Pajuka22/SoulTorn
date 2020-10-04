using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour
{
    private float direction;
    public List<float> jumpHeight = new List<float>();//list of heights of jumps. Allows for as many jumps as you want
    private int jumpIndex;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float accelerationRate;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float decelerationRate;
    [SerializeField]
    private bool onGround;
    [SerializeField]
    private Transform ground;
    [SerializeField]
    private float castRadius;
    [SerializeField]
    private LayerMask whatIsGround;

    bool movedDuring; //not doing anything rn?
    bool jump;


    //Runtime Variables
    bool inDodgeRoll;
    float dodgeLength;
    bool justFinished;
        

    //Global Control Bools
    bool dodgeRoll;
    public float additionalSpeed;
    float iframes;

    //Components
    PlayerStates states;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        inDodgeRoll = false;
        //Load();
        direction = 0;
        rb = GetComponent<Rigidbody2D>();
        states = GetComponent<PlayerStates>();
        walkSpeed += additionalSpeed;
        //rb.gravityScale = GlobalControl.GravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (states.current != PlayerStates.AnimStates.stun && states.current != PlayerStates.AnimStates.death)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (justFinished && direction > 0)
                {
                    currentSpeed = 0;
                    justFinished = false;
                }
                //Debug.Log("Left");
                direction = -1 ;

                //currentSpeed += direction * (accelerationRate * Time.fixedDeltaTime);
                //Player.Instance.spriteR.flipX = true;
                if (onGround)
                {
                    states.current = states.current == PlayerStates.AnimStates.atk ? PlayerStates.AnimStates.runatk : PlayerStates.AnimStates.run;
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        StartCoroutine(dodgeRollMove());
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (justFinished && direction < 0)
                {
                    currentSpeed = 0;
                    justFinished = false;
                }
                //Debug.Log("Right");
                direction = 1;

                //currentSpeed += direction * (accelerationRate * Time.fixedDeltaTime);
                //Player.Instance.spriteR.flipX = false;
                if (onGround)
                {
                    states.current = states.current == PlayerStates.AnimStates.atk ? PlayerStates.AnimStates.runatk : PlayerStates.AnimStates.run;
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        StartCoroutine(dodgeRollMove());
                    }
                }
                //Emily you might need to keep in mind that I'm about to add a dodge roll
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    direction = 1;
                    if (onGround)
                    {
                        StartCoroutine(dodgeRollMove());
                    }
                }
                else
                {
                    direction = 0;
                    if (onGround && states.current != PlayerStates.AnimStates.atk && states.current != PlayerStates.AnimStates.runatk)
                        states.current = PlayerStates.AnimStates.idle;
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    direction = -1;
                    if (onGround)
                    {
                        StartCoroutine(dodgeRollMove());
                    }
                }
                else
                {
                    direction = 0;
                    if (onGround && states.current != PlayerStates.AnimStates.atk && states.current != PlayerStates.AnimStates.runatk)
                        states.current = PlayerStates.AnimStates.idle;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && !inDodgeRoll)
            {
                //Debug.Log("Jumped");
                jump = true;
            }
        }
        else
            direction = 0;
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        MoveCharacter();
        //Ayyyyyyyyyyyyyy
        //Queue Giorno's theme
    }

    private void MoveCharacter()
    {
        currentSpeed += direction * (accelerationRate * Time.fixedDeltaTime);
        if (currentSpeed >= maxSpeed)
        {
            currentSpeed = maxSpeed;

        }
        else if (Mathf.Abs(currentSpeed) >= maxSpeed)
        {
            currentSpeed = (-1) * maxSpeed;

        }
        //Debug.Log(currentSpeed);
        rb.velocity = new Vector2((direction * walkSpeed) + currentSpeed, jump && jumpIndex <= jumpHeight.Count - 1 ? CalculateJumpSpeed() : rb.velocity.y);
        if (UtilityLibrary.sign(direction) != UtilityLibrary.sign(currentSpeed) && currentSpeed != 0)
        {
            int currentSign = UtilityLibrary.sign(currentSpeed);
            currentSpeed -= UtilityLibrary.sign(currentSpeed) * decelerationRate * Time.fixedDeltaTime;
            if (currentSign != UtilityLibrary.sign(currentSpeed))
            {
                currentSpeed = 0;
            }

   
        }
        
        if (jump)
        {
            jumpIndex++;
        }
       // Debug.Log(jumpIndex);
        jump = false;
    }
    
    private float CalculateJumpSpeed()
    {
        states.current = PlayerStates.AnimStates.jump;
        //Debug.Log(Mathf.Sqrt(2 * GlobalControl.GravityScale * jumpHeight[jumpIndex]));
        return Mathf.Sqrt(2 * rb.gravityScale * jumpHeight[jumpIndex]);
    }

    private bool CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(ground.position, castRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != gameObject)
            {
                onGround = true;
                jumpIndex = 0;
                //Debug.Log("True");
                return true;
            }
        }
        //Debug.Log("False");
        return false;
    }

    void OnGizmosDrawSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ground.position, castRadius);
    }

    public IEnumerator dodgeRollMove()
    {

        //The only reason a coroutine works here is because we're assuming the player cannot stop the dodge roll. Maybe that can be a passive ability?
        if (dodgeRoll)
        {
            float priorRate = accelerationRate;
            inDodgeRoll = true;
            accelerationRate = 3;
            yield return new WaitForSeconds(iframes);//First half of the dodgeroll
            accelerationRate = -3;
            yield return new WaitForSeconds(iframes);//Second half of the dodgeroll
            accelerationRate = priorRate;
            inDodgeRoll = false;
            justFinished = true;
            yield return new WaitForSeconds(0.25f);
            justFinished = false;

            //Now let's calculate distannceee


        }
        else
        {
            yield return new WaitForSeconds(0f);
        }
    }

    private void Load()
    {
        dodgeRoll = GlobalControl.Instance.canDodgeRoll;
        if (dodgeRoll)
        {
            dodgeLength = GlobalControl.Instance.dodgeLength;
        }
        else
        {
            dodgeLength = 0;
        }
        additionalSpeed = GlobalControl.Instance.speed;
        iframes = GlobalControl.Instance.InvincibleFrames;
        decelerationRate = GlobalControl.Instance.decelerationRate;
        if (!GlobalControl.Instance.canAccelerate)
        {
            accelerationRate = 0;
        }
        else
        {

        }
            accelerationRate = GlobalControl.Instance.accelerationRate;
        maxSpeed = GlobalControl.Instance.maxSpeed;
    }
 
}
