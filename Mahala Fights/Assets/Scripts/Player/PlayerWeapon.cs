using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private int numberOfAmmoRifle;
    private int numberOfAmmoPump;
    private int numberOfAmmoSniper;
    private int numberOfAmmoSMG;

    [SerializeField]
    private WeaponManager.Weapons weaponInInventory;

    private Animator anim;

    private void Start()
    {
        numberOfAmmoRifle = 0;
        numberOfAmmoPump = 0;
        numberOfAmmoSniper = 0;
        numberOfAmmoSMG = 0;

        weaponInInventory = WeaponManager.Weapons.NONE;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        chooseAnimation();
    }

    private void chooseAnimation()
    {

        if (weaponInInventory == WeaponManager.Weapons.NONE)
        {
            anim.SetInteger("Weapon", 0);
        }
        else if (weaponInInventory == WeaponManager.Weapons.AK)
        {
            anim.SetInteger("Weapon", 1);
        }
    }

    public bool haveAmmo()
    {
        if ((weaponInInventory == WeaponManager.Weapons.AK || weaponInInventory == WeaponManager.Weapons.M4) && numberOfAmmoRifle > 0)
        {
            return true;
        }
        return false;
    }

    public void switchWeapon(WeaponManager.Weapons type)
    {
        weaponInInventory = type;
    }

    public void addAmmo(AmmoManager.AmmoTypes type, int numberOfAmmo)
    {
        if(type == AmmoManager.AmmoTypes.RIFLE)
        {
            numberOfAmmoRifle += numberOfAmmo;
        }
        else if(type == AmmoManager.AmmoTypes.PUMP)
        {
            numberOfAmmoPump += numberOfAmmo;
        }else if(type == AmmoManager.AmmoTypes.SNIPER)
        {
            numberOfAmmoSniper += numberOfAmmo;
        }else if(type == AmmoManager.AmmoTypes.SMG)
        {
            numberOfAmmoSMG += numberOfAmmo;
        }
    }

    public void takeAmmo()
    {
        if (weaponInInventory == WeaponManager.Weapons.AK || weaponInInventory == WeaponManager.Weapons.M4)
        {
            numberOfAmmoRifle --;
        }
    }
}
