using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int selected_Weapon_Index;

    // Start is called before the first frame update
    void Start()
    {
        selected_Weapon_Index = 0;
        weapons[selected_Weapon_Index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }if (Input.GetKeyDown(KeyCode.Alpha6))
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

























































