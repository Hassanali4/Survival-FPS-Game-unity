using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject boar_Prefab, cannibal_Prefab;


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void MakeInstance()
    {
        
    }
} // class
