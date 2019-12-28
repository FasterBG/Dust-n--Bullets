using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region Initialization

    [SerializeField]
    private KeyCode reload;

    private int ammoRifle;
    private int ammoPump;
    private int ammoSniper;
    private int ammoSMG;

    private float MAX_SHOOT_COOLDOWN;
    private int MAX_AMMO_IN_FILLER;
    private float RELOAD_TIME;
    private float cooldown;
    private int ammoInFiller;

    private bool checkReload;
    private float reloadTime;

    [SerializeField]
    private WeaponManager.Weapons weaponInInventory;

    private GameObject gameManager;
    private AudioManager audio;
    [SerializeField]
    private PlayerCameraController cameraController;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bulletPre;

    private WeaponManager wm;

    private Animator anim;

    private void Start()
    {
        ammoRifle = 0;
        ammoPump = 0;
        ammoSniper = 0;
        ammoSMG = 0;
        ammoInFiller = 0;
        checkReload = false;
        reloadTime = 0;

        weaponInInventory = WeaponManager.Weapons.NONE;

        gameManager = GameObject.FindGameObjectWithTag("gameManager");
        anim = GetComponent<Animator>();
        wm = gameManager.GetComponent<WeaponManager>();
        audio = gameManager.GetComponent<AudioManager>();
    }

    #endregion 

    private void Update()
    {
        cooldown += Time.deltaTime;
        setAnimation();
        if (Input.GetKeyDown(reload) && ammoInFiller != MAX_AMMO_IN_FILLER)
        {
            checkReload = true;
        }
        Reload(checkReload);
    }

    private void Reload(bool activate)
    {
        if (checkReload && canReload())
        {
            reloadTime += Time.deltaTime;
            if (RELOAD_TIME <= reloadTime)
            {
                audio.Play("Reload");
                checkReload = false;
                reloadTime = 0;
                ammoInFiller = removeAmmoFromInventoryAndIncreaseFiller(weaponInInventory, MAX_AMMO_IN_FILLER - ammoInFiller);
            }
        }
    }

    private void setAnimation()
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

    private void setWeaponStats(WeaponManager.Weapons type)
    {
        if(type == WeaponManager.Weapons.AK)
        {
            MAX_SHOOT_COOLDOWN = wm.AK.shootTimeOut;
            MAX_AMMO_IN_FILLER = wm.AK.fillerAmmo;
            RELOAD_TIME = wm.AK.reloadSpeed;
        }
    }

    public void Shoot()
    {
        if(cooldown > MAX_SHOOT_COOLDOWN && ammoInFiller > 0)
        {
            if(weaponInInventory == WeaponManager.Weapons.AK)
            {
                audio.Play("AK47 Shoot");
            }
            GameObject bullet = Instantiate(bulletPre, firePoint.position, transform.rotation);
            cameraController.Shake();
            BulletController bc = bullet.GetComponent<BulletController>();
            setBulletStats(bc);
            cooldown = 0;
            takeAmmo();
        }else if(ammoInFiller <= 0)
        {
            checkReload = true;
        }
    }

    private void setBulletStats(BulletController bc)
    {
        if (weaponInInventory == WeaponManager.Weapons.AK)
        {
            bc.speed = wm.AK.bulletSpeed;
            bc.damage = wm.AK.damage;
        }
    }

    public void switchWeapon(WeaponManager.Weapons type, int ammoInWeapon)
    {
        audio.Play("Pick-up");
        weaponInInventory = type;
        ammoInFiller = ammoInWeapon;
        setWeaponStats(type);
    }

    #region Methods That Return Info
    public WeaponManager.Weapons giveWeaponInfo()
    {
        return weaponInInventory;
    }

    public int giveBullets(AmmoManager.AmmoTypes type)
    {
        if (type == AmmoManager.AmmoTypes.SMG)
        {
            return ammoSMG;
        }
        else if (type == AmmoManager.AmmoTypes.PUMP)
        {
            return ammoPump;
        }
        else if (type == AmmoManager.AmmoTypes.RIFLE)
        {
            return ammoRifle;
        }
        else if (type == AmmoManager.AmmoTypes.SNIPER)
        {
            return ammoSniper;
        }
        return 0;
    }

    public int giveFillerAmmo()
    {
        return ammoInFiller;
    }

    private bool canReload()
    {
        if(ammoInUse() == AmmoManager.AmmoTypes.RIFLE)
        {
            if(ammoRifle > 0)
            {
                return true;
            }
        }
        if (ammoInUse() == AmmoManager.AmmoTypes.PUMP)
        {
            if (ammoPump > 0)
            {
                return true;
            }
        }
        if (ammoInUse() == AmmoManager.AmmoTypes.SNIPER)
        {
            if (ammoSniper > 0)
            {
                return true;
            }
        }
        if (ammoInUse()== AmmoManager.AmmoTypes.SMG)
        {
            if (ammoSMG > 0)
            {
                return true;
            }
        }
        return false;
    }

    private AmmoManager.AmmoTypes ammoInUse()
    {
        if (weaponInInventory == WeaponManager.Weapons.AK || weaponInInventory == WeaponManager.Weapons.M4)
        {
            return AmmoManager.AmmoTypes.RIFLE;
        }
        return AmmoManager.AmmoTypes.NONE;
    }

    public float giveReloadFiller()
    {
        if(RELOAD_TIME != 0)
        {
            return reloadTime / RELOAD_TIME;
        }
        return 0;
    }
    #endregion

    public void addAmmo(AmmoManager.AmmoTypes type, int Ammo)
    {
        if(type == AmmoManager.AmmoTypes.RIFLE)
        {
            ammoRifle += Ammo;
        }
        else if(type == AmmoManager.AmmoTypes.PUMP)
        {
            ammoPump += Ammo;
        }else if(type == AmmoManager.AmmoTypes.SNIPER)
        {
            ammoSniper += Ammo;
        }else if(type == AmmoManager.AmmoTypes.SMG)
        {
            ammoSMG += Ammo;
        }
    }

    private int removeAmmoFromInventoryAndIncreaseFiller(WeaponManager.Weapons type, int number)
    {
        if(ammoInUse() == AmmoManager.AmmoTypes.RIFLE)
        {
            if(ammoRifle < number)
            {
                int buff = ammoRifle;
                ammoRifle = 0;
                return buff + ammoInFiller;
            }
            else
            {
                ammoRifle -= number;
                return MAX_AMMO_IN_FILLER;
            }
        }
        else if (ammoInUse() == AmmoManager.AmmoTypes.PUMP)
        {
            if (ammoPump < number)
            {
                int buff = ammoPump;
                ammoPump = 0;
                return buff + ammoInFiller;
            }
            else
            {
                ammoPump -= number;
                return MAX_AMMO_IN_FILLER;
            }
        }
        else if (ammoInUse() == AmmoManager.AmmoTypes.SNIPER)
        {
            if (ammoSniper < number)
            {
                int buff = ammoSniper;
                ammoSniper = 0;
                return buff + ammoInFiller;
            }
            else
            {
                ammoSniper -= number;
                return MAX_AMMO_IN_FILLER;
            }
        }
        else if (ammoInUse() == AmmoManager.AmmoTypes.SMG)
        {
            if (ammoSMG < number)
            {
                int buff = ammoSMG;
                ammoSMG = 0;
                return buff + ammoInFiller;
            }
            else
            {
                ammoSMG -= number;
                return MAX_AMMO_IN_FILLER;
            }
        }
        return 0;
    }

    private void takeAmmo()
    {
        ammoInFiller--;
    }
}
