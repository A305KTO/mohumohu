using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float jumpForce = 680.0f;
    [SerializeField] float wolakForce = 30.0f;
    [SerializeField] float maxWalkSpeed = 2.0f;
    void Start()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.rigidbody2D.AddForce(transform.up * this.jumpForce);

        }
        int key = 0;
        if (Input.GetKey(KeyCode.D))  key = 1;
        if (Input.GetKey(KeyCode.A)) key = -1;

        float speedX = Mathf.Abs(this.rigidbody2D.velocity.x);

        if (speedX < this.maxWalkSpeed)
        {
            this.rigidbody2D.AddForce(transform.right * key *this.wolakForce);
        }
    }

}
