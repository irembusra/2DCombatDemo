using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

    public float speed;
    Animator anim;
    public int dir;
    float timer = .7f;
    public int health;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        dir = Random.Range(0, 3);

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
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
       
    }
}
