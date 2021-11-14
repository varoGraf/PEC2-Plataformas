using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    private int m_hp;
    private Rigidbody2D m_rigidBody;
    private Animator m_anim;
    private Collider2D m_collider;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private LayerMask layerMaskEnemy;

    [SerializeField]
    private float jumpSpeedY = 10f;
    [SerializeField]
    private float acceleration = 8f;
    [SerializeField]
    private float movementSpeedX = 10f;
    [SerializeField]
    private float deceleration = 0.92f;


    void Start()
    {
        m_hp = 1;
        m_rigidBody = this.gameObject.GetComponent<Rigidbody2D>() ?? this.gameObject.AddComponent<Rigidbody2D>();
        m_anim = this.gameObject.GetComponent<Animator>() ?? gameObject.AddComponent<Animator>();
        m_collider = this.gameObject.GetComponent<Collider2D>() ?? this.gameObject.AddComponent<Collider2D>();
        movementSpeedX = 3f;
    }

    void Update()
    {
        if (m_hp <= 0 || this.transform.position.y < -1)
        {
            LoadDiedScene();
        }

        if (IsGrounded())
        {
            m_anim.SetBool("isGrounded", true);
            m_rigidBody.gravityScale = 1;
        }
        else
        {
            m_anim.SetBool("isGrounded", false);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            audioSource.Play();
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, jumpSpeedY);
        }
        else if (!Input.GetButton("Jump"))
        {
            if (m_rigidBody.velocity.y > 0)
            {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y * 0.96f);
            }
        }

        if (!IsGrounded() && Mathf.Floor(m_rigidBody.velocity.y) == 0)
        {
            m_rigidBody.gravityScale = 2;
        }



        if (Input.GetButton("Fast"))
        {
            if (IsGrounded())
            {
                movementSpeedX = 13f;
            }
        }
        else
        {
            movementSpeedX = 10f;
        }

        if (Input.GetButton("Horizontal"))
        {
            if (IsGrounded())
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && m_rigidBody.velocity.x <= movementSpeedX)
                {
                    if (m_rigidBody.velocity.x < -movementSpeedX / 2)
                    {
                        m_rigidBody.AddForce(new Vector3(1, 0, 0) * acceleration * 2);
                    }
                    m_rigidBody.AddForce(new Vector3(1, 0, 0) * acceleration);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0 && m_rigidBody.velocity.x >= -movementSpeedX)
                {
                    if (m_rigidBody.velocity.x > movementSpeedX / 2)
                    {
                        m_rigidBody.AddForce(new Vector3(-1, 0, 0) * acceleration * 2);
                    }
                    m_rigidBody.AddForce(new Vector3(-1, 0, 0) * acceleration);
                }

            }
            else
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && m_rigidBody.velocity.x <= movementSpeedX / 2)
                {
                    m_rigidBody.AddForce(new Vector3(1, 0, 0) * acceleration / 3);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0 && m_rigidBody.velocity.x >= -movementSpeedX / 2)
                {
                    m_rigidBody.AddForce(new Vector3(-1, 0, 0) * acceleration / 3);
                }

            }
        }
        else
        {
            if (IsGrounded())
            {
                if (m_rigidBody.velocity.x <= 0.09f && m_rigidBody.velocity.x >= -0.09f)
                {
                    m_rigidBody.velocity = new Vector2(0f, m_rigidBody.velocity.y);
                }
                else
                {
                    m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x * deceleration, m_rigidBody.velocity.y);
                }
            }
        }
        if (m_rigidBody.velocity.x != 0.0f)
        {
            if (m_rigidBody.velocity.x > 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            else if (m_rigidBody.velocity.x < 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
            }
            m_anim.SetBool("isRunning", true);
        }
        else
        {
            m_anim.SetBool("isRunning", false);
        }

    }

    bool IsGrounded()
    {
        float extraHeight = 0.2f;
        RaycastHit2D hit = Physics2D.BoxCast(m_collider.bounds.center, m_collider.bounds.size, 0f, Vector2.down, extraHeight, layerMask);

        return hit.collider != null;
    }

    public void LoadDiedScene()
    {
        SceneManager.LoadScene("DiedScene");
    }
    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FlagPole")
        {
            LoadWinScene();
        }
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, jumpSpeedY / 2);
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy")
        {
            m_hp -= 1;
        }

    }


}
