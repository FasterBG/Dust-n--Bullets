using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private KeyCode SHOOT;

    private PlayerWeapon pw;

    private void Update()
    {
        pw = GetComponent<PlayerWeapon>();
        CheckForShoot();
    }

    private void CheckForShoot()
    {
        if(Input.GetKey(SHOOT))
        {
            pw.Shoot();
        }
    }
}
