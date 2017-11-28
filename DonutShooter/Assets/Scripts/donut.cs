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

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(bulletspeed, 0);
        m_collider = GetComponent<Collider2D>();
        
    }
	
	// Update is called once per frame
	void Update () {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(this.gameObject);
        }
		
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.collider.gameObject;
        if (hitObject.GetComponent<zombie>())
        {
            if (hitObject.GetComponent<zombie>().m_ColorState == m_ColorState)
            {
                Destroy(this.gameObject);
            }
            if (hitObject.GetComponent<zombie>().m_ColorState != m_ColorState)
            {
                rb.velocity = new Vector2(-8, 5);
                rb.angularVelocity = 720.0f;
                m_collider.enabled = !m_collider.enabled;
            }
        }
       
    }
}
