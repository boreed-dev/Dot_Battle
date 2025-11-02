using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    private Rigidbody2D opponent;
    private SpriteRenderer sprite;
    Vector3 borders;

    // Start is called before the first frame update
    void Start()
    {
        opponent = GetComponent<Rigidbody2D>();
        borders = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //check distance from ball and move
        if (Ball.balls.transform.position.x > 3)
        {
            if (Ball.balls.transform.position.y > transform.position.y && opponent.velocity.y < 1)
            {
                if ((opponent.transform.position.y + (sprite.bounds.size.y / 2)) < borders.y)
                {
                    opponent.transform.Translate(new Vector3(0, 0.03f, 0));
                }
            }
            else if (Ball.balls.transform.position.y < transform.position.y && opponent.velocity.y > -1)
            {
                if ((opponent.transform.position.y - (sprite.bounds.size.y / 2)) > -borders.y)
                {
                    opponent.transform.Translate(new Vector3(0, -0.03f, 0));
                }
            }
        }
    }
}
