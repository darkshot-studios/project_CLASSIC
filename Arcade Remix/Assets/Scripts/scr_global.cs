﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global : MonoBehaviour {
    public static int scoreMain = 0;
    public static int scoreBaseball = 0;
    public static int scoreBasketball = 0;
    public static int scoreColor = 0;
    public static int scoreDodger = 0;
    public static int scoreGliding = 0;
    public static int scoreGolf = 0;
    public static int scoreJumper = 0;
    public static int scoreRacing = 0;
    public static int scoreRunner = 0;
    public static int scoreShooter = 0;
    public static int scoreSkii = 0;
    public static int scoreTennis = 0;
    public static int scoreThrower = 0;

    public static int timelimit = 5;
    public static int timeSec = 5;
    public static int lives = 3;

    public static int wincount = 0;
    public static int gamecount = 0;

    public static int goalCounter = 0;

    public static bool winner = false;
    public static List<string> games = new List<string>(new string[] {
        "scn_game_baseball",
        "scn_game_basketball",
        "scn_game_dodger",
        "scn_game_gliding",
        "scn_game_golf",
        "scn_game_jumper",
        "scn_game_runner",
        "scn_game_shooter",
        "scn_game_skii",
        "scn_game_tennis",
        "scn_game_thrower",
        });
    public static List<string> beatGames = new List<string>();
}
