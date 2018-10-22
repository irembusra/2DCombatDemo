using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    // movement varibles
    public float speed;
    public bool canMove;

    Animator anim;

    // health variables
    public Image[] hearts;
    public int maxHealth;
    public int currentHealth;

    // Sword
    public GameObject sword;
    public float trustPower;
    public bool canAttack;


    public bool iniFrames;
    float iniTimer = 1f;
    SpriteRenderer sr;

    void Start()
    {
        canMove = true;
        canAttack = true;
        iniFrames = false;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        getHealth();
    }

    void Update()
    {

        Movement();

        // healthCheck(Damage)
        // if(Input.GetKeyDown(KeyCode.P))
        //{
        //	currentHealth--;	
        //}
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //	currentHealth--;	
        //}
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (iniFrames)
        {
            iniTimer -= Time.deltaTime;
            int rn = Random.Range(0, 100);
            if (rn < 50) sr.enabled = false;
            if (rn >= 50) sr.enabled = true;
            if (iniTimer <= 0)
            {
                iniTimer = 1f;
                iniFrames = false;
                sr.enabled = true;
            }

        }
        getHealth();
        // Attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void getHealth()
    {
        for (int i = 0; i <= hearts.Length - 1; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }
        for (int i = 0; i <= currentHealth - 1; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }


    }

    void Attack()
    {
        if (!canAttack)
            return;
        canAttack = false;
        canMove = false;
        GameObject newSword = Instantiate(sword, transform.position, sword.transform.rotation);
        if (currentHealth == maxHealth)
        {
            newSword.GetComponent<Sword>().special = true;
            canMove = true;
            trustPower = 700;

        }

        int swordDir = anim.GetInteger("dir");
        anim.SetInteger("attackDir", swordDir);
        if (swordDir == 0)
        {
            newSword.transform.Rotate(0, 0, 0);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * trustPower);
        }

        else if (swordDir == 1)
        {
            newSword.transform.Rotate(0, 0, 180);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -trustPower);
        }

        else if (swordDir == 2)
        {
            newSword.transform.Rotate(0, 0, 90);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -trustPower);
        }

        else if (swordDir == 3)
        {
            newSword.transform.Rotate(0, 0, -90);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * trustPower);
        }


    }
    void Movement()
    {
        if (!canMove)
            return;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            anim.SetInteger("dir", 0);
            anim.speed = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            anim.SetInteger("dir", 1);
            anim.speed = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", 2);
            anim.speed = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", 3);
            anim.speed = 1;
        }
        else
        {
            //	Debug.Log("no animation");
            anim.speed = 0;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (!iniFrames)
            {
                currentHealth--;
                iniFrames = true;
            }
            collision.gameObject.GetComponent<Bullet>().CreateParticle();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Potion")
        {
            maxHealth = currentHealth;

            if (maxHealth >= 5)
                return;

            maxHealth++;
            currentHealth = maxHealth;
            Destroy(collision.gameObject);

         

        }
    }
}

