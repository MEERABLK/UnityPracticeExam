using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
   public float moveSpeed = 10f;
   public float jumpForce = 10f;
   public Text scoreText;
       public int score = 0;
public GameObject starPrefab;
        private Rigidbody rb; // Start is called before the first frame update
   
    void Start()
    {
     
       rb = GetComponent<Rigidbody>();
       scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
            scoreText.text = "Score:" + score;


               InvokeRepeating("SpawnStar",2f,3f);
    }

   // Update is called once per frame
     void Update()
     {
         float h =Input.GetAxis("Horizontal");
         if(Input.GetButtonUp("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (transform.position.y < -10)
{
    CancelInvoke("SpawnStar");
    SceneManager.LoadScene("SampleScene");
}
    }
 
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            score+=100;
            scoreText.text ="Score:" + score;
            
            if(score >= 1000)
            {
                CancelInvoke("SpawnStar");

                SceneManager.LoadScene("SampleScene");
            }
        }

        }
    
    void FixedUpdate()
    {
             float h =Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(h, 0, 0) * moveSpeed * Time.deltaTime;


        rb.MovePosition (rb.position + movement);
    }
    
void SpawnStar()
{
    if (starPrefab == null) return;

    float x = Random.Range(-10f, 10f);

    Vector3 spawnPosition = new Vector3(transform.position.x + x, 5f, 0);

    Instantiate(starPrefab, spawnPosition, Quaternion.identity);
}
}
