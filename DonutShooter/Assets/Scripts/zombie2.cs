using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class zombie2 : MonoBehaviour {
    float flip;
    public int hitpoints;
    public TextMesh hittext;
    public float movingSpeed;
    public GameObject blood;
    GameObject score;
    Collider2D m_collider;
    bool hitbyright;
    GameObject love1;
    public GameObject love;


	// Use this for initialization
	void Start () {
        score = GameObject.Find("scorer");
        DontDestroyOnLoad(score);
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
        if (collision.collider.tag == "bullet3")
        {
            //get the right donut, and turn back.
            hitbyright = true;
            //m_collider.enabled = !m_collider.enabled;
            score.SendMessage("returned");
            score.SendMessage("getheart");
            flip = this.gameObject.transform.localScale.x;
            this.gameObject.transform.localScale += new Vector3(-flip * 2, 0, 0);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2.0f, 0);
            love1 = Instantiate(love, pos, Quaternion.identity);
            love1.transform.parent = this.transform;

        }
        if (collision.collider.tag == "bullet")
        {   
          hitpoints -= 1;
          Debug.Log("hit");
            score.SendMessage("hit");

        }
        if (collision.collider.tag == "bullet2")
        {
            hitpoints -= 1;
            Debug.Log("hit");
            score.SendMessage("hit");

        }
        //when spawned, avoid overlap
 

        if (collision.collider.tag == "monster3")
        {
            Destroy(this.gameObject);

        }
     

    }
}
