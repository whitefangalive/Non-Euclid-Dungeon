using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private VelocityCollide velocityCollide;
    public float explodeMagnitude = 5;
    public GameObject exploded;
    // Start is called before the first frame update
    void Start()
    {
        velocityCollide = GetComponent<VelocityCollide>();
    }

    // Update is called once per frame
    void Update()
    {
        if (velocityCollide.previousVelocity.magnitude >= explodeMagnitude) 
        {
            explode();
        }
    }

    public void explode() 
    {
        Destroy(gameObject);
        Instantiate(exploded, transform);
    }
}
