using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health, attack;
    protected int director, YDirector;
    protected bool inAttack;
    public bool inRange;

    [SerializeField] protected BoxCollider2D groundCollider;
    [SerializeField] protected BoxCollider2D wallCollider;
    [SerializeField] protected SpriteRenderer sr;

    protected enum State
    {
        moving, charging, attacking, dead
    }
    protected State current;

    // Use this for initialization
    void Start()
    {
        director = 1;
        YDirector = 0;
        inAttack = false;
        inRange = false;
        current = State.moving;
    }

    public void TakeDamage(float damage) //true if this enemy dies
    {
        print("Tooketh");
        health -= damage;
        if(health <= 0)
        {
            Player.Instance.enemiesKilled += 1;
            current = State.dead;
            Destroy(this, 1f); //Accounts for the death animation
            Destroy(this.gameObject, 1f);
        }
    }

    public void DeathByRunning()
    {
        current = State.dead;
        GameObject.Destroy(this.gameObject, 1f);
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (current == State.moving && collision.gameObject.CompareTag("Player"))
        {
            int PlayerX = (int)collision.transform.position.x;
            int PlayerY = (int)collision.transform.position.y;
            if (PlayerX > transform.position.x)
            {
                director = 2;
                sr.flipX = false;
            }
            else
            {
                director = -2;
                sr.flipX = true;
            }

            current = State.charging;
        }
        else if (current == State.charging && collision.gameObject.CompareTag("Player"))
        {
            int PlayerX = (int)collision.transform.position.x;
            if (Mathf.Abs(PlayerX - transform.position.x) <= 1.5)
            {
                if (PlayerX > transform.position.x)
                {
                    director = 1;
                    sr.flipX = false;
                }
                else
                {
                    director = -1;
                    sr.flipX = true;
                }
                //Start a coroutine
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((current == State.charging || current == State.attacking) && collision.gameObject.CompareTag("Player"))
        {
            current = State.moving;
            director = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == wallCollider)
        {
            director = director * -1;
            if (director > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
        else if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == groundCollider)
        {
            director = 1;
            YDirector = 0;
        }
        if (current == State.attacking && collision.gameObject.CompareTag("Player"))
        {
            inAttack = true;
            StartCoroutine("strike");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == groundCollider)
        {
            director = 0;
            YDirector = -1;
        }

    }*/

    /*IEnumerator Strike()
    {
        yield return new WaitForSeconds(1.5f);
        if (inAttack)
        {
            Player.Instance.takeDamage(attack);
            current = State.charging;
            director = director * -1;
            inAttack = false;
        }
    }*/
}
