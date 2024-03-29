﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingSystem : MonoBehaviour
{

    public float speed = 2000.0f;

    GameObject m_target = null;
    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

		if(m_target)
        {
            if(m_lastKnownPosition != m_target.transform.position)
            {
                m_lastKnownPosition = m_target.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            }

            if(transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
            }
        }
	}

    public void SetTarget(GameObject target)
    {
        m_target = target;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            m_target = other.gameObject;
        }
    }

}
