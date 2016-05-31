﻿using UnityEngine;
using System.Collections;

public class PhantomMotor : MonoBehaviour {

    public GameObject target;
    private bool _activated = false;
    public bool activated
    {
        get
        {
            return _activated;
        }
        set
        {
            _activated = value;
            StopRunning();
        }
    }

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
	
	}

    void StopRunning()
    {
        anim.SetFloat("Forward", 0);
    }

    void StartRunning()
    {
        anim.SetFloat("Forward", 1);
    }
	
	// Update is called once per frame
	void Update () {
	    if (target != null && activated)
        {
            Vector3 targetPos = target.transform.position;
            targetPos.y = transform.position.y;
            if (Vector3.Distance(targetPos, transform.position) > 1)
            {
                Vector3 dir = (targetPos - transform.position).normalized;
                transform.forward = dir;
                StartRunning();
            }
            else
            {
                StopRunning();
            }
            
        }
	}
}
