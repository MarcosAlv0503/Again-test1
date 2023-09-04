using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{

    //Usando [SerializeField] private, hacemos que la variable sea aun accesible desde el editor de unity pero no accesible desde otras clases/scripts
    public float JumpForce;
    public float MoveForce;

    private BoxCollider2D coll;
    private Rigidbody2D rigidbody;
    private Animator anim;
    private SpriteRenderer spRender;

    public static bool position;
    
    float dirX = 0f;

    [SerializeField] private LayerMask ground;

    [SerializeField] private AudioSource jumpSound;
    private enum MoveState {idle, combat, run}



    // Start is called before the first frame update
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spRender = GetComponent<SpriteRenderer>();
        if(position)
        {
            transform.position = SceneController.instance.GetplayerMapPos();
            position = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //GetAxixRaw provoca que se reinicie a 0 (de Input.GetAxis("Horizontal")) cuando deje de pulsarse. Esto hace que el personaje se pare en seco al dejar de moverse
        dirX = Input.GetAxis("Horizontal");

        rigidbody.velocity = new Vector2(dirX * MoveForce, rigidbody.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpForce);
            jumpSound.Play(); 
        }

        UpdateAnim();
        
    }

    private void UpdateAnim()
    {
        MoveState state;

        if (dirX > 0f)
        {
            spRender.flipX = true;
            state = MoveState.run;
        }
        else if (dirX < 0f)
        {
            spRender.flipX = false;
            state = MoveState.run;
        }
        else //if(dirX == 0)
        {
            state = MoveState.idle;
        }

        if(System.Math.Abs(rigidbody.velocity.y) > 0.01f)
        {
            anim.SetBool("Grounded", false);
            anim.SetFloat("AirSpeed", rigidbody.velocity.y);
        }
        else
        {
            anim.SetBool("Grounded", true);
        }

        anim.SetInteger("AnimState", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}
