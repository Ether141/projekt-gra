//Script for player control
//by Bartek

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    [Tooltip("Speed of normal walk")] public float speed = 80;
    [Tooltip("Can player move")] public bool canMove = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        Movement();
    }

    private void Movement ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = (canMove ? new Vector2(moveHorizontal, moveVertical) : Vector2.zero);
        rb.velocity = movement * speed * Time.deltaTime;
    }
}
