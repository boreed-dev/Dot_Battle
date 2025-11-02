using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Scene load;

    public float timing = 0;

    private Animator myAnim;
    public bool hs_animationTerminated;
    public bool ng_animationTerminated;
    public bool htp_animationTerminated;

    // Start is called before the first frame update
    void Start()
    {
        hs_animationTerminated = false;
        ng_animationTerminated = false;
        htp_animationTerminated = false;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timing = Time.time;
        
        if(hs_animationTerminated)
        {
            load = SceneManager.GetSceneByName("HighScore");
            if (load.name == null)
                SceneManager.LoadScene(3, LoadSceneMode.Additive); //High score scene
        }
            
        if (ng_animationTerminated)
        {
            load = SceneManager.GetSceneByName("Main");
            if (load.name == null)
                SceneManager.LoadScene(2, LoadSceneMode.Additive); //Main scene
        }
            
        if (htp_animationTerminated)
        {
            load = SceneManager.GetSceneByName("PreGame");
            if (load.name == null)
                SceneManager.LoadScene(4, LoadSceneMode.Additive); //preGame scene
        }
            
    }

    public void NewGameScene()
    {
        myAnim.Play("FadeOutNg");
    }

    public void HighScoreScene()
    {
        myAnim.Play("FadeOut");
    }

    public void HowToPlayScene()
    {
        myAnim.Play("FadeOutHTP");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
