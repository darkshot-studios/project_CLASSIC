﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour 
{

	private Animator anim;

	public GameObject pow;
	private power scri;

	private int count = 0;
	// Use this for initialization
	void Start () 
	{
        //GameObject.Find("EventSystem").GetComponent<scr_ui_multiIcon>().OnRefresh(0);
        anim = GetComponent<Animator>();
		scri = pow.GetComponent<power>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (global.goalCounter >= 10)
		{
			if (count > 50)
				anim.SetBool("THROW", false);
			else
			{
				anim.SetTrigger("THROW");
				count++;
			}
		}
	}
}
