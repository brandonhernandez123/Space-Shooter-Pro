using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement(){

         float randomPosition = Random.Range(-9.0f, 9.0f);

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -5.42f){
            transform.position = new Vector3(randomPosition, 7.11f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.transform.tag == "Player"){

            Player player = other.transform.GetComponent<Player>();

            if(player != null){
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }

        if(other.transform.tag == "Laser"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
