using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Character : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int rocks = 0;
    public float jumpForce = 25;
    public float runForce = 30;
    private bool canJump = true;
    private float startingScale;
    public GameObject sword;
    private bool swordRotated = false;
    public bool right = true;
    private int health = 6;
    private int invinsibleTimer;
    private int invinsibleTime = 80;
    public GameObject player;
    public Image damaged;
    public projectile projectile;
    private bool hasShot;
    private int shotTime;
    private static int shotCoolDown = 25;
    public Text ammoText;
    public Image HF1, HF2, HF3, HH1, HH2, HH3, HE1, HE2, HE3; //heart full/half/empty
    public int warp;
    private float freezeJump = .25f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startingScale = transform.localScale.x;
        sword.SetActive(false);
        hasShot = false;
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
        HF1.enabled = true;
        HF2.enabled = true;
        HF3.enabled = true;
        HH1.enabled = false;
        HH2.enabled = false;
        HH3.enabled = false;
        HE1.enabled = false;
        HE2.enabled = false;
        HE3.enabled = false;
        ammoText.text = "Rocks: " + rocks.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        damaged.color = Color.Lerp(damaged.color, Color.clear, 3f * Time.deltaTime);
        if(rb2d.velocity[0] > .1)
        {
            transform.localScale = new Vector2(-startingScale, transform.localScale.y);
            right = false;
        }
        else if(rb2d.velocity[0] < -.1)
        {
            transform.localScale = new Vector2(startingScale, transform.localScale.y);
            right = true;
        }
        if (Input.GetMouseButtonDown(0) && !swordRotated)
        {
            sword.SetActive(true);
            //sword.transform.Rotate(0, 0, -90);
            if (right)
            {
                sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, -70);
            }
            else
            {
                sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, 100);
            }
            //sword.transform.Rotate(Vector3.right * Time.deltaTime);
            swordRotated = true;
        }
        else if (Input.GetMouseButtonDown(1) && swordRotated)
        {
            sword.SetActive(false);
            //sword.transform.Rotate(0, 0, 90);
            if (right)
            {
                sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, 20);
            }
            else
            {
                sword.transform.eulerAngles = new Vector3(sword.transform.eulerAngles.x, sword.transform.eulerAngles.y, 20);
            }
            //sword.transform.Rotate(Vector3.left * Time.deltaTime);
            swordRotated = false;
        }
        if((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && Input.GetKey("a") && canJump)
        {
            rb2d.velocity = new Vector2(-runForce, jumpForce);
            canJump = false;
        }
        else if ((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && Input.GetKey("d") && canJump)
        {
            rb2d.velocity = new Vector2(runForce, jumpForce);
            canJump = false;
        }
        else if ((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && canJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity[0], jumpForce);
            canJump = false;
        }
        if (Input.GetKey("d"))
        {
            //if (rb2d.velocity[0] < runForce)
            //{
            //rb2d.AddForce(new Vector2(runForce, 0));
            rb2d.velocity = new Vector2(runForce, rb2d.velocity[1]);
            //}
        }
        if (Input.GetKeyUp("d"))
        {
            if (Math.Abs(rb2d.velocity[1]) < .1)
            {
                rb2d.velocity = new Vector2(rb2d.velocity[0] / 10, rb2d.velocity[1]);
            }
        }
        if (Input.GetKey("a"))
        {
            if (Math.Abs(rb2d.velocity[1]) < .1)
            {
                rb2d.velocity = new Vector2(-runForce, rb2d.velocity[1]);
            }
            //}
        }
        if (Input.GetKeyUp("a"))
        {
            if (Math.Abs(rb2d.velocity[1]) < .1)
            {
                rb2d.velocity = new Vector2(rb2d.velocity[0] / 10, rb2d.velocity[1]);
            }
        }
        if (Input.GetKey("e"))
        {
            if (!hasShot)
            {
                if (rocks > 0)
                {
                    projectile clone = (Instantiate(projectile, transform.position, transform.rotation)) as projectile;
                    clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, 1);
                    hasShot = true;
                    shotTime = shotCoolDown;
                    clone.fire(!right);
                    rocks--;
                    ammoText.text = "Rocks: " + rocks.ToString();
                    //clone.rigidbody.AddForce(1000, 0, 0);
                }
            }
        }
        if (invinsibleTimer > 0) invinsibleTimer--;
        if (shotTime > 0) shotTime--;
        if (shotTime == 0) hasShot = false;
    }
    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            canJump = true;
            rb2d.velocity = new Vector2(0, 0);
        }
        else if (col.gameObject.tag == "enemy" && invinsibleTimer == 0)
        {
            health -= 1;
            updateHealth();
            invinsibleTimer = invinsibleTime;
            damaged.color = new Color(1f, 0f, 0f, .2f);
            if(health == 0)
            {
                SceneManager.LoadScene(2);
                Destroy(this);
                player.SetActive(false);
            }
        }
        else if(col.gameObject.tag == "rock")
        {
            if (invinsibleTimer == 0)
            {
                health -= 1;
                updateHealth();
                invinsibleTimer = invinsibleTime;
                damaged.color = new Color(1f, 0f, 0f, .2f);
                if (health == 0)
                {
                    SceneManager.LoadScene(2);
                    Destroy(this);
                    player.SetActive(false);
                }
            }
        }
        else if(col.gameObject.tag == "goodRock")
        {
            rocks++;
            ammoText.text = "Rocks: " + rocks.ToString();
        }
        else if(col.gameObject.tag == "lava" && invinsibleTimer == 0)
        {
            health -= 1;
            updateHealth();
            invinsibleTimer = invinsibleTime;
            damaged.color = new Color(1f, 0f, 0f, .2f);
            if (health == 0)
            {
                SceneManager.LoadScene(2);
                Destroy(this);
                player.SetActive(false);
            }
        } else if(col.gameObject.tag == "warp")
        {
            SceneManager.LoadScene(warp);
            Destroy(this);
            player.SetActive(false);
        }
    }
    private void updateHealth()
    {
        if(health == 0)
        {
            HF1.enabled = false; 
            HF2.enabled = false;
            HF3.enabled = false;
            HH1.enabled = false;
            HH2.enabled = false;
            HH3.enabled = false;
            HE1.enabled = false;
            HE2.enabled = false;
            HE3.enabled = false;
        }
        else if(health == 1)
        {
            HF1.enabled = false;
            HF2.enabled = false;
            HF3.enabled = false;
            HH1.enabled = true;
            HH2.enabled = false;
            HH3.enabled = false;
            HE1.enabled = false;
            HE2.enabled = true;
            HE3.enabled = true;
        }
        else if (health == 2)
        {
            HF1.enabled = true;
            HF2.enabled = false;
            HF3.enabled = false;
            HH1.enabled = false;
            HH2.enabled = false;
            HH3.enabled = false;
            HE1.enabled = false;
            HE2.enabled = true;
            HE3.enabled = true;
        }
        else if (health == 3)
        {
            HF1.enabled = true;
            HF2.enabled = false;
            HF3.enabled = false;
            HH1.enabled = false;
            HH2.enabled = true;
            HH3.enabled = false;
            HE1.enabled = false;
            HE2.enabled = false;
            HE3.enabled = true;
        }
        else if (health == 4)
        {
            HF1.enabled = true;
            HF2.enabled = true;
            HF3.enabled = false;
            HH1.enabled = false;
            HH2.enabled = false;
            HH3.enabled = false;
            HE1.enabled = false;
            HE2.enabled = false;
            HE3.enabled = true;
        }
        else if (health == 5)
        {
            HF1.enabled = true;
            HF2.enabled = true;
            HF3.enabled = false;
            HH1.enabled = false;
            HH2.enabled = false;
            HH3.enabled = true;
            HE1.enabled = false;
            HE2.enabled = false;
            HE3.enabled = false;
        }
        else if (health == 6)
        {
            HF1.enabled = true;
            HF2.enabled = true;
            HF3.enabled = true;
            HH1.enabled = false;
            HH2.enabled = false;
            HH3.enabled = false;
            HE1.enabled = false;
            HE2.enabled = false;
            HE3.enabled = false;
        }
    }
}
