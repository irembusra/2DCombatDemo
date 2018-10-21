using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour {


    public int health;
    public GameObject particleEffect;
    SpriteRenderer spriteRenderer;

    int direction;
    float timer = 1.5f;
    public Sprite facingUp , facingDown , facingLeft, facingRight;
    public float speed;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = Random.Range(0, 3);
       // spriteRenderer.sprite = facingUp;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <=0)
        {
            direction = Random.Range(0, 3);
            timer = 1.5f;
        }
        Movement();
	}
    void Movement()
    {
        switch (direction)
        {
            case 0:
                transform.Translate(0, -speed * Time.deltaTime, 0);
                spriteRenderer.sprite = facingDown;
                
            break;
            case 1:
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                spriteRenderer.sprite = facingLeft;
            break;
            case 2:
                transform.Translate(speed * Time.deltaTime, 0, 0);
                spriteRenderer.sprite = facingRight;
            break;
            case 3:
                transform.Translate(0, speed * Time.deltaTime, 0);
                spriteRenderer.sprite = facingUp;
            break;
           
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Sword")
        {
            health--;
            if(health<=0)
            {
                Destroy(gameObject);
                Instantiate(particleEffect, transform.position, transform.rotation);
            }
            collision.gameObject.GetComponent<Sword>().CreateParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("Collision enter");
        if(collision.gameObject.tag=="Player")
        {
         //   Debug.Log("tag if state");
            health--;
            if(!collision.gameObject.GetComponent<Player>().iniFrames)
            {
                collision.gameObject.GetComponent<Player>().currentHealth--;
                collision.gameObject.GetComponent<Player>().iniFrames = true;

            }
    
            if(health<=0)
            {
                Destroy(gameObject);
                Instantiate(particleEffect, transform.position, transform.rotation);
            }
           


        }
        if(collision.gameObject.tag=="Wall")
        {
            direction = Random.Range(0, 3);
        }
    }
}
