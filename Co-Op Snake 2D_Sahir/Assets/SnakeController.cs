using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SnakeController : MonoBehaviour
{

    Rigidbody2D rb;
    public float SnakeSpeed;
    public float Lastspeed;
    private Vector2 CurrentDirection;
    private KeyCode[] Snakekeys;

    private Vector2 ScreenBounds;

    //For Adding Bodyparts.......
    private List<Transform> bodyparts;
    public Transform bodypart;
    
    public Sprite SnakeUp;
    public Sprite SnakeDown;
    public Sprite SnakeLeft;
    public Sprite SnakeRight;
    private bool canChangeDirection;
    public ScoreController scoreController;

    public float XpositiveScreenpos;
    public float XnegativeScreenpos;
    public float YpositiveScreenpos;
    public float YnegativeScreenpos;


    public GameObject GameoverUI;
    public bool Shieldactive;
    public bool IsAlive;
    public float timer;

    public float SnakeDistance;


    // Start is called before the first frame update
    void Start()
    {
        IsAlive=true;



        //for adding bodyparts 
        bodyparts = new List<Transform>();
        bodyparts.Add(this.transform);

        rb = GetComponent<Rigidbody2D> ();
        //getting rigid body and start movemnt in right direction
        if (gameObject.CompareTag ("BlueSnake"))
        {
        CurrentDirection = Vector2.right;
        // rb.velocity = CurrentDirection * SnakeSpeed* Time.fixedDeltaTime;
        }
        else if (gameObject.CompareTag ("GreenSnake"))
        {  
        CurrentDirection = Vector2.left;
        // rb.velocity = CurrentDirection * SnakeSpeed* Time.fixedDeltaTime;
        }    
        canChangeDirection=true;

        //Sets Player Keys on Gameobject tab
        if (gameObject.CompareTag ("BlueSnake"))
        {
            Snakekeys = new KeyCode[] { KeyCode.W,KeyCode.S,KeyCode.A,KeyCode.D};   
        }
        else if(gameObject.CompareTag ("GreenSnake"))
        {
            Snakekeys = new KeyCode[] { KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.LeftArrow,KeyCode.RightArrow}; 
        }
        Shieldactive = false;
        Lastspeed=SnakeSpeed;

        //startcoroutine
        // StartCoroutine("Movementtimer");

       
    }

    // public IEnumerator Movementtimer()
    // {
    //     while (IsAlive)
    //     {
    //         MoveSnake(CurrentDirection);
    //         yield return new WaitForSeconds(1f);
    //     }


    // }

    // Update is called once per frame
    void Update()
    {


        // MoveSnake(CurrentDirection);
        HandleInput();

      
    }

    public void LateUpdate() {



        
    }

    void FixedUpdate()
    {
        if(SnakeSpeed==0)
        return;



        timer+= (SnakeSpeed==0?0:Time.fixedDeltaTime);

        
        if(timer >= 10/SnakeSpeed)
        { 
            for(int i =  bodyparts.Count-1;i>0; i--)
        {
            bodyparts[i].position = bodyparts[i-1].position;
        }
            timer =0;
             MoveSnake(CurrentDirection);
        }





        // MoveSnake(CurrentDirection);

    }



    void HandleInput()
    {
        if (!canChangeDirection)
            return;
        if (Input.GetKey(Snakekeys[0]) && CurrentDirection != Vector2.down)
        {
            CurrentDirection = Vector2.up;
            GetComponent<SpriteRenderer>().sprite = SnakeUp;
            Timedelaydirection() ;


        }
        if (Input.GetKey(Snakekeys[1]) && CurrentDirection != Vector2.up)
        {
            CurrentDirection = Vector2.down;
            GetComponent<SpriteRenderer>().sprite = SnakeDown;
            Timedelaydirection() ;

        }
        if(Input.GetKey(Snakekeys[3]) && CurrentDirection != Vector2.left)
        {
            CurrentDirection = Vector2.right;
            GetComponent<SpriteRenderer>().sprite = SnakeRight;
            Timedelaydirection() ;

        }
        if(Input.GetKey(Snakekeys[2]) && CurrentDirection != Vector2.right)
        {
            CurrentDirection = Vector2.left;
            GetComponent<SpriteRenderer>().sprite = SnakeLeft;
            Timedelaydirection() ;

        }


    }

    void Timedelaydirection()
    {
            canChangeDirection=false;
            Invoke("EnableChangeDirection", 0.5f) ;

    }
        void EnableChangeDirection()
    {
        canChangeDirection=true;
    }

    
    public void MoveSnake(Vector2 direction) 
    {
        // rb.velocity = direction * SnakeSpeed * Time.fixedDeltaTime;
        Vector2 position = gameObject.transform.position;
        
        transform.position += (Vector3)direction * SnakeDistance ;
        

        

        // this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, 
        //                                         Mathf.Round(this.transform.position.y)+direction.y,
        //                                          0.0f);
        // Vector2 position = gameObject.transform.position;

        if (transform.position.x>XpositiveScreenpos)
        {
            position.x=-position.x;
            gameObject.transform.position = position;
        }
        else if (transform.position.x<XnegativeScreenpos)
        {
            position.x=Mathf.Abs(position.x);
            gameObject.transform.position = position;
        }
        else if (transform.position.y>YpositiveScreenpos)
        {
            position.y=-position.y;
            gameObject.transform.position = position;
        }
        else if (transform.position.y<YnegativeScreenpos)
        {
            position.y=Mathf.Abs(position.y);
            gameObject.transform.position = position;
        }


        
    }   




    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("MassGainer"))
        {
            scoreController.ScoreIncrement(10);
            SoundManager.Instance.Play(SoundManager.Sounds.Massgainer);
            GrowSnake();
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("MassBurner"))
        {
            scoreController.ScoreDecrement(5);
            SoundManager.Instance.Play(SoundManager.Sounds.MassBurner);   
            DecreaseSnake();
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Shield"))
        {
            Debug.Log("Shield");
            SoundManager.Instance.Play(SoundManager.Sounds.PowerUps);
            Shieldactive =true;
            Invoke("DisableShield",5f);
            Destroy(other.gameObject);


        }
        else if(other.CompareTag("ScoreMultiplier"))
        {
            Debug.Log("ScoreMultiplier");
            SoundManager.Instance.Play(SoundManager.Sounds.PowerUps);
            scoreController.ScoreMultiplier();
            Destroy(other.gameObject);


        }
        else if(other.CompareTag("SpeedBuster"))
        {

            Debug.Log("SpeedBuster"); 
            SoundManager.Instance.Play(SoundManager.Sounds.SpeedPowerUps); 
            SnakeSpeed = SnakeSpeed*2;
            Invoke("normalSnakeSpeed",5f);
            Destroy(other.gameObject);
        }

        else if(other.CompareTag("BodyPart"))
        {
            if(Shieldactive==false)
            {
            Debug.Log("Snake Die");
            SoundManager.Instance.Play(SoundManager.Sounds.SnakeDeath);
            SnakeController snakeController = this.gameObject.GetComponent<SnakeController>();
            snakeController.enabled = false;
            SnakeSpeed=0;
            GameoverUI.SetActive(true);
            }

        }


    }

    public void GrowSnake()
    {
        Transform bodypart = Instantiate(this.bodypart);
        bodypart.position = new Vector2(10f,10f);
        bodyparts.Add(bodypart);



    }

    public void DecreaseSnake()
    {
        if(bodyparts.Count>1)
        {
                Transform lastSegment = bodyparts[bodyparts.Count - 1];
                bodyparts.Remove(lastSegment);
                Destroy(lastSegment.gameObject);
            
        }

    }

    void normalSnakeSpeed()
    {
        SnakeSpeed= SnakeSpeed/2;
    }

    void DisableShield()
    {
        Shieldactive=false;
        Debug.Log("Shield Deactive");
    }

    public void PauseSnake()
    {

        SnakeSpeed=0f;
    }

    public void ResumeSnake()
    {
        SnakeSpeed = Lastspeed ;
    }

    
}
