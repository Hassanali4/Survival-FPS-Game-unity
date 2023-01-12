using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private GameObject boar_Prefab, cannibal_Prefab;

    public Transform[] boar_Spawn_Points, cannibal_Spawn_Points;

    [SerializeField] private int cannibal_Enemy_Count, boar_Enemy_Count;

    private int initial_Cannibal_Count, initial_Boar_Count;

    public float waite_Before_Spawn_Enemies_Time = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        initial_Cannibal_Count = cannibal_Enemy_Count;
        initial_Boar_Count = boar_Enemy_Count;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    } // start 

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    } // Make Instance

    void SpawnEnemies()
    {
        SpawnCannibals();
        SpawnBoars();
    } // spawn more enemies

    void SpawnCannibals()
    {
        int index = 0;

        for (int i = 0; i < cannibal_Enemy_Count; i++)
        {
            if (index >= cannibal_Spawn_Points.Length)
            { index = 0; }            

            Instantiate(cannibal_Prefab, cannibal_Spawn_Points[index].position, Quaternion.identity);

            index++;
        }

        cannibal_Enemy_Count = 0;
    } // spawn more boars

    void SpawnBoars()
    {
        int index = 0;

        for (int i = 0; i < boar_Enemy_Count; i++)
        {
            if (index >= boar_Spawn_Points.Length)
            { index = 0; }

            Instantiate(boar_Prefab, boar_Spawn_Points[index].position, Quaternion.identity);

            index++;
        }

        boar_Enemy_Count = 0;
    } // spawn more boars 

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waite_Before_Spawn_Enemies_Time);

        SpawnCannibals();
        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");
    } // check to spawn more enemies in the game

    public void EnemyDied(bool cannibal)
    {
        if (cannibal)
        {
            cannibal_Enemy_Count++;

            if (cannibal_Enemy_Count == initial_Cannibal_Count)
            { 
                cannibal_Enemy_Count = initial_Cannibal_Count; 
            }

        }else
        {
            boar_Enemy_Count++;

            if (boar_Enemy_Count == initial_Cannibal_Count)
            {
                boar_Enemy_Count = initial_Cannibal_Count;
            }
        }
    } // Enemy Died
    
    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    } // if player Died it will stop the spawnning of the enemies 

} // class 
