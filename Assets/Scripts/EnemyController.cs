using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    [SerializeField] GameObject shootOut;
    //Oh no it's a shooter mechanic

    // Use this for initialization
/*    void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        if (current == State.moving || current == State.charging)
        {
            transform.Translate(new Vector3(director * Time.deltaTime , YDirector * Time.deltaTime, 0));
            if (!inAttack)
            {
                inAttack = true;
                StartCoroutine(Strike());
            }
        }
        else if (current == State.dead)
        {
            //Set animation
            enabled = false;
        }
        //print(director);
      //  print(current);
        if (inRange)
        {
            transform.Translate(new Vector3(director * -1 * 5, 0));
            inRange = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((current == State.moving || current==State.charging) && collision.gameObject.CompareTag("Player"))
        {
            print("Please");
            
            int PlayerX =(int) collision.transform.position.x;
            int PlayerY =(int) collision.transform.position.y;
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
           // print("Happened 10");
            int PlayerX = (int)collision.transform.position.x;
            if (Mathf.Abs(PlayerX - transform.position.x) <=1.5)
            {
                print("Possible");
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
  
        if ( collision.gameObject.CompareTag("Player"))
        {
            print("Why?");
            current = State.moving;
            //director = 1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == wallCollider)
        {
            print("Happened 2");
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
            print("Happened 4");
           // director = 1;
            YDirector = 0; 
        }

        if (current == State.attacking && collision.gameObject.CompareTag("Player"))
        {
            //Access the player script and lower health
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print("Happened 0");
          if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == groundCollider) { 
      //  if (collision.gameObject.CompareTag("Ground")){ 
          
    
            //director = 0;
            YDirector = -1;
        }   

    }

    IEnumerator Strike()
    {
        yield return new WaitForSeconds(1.5f);
        float posX = transform.position.x;
        float posY = transform.position.y;
        GameObject temp = Instantiate(shootOut, new Vector3(posX, posY, 0),Quaternion.identity);
        director *= -1;
        
        temp.GetComponent<ProjectileScript>().SetSpeed(2 *  -director);
        inAttack = false;
    }
}
