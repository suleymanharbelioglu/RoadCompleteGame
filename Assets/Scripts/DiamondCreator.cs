using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondCreator : MonoBehaviour
{

    public static DiamondCreator instance{set; get;}
    private void Awake() {
        instance = this;
    }
    
    public GameObject[] diamondprefab;
    
    
    public float speed;

    public bool createDiamond;
    public int createTimer = 3 ;

    public int _createYelowDiamond = 5;

    public bool isHaveYellowDiamond;
    public bool ishaveDoubleYellowDiamonds;

    public int maxRandom = 10;
    public int minRandom = 5;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    public  void DiamondInstantiate( GameObject referenceObj, Color _firstColor)
    {
        if(_createYelowDiamond <= 0 )
        {
            GameObject  _diamond = Instantiate(diamondprefab[1], new Vector3(referenceObj.transform.position.x, referenceObj.transform.position.y ,referenceObj.transform.position.z), Quaternion.Euler(-90f, 0f, 0f));
            Debug.Log("yellow diamond created");
            if(ishaveDoubleYellowDiamonds)
            {
                _diamond.GetComponent<MeshRenderer>().material.color = Color.black;
            }
            else if(isHaveYellowDiamond)
            {
                _diamond.GetComponent<MeshRenderer>().material.color = Color.blue;

            }
            else
            {
                _diamond.GetComponent<MeshRenderer>().material.color = new Color(255f/255f, 202f/255f, 76f/225f);
            }
            diamondMove(_diamond);
           _createYelowDiamond = Random.Range(minRandom,maxRandom);
           maxRandom++;
           minRandom++;
        
        }
        else
        {
            GameObject  _diamond = Instantiate(diamondprefab[0], new Vector3(referenceObj.transform.position.x, referenceObj.transform.position.y ,referenceObj.transform.position.z), Quaternion.Euler(-90f, 0f, 0f));
            Debug.Log("star created");
        _diamond.GetComponent<MeshRenderer>().material.color =  _firstColor;
        _diamond.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = _firstColor;
        diamondMove(_diamond);
        _createYelowDiamond--;

        }
        
        
        
        
    }


    public bool CreateDimondOrNot()
    {
        if(createTimer > 0 )
        {
            createTimer--;
            createDiamond = false;
            
            
        }
        else if(createTimer <= 0)
        {

            createTimer = Random.Range(0,3);
           
            createDiamond = true;
            

        }
        return createDiamond;

        
         
    }

    

    private void RotateDiamond()
    {
        transform.Rotate(Vector3.up * speed);
    }


    private void diamondMove(GameObject _diamond)
    {
        float addYValue = 1f;
        Vector3 delta = _diamond.transform.position;

        
        while(delta.y <= 8f)
        {
            delta.y += addYValue * Time.deltaTime;
            _diamond.transform.position = delta;

            

        }
        
    }
   
    

    
}
