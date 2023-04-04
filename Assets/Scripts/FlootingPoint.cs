using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlootingPoint : MonoBehaviour

{
    public float speed ;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.gameObject, 1f);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, 1f, 1f) * speed );
        
    }
}
