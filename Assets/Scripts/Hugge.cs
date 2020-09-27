using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugge : Enemy
{
    [SerializeField]
    BoxCollider2D HugBox;

    public int timesPlayerDidIt;
    bool grabbing;
    int damage;

    // Start is called before the first frame update
    void Start()
    {
        grabbing = true;
    }

    // Update is called once per frame
    void Update()
    {
        damage = attack;
    }

    public void GrabPlayer(GameObject playerObject)
    {
        print("grabbed");
        playerObject.GetComponent<PlayerStates>().SetStateToStun();
        //playerObject.GetComponent<Player>().TakeDamage(attack);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (grabbing)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                print("Grabbed Player");

                StartCoroutine(grabTime());
                GrabPlayer(collision.gameObject);

            }
        }
    }
    IEnumerator grabTime()
    {
        grabbing = false;
        yield return new WaitForSeconds(3f);
        grabbing = true;

    }

}
