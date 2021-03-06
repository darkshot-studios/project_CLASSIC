﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class playerMovement : MonoBehaviour 
{

	private float speed = 7.0f;
	private Vector3 pos;
	private Transform tr;

	public bool lost;

	private Animator anim;
    private SpriteRenderer sprite;

    public GameObject results;
    public void Awake()
    {
        SimpleGesture.On4AxisSwipeRight(SwipeRight);
        SimpleGesture.On4AxisSwipeDown(SwipeDown);
        SimpleGesture.On4AxisSwipeLeft(SwipeLeft);
        SimpleGesture.On4AxisSwipeUp(SwipeUp);
    }

    void Start() 
	{
		anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
		pos = transform.position;
		tr = transform;
		lost = false;
        global.winner = true;
    }

	void Update() 
	{
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SwipeUp();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SwipeDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwipeLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwipeRight();
        }

        var mousePos = Input.mousePosition;
		// Check to see if the player lost
		if ((pos.x >= 3) || 
			(pos.x <= -3) ||
			(pos.y >= 3) ||
			(pos.y <= -5))
			lost = true;

		if (lost)
		{
			transform.Rotate( new Vector3(0, 0, 500) * Time.deltaTime);
            // falling effect
            if (transform.localScale.x >= 0 && transform.localScale.y >= 0)
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.0f);
            else
            {
                // Restart when completely shrunk
                results.SetActive(true);
                Destroy(this);
            }

                
				
		}
		else
		{
            if (Input.GetMouseButtonDown(0))
            {
                print(mousePos + " ||| " + Screen.width + " width|height" + Screen.height);
                print((Screen.height * .7) + " .7 |||  .5 " + (Screen.height * .4));
                if (mousePos.y >= (Screen.height * .4f))
                {
                    SwipeUp();
                }
                else if (mousePos.y <= (Screen.height * .2f))
                {
                    SwipeDown();
                }
                else
                {
                // now in between up and down... gotta do same for middle
                    if (mousePos.x >= (Screen.width * .5))
                    {
                        sprite.flipX = true;
                        SwipeRight();
                    }
                    else 
                    {
                        sprite.flipX = false;
                        SwipeLeft();
                    }
                }
            }

			/*if (Input.GetKeyDown(KeyCode.D))
			{
                sprite.flipX = true;
                SwipeRight();
			}
			else if (Input.GetKeyDown(KeyCode.A)) 
			{
                sprite.flipX = false;
                SwipeLeft();
			}
			else if (Input.GetKeyDown(KeyCode.W)) 
			{
                SwipeUp();
			}
			else if (Input.GetKeyDown(KeyCode.S)) 
			{
                SwipeDown();
			}
			//else
				//anim.SetBool("moving", false);*/
		}
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
	}   

	// HANDLE THE COLLISION HERE
    void OnTriggerEnter2D(Collider2D other)
    {
    	if (!lost)
    	{
            global.winner = false;
            results.SetActive(true);
            ballMovement ballScript = other.GetComponent<ballMovement>();
	    	int dir = ballScript.dir;

	        if (other.gameObject.tag == "ball")
	    	{
	    		Destroy(other.gameObject);
	    		if (dir == 0)
	    			pos += Vector3.up * 2;
	            else if (dir == 1)
	            {
	            	if (pos.y == -3)
	            		pos += Vector3.down * 3;
	            	else
	            		pos += Vector3.down * 2;
	            }
	            else if (dir == 2)
	            	pos += Vector3.right * 2;
	            else
	            	pos += Vector3.left * 2;
	        }
    	}
    }

    public void SwipeUp()
    {
        if (tr.position == pos)
        {
            anim.Play("anim_dodge_down", -1, 0f);
            pos += Vector3.down;
        }
    }
    public void SwipeDown()
    {
        if (tr.position == pos)
        {
            anim.Play("anim_dodge_up", -1, 0f);
            pos += Vector3.up;

        }
    }
    public void SwipeLeft()
    {
        if (tr.position == pos)
        {
            anim.Play("jump", -1, 0f);
            pos += Vector3.left;
        }
    }
    public void SwipeRight()
    {
        if(tr.position == pos)
        {
            anim.Play("jump", -1, 0f);
            pos += Vector3.right;
        }
    }
}
