using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : Enemy
{
    // Use this for initialization
    /*    void Start()
        {

        }
    */
    // Update is called once per frame




    void Update()
    {
        if (current == State.moving || current == State.charging)
        {
            transform.Translate(new Vector3(director * Time.deltaTime, YDirector * Time.deltaTime, 0));
        }
        else if (current == State.dead)
        {
            //Set animation
            enabled = false;
        }
        if (inRange)
        {
            transform.Translate(new Vector3(director * -1 * 5, 0));
            inRange = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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
            director *= -1;
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

    }

    IEnumerator Strike()
    {
        yield return new WaitForSeconds(1.5f);
        if (inAttack)
        {
            Player.Instance.TakeDamage(attack);
            current = State.charging;
            director *= -1;
            inAttack = false;
        }
    }
}
