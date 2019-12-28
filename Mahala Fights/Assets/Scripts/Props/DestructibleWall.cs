using UnityEngine;

public class DestructibleWall : MonoBehaviour, IDamageble
{
    [SerializeField]
    private float MIN_SCALE;
    [SerializeField]
    private float MAX_HEALTH;

    private float scale;
    private float health;
    
    private void Start()
    {
        scale = transform.localScale.x;
        health = MAX_HEALTH;
    }

    private void Update()
    {
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        scale = health / MAX_HEALTH;
        if(scale < MIN_SCALE)
        {
            scale = MIN_SCALE;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
