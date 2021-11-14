using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private int m_hp;
    private Rigidbody2D m_rigidBody;
    private Animator m_anim;
    private Collider2D m_collider;
    private bool isGrounded = true;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float jumpSpeedY = 10f;
    [SerializeField]
    private float movementSpeedX = 3f;
    private bool isJumping = false;

    void Start()
    {
        m_hp = 1;
        m_rigidBody = this.gameObject.GetComponent<Rigidbody2D>() ?? this.gameObject.AddComponent<Rigidbody2D>();
        m_anim = this.gameObject.GetComponent<Animator>() ?? gameObject.AddComponent<Animator>();
        m_collider = this.gameObject.GetComponent<Collider2D>() ?? this.gameObject.AddComponent<Collider2D>();
    }

    void Update()
    {
        if (IsGrounded())
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }

        if (!isJumping)
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                isJumping = true;
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, jumpSpeedY);
            }
        }

        if (Input.GetButton("Horizontal"))
        {
            if (!isJumping)
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && m_rigidBody.velocity.x <= movementSpeedX)
                {
                    m_rigidBody.AddForce(new Vector3(1, 0, 0) * 1.5f);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0 && m_rigidBody.velocity.x >= -movementSpeedX)
                {
                    m_rigidBody.AddForce(new Vector3(-1, 0, 0) * 1.5f);
                }

            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && m_rigidBody.velocity.x <= movementSpeedX / 2)
                {
                    m_rigidBody.AddForce(new Vector3(1, 0, 0) * 0.5f);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0 && m_rigidBody.velocity.x >= -movementSpeedX / 2)
                {
                    m_rigidBody.AddForce(new Vector3(-1, 0, 0) * 0.5f);
                }

            }
        }
        else
        {
            if (!isJumping)
            {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x * 0.99f, m_rigidBody.velocity.y);
            }
        }
    }

    bool IsGrounded()
    {
        float extraHeight = 0.2f;
        RaycastHit2D hit = Physics2D.BoxCast(m_collider.bounds.center, m_collider.bounds.size, 0f, Vector2.down, extraHeight, layerMask);

        return hit.collider != null;
    }

}
