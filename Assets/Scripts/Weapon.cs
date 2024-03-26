using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(VelocityCollide))]
public class Weapon : MonoBehaviour
{
    public float DamageMutliplier;

    public float currentDamage;
    private VelocityCollide velocityCollide;
    private void Start()
    {
        velocityCollide = GetComponent<VelocityCollide>();

    }
    private void Update()
    {
        currentDamage = velocityCollide.previousVelocity.magnitude;
    }

}
