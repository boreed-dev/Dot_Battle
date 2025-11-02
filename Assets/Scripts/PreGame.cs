using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PreGame : MonoBehaviour
{
    private Scene load;
    public bool animationTerminated;
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.UnloadSceneAsync(1);
        animationTerminated = false;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTerminated)
        {
            load = SceneManager.GetSceneByName("Menu");
            if (load.name == null)
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(4);
        }
            
    }

    public void returnToMenu()
    {
        myAnim.Play("FadeOut");
    }
}
