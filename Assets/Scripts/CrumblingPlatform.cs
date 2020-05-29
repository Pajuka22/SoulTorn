using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    // Start is called before the first frame update


    BoxCollider2D collider2;
    Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
        collider2 = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Will crumble");
            StartCoroutine(crumble());
        }
    }

    IEnumerator crumble()
    {
        ani.SetBool("Crumbling", true);
        yield return new WaitForSeconds(2f);
        ani.SetBool("Crumbling", false);
        ani.SetBool("Uncrumbling", true);
        collider2.enabled = false;
        yield return new WaitForSeconds(3f);
        collider2.enabled = true;
        ani.SetBool("Uncrumbling", false);
    }
}
