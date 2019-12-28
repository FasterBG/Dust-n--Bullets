using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Image healthbar;
    [SerializeField]
    private Image reloadBar;
    [SerializeField]
    private TextMeshProUGUI SMGAmmo;
    [SerializeField]
    private TextMeshProUGUI pumpAmmo;
    [SerializeField]
    private TextMeshProUGUI rifleAmmo;
    [SerializeField]
    private TextMeshProUGUI sniperAmmo;
    [SerializeField]
    private TextMeshProUGUI fillerAmmo;

    [SerializeField]
    private PlayerController pc;
    [SerializeField]
    private PlayerWeapon pw;

    [SerializeField]
    private float fillAnimationSpeed;

    private float fillAmoundBar = 1;

    private void Update()
    {
        UpdateAmmo();
        UpdateHealtBar();
        UpdateReloadBar();
    }

    private void UpdateReloadBar()
    {
        reloadBar.fillAmount = pw.giveReloadFiller();
    }

    private void UpdateAmmo()
    {
        sniperAmmo.SetText(pw.giveBullets(AmmoManager.AmmoTypes.SNIPER).ToString());
        rifleAmmo.SetText(pw.giveBullets(AmmoManager.AmmoTypes.RIFLE).ToString());
        SMGAmmo.SetText(pw.giveBullets(AmmoManager.AmmoTypes.SMG).ToString());
        pumpAmmo.SetText(pw.giveBullets(AmmoManager.AmmoTypes.PUMP).ToString());
        fillerAmmo.SetText(pw.giveFillerAmmo().ToString());
    }

    private void UpdateHealtBar()
    {
        if(healthbar.fillAmount != pc.returnHealthFormMaxHealth())
        {
            SmoothHealthBar();
        }
        fillAmoundBar = healthbar.fillAmount;
    }

    private void SmoothHealthBar()
    {
        if(fillAmoundBar > pc.returnHealthFormMaxHealth())
        {
            healthbar.fillAmount -= fillAnimationSpeed;
        }else if(fillAmoundBar < pc.returnHealthFormMaxHealth())
        {
            healthbar.fillAmount += fillAnimationSpeed;
        }
    }
}
