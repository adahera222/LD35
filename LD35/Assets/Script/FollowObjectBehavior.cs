﻿using UnityEngine;
using System.Collections;

public class FollowObjectBehavior : MonoBehaviour {

    public GameObject target;

	void LateUpdate () {
        transform.position = target.transform.position;
	}
}
