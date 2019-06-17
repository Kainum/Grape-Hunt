using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

	[SerializeField]
	private int value;
	[SerializeField]
	private GameObject item_effect;
	public float dropChance;
	public bool respawn;

	protected void OnTriggerEnter2D (Collider2D col) {
		Player player = col.GetComponent<Player>();
		if (player != null) {
			SoundManagerScript.PlaySound("item");
			ItemEffect(player, value);
			Instantiate (item_effect, transform.position, transform.rotation);
			if (respawn) {
				GameObject.Find(gameObject.name + ("spawn point")).GetComponent<ItemRespawn>().Death = true;
			}
			Destroy (gameObject);
		}
	}

	// método que dispara quando o player 
	protected abstract void ItemEffect (Player player, int value);

}