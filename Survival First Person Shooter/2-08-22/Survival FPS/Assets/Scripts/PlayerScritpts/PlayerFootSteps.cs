using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    
    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController characterController;

    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

    // Start is called before the first frame update
    void Start()
    {
        footstep_Sound = GetComponent<AudioSource>();

        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        //if we are NOT on the ground
        //e.g. make a step or sprint, or move while crouching
        // until we play the footstep sound
        if (!characterController.isGrounded)    return;

        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulated_Distance += Time.deltaTime;

            if (accumulated_Distance > step_Distance)
            {
                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumulated_Distance = 0f;

                //Random.Range(1f, 3f);
            }

        }
        else
            accumulated_Distance = 0f;

    }
}
