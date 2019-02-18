using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Monster speed")] public float speed = 2f;
    [Tooltip("Minimal distance between  player and monster")]public float distance = 2f;
    private Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,playerPosition.position) > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        }
        
    }
}
