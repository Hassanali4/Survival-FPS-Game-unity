using UnityEngine;

//using TagHolder;

public class PlayerMovement : MonoBehaviour
{

    // Start is called before the first frame update
    private CharacterController character_controller;

    private Vector3 move_Direction;
    public VariableJoystick variableJoystick;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_Veloctiy;

    //variables for Mobile UI
    public Button_Sprint Is_Jump;

    void Awake()
    {
        character_controller = GetComponent<CharacterController>();
    }

    void Update()
    {


        MoveThePlayerJoystick();
        //MoveThePlayer();
        ApplyGravity();
    }

    void MoveThePlayerJoystick()
    {
        move_Direction = new Vector3(variableJoystick.Horizontal, 0f,
                                     variableJoystick.Vertical);

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;
        ApplyGravity();
        character_controller.Move(move_Direction);
       // Debug.Log("PM speed" + speed);
    }// move apply

    void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
                                     Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();
        character_controller.Move(move_Direction);
    }// move apply

    public void ApplyGravity()
    {
        vertical_Veloctiy -= gravity * Time.deltaTime;

        //jump
        //PlayerJump();

        move_Direction.y = vertical_Veloctiy * Time.deltaTime;
        //move_Direction.y = vertical_Veloctiy;

    }// apply Gravity 

    public void PlayerJump()
    {
        if (character_controller.isGrounded)
        {
            vertical_Veloctiy = jump_Force;
        }
    }
}
