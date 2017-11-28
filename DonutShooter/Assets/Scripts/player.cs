using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DonutShooter.Base;

public class player : MonoBehaviour
{
    public ColorState m_ColorState=ColorState.None;
    public GameObject ballSprite;
    private GameObject score;
    public bool isRolling = false;
    public bool towardsRight = false;
    public float movespeedx;
    public float movespeedy;
    private Rigidbody2D rb;
    private SpriteRenderer donutcate;
    public GameObject donut;
    public GameObject donut2;
    public GameObject donut3;
    private int donutType = 1;
    public int shootrate = 1;
    private int shoottimer;
    public int donutnum;
    public int refill;
    public TextMesh donutdisplay;
    [Header("indicator")]
    public Sprite do1;
    public Sprite do2;
    public Sprite do3;

	// Use this for initialization
	void Start () {
        score = GameObject.Find("scorer");
        rb = GetComponent<Rigidbody2D>();
        donutcate = GameObject.Find("indicator").GetComponent<SpriteRenderer>();
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.collider.gameObject;
        if (hitObject.tag == "reload")
        {
            m_ColorState = ColorState.Red;
            donutType = 1;
            donutnum = refill;
            donutcate.sprite = do1;
            score.SendMessage("reload");
        }
        if (hitObject.tag == "reload2")
        {
            m_ColorState = ColorState.Green;
            donutType = 2;
            donutnum = refill;
            donutcate.sprite = do2;
            score.SendMessage("reload");

        }
        if (hitObject.tag == "reload3")
        {
            m_ColorState = ColorState.Blue;
            donutType = 3;
            donutnum = refill;
            donutcate.sprite = do3;
            score.SendMessage("reload");
        }
        if (hitObject.GetComponent<ReturnArea>())
        {
            isRolling = true;
            towardsRight = false;
            rb.AddForce(new Vector2(-100f,0));
        }
        if (hitObject.GetComponent<BaseArea>())
        {
            isRolling = false;
            towardsRight = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GameObject hitObject = collider.gameObject;
        if (hitObject.GetComponent<BaseArea>())
        {
            isRolling = true;
            towardsRight = true;
            rb.AddForce(new Vector2(100f,0));
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject hitObject = collider.gameObject;
        if (hitObject.GetComponent<BaseArea>())
        {
            isRolling = false;
            towardsRight = true;
        }
    }

    // Update is called once per frame
    void Update () {
        donutdisplay.text = donutnum.ToString();
        //shoottimer += 1;
        //movement
        if (!isRolling)//(transform.position.x <= -6.5f))
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(0, movespeedy);

            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(0, -movespeedy);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-movespeedx, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(movespeedx, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
            ballSprite.transform.eulerAngles = Vector3.zero;
        }
        else
        {
            float upspeed = 0f, rightspeed = 0f;
            if (Input.GetKey(KeyCode.W))
            {
                upspeed+=movespeedy*Time.deltaTime;

            }
            else if (Input.GetKey(KeyCode.S))
            {
                upspeed+=-movespeedy*Time.deltaTime;

            }
            
            if (Input.GetKey(KeyCode.A))
            {
                rightspeed= -movespeedx*Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rightspeed= movespeedx*Time.deltaTime;
            }
            if (towardsRight)
            {
                rb.velocity = new Vector2(rb.velocity.x+rightspeed,rb.velocity.y+ upspeed);
                ballSprite.transform.eulerAngles = new Vector3(0,0, ballSprite.transform.eulerAngles.z-10);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x+rightspeed,rb.velocity.y+ upspeed);
                ballSprite.transform.eulerAngles = new Vector3(0,0, ballSprite.transform.eulerAngles.z+10);
            }
        }
        // shooting donut
        if (Input.GetKey(KeyCode.Space)&&(donutnum>0))
        {
            if(Time.time-BaseValue.lastTimeShot>BaseValue.shootingTimeGap)
            //the greater the number, the slower of shoot rate;kinda anti-intuitive...
            //if (shoottimer%shootrate==0)
            {
                BaseValue.lastTimeShot = Time.time;
                score.SendMessage("shoot");
                if (donutType == 1)
                {
                    Vector3 newPos = new Vector3(rb.position.x+1.0f, rb.position.y, 0);
                    Instantiate(donut, newPos, Quaternion.identity);
                    donutnum -= 1;
                    shoottimer = 0;
                }
                if (donutType == 2)
                {
                    Vector3 newPos = new Vector3(rb.position.x + 1.0f, rb.position.y, 0);
                    Instantiate(donut2, newPos, Quaternion.identity);
                    donutnum -= 1;
                    shoottimer = 0;
                }
                if (donutType == 3)
                {
                    Vector3 newPos = new Vector3(rb.position.x + 1.0f, rb.position.y, 0);
                    Instantiate(donut3, newPos, Quaternion.identity);
                    donutnum -= 1;
                    shoottimer = 0;
                }
            }
        }

		
	}
}
