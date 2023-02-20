using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
	[SerializeField] int life;

	public void TakeDamage(int damage)
	{
		life -= damage;

		if (life <= 0)
		{
			Destroy(gameObject);
		}
	}
}