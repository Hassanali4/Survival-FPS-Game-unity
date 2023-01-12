using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Touch Field Experiment 1
    public FixedTouchField TouchField;

    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_unlock = true;

    [SerializeField]
    private float sensivity = 0.2f;

    [SerializeField]
    private int smooth_Steps = 10;

    [SerializeField]
    private float smooth_Weight = 0.4f;

    [SerializeField]
    private float roll_Angel = 10f;

    [SerializeField]
    private float roll_speed = 3f;

    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-70f, 80f);

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;

    private Vector2 smooth_Move;

    private float current_Roll_Angle;

    private int last_Look_Frame;

    //    public DynamicJoystick dynamicJoystick;
        //private Vector2 worldStartPoint;
        //public float a;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //TouchField = GetComponent<FixedTouchField>();
    }

    private void Update()
    {
        LookAround();

      //// The cursor locking code calling in update to enable functionality
        //LockAndUnlockCursor();
        //if (Cursor.lockState == CursorLockMode.Locked)
        //{
        //    LookAround();
        //}

    }

    private void LookAround()
    {
        current_Mouse_Look = new Vector2(TouchField.TouchDist.y, TouchField.TouchDist.x);
        look_Angles.x += current_Mouse_Look.x * sensivity * (invert ? 1f : -1f);
        look_Angles.y += current_Mouse_Look.y * sensivity;

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);

        //current_Roll_Angle =
        //    Mathf.Lerp(current_Roll_Angle,Input.GetAxisRaw(MouseAxis.MOUSE_X) 
        //    * roll_Angel, Time.deltaTime* roll_speed);

        //lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, current_Roll_Angle);
        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

        //print("Value is :" + Input.GetAxis("Horizontal"));
        //print("Value is :" + Input.GetAxisRaw("Horizontal"));
    }


    //Mouse Cursor Locking Code can be controled with ESC key -> till line 86

    //private void LockAndUnlockCursor()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        if (Cursor.lockState == CursorLockMode.Locked)
    //        {
    //            Cursor.lockState = CursorLockMode.None;
    //        }
    //        else if (Cursor.lockState == CursorLockMode.None)
    //        {
    //            Cursor.lockState = CursorLockMode.Locked;
    //            Cursor.visible = false;
    //        }
    //    }
    //}//lock & Unlock


    //The Dynamic Joystic attempt -> till line 142
    //private void LookAround()
    //{
    //current_Mouse_Look = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y),Input.GetAxis(MouseAxis.MOUSE_X));
    // current_Mouse_Look = new Vector2(dynamicJoystick.Vertical,dynamicJoystick.Horizontal);

    //The Dynamic Joystic attempt till line 142
    //    if (Input.touchCount == 1)
    //    {
    //        Touch currentTouch = Input.GetTouch(0);

    //        if (currentTouch.phase == TouchPhase.Began)
    //        {
    //            this.worldStartPoint = this.getWorldPoint(currentTouch.position);
    //        }

    //        if (currentTouch.phase == TouchPhase.Moved)
    //        {
    //            Vector2 worldDelta = this.getWorldPoint(currentTouch.position) - this.worldStartPoint;

    //            Camera.main.transform.Translate(
    //                -worldDelta.x,
    //                -worldDelta.y,
    //                0
    //            );



    //            current_Mouse_Look = new Vector2(worldDelta.x, worldDelta.y);
    //            look_Angles.x += current_Mouse_Look.x * sensivity * (invert ? 1f : -1f);
    //            look_Angles.y += current_Mouse_Look.y * sensivity;

    //            look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);

    //            //Touch field Exp1


    //            //current_Roll_Angle =
    //            //    Mathf.Lerp(current_Roll_Angle,Input.GetAxisRaw(MouseAxis.MOUSE_X) 
    //            //    * roll_Angel, Time.deltaTime* roll_speed);

    //            //lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, current_Roll_Angle);
    //            lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
    //            playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

    //            //print("Value is :" + Input.GetAxis("Horizontal"));
    //            //print("Value is :" + Input.GetAxisRaw("Horizontal"));
    //        }


    //    }
    //}

    // private Vector2 getWorldPoint(Vector2 screenPoint)
    // {
    //       RaycastHit hit;
    //       Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit);
    //       return hit.point;
    // }
}



































