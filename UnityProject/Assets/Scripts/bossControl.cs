using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class bossControl : MonoBehaviour
{
    //My ideas are as follows:
    //The boss does not move from the side of the screen
    //It fires fireballs and flaming stones at you
    //Flaming stones will cool down, allowing you to pick them up and throw them at the boss
    //Touching the boss will hurt you

    public Rigidbody2D rb2d;
    private float startingScale;
    public GameObject boss;
    private int health = 50;
    private int maxHealth = 50;
    public int attack = 0;
    public bool idle = true;
    private int hiddenTimer = 0;
    private int chargeTimer = 0;
    private int attaTimer = 0;
    public GameObject fireSpawn;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startingScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0 && hiddenTimer == 0)
        {
            Destroy(gameObject);
            boss.SetActive(false);
        }
        else
        {
            if(!(attaTimer > 0 || chargeTimer > 0 || hiddenTimer > 0))
            {
                if(idle)
                {
                    switch(attack)
                    {
                        case 0: //Charge
                            chargeTimer = 10;
                            idle = false;
                            break;

                        case 1: //Fireball
                            attaTimer = 10;
                            idle = false;
                            break;

                        case 2: //Platform Sink
                            attaTimer = 20;
                            idle = false;
                            break;

                        case 3: //Rockslide
                            attaTimer = 15;
                            idle = false;
                            break;

                        default:
                            hiddenTimer = 5;
                            idle = true;
                            break;
                    }
                }
                else
                {
                    attack = attackChoose(attack, health, maxHealth);
                    if(!(attack == 1))
                    {
                        hiddenTimer = 5;
                        idle = true;
                    }
                }
            }
            if(attaTimer > 0) attaTimer--;
            if(chargeTimer > 0) chargeTimer--;
            if(hiddenTimer > 0) hiddenTimer--;
        }
    }
    int attackChoose(int prevAttack, int health, int maxHealth)
    {
        switch(prevAttack)
        {
            case 0: //Charge
                return (1);
                break;

            case 1: //Fireball
                if((float)health/maxHealth < .25)
                {
                    return (3);
                }
                else if((float)health/maxHealth < .5)
                {
                    return (2);
                }
                else
                {
                    return (0);
                }
                break;

            case 2: //Platform Sink
                return (0);
                break;

            case 3: //Rockslide
                return (0);
                break;

            default:
                return (0);
                break;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "goodRock" && chargeTimer > 0)
        {
            health-=5;
            hiddenTimer = 5;
            chargeTimer = 0;
        }
    }
}
