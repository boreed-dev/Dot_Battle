using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;
    private SpriteRenderer sprite;
    Vector3 borders;
    FSM.gamestate state;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        borders = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        state = FSM.fsm.state;
        switch (state)
        {
            case FSM.gamestate.play:
                if (Input.GetKey(KeyCode.W))
                {
                    if (player.transform.position.y + (sprite.bounds.size.y / 2) < borders.y)
                    {
                        player.transform.Translate(new Vector3(0, 0.02f, 0));
                    }
                }

                if (Input.GetKey(KeyCode.S))
                {
                    if (player.transform.position.y - (sprite.bounds.size.y / 2) > -borders.y)
                    {
                        player.transform.Translate(new Vector3(0, -0.02f, 0));
                    }
                }
                break;
        }

    }
}
