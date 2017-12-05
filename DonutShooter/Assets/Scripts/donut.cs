using System.Collections;
using System.Collections.Generic;
using DonutShooter.Base;
using UnityEngine;

public class donut : MonoBehaviour
{
    public ColorState m_ColorState = ColorState.Red;
    private Rigidbody2D rb;
    public float bulletspeed;
    public float lifespan;
    Collider2D m_collider;
    private bool inited = false;

	// Use this for initialization
	void Init ()
	{
	    if (inited) return;
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(bulletspeed, 0);
        m_collider = GetComponent<Collider2D>();
	    inited = true;
	}

    public void InitDonut(Vector3 target,float speed=20f)
    {
        Init();
        rb.velocity = new Vector2((target - transform.position).x,(target - transform.position).y ).normalized*speed;
        //StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
	void Update () {
        /*lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(this.gameObject);
        }*/
		
	}

    IEnumerator DelayedSelfDestroy()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.GetComponent<zombie>())
        {
            hitObject.GetComponent<zombie>().HitByColor(m_ColorState);
            if (hitObject.GetComponent<zombie>().m_ColorState == m_ColorState)
            {
                //Destroy(this.gameObject);
            }
            /*if (hitObject.GetComponent<zombie>().m_ColorState != m_ColorState)
            {
                rb.velocity = new Vector2(-8, 5);
                rb.angularVelocity = 720.0f;
                m_collider.enabled = false; //!m_collider.enabled;
            }*/
        }
       
    }
}
