using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color2Controller : MonoBehaviour
{
    public static Color2Controller instance{set; get;}
    private void Awake() {
        instance = this;
    }
    
    public List<GameObject> stables ;
    public List<GameObject> grounds ;

    public Color[] _colors;
    public Color currentColor =Color.white;

    // color changer
    public int index ;
    public float timer = 8f;
    

    
    public float _ColorLerpValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
            MainColorChanger();
            ColorChanger();

        


        
        
        
    }
    public void MainColorChanger()
    {
        ChangeStablesColor();
        ChangeGroundsColor();

    }

    public void ChangeStablesColor(){
        stables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Stable"));
        for(int i = 0; i < stables.Count ; i++)
        {
            ColorLerp( stables[i]);
            
          // stables[i].gameObject.GetComponent<MeshRenderer>().material.color =  _FallObj.gameObject.GetComponent<MeshRenderer>().material.color ;

        }
    }

    public void ChangeGroundsColor()
    {
        grounds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ground"));
        for(int i = 0; i < grounds.Count ; i++)
        {
            ColorLerp( grounds[i]);
        
          // grounds[i].gameObject.GetComponent<MeshRenderer>().material.color =  _FallObj.gameObject.GetComponent<MeshRenderer>().material.color ;

        }

    }




    public void ColorLerp( GameObject _toChangeColorObj)
    {
        _toChangeColorObj.GetComponent<MeshRenderer>().material.color = Color.Lerp(_toChangeColorObj.GetComponent<MeshRenderer>().material.color, currentColor, _ColorLerpValue*Time.deltaTime);
    }
    


    private void ColorChanger()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 8f;
            if(index < _colors.Length)
            {
                index++;
                currentColor = _colors[index];
            }
            else
            {
                index = 0;
                currentColor = _colors[index];
                
            }
        }
        

    }
    
}
