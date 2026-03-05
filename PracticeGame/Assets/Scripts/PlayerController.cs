using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
   public float moveSpeed = 10f;
   public float jump = 10;
   public Text scoreText ;
       public int score = 0;
public GameObject star;
        private Rigidbody rb; // Start is called before the first frame update
   
    void Start()
    {
     
       rb = GetComponent<Rigidbody>();
       scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();


InvokeRepeating("SpawnStar",2f,3f);
    }

   // Update is called once per frame
     void Update()
     {
         float h =Input.GetAxis("Horizontal");
         if(Input.GetButtonUp("Jump"))
        {
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse)
        }
    }
 
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Star")
        {
            Destroy(other.gameObject);
            score+=100;
            scoreText="Score:" + score;
            if(score>=1000)
            {
                SceneManager.LoadScene("SampleScene")
            }


        }
    }
    void FixedUpdate()
    {

        Vector3 movement = movement*moveSpeed*Time.deltaTime;


        rb.MovePosition = movement;
    }
    
void SpawnStar()
    {
        Vector3 spawnPosition = transform.position *new Vector3 (Random.Range(-25,24),5,0);
        instantiate(starPrefab,spawnPosition,Quaternion.identity);
    }
}
