using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class Hud : MonoBehaviour
{
    public TMP_Text pointPlayer;
    public TMP_Text pointOpponent;
    public TMP_Text pressText;

    public int pointplayer;
    public int pointopponent;
    int maxpoint;

    public static Hud hud;

    FSM.gamestate state;

    // Start is called before the first frame update
    void Start()
    {
        hud = this;
        pointplayer = 0;
        pointopponent = 0;
        maxpoint = 5;
    }

    // Update is called once per frame
    void Update()
    {
        state = FSM.fsm.state;
        switch (state)
        {
            case FSM.gamestate.init:
                if (pointopponent == maxpoint || pointplayer == maxpoint)
                    state = FSM.gamestate.end;
                else
                    pressText.enabled = true; 
                break;
            case FSM.gamestate.play:
                    pressText.enabled = false;
                break;
            case FSM.gamestate.pause:
                //display pause menu
                break;
            case FSM.gamestate.end:
                pressText.enabled = false;
                break;
        }
        pointPlayer.text = pointplayer.ToString();
        pointOpponent.text = pointopponent.ToString();
        FSM.fsm.state = state;
    }
}
