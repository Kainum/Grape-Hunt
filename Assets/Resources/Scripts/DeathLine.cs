using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col) {
		Player player = col.GetComponent<Player>();
		if (player != null) {
			player.TakeDamage(10);
		}
	}
}
