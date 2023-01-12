using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    public GameObject attack_Point;

    private EnemyAudio enemy_Audio;

    private Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        enemy_Audio = GetComponentInChildren<EnemyAudio>();
    }

    private void Start()
    {
        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        //when the enemy first get to the player
        //attack right away
        attack_Timer = wait_Before_Attack;

        //memorize the value of chase distance
        //so that we can put it back
        current_Chase_Distance = chase_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemy_State == EnemyState.CHASE)
        {
            Chase();
        }
        if (enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        //tell nav agent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        //add to the patrol timer
        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();

            patrol_Timer = 0;
        }

        if (navAgent.velocity.sqrMagnitude > 0f)
        {
            enemy_Anim.Walk(true);
        }
        else
        {
            enemy_Anim.Walk(false);
        }

        //test the distance between player and the enemy
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
            enemy_Anim.Walk(false);

            enemy_State = EnemyState.CHASE;

            Chase();

            //play spotted audio
            enemy_Audio.Play_ScreamSound();
        }


    } // patrol 

    void Chase()
    {
        // enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        //set the players position as the destination position
        //because enemy is chasing(running towards) the player
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);
        }

        //if the distance between the player and the enemy is less then the attack distance
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            //stop animations
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);

            enemy_State = EnemyState.ATTACK;


            //reset the chase distance to the previouse 
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }

        }
        else if (Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            //player started to run away from enemy


            //stop running
            enemy_Anim.Run(false);

            enemy_State = EnemyState.PATROL;

            // reset the patrol timer so that function 
            // can calcualte the new patrol location right away

            patrol_Timer = patrol_For_This_Time;

            if (chase_Distance != current_Chase_Distance)
                chase_Distance = current_Chase_Distance;
            
        }



    } // chase 

    void Attack()
    {

        //start our enemy completly
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        //adding in the attack time after each frame so it surpass the chase_After_Attack_Distance
        attack_Timer += Time.deltaTime;

        if (attack_Timer > chase_After_Attack_Distance)
        {//attack time becomes greater then chase_After_Attack_Distance so the enemy will attack

            enemy_Anim.Attack();

            //reseting the attack timer 
            attack_Timer = 0f;

            //play attack sound
            enemy_Audio.Play_AttackSound();
        }
        //if the player ran away but the attack_distance & the chase after attack distance combined is in range then chase(run towards the) player

        if (Vector3.Distance(transform.position, target.position) > attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }

    } // attack 

    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);
            
        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navAgent.SetDestination(navHit.position);
    }

    public void Turn_On_AttackPoint()
    {//Setting the On function for the Attack Point
        attack_Point.SetActive(true);
    }
    public void Turn_Off_AttackPoint()
    {//Setting the Off function for the Attack Point
        attack_Point.SetActive(false);
    }

    public EnemyState Enemy_State
    {
        //get; set;
        get { return enemy_State; }
        set { enemy_State = value; }
    }
     
} // class
