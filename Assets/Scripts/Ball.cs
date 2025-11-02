using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    int x_axis;
    int y_axis;
    int speed;
    int x;
    int y;

    Vector2 temp_velocity;

    private Rigidbody2D ball;
    public static Ball balls;

    [SerializeField] private AudioClip scorePlayer;
    [SerializeField] private AudioClip scoreOpponent;
    [SerializeField] private AudioClip wallBounce;
    [SerializeField] private AudioClip blockBounce;
    private AudioSource effects;

    FSM.gamestate state;

    // Start is called before the first frame update
    void Start()
    {
        effects = GetComponent<AudioSource>();
        speed = 2;
        balls = this;
    }

    // Update is called once per frame
    void Update()
    {
        state = FSM.fsm.state;
        switch (state)
        {
            case FSM.gamestate.init:
                if (Input.GetKeyDown(KeyCode.Space))
                    StartGame();
                break;
            case FSM.gamestate.play:
                temp_velocity = ball.velocity;
                break;
            case FSM.gamestate.pause:
                ball.velocity = Vector2.zero;
                break;
        }
        FSM.fsm.state = state;
    }

    void StartGame()
    {
        //right or left
        x_axis = Random.Range(0, 2);
        //up or down
        y_axis = Random.Range(0, 2);

        //create rigidbody
        ball = GetComponent<Rigidbody2D>();

        //calculate y axis
        if (y_axis == 0)
            y = Random.Range(1, 4);
        else
            y = Random.Range(-4, -1);


        if (y != 0)
        {
            if (x_axis == 0)
                x = Random.Range(1, 4);
            else
                x = Random.Range(-4, -1);
        }
        else
        {
            if (x_axis == 0)
                x = Random.Range(2, 4);
            else
                x = Random.Range(-4, -2);
        }

        ball.AddForce(new Vector3(x, y, 0) * speed, ForceMode2D.Impulse);
        state = FSM.gamestate.play;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UpLine") || collision.gameObject.CompareTag("DownLine"))
        {
            
            ball.velocity = new Vector2(0, 0);
            y = -y;
            ball.AddForce(new Vector3(x, y, 0) * speed, ForceMode2D.Impulse);
            effects.clip = wallBounce;
            effects.Play();

        }
        else if (collision.gameObject.CompareTag("SXGoalLine"))
        {
            ResetGame();
            Hud.hud.pointopponent += 1;
            state = FSM.gamestate.init;
            effects.clip = scoreOpponent;
            effects.Play();
        }
        else if (collision.gameObject.CompareTag("DXGoalLine"))
        {
            ResetGame();
            Hud.hud.pointplayer += 1;
            state = FSM.gamestate.init;
            effects.clip = scorePlayer;
            effects.Play();
        }
        else if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Opponent"))
        {
            ball.velocity = new Vector2(0, 0);
            ball.rotation = 0;
            x = -x;
            ball.AddForce(new Vector3(x, y, 0) * speed, ForceMode2D.Impulse);
            effects.clip = blockBounce;
            effects.Play();
        }
        FSM.fsm.state = state;
    }

    private void ResetGame()
    {
        ball.transform.position = new Vector3(0, 0, -1);
        ball.velocity = new Vector2(0, 0);
    }

    public void RestoreVelocity()
    {
        ball.velocity = temp_velocity;
    }

}
