using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int attack;
    public float speed;
    void Start()
    {
        //Set damage here based off of the floor you're on

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0));

    }

    public void SetDamage(int damage)
    {
        attack = damage;
    }
    public void SetSpeed(int velocity)
    {
        speed = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collided");
        if (collision.gameObject.CompareTag("Player"))
        {
            //Access Player Class
            print("Hit!");
            collision.gameObject.GetComponent<Player>().TakeDamage(attack);

            Destroy(gameObject);

        }
       // Destroy(this.gameObject);

    }

}
