using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCube : MonoBehaviour
{
    
    public float rotateSpeed = 0;
    public Rigidbody rb;
    public bool _interaction ;

    public int i;
    public int k = 0 ;

    // dimaond
    public Color  firstColor;

    public bool touchedToGround;

   
    
    void Start()
    {
        

        
    }

    

    void FixedUpdate()
    {
        Rotate();
        
        
    }

    private void Rotate(){
        
            transform.Rotate(Vector3.up * Time.deltaTime*rotateSpeed);
            

        
        
        

    }

    public void MainCubeInteraction()
    {
        rb.isKinematic = false; 
            rotateSpeed = 0f;

    }


    public void RandomRotateSpeed()
    {
       // int random = Random.Range(0,5);
        rotateSpeed = 100f;
    }


    private void OnCollisionEnter(Collision other) {
        IfCubeTouchsToGround(other.gameObject);
        
    }

    private void IfCubeTouchsToGround(GameObject other){
        if(other.gameObject.CompareTag("Ground") && !touchedToGround)
        {
            touchedToGround = true;
            Debug.Log("cube touched to ground");
            FitScaleToGround();
            // dimaond
            firstColor = transform.gameObject.GetComponent<MeshRenderer>().material.color;
            
            if(transform.gameObject.CompareTag("H"))
            {
                transform.GetChild(0).gameObject.tag = "Stable";
                transform.GetChild(1).gameObject.tag = "Stable";
                transform.GetChild(2).gameObject.tag = "Stable";
                transform.GetChild(0).transform.gameObject.GetComponent<MeshRenderer>().material.color = Color2Controller.instance.currentColor;
                transform.GetChild(1).transform.gameObject.GetComponent<MeshRenderer>().material.color = Color2Controller.instance.currentColor;
                transform.GetChild(2).transform.gameObject.GetComponent<MeshRenderer>().material.color = Color2Controller.instance.currentColor;
            }
            if(transform.gameObject.CompareTag("Plus"))
            {
                transform.GetChild(0).gameObject.tag = "Stable";
                transform.GetChild(1).gameObject.tag = "Stable";
                transform.GetChild(0).transform.gameObject.GetComponent<MeshRenderer>().material.color = Color2Controller.instance.currentColor;
                transform.GetChild(1).transform.gameObject.GetComponent<MeshRenderer>().material.color = Color2Controller.instance.currentColor;
            }
            transform.gameObject.GetComponent<MeshRenderer>().material.color = Color2Controller.instance.currentColor;
            transform.gameObject.tag = "Stable";
            
            DiamondCreator.instance.DiamondInstantiate(transform.gameObject, firstColor);
        }
        


    }


    private void FitScaleToGround()
    {
        if(transform.CompareTag("Cube"))
        {
            transform.localScale = new Vector3 (7f, 6.5f, 7f);
        }
        else if(transform.CompareTag("Plus"))
        {
            transform.GetChild(0).transform.localScale = new Vector3(9f, 6.5f,3f);
            transform.GetChild(1).transform.localScale = new Vector3(9f, 6.5f,3f);
                

        }
        else if(transform.CompareTag("Rectangle"))
        {
            transform.localScale = new Vector3(9f, 6.5f, 3f);

        }
        else if(transform.CompareTag("H"))
        {
            transform.GetChild(0).transform.localScale = new Vector3(9f, 6.5f,3f);
            transform.GetChild(1).transform.localScale = new Vector3(9f, 6.5f,3f);
            transform.GetChild(1).transform.localPosition = new Vector3(3f,10f,0);
            transform.GetChild(2).transform.localScale = new Vector3(9f, 6.5f,3f);
            transform.GetChild(2).transform.localPosition = new Vector3(-3f,10f,0);

        }
        else if(transform.CompareTag("RectangleZ"))
        {
            transform.localScale = new Vector3(9f, 6.5f, 3f);
            
        }
        
    }

    
}
