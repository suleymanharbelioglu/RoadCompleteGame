using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance{set; get;}
    


    
    public int i = 0;

    // Instantiate
    public GameObject[] Prefabs; // mechanics array
    public GameObject normalRooadPrefab;
    public int j = 0 ;
    public int _rotateSpeedVar = 0; 
    public int k  = 1;
    public int random;

    //dimond
    
    //gameOver
    public bool _StopTheGame;
    public bool _gameStarted ;
    // panels
    public GameObject GameStartPanel;
    public GameObject GameScorePanel;
    public Text ScoreUI;
    public GameObject GameOverPanel;
    public Text GoScoreUI;
    public Text RecordUI;
    //score
    
    public int _score = 0   ;

    
    
    public Vector3 InstantiatePos;
    public List<GameObject> cube = new List<GameObject>();

    //character 
    public bool charCanMove;

    
    private void Awake() {
        instance = this ;
        GamStartInstanitate();
    }
    
    void Start()
    {
       
        InvokeRepeating("MainInstantiate",1f, 0.3f);
        
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && i == 0)
        {
            i = 1;
            GameStarted();


        }
        else if(Input.GetMouseButtonDown(0) && _gameStarted)
        {

            
            OpenCubeInteraction();
            
        }
        if(cube[0].transform.tag == "Stable" )
            {
                cube.Remove(cube[0]);

            }
            
        
        
        
    }


    private void OpenCubeInteraction()
    {
        if(!_StopTheGame)
        {
            cube[0].GetComponent<MainCube>().MainCubeInteraction();

        }
        


    }

    private void GamStartInstanitate()
    {
        for(int i = 0; i < 50; i++)
        {
            MainInstantiate();

        }

    }

    private void MainInstantiate()
    {
        
            //int random = Random.Range(0,4);
            
            if( j == 0)
            {
                NormalRoadInstantiate();
                NormalRoadInstantiate();
                NormalRoadInstantiate();
                j = 1;
                

            }
            else if( j == 1)
            {
                RandomMechInstantiate();
                j = 0;
                
            }
        
        

    }

    private void RandomMechInstantiate()
    {
        
        
        InstantiatePos.z+= +9f;
        
        if(k <= 0)
        { 
            
            random = Random.Range(0,5);
            k = 1;
        }
        else
        {
            k--;
        }
          
        int mechArrayIndex = random;
       GameObject cubeMech =  Instantiate(Prefabs[mechArrayIndex], InstantiatePos, Quaternion.identity);
      
       rotateSpeedCounter(cubeMech);
    
       AddMechtoList(cubeMech);    

    }
    private void rotateSpeedCounter(GameObject cubeMech)
    {
        _rotateSpeedVar++;
        if(_rotateSpeedVar >=3)
        {
            _rotateSpeedVar = 0;
            cubeMech.gameObject.transform.GetChild(0).GetComponent<MainCube>().rotateSpeed = 100f;

        }

    }
    
    private void NormalRoadInstantiate()
    {
       
            InstantiatePos.z+= +9f;
        
       GameObject normalRoad=  Instantiate(normalRooadPrefab, InstantiatePos, Quaternion.identity);
       /*
       if(_diamondCreator.CreateDimondOrNot())
       {
        _diamondCreator.DiamondInstantiate(new Vector3( normalRoad.transform.position.x, normalRoad.transform.position.y + 10f, normalRoad.transform.position.z));

       }
       */
       
        

    }


    private void AddMechtoList(GameObject _cubeMech)
    {
        cube.Add(_cubeMech.transform.GetChild(0).gameObject) ;
    }


    public void ScoreUp(int _addToScore)
    {
        _score +=_addToScore;
        ScoreUI.text = _score.ToString();
        
    }
     
    
    public void GameStarted()
    {
        charCanMove = true;
        _gameStarted = true;
        GameStartPanel.SetActive(false);

    }
    public void ReStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void holdRecord()
    {
        if(PlayerPrefs.GetInt("RecordPrefs") < _score){
            PlayerPrefs.SetInt("RecordPrefs",_score);
        }
        RecordUI.text = PlayerPrefs.GetInt("RecordPrefs").ToString();
    }
    
    

    


    
}
