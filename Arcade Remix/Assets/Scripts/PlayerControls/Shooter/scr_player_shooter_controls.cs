﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_player_shooter_controls : MonoBehaviour {
    //touch button to fire 
    public Button button;
    //parent bullet to clone
    public GameObject bullet;
    //Ammo Limmit
    public int ammo = 5;
    //Movement speed
    public float speed = 3;
    //Timer for changing the player direction
    public int alarm = 60;
    public int alarmMin = 35;
    public int alarmMax = 55;
    //reload alarm
    public int alarmRe = -1;

    private Animator anim;

    public int[] goals;
    public Text goalText;
    public GameObject results;

    // Use this for initialization
    void Start () {
        global.goalCounter = 0;
        button.onClick.AddListener(OnShoot);
        ammo = 5;
        GameObject.Find("EventSystem").GetComponent<scr_ui_multiIcon>().OnRefresh(ammo);
        anim = this.GetComponent<Animator>();
        anim.speed = 0;
        scr_game_launcher.winstate = -1;
        goalText.text = "Hit " + goals[global.difficulty - 1] + " Targets!";
        results.GetComponent<scr_ui_results>().next = "scn_game_thrower";
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        //alarm--;
        alarmRe--;
        if (alarm == 0)
        {
            OnFlip();
        }
        if (alarmRe == 0)
        {
            ammo = 5;
            GameObject.Find("EventSystem").GetComponent<scr_ui_multiIcon>().OnRefresh(ammo);
            alarmRe = -1;
        }

        if (transform.position.x >= 2.3)
        {
            speed = Mathf.Abs(speed) * -1;
            alarm = Random.Range(alarmMin, alarmMax);
        }
        if (transform.position.x <= -2.6)
        {
            speed = Mathf.Abs(speed);
            alarm = Random.Range(alarmMin, alarmMax);
        }

        if (global.goalCounter >= goals[global.difficulty - 1])
        {
            global.winner = true;
            results.SetActive(true);
            Destroy(this);
        }
    }

    void OnShoot() {
        if (ammo > 0)
        {
            anim.speed = 2;
            anim.Play("shoot", -1, 0f);
        }
    }

    public void OnFire()
    {
        ammo--;
        GameObject shot = Instantiate(bullet, this.transform.position, bullet.transform.rotation);
        shot.SetActive(true);
        shot.transform.parent = bullet.transform.parent;
        shot.transform.localScale = bullet.transform.localScale;
        GameObject.Find("EventSystem").GetComponent<scr_ui_multiIcon>().OnRefresh(ammo);
        if (ammo >= 0)
        {
            alarmRe = 60;
        }
    }

    void OnFlip() {
        alarm = Random.Range(alarmMin, alarmMax);
        speed *= -1;
    }
}
