using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*Yo this is Noah speaking to himself
     * These are the lines that reference playerattack:
     * hb.GetComponent<PlayerAttack>().Box();
     * hb.GetComponent<PlayerAttack>().Capsule();
     * hb.GetComponent<PlayerAttack>().setDimensions(height, width);
     * hb.GetComponent<PlayerAttack>().setDamage(damage);
     * i.GetComponent<PlayerAttack>().rotateCapsule();,
     * The capsule is horizontal by default
     */

    [SerializeField]
    BoxCollider2D hitBox;
    [SerializeField]
    CapsuleCollider2D hitCapsuleHorizontal;
    [SerializeField]
    CapsuleCollider2D hitCapsuleVertical;

    int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        hitBox.enabled = false;
        hitCapsuleHorizontal.enabled = false;
        hitCapsuleVertical.enabled = false;
        setDimensions(.0001f, .0001f);
    }

    public void Box()
    {
        hitBox.enabled = true;
        hitCapsuleHorizontal.enabled = false;
        hitCapsuleVertical.enabled = false;
    }
    public void Capsule()
    {
        hitCapsuleHorizontal.enabled = true;
        hitBox.enabled = false;
    }
    public void setDimensions(float yDim, float xDim)
    {
        hitBox.size = new Vector2(xDim, yDim);
        hitCapsuleHorizontal.size = new Vector2(xDim, yDim);
        hitCapsuleVertical.size = new Vector2(xDim, yDim);
    }
    public void rotateCapsule()
    {
        if (hitCapsuleVertical.enabled == true)
        {
            hitCapsuleVertical.enabled = false;
            hitCapsuleHorizontal.enabled = true;
        }
        else
        {
            hitCapsuleHorizontal.enabled = false;
            hitCapsuleVertical.enabled = true;
        }
    }
    public void setDamage(int dmg)
    {
        damage = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
