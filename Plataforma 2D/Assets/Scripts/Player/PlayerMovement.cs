using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    [SerializeField] BoxCollider2D box;
    [SerializeField] LayerMask groundLayer;

    void Update()
    {
        var isGound = Physics2D.OverlapBox((Vector2)transform.position + box.offset, box.size - new Vector2(.1f, 0), 0, groundLayer);
        if (isGound)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        float h = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(h, rb.velocity.y);

        if (h != 0)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 0);

        anim.SetBool("Run", h != 0);
        anim.SetInteger("VelocityY", (int)rb.velocity.y);
    }
}
