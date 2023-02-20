using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] float speed;
    [SerializeField] float speedCrounching;
    [SerializeField] float jumpForce;

    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    [Header("Ground Check Configs")]
    [SerializeField] LayerMask groundLayer;

    [Header("Colliders")]
    [SerializeField] BoxCollider2D boxDefault;
    [SerializeField] BoxCollider2D boxCrouch;

    bool isCrouching = false;
	public bool canMove;

	void Update()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        var realSpeed = (!isCrouching) ? speed : speedCrounching;
        MoveHandler(realSpeed);
        JumpHandler();
        CrouchHandler();
    }

    void MoveHandler(float speed)
    {
		float h = Input.GetAxis("Horizontal") * speed;
		rb.velocity = new Vector2(h, rb.velocity.y);

		if (h != 0)
			transform.localScale = new Vector3(Mathf.Sign(h), 1, 0);

		anim.SetBool("Run", h != 0);
		anim.SetInteger("VelocityY", (int)rb.velocity.y);
	}

    void JumpHandler()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			var isGound = Physics2D.OverlapBox((Vector2)transform.position + boxDefault.offset, boxDefault.size - new Vector2(.1f, 0), 0, groundLayer);
			if (isGound)
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		}
	}

    void CrouchHandler()
    {
        isCrouching = Input.GetKey(KeyCode.S);

        anim.SetBool("isCrouching", isCrouching);

        if (isCrouching)
        {
            boxDefault.enabled = false;
            boxCrouch.enabled = true;
        }
        else
        {
			boxDefault.enabled = true;
			boxCrouch.enabled = false;
		}
    }
}
