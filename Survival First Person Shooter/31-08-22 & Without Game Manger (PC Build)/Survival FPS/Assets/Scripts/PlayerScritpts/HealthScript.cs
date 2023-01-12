using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent nevAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    public bool Is_Player, Is_Boar, Is_Cannibal;
    public bool Is_Dead;

    private EnemyAudio enemyAudio;

    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Awake()
    {
        if (Is_Boar || Is_Cannibal)
        {
            enemy_Anim = GetComponent<EnemyAnimator>();

            nevAgent = GetComponent<NavMeshAgent>();

            enemy_Controller = GetComponent<EnemyController>();

            enemyAudio = GetComponentInChildren<EnemyAudio>();

            //get the enemy audio
        }

        if (Is_Player)
        {
            //show the Stats(display the health UI value)
            playerStats = GetComponent<PlayerStats>();
        }

        //playe the audio source
    }

    public void ApplyDamage(float damage)
    {
        if (Is_Dead)//if we died don't execute the rest of the code
            return;

        health -= damage;

        if (Is_Player)
        {
            //show the stats(dispaly the health UI value)
            playerStats.Display_Health_Stats(health);
        }

        if (Is_Boar || Is_Cannibal)
        {
            if (enemy_Controller.Enemy_State == EnemyState.PATROL)
            {
                enemy_Controller.chase_Distance = 50f;
            }
        }
        
        if (health <= 0f)
            {
                PlayerDied();
                Is_Dead = true;
            }
        
    }//apply damage

    void PlayerDied()
    {
        if (Is_Cannibal)
        {//This function is Only Because we don't have the Death Animation for the Cannibal Type Enemy.
         //Other wise the Death will be as same as of Boar Type Enemy and it will be universal for all the other type of enemies if using this Projects Scripts

            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.position * 50f);

            enemy_Controller.enabled = false;
            nevAgent.enabled = false;
            enemy_Anim.enabled = false;

            //start Coroutine
            StartCoroutine(DeadSound());

            //EnemyManager spawn more enemies

        }

        if (Is_Boar)
        {
            nevAgent.velocity = Vector3.zero;
            nevAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();

            //start coroutine 
            StartCoroutine(DeadSound());

            //EnemyManager spawn more enemies

        }

        if (Is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            for (int i = 0;i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            //call enemy Manger to stop spawning enemies because we have died

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }

        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartTheGame", 3f);
        }else
        {
            Invoke("TurnOffGameObject", 3f);
        }

    } // player Died

    void RestartTheGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FPS");
    } // restart the Game
    
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    } // restart the Game

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }

} // class




























































