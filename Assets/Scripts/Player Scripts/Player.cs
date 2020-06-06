using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField]
    Collider2D hitCollider;
    //The basic hitbox for the player attack. Can be modified as necessary if power ups call for it.

    [SerializeField]
    Collider2D hitColliderVertical;

    [SerializeField]
    GameObject hitSprite;

    [SerializeField]
    GameObject hitSpriteVertical;

    [SerializeField]
    GameObject atkPrefab;
    
    public int atkLag;
    public bool isAttacking;
    //Prevents the player from being able to execute multiple attacks instantly, adds end lag to attack

    //Game variables
    public int health; //Health, self explanatory
    public int attack; //Just a placeholder for strength
    public GameObject[] passives;
    public GameObject[] actives;
    //The skills the player has equipped

    public int enemiesKilled = 0;

    //Invincibility Frames
    public bool AbleToGetHurt;
    public float IFrames;

    //Upgrade Variables
    public bool AbleToUse;
    public bool CanDodge;
    public bool CanDodgeRoll;

    //Hugger Based Variables
    public int timesJumped;
    Coroutine currentGrab;
    public bool stunned; //Since the stun state doesn't work

    public SpriteRenderer spriteR;
    public PlayerStates states;
    // Start is called before the first frame update

    //Level stuff
    public int levelAt;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        AbleToGetHurt = true;
        levelAt = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        LoadPlayer();
        timesJumped = 0;
        //Allows us to use our own physics system but still have the player interact with collisions

        spriteR = GetComponent<SpriteRenderer>();
        spriteR.flipX = false;
        //Starts the player off facing right

        hitCollider.enabled = false;
        hitColliderVertical.enabled = false;
        hitSprite.GetComponent<SpriteRenderer>().enabled = false;
        hitSpriteVertical.GetComponent<SpriteRenderer>().enabled = false;
        isAttacking = false;
        AbleToGetHurt = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (states.current != PlayerStates.AnimStates.stun && states.current != PlayerStates.AnimStates.death && !isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                print("attacking");
                isAttacking = true;
                states.current = PlayerStates.AnimStates.atk;
                atkLag = GlobalControl.Instance.atkLag;
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    hitColliderVertical.offset = new Vector2(0, -0.3f);
                    hitColliderVertical.enabled = true;
                    hitSpriteVertical.transform.localPosition = new Vector2(0, -0.3f);
                    hitSpriteVertical.GetComponent<SpriteRenderer>().enabled = true;
                    
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    hitColliderVertical.offset = new Vector2(0, 0.3f);
                    hitColliderVertical.enabled = true;
                    hitSpriteVertical.transform.localPosition = new Vector2(0, 0.3f);
                    hitSpriteVertical.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    hitCollider.enabled = true;
                    hitSprite.GetComponent<SpriteRenderer>().enabled = true;
                }
                
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(ChainAttack());
            }
        }
        //Makes sure the player cannot act while dead
        else if (isAttacking)
        {
            if (atkLag > 0)
                atkLag--;
            else
            {
                isAttacking = false;
                states.current = states.current == PlayerStates.AnimStates.runatk ? PlayerStates.AnimStates.run : PlayerStates.AnimStates.idle;
                hitCollider.enabled = false;
                hitSprite.GetComponent<SpriteRenderer>().enabled = false;
                hitColliderVertical.enabled = false;
                hitSpriteVertical.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        /*if (Input.GetKeyDown(KeyCode.LeftShift) && CanDodge && CanDodgeRoll)
        {
            DodgeRoll();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && CanDodge)
        {
            Dodge();
        }*/

/*        if (timesJumped == 3)
        {
            stunned = false;
            cState = State.Idle;
            timesJumped = 0;
            print("freedom");
            StartCoroutine(InvincibleFrames());
            //StopCoroutine(currentGrab);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == hitCollider && collision.gameObject.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(attack);
                isAttacking = false;
            }
        }
        if (collision.otherCollider == hitCollider && collision.gameObject.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(attack);
                isAttacking = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (AbleToUse)
            {
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    collision.gameObject.GetComponent<Enemy>().inRange = true;
                }
            }
        }
    }

    public void SavePlayer()
    {
        GlobalControl.Instance.health = health;
    }

    void LoadPlayer()
    {
        attack = GlobalControl.Instance.atkDamage;
        atkLag = 0;
        health = GlobalControl.Instance.health;
        actives = GlobalControl.Instance.actives;
        passives = GlobalControl.Instance.passives;
        Vector2 playerPos;
        playerPos.x = SaveSystem.LoadPlayer(GlobalControl.Instance.saveFile).playerLocation[0];
        playerPos.y = SaveSystem.LoadPlayer(GlobalControl.Instance.saveFile).playerLocation[1];
        transform.position = playerPos;


    }

    public void TakeDamage(int damage)
    {
        if (AbleToGetHurt)
        {
            StartCoroutine(InvincibleFrames());
            //print("Hit 2!");
            health -= damage;
        }
        if (health <= 0)
        {
            states.current = PlayerStates.AnimStates.death;
            StartCoroutine(GlobalControl.Instance.Die());
        }
        //The player takes damage equal to the attack of the enemy
        //If the player reaches 0 health, they are sent back to the level select
    }


    public GameObject makeAttackBox(int damage, bool isBox, float height, float width, GameObject atk, float Xspacing, float Yspacing)
    {
        float Xplace = 0;
        //FlipX is false means facing right 
        if (spriteR.flipX == false)
        {
            Xplace = this.transform.position.x + Xspacing;
        }
        else
        {
            Xplace = this.transform.position.x - Xspacing;
        }
        //How far away from the player the attack is, horizontally

        var spawnVec = new Vector3(Xplace, this.transform.position.y+Yspacing, this.transform.position.z);

        print("spawning at " + spawnVec);
        GameObject hb = Instantiate(atk, spawnVec, Quaternion.identity);
            hb.GetComponent<PlayerAttack>().setDimensions(height, width);
            if (isBox)
            {
                hb.GetComponent<PlayerAttack>().Box();
            }
            else
            {
                hb.GetComponent<PlayerAttack>().Capsule();
            }
        hb.GetComponent<PlayerAttack>().setDamage(damage);
        return hb;
    }
    /*makeAttackBox (this one is a D00ZY)
     * float damage: the damage for the attack to deal
     * bool isBox: should be true if the hitbox for this attack is a box, false if it's a capsule
     * int height: the height of the desired hitbox
     * int width: the width of the desired hitbox
     * GameObject atk: the attack hitbox prefab. Should be the same one every time.
     * float Xspacing: How far in front of the player the attack is. Can be negative.
     * float Yspacing: How far above the player the attack is. Will probably often be Zero.
     * Worth noting these values can be negative for attacks in front of / behind the player
     * 
     * Creates the attack hitbox with specified properties.
     * Attacks should be a series of iterations of this method, one for each hitbox.
     * Returns the created hitbox, in case you want to, say, destroy it.
     */

    public GameObject makeAttackBox(int damage, bool isBox, float height, float width, GameObject atk, float Xspacing, float Yspacing, bool isVertical)
    {
        GameObject i = makeAttackBox(damage, isBox, height, width, atk, Xspacing, Yspacing);
        if(isVertical == true)
        {
            i.GetComponent<PlayerAttack>().rotateCapsule();
        }
        return i;
    }
    //same method but if you want a vertical capsule you can add a true to the end
    //Not necessary for a horizontal capsule

    IEnumerator ChainAttack()
    {
        isAttacking = true;

        print("we out here");

        GameObject hb1 = makeAttackBox(10, false, .4f, .25f, atkPrefab, .342f, 0);
        print("1");
        hb1.GetComponent<PlayerAttack>().Capsule();
        print("2");
        yield return new WaitForSeconds(.1f);
        print("3");
        Destroy(hb1);

        print("block executed");

        GameObject hb2 = makeAttackBox(10, false, .6f, .25f, atkPrefab, .519f, 0);
        hb2.GetComponent<PlayerAttack>().Capsule();
        yield return new WaitForSeconds(.03f);
        Destroy(hb2);

        print("block executed");

        GameObject hb3 = makeAttackBox(10, false, .8f, .25f, atkPrefab, .6f, 0);
        hb3.GetComponent<PlayerAttack>().Capsule();
        yield return new WaitForSeconds(.03f);
        Destroy(hb3);

        print("block executed");

        GameObject hb4 = makeAttackBox(10, false, 1f, .25f, atkPrefab, .75f, 0);
        hb4.GetComponent<PlayerAttack>().Capsule();
        yield return new WaitForSeconds(.03f);
        Destroy(hb4);

        GameObject hb5 = makeAttackBox(10, false, 1.2f, .25f, atkPrefab, .842f, 0);
        hb5.GetComponent<PlayerAttack>().Capsule();
        yield return new WaitForSeconds(.03f);
        Destroy(hb5);

        GameObject hb6 = makeAttackBox(10, false, 1.4f, .25f, atkPrefab, .919f, 0);
        hb6.GetComponent<PlayerAttack>().Capsule();
        yield return new WaitForSeconds(.03f);
        Destroy(hb6);

        GameObject hb7 = makeAttackBox(10, false, 1.6f, .25f, atkPrefab, .75f, 0);
        hb7.GetComponent<PlayerAttack>().Capsule();
        yield return new WaitForSeconds(.05f);
        Destroy(hb7);
        
        GameObject hb8 = makeAttackBox(10, false, 1.8f, .25f, atkPrefab, 1.01f, 0);
        hb8.GetComponent<PlayerAttack>().Capsule();
        yield return new WaitForSeconds(.05f);
        GameObject sweetSpot = makeAttackBox(10, false, .4f, .4f, atkPrefab, 1.89f, 0);
        sweetSpot.GetComponent<PlayerAttack>().Capsule();
        yield return new WaitForSeconds(.1f);
        Destroy(hb8);
        Destroy(sweetSpot);

        isAttacking = false;
    }
    /*public void Dodge()
    {
        if (CanDodge)
        {
            StartCoroutine(InvincibleFrames());
        }
    }
    public void DodgeRoll()
    {
        if (CanDodge && CanDodgeRoll)
        {
            if (!spriteR.flipX)
            {
                rb.AddForce(new Vector2(10, 0));

            }
            else
            {
                rb.AddForce(new Vector2(-10, 0));
            }
            StartCoroutine(InvincibleFrames());
        }
    }*/
    IEnumerator InvincibleFrames()
    {
        AbleToGetHurt = false;
        yield return new WaitForSeconds(1.5f);
        AbleToGetHurt = true;
    }
    //Coroutine to do the chain attack I brainstormed

    //Hugger stuff section
    IEnumerator Grappled(int damage)
    {
        if (AbleToGetHurt)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(damage);
            if (states.current == PlayerStates.AnimStates.stun)
            {
                currentGrab = StartCoroutine(Grappled(damage));
            }
        }
    }

    public void SetStateToStun(int damage)
    {
        if (AbleToGetHurt)
        {
            print("GotStunned");
            states.current = PlayerStates.AnimStates.stun;
            currentGrab = StartCoroutine(Grappled(damage));
        }
    }

    //Save file stuff
}
