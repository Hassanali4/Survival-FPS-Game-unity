 using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public enum WeaponAim
{
    NON,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NON
}

public class WeaponHandler : MonoBehaviour
{
    private Animator anim;

    public WeaponAim weapon_Aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound , reload_Sound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attack_Point;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //shootSound,reload_Sound = GetComponent<AudioSource>();       
    }

    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    public void Turn_On_MuzzelFlash()
    {
        muzzleFlash.SetActive(true);
    }
    public void Turn_Off_MuzzelFlash()
    {
        muzzleFlash.SetActive(false);
    }

    public void Play_ShootSound()
    {
        shootSound.Play();
    }
    public void Play_ReloudSound()
    {
        reload_Sound.Play();
    }

    public void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }
    public void Turn_Off_AttackPoint()
    {
        attack_Point.SetActive(false);
    }



}// class






























































