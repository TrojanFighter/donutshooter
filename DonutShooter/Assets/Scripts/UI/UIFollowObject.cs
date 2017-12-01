using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObject : MonoBehaviour
{

	public RectTransform m_UIObject;

	public GameObject m_FollowedObject;
	public Vector3 m_Offset;
	
	// Use this for initialization
	void Start () {
		if (m_UIObject == null)
		{
			m_UIObject = GetComponent<RectTransform>();
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_UIObject.position = Camera.main.WorldToScreenPoint(m_FollowedObject.transform.position)+m_Offset;
	}
}
