using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageble
{
    [SerializeField]
    private float MAX_HEALTH;

    private float health;

    [SerializeField]
    private float speed;

    [SerializeField]
    private KeyCode up;
    [SerializeField]
    private KeyCode left;
    [SerializeField]
    private KeyCode down;
    [SerializeField]
    private KeyCode right;

    [SerializeField]
    private Rigidbody2D rb;

    private void Start()
    {
        health = MAX_HEALTH;
    }

    void Update()
    {
        Debug.Log(health);
        Move();
    }

    void Move()
    {
        Vector2 _velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        if (Input.GetKey(up))
        {
            _velocity = new Vector2(_velocity.x, speed);
        }
        else if (Input.GetKey(down))
        {
            _velocity = new Vector2(_velocity.x, -speed);
        }
        else
        {
            _velocity = new Vector2(_velocity.x, 0);
        }
        if (Input.GetKey(right))
        {
            _velocity = new Vector2(speed, _velocity.y);
        }
        else if (Input.GetKey(left))
        {
            _velocity = new Vector2(-speed, _velocity.y);
        }
        else
        {
            _velocity = new Vector2(0, _velocity.y);
        }
        rb.velocity = _velocity;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
