using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviour
{
    private Rigidbody myBody;

    public float speed = 30f;

    public float deactivate_timer = 3f;

    public float damage = 50f;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Invoke("DeactivateGameObject", deactivate_timer);
    }

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + myBody.velocity);
    }

    void DeactivateGameObject()
    {//
        if(gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider target)
    {
        //after we touch the enemy deactivate game object
        if (target.tag == Tags.ENEMY_TAG)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }


}// class
