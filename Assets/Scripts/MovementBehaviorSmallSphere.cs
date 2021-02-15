using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovementBehaviorSmallSphere : MonoBehaviour
{
    [SerializeField] private float smallSphereM;

    [SerializeField] private Vector3 smallSphereRStart;

    [SerializeField] private Vector3 VStart;

    [SerializeField] private Vector3 bigSphereRStart;

    [SerializeField] private float bigSphereM;
    
    private const float G = 6.67f * 0.00000000001f;
    // private float delta_time = 2;

    public GameObject BigSphere;
    private void Start()
    {
        BigSphere.transform.position = bigSphereRStart;
        transform.position = smallSphereRStart;
    }

    void Update()
    {
        Vector3 Big_position = BigSphere.transform.position;
        Vector3 small_position = transform.position;
        float dist = Vector3.Dot(Big_position - small_position, Big_position - small_position);
        Vector3 F = G * smallSphereM * bigSphereM * (Big_position - small_position) / (dist * (Big_position - small_position).magnitude);
        Vector3 a = F / smallSphereM;
        
        Vector3 deltaTarget = (VStart * ( Time.deltaTime ))
                                                       + (a * (Time.deltaTime * Time.deltaTime)) / 2;
        Vector3 current_speed = VStart + a * ( Time.deltaTime);

        VStart = current_speed;
        
        transform.Translate(deltaTarget);
    }
}
