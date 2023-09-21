using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float thrustForce = 35f;
    public float rotationSpeed = 200f;

    public GameObject gun, bulletPrefab;
    Vector2 thrustDirection; 
    Rigidbody _rigidbody;

    public static int SCORE = 0;

    public static float yBorderLimit;
    public static float xBorderLimit;

    GameObject pausePanel;
    void Start()
    {
        pausePanel = GameObject.FindGameObjectWithTag("Panel Pause");
        pausePanel.SetActive(false);
        _rigidbody = GetComponent<Rigidbody>();
        Camera camara = Camera.main;
        if(camara != null){
            float alto = camara.orthographicSize;
            float ancho = alto * camara.aspect;
            xBorderLimit = ancho;
            yBorderLimit = alto;
        }
    } 
    private void Update()
    {
        Limites();
        Pausa();
        
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * thrustForce * Time.deltaTime;
        thrustDirection = transform.right;
        transform.Rotate(Vector3.forward, -rotation); //Recibe el vector sobre el que girar y el ángulo del giro
        _rigidbody.AddForce(thrust * thrustDirection);

        if(Input.GetKeyDown(KeyCode.Space)){
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            Bullet balaScript = bullet.GetComponent<Bullet>();
            balaScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Enemy"){
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }else{
            Debug.Log("Ha colisionado con un objeto el cual no es el enemigo");
        }
    }

    public void Limites(){
        var newPos = transform.position;
        if(newPos.x > xBorderLimit){
            newPos.x = -xBorderLimit + 1;
        }else if(newPos.x < -xBorderLimit){
            newPos.x = xBorderLimit - 1;
        }else if(newPos.y > yBorderLimit){
            newPos.y = -yBorderLimit + 1;
        }else if(newPos.y < -yBorderLimit){
            newPos.y = yBorderLimit - 1;
        }
        transform.position = newPos;
    }
    public void Pausa(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 1){
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }else if(Time.timeScale == 0){
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
    }
}
