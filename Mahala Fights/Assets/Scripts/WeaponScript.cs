using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    private WeaponSriptable weapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerWeapon pw = collision.GetComponent<PlayerWeapon>();
            pw.switchWeapon(weapon.type);
            Destroy(gameObject);
        }
    }
}
