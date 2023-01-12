using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int selected_Weapon_Index;

    //variables for Mobile UI
    public Button_Sprint Is_Axe;
    public Button_Sprint Is_Revolver;
    public Button_Sprint Is_Shotgun;
    public Button_Sprint Is_Rifle;
    public Button_Sprint Is_Spear;
    public Button_Sprint Is_Bow;

    //to stop zoomin animation on other weapons
    private Animator zoomCameraAnim;

    // Start is called before the first frame update
    void Start()
    {
        selected_Weapon_Index = 0;
        weapons[selected_Weapon_Index].gameObject.SetActive(true);
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) // Kyeboard Bindings
        if (Input.GetKeyDown(KeyCode.Alpha1) || Is_Axe.isPressed) // Mobile Bindings
        {
            TurnOnSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Is_Revolver.isPressed)
        {
            TurnOnSelectedWeapon(1);
        }if (Input.GetKeyDown(KeyCode.Alpha3) || Is_Shotgun.isPressed)
        {
            TurnOnSelectedWeapon(2);
        }if (Input.GetKeyDown(KeyCode.Alpha4) || Is_Rifle.isPressed)
        {
            TurnOnSelectedWeapon(3);
        }if (Input.GetKeyDown(KeyCode.Alpha5) || Is_Spear.isPressed)
        {
            zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIMATOIN);
            TurnOnSelectedWeapon(4);
        }if (Input.GetKeyDown(KeyCode.Alpha6) || Is_Bow.isPressed)
        {
            TurnOnSelectedWeapon(5);
        }
    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        if (weaponIndex == selected_Weapon_Index)
        {
            return;
        }
        //turn off Current Weapon
        weapons[selected_Weapon_Index].gameObject.SetActive(false);
        //turn on Selected Weapon
        weapons[weaponIndex].gameObject.SetActive(true);
        //Change the Selected Weapon Index with Current Index
        selected_Weapon_Index = weaponIndex;
    }   

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[selected_Weapon_Index];
    }

}// Class

























































