using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Opossum : Enemy
	{
        
        [SerializeField]
        private float xMin;
        [SerializeField]
        private float xMax;
        [SerializeField]
        private float speed;
		void Start () {
			facingRight = false;
			health = maxHealth;
			spawnLocation = transform.position;
			spawnRotation = transform.rotation;
			anim = GetComponent<Animator> ();
			rb = GetComponent<Rigidbody2D> ();
			deathSound = "opossum";
		}

		void FixedUpdate () {
			IsGrounded ();
            Walk();
			UpdateAnim ();
		}

		private void Walk () {
            float xAtual = transform.position.x;
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if ((xAtual < xMin && speed < 0) || (xAtual > xMax && speed > 0)) {
                Flip();
                speed /= -1;
            }
        }

		protected override void UpdateAnim () {
			// ...
		}
	}
}
