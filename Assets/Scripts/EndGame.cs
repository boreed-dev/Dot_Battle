using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TMP_Text win;
    public TMP_Text lose;

    public bool animationTerminated;
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        if (Hud.hud.pointopponent > Hud.hud.pointplayer)
            lose.enabled = true;
        else
            win.enabled = true;

        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animationTerminated)
        {
            SceneManager.UnloadSceneAsync(2);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);

            Hud.hud.pointopponent = 0;
            Hud.hud.pointplayer = 0;
            FSM.fsm.state = FSM.gamestate.init;

            SceneManager.UnloadSceneAsync(5);
        }
    }

    public void ReturnToMenu()
    {
        myAnim.Play("FadeOut");
    }
}
