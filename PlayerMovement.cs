using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
        HandleRotation();
    }

    void HandleRotation()
    {
        if (movement == Vector2.zero) return;

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            if (movement.x > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            // Atas / Bawah
            if (movement.y > 0)
                transform.rotation = Quaternion.Euler(0, 0, 90);   // atas
            else
                transform.rotation = Quaternion.Euler(0, 0, -90);  // bawah
        }
    }
}
