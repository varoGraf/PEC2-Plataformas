using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMovement : MonoBehaviour
{
    public float goombaSpeed;
    [SerializeField]
    private LayerMask layerMask;
    private Rigidbody2D m_rigidbody;
    [SerializeField]
    private Transform Mario;
    private Collider2D m_collider;
    private bool startedMoving = false;

    void Start()
    {
        goombaSpeed = -1f;
        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_collider = this.gameObject.GetComponent<Collider2D>() ?? this.gameObject.AddComponent<Collider2D>();
    }
    void Update()
    {
        if (this.transform.position.x - Mario.position.x <= 12f && !startedMoving)
        {
            startedMoving = true;
            m_rigidbody.velocity = new Vector2(goombaSpeed, m_rigidbody.velocity.y);
        }
        if (IsTouchingLeft() && startedMoving)
        {
            m_rigidbody.velocity = new Vector2(-goombaSpeed, m_rigidbody.velocity.y);
        }
        if (IsTouchingRight() && startedMoving)
        {
            m_rigidbody.velocity = new Vector2(goombaSpeed, m_rigidbody.velocity.y);
        }
        if (this.transform.position.y <= -3)
        {
            Destroy(this);
        }
    }

    bool IsTouchingRight()
    {
        float extraHeight = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(m_rigidbody.position, Vector2.right, m_collider.bounds.extents.x + extraHeight, layerMask);

        return hit.collider != null;
    }

    bool IsTouchingLeft()
    {
        float extraHeight = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(m_rigidbody.position, Vector2.left, m_collider.bounds.extents.x + extraHeight, layerMask);

        return hit.collider != null;
    }
}
