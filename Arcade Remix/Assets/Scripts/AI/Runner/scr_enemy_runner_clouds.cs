﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemy_runner_clouds : MonoBehaviour {
    Collider2D m_Collider;
    Vector3 m_Center;
    Vector3 m_Size, m_Min, m_Max;
    public GameObject clouds;
    public int alarm = 60;
    // Use this for initialization
    void Start () {
        //Fetch the Collider from the GameObject
        m_Collider = GetComponent<Collider2D>();
        //Fetch the center of the Collider volume
        m_Center = m_Collider.bounds.center;
        //Fetch the size of the Collider volume
        m_Size = m_Collider.bounds.size;
        //Fetch the minimum and maximum bounds of the Collider volume
        m_Min = m_Collider.bounds.min;
        m_Max = m_Collider.bounds.max;
    }

    // Update is called once per frame
    void Update () {
        alarm--;
        if (alarm == 0)
        {
            alarm = 60;
            GameObject cloudokun = Instantiate(clouds);
            cloudokun.transform.position = new Vector2(Random.Range(m_Min.x, m_Max.x), Random.Range(m_Min.y, m_Max.y));
            cloudokun.SetActive(true);
        }
	}
}
