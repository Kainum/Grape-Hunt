using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    private SceneManagerScript sceneManager;

    public bool battleOn;
    public bool isDead;

    public float moveSpeed;
	[SerializeField]
	private float jumpForce;
	private bool abaixado;

    private float posAtualX;

    SpriteRenderer sp;

    CapsuleCollider2D capCol;
    CircleCollider2D cirCol;
    
    void Start()
    {
        facingRight = true;
	    health = maxHealth;
        anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
        sp = GetComponent<SpriteRenderer>();
        capCol = GetComponent<CapsuleCollider2D>();
        cirCol = GetComponent<CircleCollider2D>();

        cirCol.enabled = false;

        sceneManager = GameObject.Find("/SceneManager").GetComponent<SceneManagerScript>();
		sceneManager.boss = this;
    }

    void InvColl() {
        cirCol.enabled = !cirCol.enabled;
        capCol.enabled = !capCol.enabled;
    }

    void Update()
    {
        posAtualX = transform.position.x;
        IsGrounded();

        if (!isDead && battleOn) {
            StartCoroutine(Battle());
            frame++;
        } else {
            cirCol.enabled = false;
            capCol.enabled = false;
            facingRight = true;
            transform.rotation = new Quaternion (0, 0, 0, 0);
            sp.flipX = true;
        }
        
        UpdateAnim();
    }

    private int frame = 0;

    [SerializeField]
    private float cont = 0;

    IEnumerator Battle () {
        float posx;

        if (cont == 0) {
            sp.flipX = false;
            Jump(0.7f);
            InvColl();
            cont = 1;
            frame = 0;
        }
        yield return new WaitUntil (() => frame >= 50);

        if (cont == 1) {
            InvColl();
            cont = 2;
        }

        posx = 33.45f;
        if (cont == 2) {
            Flip();
            sp.flipX = !sp.flipX;
            rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
            yield return new WaitUntil (() => posAtualX >= posx);
            cont = 3;
        }

        if (cont == 3) {
            Jump(0.6f);
            InvColl();
            frame = 0;
            cont = 4;
        }
        yield return new WaitUntil (() => frame >= 50);

        if (cont == 4) {
            Flip();
            Jump(-0.7f);
            cont = 5;
        }
        if (cont == 5) {
            frame = 0;
            cont = 6;
        }
        yield return new WaitUntil (() => frame >= 50);

        posx = 32.6f;
        if (cont == 6) {
            InvColl();
            moveSpeed *= -1;
            cont = 7;
        }
        
        if (cont == 7) {
            Flip();
            sp.flipX = !sp.flipX;
            rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
            yield return new WaitUntil (() => posAtualX <= posx);
            cont = 8;
        }

        if (cont == 8) {
            InvColl();
            Jump(-0.8f);
            cont = 9;
            frame = 0;
        }
        yield return new WaitUntil (() => frame >= 50);

        if (cont == 9) {
            InvColl();
            cont = 10;
        }

        posx = 31.6f;
        if (cont == 10) {
            Flip();
            sp.flipX = !sp.flipX;
            rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
            yield return new WaitUntil (() => posAtualX <= posx);
            cont = 11;
        }

        if (cont == 11) {
            InvColl();
            Jump(-0.8f);
            cont = 12;
            frame = 0;
        }
        yield return new WaitUntil (() => frame >= 50);
        if (cont == 12) {
            InvColl();
            cont = 13;
        }

        posx = 30.8f;
        if (cont == 13) {
            Flip();
            sp.flipX = !sp.flipX;
            rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
            yield return new WaitUntil (() => posAtualX <= posx);
            cont = 14;
        }

        if (cont == 14) {
            InvColl();
            Flip();
            Jump(0f);
            frame = 0;
            cont = 15;
        }
        yield return new WaitUntil (() => frame >= 50);

        if (cont == 15) {
            InvColl();
            moveSpeed *= -1;
            cont = 16;
        }

        posx = 32.07f;
        if (cont == 16) {
            if (!facingRight) {
                facingRight = true;
                transform.rotation = new Quaternion (0, 0, 0, 0);
            }
            cont = 17;
        }
        if (cont == 17) {
            Flip();
            sp.flipX = !sp.flipX;
            rb.velocity = new Vector2 (moveSpeed, rb.velocity.y);
            yield return new WaitUntil (() => posAtualX >= posx);
            cont = 18;
        }

        if (cont == 18) {
            if (!facingRight) {
                facingRight = true;
                transform.rotation = new Quaternion (0, 0, 0, 0);
            }
            rb.velocity = new Vector2(0, 0);
            cont = 0;
        }
    }

    void Jump (float velx) {
        rb.velocity = new Vector2(velx, jumpForce);
        SoundManagerScript.PlaySound("jump");
    }

    protected override void Die () {
        battleOn = false;
        isDead = true;
	}

    protected override void UpdateAnim () {
        anim.SetFloat ("velocityX", Mathf.Abs(rb.velocity.x));
		anim.SetFloat ("velocityY", rb.velocity.y);
		anim.SetBool ("grounded", grounded);
		anim.SetBool ("crouched", abaixado);
    }
}
