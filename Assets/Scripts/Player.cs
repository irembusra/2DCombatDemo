using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

// movement varibles
	public float speed;

	Animator anim;

// health variables
	public Image[] hearts;
	public int maxHealth;
	int currentHealth;

	// Sword
	public GameObject sword;

	void Start () {
		anim=GetComponent<Animator>();
		currentHealth=maxHealth;
		getHealth();
	}
	
	void Update () {
		
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
		if(currentHealth>maxHealth)
		{
			currentHealth=maxHealth;
		}
		getHealth();
		// Attack
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Attack();
		}
	}

	void getHealth()
	{
		for(int i =0; i<=hearts.Length-1; i++)
		{
			hearts[i].gameObject.SetActive(false);
		}
		for(int i =0; i<=currentHealth-1 ; i++)
		{
			hearts[i].gameObject.SetActive(true);
		}
	}

void Attack()
{
	GameObject newSword= Instantiate(sword , transform.position , sword.transform.rotation);
	int swordDir= anim.GetInteger("dir");
	if(swordDir==0)
	newSword.transform.Rotate(0,0,0);
	else if(swordDir==1)
	newSword.transform.Rotate(0,0,180);
	else if(swordDir==2)
	newSword.transform.Rotate(0,0,90);
	else if(swordDir==3)
	newSword.transform.Rotate(0,0,-90);

}
	void Movement()
	{
		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate(0,speed * Time.deltaTime , 0);
			anim.SetInteger("dir",0);
			anim.speed=1;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(0,- speed * Time.deltaTime , 0);
			anim.SetInteger("dir",1);
			anim.speed=1;
		}
		else if(Input.GetKey(KeyCode.A))
		{
			transform.Translate(-speed * Time.deltaTime ,0, 0);
			anim.SetInteger("dir",2);
			anim.speed=1;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(speed * Time.deltaTime ,0, 0);
			anim.SetInteger("dir",3);
			anim.speed=1;
		}
		else
		{
		//	Debug.Log("no animation");
			anim.speed=0;
		}


	}
}
