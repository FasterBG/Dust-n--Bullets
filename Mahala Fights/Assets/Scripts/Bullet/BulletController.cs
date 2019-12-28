using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    public float damage;
    [SerializeField]
    private GameObject impactEF;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            pc.TakeDamage(damage);
            Instantiate(impactEF, transform.position, transform.rotation);
            Destroy(gameObject);
        }else if(collision.tag == "destructible")
        {
            IDamageble idmg = collision.GetComponent<IDamageble>();
            idmg.TakeDamage(damage);
            Instantiate(impactEF, transform.position, transform.rotation);
            Destroy(gameObject);
        }else if(collision.tag == "wall")
        {
            Instantiate(impactEF, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
