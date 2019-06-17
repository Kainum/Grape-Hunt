using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Eagle : Enemy
	{
        
        [SerializeField]
        private float xMin;
        [SerializeField]
        private float xMax;
        [SerializeField]
        private float speedX;
        [SerializeField]
        private float yMin;
        [SerializeField]
        private float yMax;
        [SerializeField]
        private float speedY;

        private SceneManagerScript sceneManager;
		void Start () {
			facingRight = false;
			health = maxHealth;
			spawnLocation = transform.position;
			spawnRotation = transform.rotation;
			anim = GetComponent<Animator> ();
			rb = GetComponent<Rigidbody2D> ();
			deathSound = "hawk";
            sceneManager = GameObject.Find("/SceneManager").GetComponent<SceneManagerScript>();
		}

		void FixedUpdate () {
            Move();
			UpdateAnim ();
		}

		private void Move () {
            float xAtual = transform.position.x;
            float yAtual = transform.position.y;
            rb.velocity = new Vector2(speedX, speedY);
            
            if (speedX != 0) {
                if ((xAtual < xMin && speedX < 0) || (xAtual > xMax && speedX > 0)) {
                    Flip();
                    speedX /= -1;
                }
            } else {
                float playerPositionX = sceneManager.player.transform.position.x;
                if ((playerPositionX < xAtual && facingRight) || (playerPositionX > xAtual && !facingRight)) {
                    Flip();
                }
            }

            if (speedY != 0) {
                if ((yAtual < yMin && speedY < 0) || (yAtual > yMax && speedY > 0)) {
                    speedY /= -1;
                }
            }
        }

		protected override void UpdateAnim () {
            // ...
		}
	}
}
