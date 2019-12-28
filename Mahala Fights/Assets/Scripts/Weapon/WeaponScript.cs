using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    private WeaponSriptable weapon;

    private int ammo;

    private void Start()
    {
        ammo = weapon.fillerAmmo;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            PlayerWeapon pw = collision.GetComponent<PlayerWeapon>();
            if (Input.GetKeyDown(pc.pick) || pw.giveWeaponInfo() == WeaponManager.Weapons.NONE)
            {
                pw.switchWeapon(weapon.type, ammo);
                Destroy(gameObject);
            }
        }
    }
}
