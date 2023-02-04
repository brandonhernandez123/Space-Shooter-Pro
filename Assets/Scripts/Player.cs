using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool isTripleShotActive;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    void Start()
    {
       transform.position = new Vector3(0,0,0);
       _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
       if(_spawnManager == null){
        Debug.LogError("The spawn manager is null");
       }
    }

    void Update()
    {
        CalculateMovement();

    if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire){
          ShootLaser();
           
        }
        

        
    }

    void CalculateMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");

        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);


        transform.Translate(direction * _speed * Time.deltaTime);

        if(transform.position.y >= 0){
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if(transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if(transform.position.x < -11)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        

    }

    void ShootLaser(){
        
            _canFire = Time.time + _fireRate;
            if(isTripleShotActive == true){
                Instantiate(_tripleShotPrefab, transform.position , Quaternion.identity);
            }
            else {
                Instantiate(_laserPrefab, transform.position + new Vector3(0,1.05f,0), Quaternion.identity);
            }
           
           
        
    }

    public void Damage(){
        _lives--;
        if(_lives < 1){
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    
    
}
