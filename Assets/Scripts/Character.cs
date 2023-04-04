using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public float speed;
    // animation
    public Animator animator;
    // cube 
    public Transform cube0Transform;

    //
    public Rigidbody rb;

    public float _jumpSpeed ;

    //scale
    public float scaleUpVar;

    // partice effect 
    public GameObject particleEffectPrefab;

    // flaoting Point 
    public GameObject[] floatingPointPrefab;

    // ads
    public Ads _ads;

    
    
     
     private void Update() {
        SetAnimator();
     }
    
    


    private void FixedUpdate() {
        Move();
        
    }


    public void Move()
    {
        if(GameManager.instance.charCanMove)
        {
            cube0Transform = GameManager.instance.cube[0].transform;
        if(DiamondCreator.instance.ishaveDoubleYellowDiamonds)
        {
            var deltaSpeed = (cube0Transform.transform.position.z - transform.position.z)*5f/4f;
            speed = deltaSpeed;
            if(speed < 25f)
            {
               speed = 25f;
            }
 
        }
        else if(DiamondCreator.instance.isHaveYellowDiamond && !DiamondCreator.instance.ishaveDoubleYellowDiamonds)
        {
            var deltaSpeed = (cube0Transform.transform.position.z - transform.position.z);
            speed = deltaSpeed;
            if(speed < 20f)
            {
               speed = 20f;
            }

        } 
        else if(!DiamondCreator.instance.isHaveYellowDiamond && !DiamondCreator.instance.ishaveDoubleYellowDiamonds) 
        {
            

            var deltaSpeed = (cube0Transform.transform.position.z - transform.position.z)*3f/4f;
            speed = deltaSpeed;
            if(speed < 15f)
            {
               speed = 15f;
            }
            
        }
        Vector3 _position = transform.position;
            _position.z += speed * Time.deltaTime;
            transform.position = _position;

            
        
        

             // stop runnin animation 
            if(!animator.GetBool("running"))
            {
                animator.SetBool("running",true);
            }
   

        }
        
    }

    private void OnCollisionEnter(Collision other) {
        ControlHitParticleVCharEffectVCharScale(other.gameObject);
        
    }
    private void ControlHitParticleVCharEffectVCharScale(GameObject other)
    {
        if(other.gameObject.CompareTag("Cube") || other.gameObject.CompareTag("Plus") ||
         other.gameObject.CompareTag("Rectangle") || other.gameObject.CompareTag("H") || other.gameObject.CompareTag("RectangleZ"))
        {
            if(DiamondCreator.instance.ishaveDoubleYellowDiamonds)
            {
                DoubleYellwCharHitsToObst(other.gameObject);
                AudioController.instance._destroyObstacleAudio.Play();
                
               
                

            }
            else if(DiamondCreator.instance.isHaveYellowDiamond)
            {
                YellowCharHitsToObst(other.gameObject);
                AudioController.instance._destroyObstacleAudio.Play();
                

            }
            else
            {
                GameManager.instance._StopTheGame = true;
                GameOver(other.gameObject);
                AudioController.instance._gameOverAudio.Play();
                
            }
            
            
        }

    }


    public void DoubleYellwCharHitsToObst(GameObject other)
    {
        GameManager.instance.cube.Remove(GameManager.instance.cube[0]);
                CreateHitParticle(other.gameObject);
                Destroy(other.gameObject);
                DiamondCreator.instance.ishaveDoubleYellowDiamonds = false;
                rb.AddForce(Vector3.up*_jumpSpeed * 100* Time.deltaTime, ForceMode.Impulse);
                transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = new Color(255f/255f, 202f/255f, 76f/225f);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                CharScaleDown();
                DiamondCreator.instance.ishaveDoubleYellowDiamonds = false;

    }
    public void YellowCharHitsToObst(GameObject other){
        GameManager.instance.cube.Remove(GameManager.instance.cube[0]);
                CreateHitParticle(other.gameObject);
                Destroy(other.gameObject);
                DiamondCreator.instance.isHaveYellowDiamond = false;
                rb.AddForce(Vector3.up*_jumpSpeed * 100* Time.deltaTime, ForceMode.Impulse);
                transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = new Color(144f/255f, 47f/255f, 1f);
                transform.GetChild(1).gameObject.SetActive(false);
                CharScaleDown();
                DiamondCreator.instance.isHaveYellowDiamond = false;

    }
    private void OnTriggerEnter(Collider other) {
        DiamondTrigger(other.gameObject);
    }

    public void DiamondTrigger(GameObject other)
    {
        CreateFloationgPointAndScoreUp(other.gameObject);
        TriggerCharEffectAndSizeController(other.gameObject);
        
        

    }

    private void TriggerCharEffectAndSizeController(GameObject other)
    {
        if(other.gameObject.CompareTag("Diamond"))
        {
            
            
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("YellowDiamond"))
        {
            
            Destroy(other.gameObject);

            if(DiamondCreator.instance.ishaveDoubleYellowDiamonds)
            {
                

            }
            else if(DiamondCreator.instance.isHaveYellowDiamond)
            {
                
                DiamondCreator.instance.ishaveDoubleYellowDiamonds = true;
                CharScaleUp();
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
                
                
            }
            else if(!DiamondCreator.instance.isHaveYellowDiamond)
                {
                    DiamondCreator.instance.isHaveYellowDiamond = true;
                    CharScaleUp();
                    
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = other.gameObject.GetComponent<MeshRenderer>().material.color;
                    
                    
                }
                
            


        }
        
    }

    public void CharScaleUp()
    {
        
        Vector3 delta = transform.localScale;
        delta  += Vector3.one*3/4;
        transform.localScale = delta;
    }
    public void CharScaleDown()
    {
        Vector3 delta = transform.localScale;
        delta  -= Vector3.one*3/4;
        transform.localScale = delta;

    }

    
    

    public void CreateHitParticle( GameObject _object)
    {
        GameObject _particleEffect = Instantiate(particleEffectPrefab, _object.transform.position,Quaternion.identity);
        _particleEffect.GetComponent<ParticleSystem>().startColor = _object.gameObject.GetComponent<MeshRenderer>().material.color;
    }

    public void CreateFloationgPointAndScoreUp(GameObject _object)
    {
        
        if(_object.CompareTag("Diamond") && !DiamondCreator.instance.isHaveYellowDiamond)
        {
            AudioController.instance._collectStartAudio.Play();
        GameObject _floatingP = Instantiate(floatingPointPrefab[0] , _object.transform.position, Quaternion.identity);
        _floatingP.GetComponent<MeshRenderer>().material.color = _object.GetComponent<MeshRenderer>().material.color;
        Debug.Log("floationg point +1");
        GameManager.instance.ScoreUp(1);
        
           

        }
        else if(_object.CompareTag("Diamond") && DiamondCreator.instance.isHaveYellowDiamond && !DiamondCreator.instance.ishaveDoubleYellowDiamonds)
        {
            AudioController.instance._collectStartAudio.Play();
            
            
                GameObject _floatingP = Instantiate(floatingPointPrefab[1] , _object.transform.position, Quaternion.identity);
        _floatingP.GetComponent<MeshRenderer>().material.color = _object.GetComponent<MeshRenderer>().material.color;
        Debug.Log("floationg point +5");
        GameManager.instance.ScoreUp(5);
                
        }
        else if(_object.CompareTag("Diamond") && DiamondCreator.instance.ishaveDoubleYellowDiamonds)
        {
            AudioController.instance._collectStartAudio.Play();
             GameObject _floatingP = Instantiate(floatingPointPrefab[2] , _object.transform.position, Quaternion.identity);
            _floatingP.GetComponent<MeshRenderer>().material.color = _object.GetComponent<MeshRenderer>().material.color;
            Debug.Log("floationg point +10");
            GameManager.instance.ScoreUp(10);

        }
        else if(_object.CompareTag("YellowDiamond") && DiamondCreator.instance.ishaveDoubleYellowDiamonds )
        {
            AudioController.instance._collectDiamondAudio.Play();
             GameObject _floatingP = Instantiate(floatingPointPrefab[3] , _object.transform.position, Quaternion.identity);
            _floatingP.GetComponent<MeshRenderer>().material.color = _object.GetComponent<MeshRenderer>().material.color;
            Debug.Log("floationg point +20");
            GameManager.instance.ScoreUp(20);

        }
        else if(_object.CompareTag("YellowDiamond") && DiamondCreator.instance.isHaveYellowDiamond && !DiamondCreator.instance.ishaveDoubleYellowDiamonds)
        {
            AudioController.instance._collectDiamondAudio.Play();
           //  GameObject _floatingP = Instantiate(floatingPointPrefab[2] , _object.transform.position, Quaternion.identity);
           // _floatingP.GetComponent<MeshRenderer>().material.color = _object.GetComponent<MeshRenderer>().material.color;
            //GameManager.instance.ScoreUp(10);

        }
        else if(_object.CompareTag("YellowDiamond") && !DiamondCreator.instance.isHaveYellowDiamond && !DiamondCreator.instance.ishaveDoubleYellowDiamonds)
        {
            AudioController.instance._collectDiamondAudio.Play();
          //   GameObject _floatingP = Instantiate(floatingPointPrefab[1] , _object.transform.position, Quaternion.identity);
           // _floatingP.GetComponent<MeshRenderer>().material.color = _object.GetComponent<MeshRenderer>().material.color;
            //GameManager.instance.ScoreUp(5);

        }
        
    }

    public void SetAnimator()
    {
        if(GameManager.instance._StopTheGame)
        {
            animator.enabled = false;
        }
    }   
    public void GameOver(GameObject other)
    {
        print("gamOver"); 
            other.gameObject.GetComponent<MainCube>().rotateSpeed = 0f;
            GameManager.instance.charCanMove = false;  
            animator.SetBool("running",false);
            rb.isKinematic = true;
            StartCoroutine("GameOverCo");

    }
    IEnumerator  GameOverCo()
    {
        
        
        yield return new WaitForSeconds(0.1f);
        _ads.LoadInterstialAds();
        
        
    }

    public void AfterAdsGO()
    {
        GameManager.instance.GoScoreUI.text = GameManager.instance._score.ToString();
        GameManager.instance.holdRecord();
        GameManager.instance.GameOverPanel.SetActive(true);

    }

    


    
    
}
