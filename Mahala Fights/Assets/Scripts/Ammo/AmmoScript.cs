using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    [SerializeField]
    private int numberOfAmmo;
    [SerializeField]
    private AmmoManager.AmmoTypes type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerWeapon pw = collision.GetComponent<PlayerWeapon>();
            pw.addAmmo(type, numberOfAmmo);
            Destroy(gameObject);
        }
    }
}
