using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float timeToTakeDamage;
    [SerializeField] float colldownAttack;
    [SerializeField] float rangeAttack;
    [SerializeField] LayerMask enemysLayer;
    [SerializeField] Transform pointAttack;
    [SerializeField] Animator anim;
    [SerializeField] PlayerMovement pMove;

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            pMove.canMove = false;
            anim.SetTrigger("Attack");
            StartCoroutine(TakeDamageInEnemys());
            StartCoroutine(CollDownAttack());
        }
    }

	private void AttackHandler()
	{
        var colliders = Physics2D.OverlapCircleAll(pointAttack.position, rangeAttack, enemysLayer);

        if (colliders != null)
        {
            foreach (var collider in colliders)
            {
                var script = collider.GetComponent<IEntity>();
                script.TakeDamage(damage);
            }
        }
	}

    IEnumerator TakeDamageInEnemys()
    {
        yield return new WaitForSeconds(timeToTakeDamage);
        AttackHandler();
    }

    IEnumerator CollDownAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(colldownAttack);
        canAttack = true;
        pMove.canMove = true;
    }

	void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointAttack.position, rangeAttack);
	}
}
