using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    private Scene load;

    // Start is called before the first frame update
    void Start()
    {
        
        SceneManager.UnloadSceneAsync(1);
    }

    // Update is called once per frame
    void Update()
    {
        switch (FSM.fsm.state)
        {
            case FSM.gamestate.init:
                //wai until space is pressed (hud message blinking)
                break;

            case FSM.gamestate.play:
                if (Input.GetKey(KeyCode.Escape))
                    FSM.fsm.state = FSM.gamestate.pause;
                break;

            case FSM.gamestate.pause:
                if (Input.GetKey(KeyCode.Escape))
                {
                    Ball.balls.RestoreVelocity();
                    FSM.fsm.state = FSM.gamestate.play;
                }
                break;

            case FSM.gamestate.end:
                load = SceneManager.GetSceneByName("Win");
                if(load.name == null)
                    SceneManager.LoadScene(5, LoadSceneMode.Additive);
                break;
        }

    }
}
