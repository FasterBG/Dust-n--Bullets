using UnityEngine;

public class DestructibleWall : MonoBehaviour, IDamageble
{
    [SerializeField]
    private float MIN_SCALE;
    [SerializeField]
    private float MAX_HEALTH;

    private float health;
    private Vector2 startScale;
    private float multiplayer = 1;

    private void Start()
    {
        startScale = new Vector2(transform.localScale.x, transform.localScale.y);
        health = MAX_HEALTH;
    }


    private void Update()
    {
        transform.localScale = new Vector3(startScale.x * multiplayer, startScale.y * multiplayer, transform.localScale.z);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        multiplayer = health / MAX_HEALTH;
        if(multiplayer < MIN_SCALE)
        {
            multiplayer = MIN_SCALE;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
