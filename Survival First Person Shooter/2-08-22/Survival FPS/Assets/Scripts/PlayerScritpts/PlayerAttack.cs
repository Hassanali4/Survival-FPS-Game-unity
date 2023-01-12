using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private WeaponManager weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;
    private bool zoom;

    private Camera mainCamera;

    private GameObject crosshair;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private GameObject arrow_Bow_StartPosition;
    // values for the mouse UI shoot
    public Button_Sprint shoot;
    public Button_Sprint Is_Aim;
    private int canShoot;

    // Start is called before the first frame update
    void Awake()
    {
        weapon_Manager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        //if we have assault Rifle 
        if (weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            // if we press and hold left mouse click AND
            // if Time is greater than the nextTimeToFire
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();
            }
        }
        else//if we have a regular weapon that shoots once
        {

            //if (Input.GetMouseButtonDown(0)) // This is for keyboard Input
            if (!shoot.isPressed && canShoot > 0 )
            {
                canShoot = 0;
                    return;
            }
            if (shoot.isPressed && canShoot == 0)
            {
                canShoot++;

             //   Debug.Log(shoot.isPressed);
            
                //Axe handle
                if (weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                // handle shoot
                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                    //BulletFired();
                    BulletFired();
                }
                else
                {
                    //if we have Arrow or Spear

                    if(true)
                    {
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                        if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {
                            //throw Arrow
                            //ThrowArrowOrSpear(true);
                            return;
                        }
                        else if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            //throw spear
                         //   ThrowArrowOrSpear(false);
                            return;
                        }
                    }
                    

                }//else

            } // if input get mouse 0

        }//else 

    }//weapon Shoot

    void ZoomInAndOut()
    {
        //if we are going to aim our camera on the weapon
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM)
        {
            //if we press and hold the mouse button
            if (Input.GetMouseButtonDown(1) || Is_Aim.isPressed)
            {
                if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.NON)
                    zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIMATOIN);
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIMATOIN);
                crosshair.gameObject.SetActive(false);
            }
            //if we release the mouse button
            if (Input.GetMouseButtonUp(1) || !Is_Aim.isPressed)
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIMATOIN);
                crosshair.gameObject.SetActive(true);
            }
        }// if we need to zoom the weapon

        //if we are aiming weapons other then Bow & Arrow
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.SELF_AIM)
        {
            
            if (Input.GetMouseButtonDown(1))
            {// if we press and Hold Right mouse button on Self_Aim Type weapons
                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);
                is_Aiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {// if we release Right mouse button on Self_Aim Type weapons
                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;
            }
        }
    }//zoom in and out

   public void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {//if we are throwing arrow
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Bow_StartPosition.transform.position;

            arrow.GetComponent<ArrowBowScript>().Launch(mainCamera);
        }
        else
        {//if we are throwing arrow
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Bow_StartPosition.transform.position;

            spear.GetComponent<ArrowBowScript>().Launch(mainCamera);
        }
    }//throw arrow or Spear

    public void BulletFired()
    {

        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
                //print("Enemy Hit");
            }
        }

    }// Bullet fired

} // class 
