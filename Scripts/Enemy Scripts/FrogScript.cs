using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour {

	private Animator anim;
	private bool animation_Started;
	private bool animation_Finished;
	private int jumpedTimes;
	private bool jumpLeft = true;
	private string coroutine_Name = "FrogJump";
    public LayerMask playerLayer;
	private GameObject player;
	public Transform top_Collision;

	void Awake(){
		anim = GetComponent<Animator> ();
	}
	void Start () {
		StartCoroutine (coroutine_Name);
		player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
	}

	void Update () {
       if(Physics2D.OverlapCircle(transform.position,0.5f, playerLayer)){
         player.GetComponent<PlayerDamage> ().DealDamage ();
	   }

	   CheckCollision();
	}
	
	void LateUpdate () {
		if(animation_Finished && animation_Started) {
			animation_Started = false;

			transform.parent.position = transform.position;
			transform.localPosition = Vector3.zero;
		}
	}

	IEnumerator FrogJump(){
		yield return new WaitForSeconds (Random.Range(1f, 4f));

		animation_Started = true;
		animation_Finished = false;

		jumpedTimes++;

		if(jumpLeft){
          anim.Play ("FrogJumpLeft");
		}
		else{
			anim.Play("FrogJumpRight");

		}

		StartCoroutine (coroutine_Name);
	}

	void AnimationFinished(){

		animation_Finished = true;

		if(jumpLeft){
          anim.Play ("FrogIdleLeft");
		}
		else{
			anim.Play("FrogIdleRight");

		}

		if(jumpedTimes == 3){
           jumpedTimes = 0;

		   Vector3 tempScale = transform.localScale;
		   tempScale.x *= -1f;
		   transform.localScale = tempScale;

		   jumpLeft = !jumpLeft;
		}
	}
	void OnTriggerEnter2D(Collider2D target){
		if(target.tag == MyTags.BULLET_TAG){
			anim.Play("FrogDead");
            StartCoroutine(FrogDead());
		}
	}

	void CheckCollision(){
      Collider2D topHit = Physics2D.OverlapCircle (top_Collision.position, 0.2f, playerLayer);

	   if(topHit != null){
		   if(topHit.gameObject.tag == MyTags.PLAYER_TAG){
			   	 topHit.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                 anim.Play("FrogDead");
                StartCoroutine(FrogDead());
			
		   }
	   }
	}
	IEnumerator FrogDead(){
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);
	}
} // class


















