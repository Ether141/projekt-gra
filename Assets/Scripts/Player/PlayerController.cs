//Script for player control
//by Bartek

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    [Tooltip("Speed of normal walk")] public float speed = 80;
    [Tooltip("Speed of normal walk")] public float sprintSpeed = 110;
    [Tooltip("Can player move")] public bool canMove = true;
    [Tooltip("Can player sprint")] public bool canSprint = true;

    [Header("Info")]
    [Tooltip("Is player sprinting")] public bool isSprinting = false;
    [Tooltip("Is player moving")] public bool isMoving = false;

    private Rigidbody2D rb;

    private float moveHorizontal = 0;
    private float moveVertical = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetRotation();
    }

    void LateUpdate()
    {
        Movement();
    }

    private void Movement ()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        isMoving = (moveHorizontal != 0 || moveVertical != 0) && canMove;
        isSprinting = canSprint && Input.GetButton("Sprint") && canMove;

        Vector2 movement = (canMove ? new Vector2(moveHorizontal, moveVertical) : Vector2.zero);
        rb.velocity = movement * (isSprinting ? sprintSpeed : speed) * Time.deltaTime;
    }

    private void SetRotation ()
    {
        if (moveHorizontal == 0 && moveVertical > 0)                    //Going up
            transform.localEulerAngles = new Vector3(0, 0, 90);         
        else if (moveHorizontal == 0 && moveVertical < 0)               //Going down
            transform.localEulerAngles = new Vector3(0, 0, -90);        
        else if(moveHorizontal > 0 && moveVertical == 0)                //Going right
            transform.localEulerAngles = new Vector3(0, 0, 0);
        else if (moveHorizontal < 0 && moveVertical == 0)               //Going left
            transform.localEulerAngles = new Vector3(0, 0, 180);
        else if (moveHorizontal > 0 && moveVertical > 0)                //Going top-right
            transform.localEulerAngles = new Vector3(0, 0, 45);
        else if (moveHorizontal < 0 && moveVertical > 0)                //Going top-left
            transform.localEulerAngles = new Vector3(0, 0, 135);
        else if (moveHorizontal > 0 && moveVertical < 0)                //Going down-right
            transform.localEulerAngles = new Vector3(0, 0, 315);
        else if (moveHorizontal < 0 && moveVertical < 0)                //Going down-left
            transform.localEulerAngles = new Vector3(0, 0, 230);
    }
}
