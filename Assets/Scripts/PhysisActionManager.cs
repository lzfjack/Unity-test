using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisActionManager : MonoBehaviour, IActionManager {
    private float speed;

    void Awake() {
        speed = 15f;
    }

    public void arrowfly(GameObject arrow, Vector3 target, Vector3 forceDirection) {
        Rigidbody rigidbody = arrow.GetComponent<Rigidbody>();
        rigidbody.velocity = target * speed;
        rigidbody.AddForce(forceDirection, ForceMode.Force);
    }
}
