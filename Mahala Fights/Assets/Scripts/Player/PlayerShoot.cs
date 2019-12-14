using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private KeyCode SHOOT;
    [SerializeField]
    private float MAX_SHOOT_COOLDOWN;

    private float cooldownLeft = 0;

    private PlayerWeapon pw;

    private void Update()
    {
        pw = GetComponent<PlayerWeapon>();
        Shoot();
    }

    private void Shoot()
    {
        cooldownLeft += Time.deltaTime;
        if(Input.GetKey(SHOOT) && cooldownLeft > MAX_SHOOT_COOLDOWN && pw.haveAmmo())
        {
            cooldownLeft = 0;
            Instantiate(bullet, firePoint.position, transform.rotation);
            pw.takeAmmo();
        }
    }
}
