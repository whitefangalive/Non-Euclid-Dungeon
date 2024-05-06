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
        currentDamage = velocityCollide.previousVelocity.magnitude * DamageMutliplier;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.transform.tag == "Enemy")
        {
            EntityData data = collision.collider.transform.gameObject.GetComponent<EntityData>();
            Transform current = collision.collider.transform;
            while (data == null && current.parent != null) 
                {
                    current = current.parent;
                    data = current.gameObject.GetComponent<EntityData>();
                }
            if (data != null) 
            {
                Vector3 globalPositionOfContact = collision.contacts[0].point;
                Transform fullpos = transform;
                fullpos.position = globalPositionOfContact;
                data.takeDamage(Mathf.FloorToInt(currentDamage), fullpos);
            }
        }
    }
}
