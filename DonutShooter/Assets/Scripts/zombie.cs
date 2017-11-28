using System.Collections;
using System.Collections.Generic;
using DonutShooter.Base;
using UnityEngine;

public class zombie : MonoBehaviour {
    public ColorState m_ColorState = ColorState.Red;
    public bool hitten = false;
    float flip;
    public int hitpoints;
    public TextMesh hittext;
    public float movingSpeed;
    GameObject score;
    Collider2D m_collider;
    bool hitbyright;
    public GameObject blood;
    public GameObject love;
    GameObject love1;


	// Use this for initialization
	void Start () {
        score = GameObject.Find("scorer");
        m_collider = GetComponent<Collider2D>();
        hitbyright = false;

		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > -6.5f && hitbyright==false)
        {
          transform.Translate(-movingSpeed, 0, Time.deltaTime);
        }
        if (transform.position.x <= -6.5f)
        {   // game should be over by now 
            transform.Translate(0, 0, Time.deltaTime);
            Application.LoadLevel("ending");
        }
        if (hitbyright == true)
        {
            transform.Translate(0.05f, 0, Time.deltaTime);
            if (transform.position.x >= 20)
            {
                Destroy(this.gameObject);
            }
        }
        hittext.text = hitpoints.ToString();
        if (hitpoints <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            score.SendMessage("killed");
            score.SendMessage("ded");
            Destroy(this.gameObject);
           
        }
		
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.collider.gameObject;
        if (hitObject.GetComponent<donut>())
        {
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
            }
        }
        else if (hitObject.GetComponent<player>())
        {
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
        }


    }
}
