using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : Enemy
{
    [SerializeField]
    BoxCollider2D HugBox;

    public int timesPlayerDidIt;
    bool grabbing;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        damage = attack;
    }

    public void GrabPlayer(GameObject playerObject)
    {
        playerObject.GetComponent<Player>().SetStateToStun(attack);
        playerObject.GetComponent<Player>().TakeDamage(attack);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Grabbed Player");
            GrabPlayer(collision.gameObject);
        }
    }

}
