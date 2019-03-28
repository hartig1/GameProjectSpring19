using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int rocks = 0;
    private float jumpForce = 15;
    private float runForce = 25;
    private bool canJump = true;
    private float startingScale;
    public GameObject sword;
    private bool swordRotated = false;
    public bool right = true;
    private int health = 10;
    private int invinsibleTimer;
    public GameObject player;
    public Slider slider;
    public Image damaged;
    public projectile projectile;
    private bool hasShot;
    private int shotTime;
    private static int shotCoolDown = 25;
    public Text ammoText;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startingScale = transform.localScale.x;
        sword.SetActive(false);
        slider.maxValue = health;
        slider.value = health;
        hasShot = false;
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
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && canJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity[0], jumpForce);
            canJump = false;
        }
        if (Input.GetKey("d"))
        {
            if (rb2d.velocity[0] < runForce)
            {
                rb2d.AddForce(new Vector2(runForce, 0));
            }
        }
        if (Input.GetKey("a"))
        {
            if (rb2d.velocity[0] > -runForce)
            {
                rb2d.AddForce(new Vector2(-runForce, 0));
            }
        }
        if (Input.GetKey("e"))
        {
            if (!hasShot)
            {
                if (rocks > 0)
                {
                    projectile clone = (Instantiate(projectile, transform.position, transform.rotation)) as projectile;
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
        }
        else if (col.gameObject.tag == "enemy" && invinsibleTimer == 0)
        {
            health -= 1;
            slider.value = health;
            invinsibleTimer = 5;
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
                slider.value = health;
                invinsibleTimer = 5;
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
    }
}
