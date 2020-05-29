using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    int damage;
    //the damage this attack will deal

    //This is the object enemies will create on attacks
    //They will be instantiated with damage and will have methods that move them etc as we design enemies
    //For example, a shooting enemy would instantiate one of these and make it do a method that gradually moves it
    //Conversely, a melee enemy would create one right in front of it

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerHurtBox"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            //Hits the player if a hit it detected
        }
    }
}
