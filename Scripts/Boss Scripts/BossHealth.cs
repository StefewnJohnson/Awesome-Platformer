using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

	private Animator anim;
	private int health = 10;
    private bool canDamage;

	void Awake () {
      anim = GetComponent<Animator> ();
	  canDamage = true;
	}
	IEnumerator WaitForDamage() {
		yield return new WaitForSeconds (2f);
		canDamage = true;
	} 
	
	void OnTriggerEnter2D(Collider2D target){
	if(canDamage){
		if(target.tag == MyTags.BULLET_TAG) {
			
           health--;
		   canDamage = false;

		   if(health == 0){
			   GetComponent<Boss4Script>().DeactivateBossScript();
			   anim.Play ("BossDead");
			   StartCoroutine (DeactivateBoss());
		   }

		   StartCoroutine (WaitForDamage());
		}
	  }
	}

	IEnumerator DeactivateBoss(){
		yield return new WaitForSeconds (3f);
		gameObject.SetActive(false);
	}


} // class



























