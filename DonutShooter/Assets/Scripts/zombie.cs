using System.Collections;
using System.Collections.Generic;
using DonutShooter.Base;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class zombie : MonoBehaviour {
    public ColorState m_ColorState = ColorState.Red;
    public bool hitten = false,isMoving=false;
    float flip;
    public int hitpoints;
    //public TextMesh hittext;
    public Text hittext;
    public float movingSpeed;
    GameObject score;
    Collider2D m_collider;
    bool hitbyplayer;
    public GameObject blood;
    public GameObject love,explosionPrefab;
    GameObject love1;


	// Use this for initialization
	void Start () {
        score = GameObject.Find("scorer");
        m_collider = GetComponent<Collider2D>();
        hitbyplayer = false;

		
	}
	
	// Update is called once per frame
	void Update () {
        if (hitbyplayer==false)
        {
          transform.Translate(-movingSpeed*Time.deltaTime, 0, Time.deltaTime);
        }

        if (hitbyplayer == true)
        {
            transform.Translate(0.05f, 0, Time.deltaTime);
            if (transform.position.x >= 20)
            {
                Destroy(this.gameObject);
            }
        }
        hittext.text = hitpoints.ToString();
        /* (hitpoints <= 0)
        {
            //Instantiate(blood, transform.position, Quaternion.identity);
            score.SendMessage("killed");
            score.SendMessage("ded");
            SelfDestory(true);
           
        }*/
		
	}

    public void SelfDestory(bool isExplosion = false)
    {
        Instantiate(blood, transform.position, Quaternion.identity);
        if(isExplosion)
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        score.SendMessage("killed");
        score.SendMessage("ded");
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.collider.gameObject;
        if (hitObject.GetComponent<donut>())
        {
            /*
            //if (collision.collider.tag == "bullet")
            if(hitObject.GetComponent<donut>().m_ColorState==m_ColorState)
            {
                //get the right donut, and turn back.
                hitbyright = true;
                //m_collider.enabled = !m_collider.enabled;
                score.SendMessage("returned");
                score.SendMessage("getheart");
                if (!hitten)
                {
                    flip = this.gameObject.transform.localScale.x;
                    this.gameObject.transform.localScale += new Vector3(-flip * 2, 0, 0);
                    hitten = true;
                }
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2.5f, 0);
                love1 = Instantiate(love, pos, Quaternion.identity);
                love1.transform.parent = this.transform;


            }
            if(hitObject.GetComponent<donut>().m_ColorState!=m_ColorState)
            {
                hitpoints -= 1;
                score.SendMessage("hit");
            }*/
            if (HitByColor(hitObject.GetComponent<donut>().m_ColorState))
            {
                hitObject.GetComponent<donut>().SelfDestroy();
            }
        }
        else if (hitObject.GetComponent<player>())
        {
            /*
            if(hitObject.GetComponent<player>().m_ColorState==m_ColorState)
            {
                //get the right donut, and turn back.
                hitbyright = true;
                //m_collider.enabled = !m_collider.enabled;
                score.SendMessage("returned");
                score.SendMessage("getheart");
                flip = this.gameObject.transform.localScale.x;
                if (!hitten)
                {
                    this.gameObject.transform.localScale = new Vector3(-flip, this.gameObject.transform.localScale.y,this.gameObject.transform.localScale.z);
                    hitten = true;
                }
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2.5f, 0);
                love1 = Instantiate(love, pos, Quaternion.identity);
                love1.transform.parent = this.transform;


            }
            if(hitObject.GetComponent<player>().m_ColorState!=m_ColorState)
            {
                hitpoints -= 1;
                score.SendMessage("hit");
            }
            */
            if (hitObject.GetComponent<player>().isRolling)
            {
            
            HitByColor(hitObject.GetComponent<player>().m_ColorState, 2);
                Vector2 hitbackForce = new Vector2(-70f, 0);
                if(collision.contacts.Length>0)hitbackForce=(collision.contacts[0].point - new Vector2(transform.position.x, transform.position.y)).normalized *70f;
            //Debug.Log("hitbackForce: " + hitbackForce);
            hitObject.GetComponent<Rigidbody2D>().AddForce(hitbackForce, ForceMode2D.Impulse);
            }
            else
            {
                if (hitObject.GetComponent<player>().extradonutnum > 0)
                {
                    hitObject.GetComponent<player>().extradonutnum--;
                    hitObject.GetComponent<player>().donutnum =hitObject.GetComponent<player>().magazinedonutnum +hitObject.GetComponent<player>().extradonutnum;
                    if (hitObject.GetComponent<player>().donutnum <= 0)
                    {
                        hitObject.GetComponent<player>().ChangeColorState(ColorState.None);
                    }
                    HitByColor(hitObject.GetComponent<player>().m_ColorState, 10);
                }
                else if (hitObject.GetComponent<player>().magazinedonutnum > 0)
                {
                    hitObject.GetComponent<player>().magazinedonutnum--;
                    hitObject.GetComponent<player>().donutnum =hitObject.GetComponent<player>().magazinedonutnum +hitObject.GetComponent<player>().extradonutnum;
                    if (hitObject.GetComponent<player>().donutnum <= 0)
                    {
                        hitObject.GetComponent<player>().ChangeColorState(ColorState.None);
                    }
                    HitByColor(hitObject.GetComponent<player>().m_ColorState, 10);
                }
            }
        //GetComponent<Collider2D>().
        }
        else if (hitObject.GetComponent<zombie>())
        {
            if (isMoving)
            {
                hitpoints = hitObject.GetComponent<zombie>().RamByZombie(hitpoints, m_ColorState);
                if (hitpoints == 0)
                {
                    SelfDestory(true);
                }
                else if(hitpoints<0)
                {
                    SelfDestory(false);
                }
            }
        }

    }

    public bool HitByColor(ColorState colorState,int hpHit=1)
    {
        if (m_ColorState == colorState)
            //get the right donut, and turn back.
        {
            hitbyplayer = true;
            //m_collider.enabled = !m_collider.enabled;
            score.SendMessage("returned");
            score.SendMessage("getheart");
            if (!hitten)
            {
                flip = this.gameObject.transform.localScale.x;
                this.gameObject.transform.localScale += new Vector3(-flip * 2, 0, 0);
                hitten = true;
            }
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2.5f, 0);
            love1 = Instantiate(love, pos, Quaternion.identity);
            love1.transform.parent = this.transform;
            isMoving = true;
            return true;
        }
        else
        {
            hitpoints -= hpHit;
            if (hitpoints <= 0)
            {
                //Instantiate(blood, transform.position, Quaternion.identity);
                //score.SendMessage("killed");
                SelfDestory(true);
           
            }
            score.SendMessage("hit");
            return false;
            //score.SendMessage("hit");
        }
    }

    public int RamByZombie(int ramhitpoint,ColorState ramcolor)
    {
        if (ramcolor != m_ColorState)//颜色不同 互撸
        {
            //hitpoints -= ramhitpoint;
            //if(m_ColorState==ColorState.Red)
            if (hitpoints <= ramhitpoint)//外来撞击者血条更高
            {
                ramhitpoint -= hitpoints;
                SelfDestory(true);
                //Debug.Log("ramhitpoint:"+ramhitpoint+"  hitpoint left"+hitpoints+"  point left:"+(ramhitpoint - hitpoints));
                score.SendMessage("killed");
                return ramhitpoint;//销毁本物体，返回剩余血量
            }
            else
            {
                hitpoints -= ramhitpoint;
                return 0;//通知撞击者自我销毁
            }
        }
        else//颜色相同 合体
        {
            hitpoints += ramhitpoint;
            return -1;
        }
    }
}
