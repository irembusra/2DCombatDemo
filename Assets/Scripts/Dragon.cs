using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

    public float speed;
    Animator anim;
    public int dir;
    float timer = .7f;
    public int health;
    bool canAttack;
    float attackTimer=2f;

    public GameObject projecTile;
    public float thrustPower;

    public GameObject deathParticle;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        dir = Random.Range(0, 3);
        canAttack = false;

	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = .7f;
            dir = Random.Range(0, 3);

        }
        Movement();
       
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0)
        {
            attackTimer = 2f;
            canAttack = true;
        }
        Attack();
    }
    void Attack()
    {
        if (!canAttack)
            return;
        canAttack = false;
       if(dir==0)
        {
            GameObject newProjectTile = Instantiate(projecTile, transform.position, transform.rotation);
            newProjectTile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
        }
        else if(dir==1)
        {
            GameObject newProjectTile = Instantiate(projecTile, transform.position, transform.rotation);
            newProjectTile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
        }
        else if (dir == 2)
        {
            GameObject newProjectTile = Instantiate(projecTile, transform.position, transform.rotation);
            newProjectTile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
        }
        else if(dir==3)
        {
            GameObject newProjectTile = Instantiate(projecTile, transform.position, transform.rotation);
            newProjectTile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
        }
    
                
            
            

        
          
    }
    void Movement()
    {
        switch (dir)
        {
            case 0:
                transform.Translate(0, speed * Time.deltaTime, 0);
                anim.SetInteger("dir", dir);
                break;
            case 1:
                transform.Translate(-speed * Time.deltaTime,0, 0);
                anim.SetInteger("dir", dir);
                break;
            case 2:
                transform.Translate(0, -speed * Time.deltaTime, 0);
                anim.SetInteger("dir", dir);
                break;
            case 3:
                transform.Translate(speed * Time.deltaTime,0, 0);
                anim.SetInteger("dir", dir);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Sword")
        {
            health--;
            collision.gameObject.GetComponent<Sword>().CreateParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision enter");
        if (collision.gameObject.tag == "Player")
        {
            //   Debug.Log("tag if state");
            health--;
            if (!collision.gameObject.GetComponent<Player>().iniFrames)
            {
                collision.gameObject.GetComponent<Player>().currentHealth--;
                collision.gameObject.GetComponent<Player>().iniFrames = true;

            }

            if (health <= 0)
            {
                Destroy(gameObject);
                Instantiate(deathParticle, transform.position, transform.rotation);
            }



        }
        if (collision.gameObject.tag == "Wall")
        {

            dir--;
            if (dir < 0)
                dir = 3;
        
        }
    }
}

