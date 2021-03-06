﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_player_runner_controls : MonoBehaviour {
    //Movement speed
    public int speed = 3;
    public int jspeed = 7;
    private Animator anim;

    public int[] goals;
    public GameObject results;
    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        global.winner = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space")) { OnJump(); }
        if (Input.GetKeyUp("space")) { OnDrop(); }

        if (this.GetComponent<Rigidbody2D>().velocity.y == 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            anim.Play("run", -1, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RunnerEnemy" && global.winner != true)
        {
            collision.gameObject.GetComponent<scr_enemy_runner_jumping>().goal.SetActive(false);
            if (this.GetComponent<scr_mod_iframes>().alarm>-1)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision, false);
            }
            else
            {
                this.GetComponent<scr_mod_iframes>().OnStart(60);               
            }
            global.winner = false;
            results.SetActive(true);
        }
        if (collision.gameObject.tag == "RunnerGoal" && GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
        {
            global.scoreRunner += 500;
        }
    }

    public void OnJump()
    {
        if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jspeed);
            anim.Play("jump", -1, 0f);
        }
    }
    public void OnDrop()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
    }
}
