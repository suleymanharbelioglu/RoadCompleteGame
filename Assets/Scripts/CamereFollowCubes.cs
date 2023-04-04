using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereFollowCubes : MonoBehaviour
{
    
    // character
    public Vector3 offset;
    public float _lerpValue;
    public Transform _charTransform;
    
    void Start()
    {
        GettingOffset();
        
        
    }

    void FixedUpdate()
    {
        followChar(); 
    }


    private void GettingOffset()
    {
        offset = transform.position - _charTransform.position;

    }


    private void followChar()
    {
        Vector3 delta = Vector3.Lerp(transform.position, _charTransform .position+offset,_lerpValue* Time.deltaTime );
        transform.position = delta;
    }
    
}
