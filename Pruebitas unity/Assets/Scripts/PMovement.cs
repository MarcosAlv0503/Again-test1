using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{

    //Usando [SerializeField] private, hacemos que la variable sea aun accesible desde el editor de unity pero no accesible desde otras clases/scripts
    public float JumpForce;
    public float MoveForce;
    public float ClimbForce;

    public bool climbing = false;

    private BoxCollider2D coll;
    private Rigidbody2D rigidBd;
    private Animator anim;
    private SpriteRenderer spRender;

    public static bool position;
    
    float dirX = 0f;
    float dirY = 0f;

    [SerializeField] private LayerMask ground;

    [SerializeField] private AudioSource jumpSound;
    private enum MoveState {idle, combat, run}
    private Vector3 lScale;



    // Start is called before the first frame update
    private void Start()
    {
        lScale = transform.localScale;
        coll = GetComponent<BoxCollider2D>();
        rigidBd = GetComponent<Rigidbody2D>();
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
        if (!SceneController.instance.paused)
        {
            //GetAxixRaw provoca que se reinicie a 0 (de Input.GetAxis("Horizontal")) cuando deje de pulsarse. Esto hace que el personaje se pare en seco al dejar de moverse
            dirX = Input.GetAxis("Horizontal");
            dirY = Input.GetAxis("Vertical");

            if(climbing == true)
            {
                rigidBd.velocity = new Vector2(dirX * ClimbForce, dirY * ClimbForce);
            }
            else
            {
                rigidBd.velocity = new Vector2(dirX * MoveForce, rigidBd.velocity.y);
            }
            

            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                rigidBd.velocity = new Vector2(rigidBd.velocity.x, JumpForce);
                jumpSound.Play();
            }

            UpdateAnim();
        }
        
    }

    private void UpdateAnim()
    {
        MoveState state;

        if (dirX > 0f)
        {
            transform.localScale = new Vector3(-lScale.x, lScale.y, lScale.z);
            state = MoveState.run;
        }
        else if (dirX < 0f)
        {
            transform.localScale = new Vector3(lScale.x, lScale.y, lScale.z);
            state = MoveState.run;
        }
        else //if(dirX == 0)
        {
            state = MoveState.idle;
        }

        if(System.Math.Abs(rigidBd.velocity.y) > 0.05f)
        {
            anim.SetBool("Grounded", false);
            anim.SetFloat("AirSpeed", rigidBd.velocity.y);
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
