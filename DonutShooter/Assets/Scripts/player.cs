using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DonutShooter.Base;

public class player : MonoBehaviour
{
    public ColorState m_ColorState=ColorState.None;
    public GameObject ballSprite;
    public SpriteRenderer m_spriteRenderer;
    private GameObject score;
    public bool isRolling = false;
    public bool towardsRight = false;
    public float movespeedx;
    public float movespeedy,rollspeedy;
    private Rigidbody2D rb;
    private SpriteRenderer donutcate;
    public GameObject donut;
    public GameObject donut2;
    public GameObject donut3;
    public int shootrate = 1;
    private int shoottimer;
    public int magazinedonutnum = 0,extradonutnum = 0;
    public int donutnum;
    public int refillnum;
    public TextMesh donutdisplay;
    [Header("indicator")]
    public Sprite do1;
    public Sprite do2;
    public Sprite do3;
    public float donutspeed = 30f;
    public Color[] m_color;

	// Use this for initialization
	void Start () {
        score = GameObject.Find("scorer");
        rb = GetComponent<Rigidbody2D>();
        donutcate = GameObject.Find("indicator").GetComponent<SpriteRenderer>();
	}

    public void ChangeColorState(ColorState colorState)
    {
        m_ColorState = colorState;

        //donutnum += refillnum - magazinedonutnum;
        //magazinedonutnum = refillnum;
        switch (colorState)
        {
            case ColorState.Red: 
                donutcate.sprite = do1;
                m_spriteRenderer.color = m_color[0];
                donutnum += refillnum - magazinedonutnum;
                magazinedonutnum = refillnum;
                break;
            case ColorState.Green: 
                donutcate.sprite = do2;
                m_spriteRenderer.color = m_color[1];
                donutnum += refillnum - magazinedonutnum;
                magazinedonutnum = refillnum;
                break;
            case ColorState.Blue: 
                donutcate.sprite = do3;
                m_spriteRenderer.color = m_color[2];
                donutnum += refillnum - magazinedonutnum;
                magazinedonutnum = refillnum;
                break;
            case ColorState.None: 
                donutcate.sprite = do1;
                m_spriteRenderer.color = Color.white;
                magazinedonutnum = extradonutnum = donutnum = 0;
                break;
        }
        
        //score.SendMessage("reload");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.collider.gameObject;
        if (hitObject.GetComponent<reloadpointbehave>())
        {
            ChangeColorState(hitObject.GetComponent<reloadpointbehave>().m_ColorState);
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
        if (hitObject.GetComponent<donut>())
        {
            hitObject.GetComponent<donut>().SelfDestroy();
            extradonutnum++;
            donutnum = magazinedonutnum + extradonutnum;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GameObject hitObject = collider.gameObject;
        if (hitObject.GetComponent<BaseArea>())
        {
            isRolling = true;
            towardsRight = true;
            rb.gravityScale = 1;
            rb.AddForce(new Vector2(150f,0));
            //rb.isKinematic = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject hitObject = collider.gameObject;
        if (hitObject.GetComponent<BaseArea>())
        {
            isRolling = false;
            towardsRight = true;
            rb.gravityScale = 0;
            //rb.isKinematic = true;
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
            if (Math.Abs(rb.velocity.x) > 10f&&Math.Abs(rb.velocity.y) < 10f)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    upspeed += rollspeedy * Time.deltaTime;

                }
                else if (Input.GetKey(KeyCode.S))
                {
                    upspeed += -rollspeedy * Time.deltaTime;

                }
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
        if (Input.GetKey(KeyCode.Mouse0)&&(magazinedonutnum+extradonutnum>0))
        {
            if(Time.time-BaseValue.lastTimeShot>BaseValue.shootingTimeGap)
            //the greater the number, the slower of shoot rate;kinda anti-intuitive...
            //if (shoottimer%shootrate==0)
            {
                BaseValue.lastTimeShot = Time.time;
                score.SendMessage("shoot");
                GameObject shotDonut=null;
                Vector3 newPos = new Vector3(rb.position.x+1.5f, rb.position.y, 0);
                if (m_ColorState == ColorState.Red)
                {
                    shotDonut= Instantiate(donut, newPos, Quaternion.identity);
                    if (extradonutnum > 0)
                    {
                        extradonutnum--;
                    }
                    else
                    {
                        magazinedonutnum--;
                    }
                    donutnum = magazinedonutnum+extradonutnum;
                    shoottimer = 0;
                    
                }
                if (m_ColorState == ColorState.Green)
                {
                    shotDonut=Instantiate(donut2, newPos, Quaternion.identity);
                    if (extradonutnum > 0)
                    {
                        extradonutnum--;
                    }
                    else
                    {
                        magazinedonutnum--;
                    }
                    donutnum = magazinedonutnum+extradonutnum;
                    shoottimer = 0;
                }
                if (m_ColorState == ColorState.Blue)
                {
                    shotDonut=Instantiate(donut3, newPos, Quaternion.identity);
                    if (extradonutnum > 0)
                    {
                        extradonutnum--;
                    }
                    else
                    {
                        magazinedonutnum--;
                    }
                    donutnum = magazinedonutnum+extradonutnum;
                    shoottimer = 0;
                }
                if(shotDonut!=null)
                shotDonut.GetComponent<donut>().InitDonut(Camera.main.ScreenToWorldPoint(Input.mousePosition),donutspeed);
            }
            if (donutnum <= 0)
            {
                ChangeColorState(ColorState.None);
            }
        }

		
	}
}
