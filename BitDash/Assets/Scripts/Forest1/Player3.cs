/*
 * The file is for player to move, jump. Current version v1.0 27/12/2020 @Xiaoyan Zhou
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3 : MonoBehaviour
{
    //get animator
    public Animator animator;
    //initial speed
    public float speed;
    
    //jump force
    public float jumpForce;
    //jump condition
    public bool canJump = false;
    
    //get rigidbody
    public Rigidbody2D rigidBody;
    //import GameManager
    public GameManager3 gameManager;
    
    //give hero an initial body size
    public Vector2 bodySize;
    public Vector2 bodyCenter;

    public bool dashSwitch = true;
    
    //death condition
    public bool playerDeath = false;
    public bool playerWin = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //load game manager
        gameManager = GameObject.Find("Pixel UI Canvas").GetComponent<GameManager3>();
        //load animator
        animator = GetComponent<Animator>();
        //load rigidbody
        rigidBody = GetComponent<Rigidbody2D>();
        //load body size
        bodySize = GetComponent<BoxCollider2D>().size;
        //load body center
        bodyCenter = GetComponent<BoxCollider2D>().offset;
    }

    // Update is called once per frame
    void Update()
    {
        //Detecting button can not write in FixedUpdated() method,
        //because detection is once per frame and FixedUpdate may be triggered multiple times per frame.
        //Dash Function, Triggered once when the button is pressed
        if (Input.GetKeyDown(KeyCode.DownArrow)&& canJump&&dashSwitch==true && GameManager3.GameStart)
        {
            //triger Dash of three frames animation
            animator.SetBool("dash", true);
            //Make the hero move a distance down the Y axis
            GetComponent<BoxCollider2D>().size = new Vector2(bodySize.x, bodySize.y * 0.5f);
            GetComponent<BoxCollider2D>().offset = new Vector2(bodyCenter.x, bodyCenter.y - 0.19f);
            
            dashSwitch = false;
            //Keep dash time at 1 seconds
            Invoke("Dash", 0.5f);
            
        }
        
        //jump script could also put in FixedUpdate but the unit time should be modified,
        //or there will be delay on jump
        canJump = onGround();
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump && GameManager3.GameStart)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce * 10));
            //jump animation active
            animator.SetBool("jump", true);
        }
        else
        {
            //jump animation deactive
            animator.SetBool("jump", !canJump);
        }
        
        //death trigger
        if (playerDeath == true || playerWin == true)
        {
            gameManager.GameOver();
        }
        
    }
    
    // The method for run
    public void Run()
    {
        //set speed
        rigidBody.velocity = new Vector2(speed, 0);
        //animator speed
        animator.SetFloat("speed", Mathf.Abs(speed));
    }
    //ground justification
    public bool onGround()
    {
        //get collider bound
        Bounds bounds = GetComponent<Collider2D>().bounds;
        //use lincast to check if player on ground or not
        float range = bounds.size.y * 0.1f;
        Vector2 v = new Vector2(bounds.center.x,bounds.min.y - range);
        RaycastHit2D hit = Physics2D.Linecast(v, bounds.center);
        return (hit.collider.gameObject != gameObject);
    }
    
    //Return to running
    public void Dash()
    {
        animator.SetBool("dash", false);
        GetComponent<BoxCollider2D>().size = new Vector2(bodySize.x, bodySize.y);
        GetComponent<BoxCollider2D>().offset = new Vector2(bodyCenter.x, bodyCenter.y);
        dashSwitch = true;
    }
    
    
    //game complete   current idea is to move next stage
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "End")
        {
            Debug.Log("Congratulation, You Win.");
        }
        //moveNextStage();
        
    }

    //collider judgement for death 
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Death")
        {
            playerDeath = true;
            speed = 0;
            rigidBody.Sleep();
            animator.SetBool("die", true);
            Debug.Log("die,die,die");
        }
        else if (other.transform.tag == "End")
        {
            playerWin = true;
            Debug.Log("Congratulation, You Win.");
            //moveNextStage();
        }
    }
    
}
