using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponSriptable : ScriptableObject
{
    public new string name;

    public WeaponManager.Weapons type;

    public Sprite image;

    public int bulletSpeed;
    public float damage;
    public float reloadSpeed;
    public float shootTimeOut;
    public int fillerAmmo;
}
