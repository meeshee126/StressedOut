using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    Rigidbody2D rb;
 
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        rb.velocity = move * movementSpeed * Time.deltaTime;
    }
}
