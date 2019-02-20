//Script for player control
//by Bartek

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    [Tooltip("Speed of normal walk")] public float speed = 80;
    [Tooltip("Speed of sprint")] public float sprintSpeed = 110;
    [Tooltip("Can player move")] public bool canMove = true;
    [Tooltip("Can player sprint")] public bool canSprint = true;

    [Header("Info")]
    [Tooltip("Is player sprinting")] public bool isSprinting = false;
    [Tooltip("Is player moving")] public bool isMoving = false;

    [Header("Other")]
    [SerializeField] private PlayerStats stats;

    private Rigidbody2D rb;

    private float moveHorizontal = 0;
    private float moveVertical = 0;
    private Vector2 movement = Vector2.zero;
    private float lastRot = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        Movement();
        SetRotation();                  //I think, we won't need that function in future, so it'll be removed in future
    }

    private void Movement ()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        isMoving = (moveHorizontal != 0 || moveVertical != 0) && canMove;
        isSprinting = canSprint && Input.GetButton("Sprint") && canMove && isMoving && (stats.actualStamina > 0);

        movement = (canMove ? new Vector2(moveHorizontal, moveVertical) : Vector2.zero);
        rb.velocity = movement * (isSprinting ? sprintSpeed : speed) * Time.deltaTime;

        if(isSprinting)
            stats.MinusStamina(1);
    }

    private void SetRotation ()
    {
        if (isMoving)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            lastRot = angle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(lastRot, Vector3.forward);
        }
    }
}
