using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private float _magnetSpeed = 5.0f;

    [SerializeField]
    private GameObject Player;


    [SerializeField]
    //0 = Triple Shot | 1 = Speed | 2 = Shield | 3 = Blank Slate | 4 = Anti-Power-Up
    private int powerupID;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(gameObject);
        }

        if (Input.GetKey(KeyCode.C))
        {
            Magnet();
            
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                

                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        GameObject.Find("Powerup_Audio").GetComponent<AudioSource>().Play();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        GameObject.Find("Powerup_Audio").GetComponent<AudioSource>().Play();
                        break;
                    case 2:
                        player.ShieldPowerupActive();
                        GameObject.Find("Powerup_Audio").GetComponent<AudioSource>().Play();
                        break;
                    case 3:
                        player.AmmoFillPowerupActive();
                        GameObject.Find("Powerup_Audio").GetComponent<AudioSource>().Play();
                        break;
                    case 4:
                        player.ExtraLifePowerupActive();
                        GameObject.Find("Powerup_Audio").GetComponent<AudioSource>().Play();
                        break;
                    case 5:
                        player.StarPowerupActive();
                        GameObject.Find("Powerup_Audio").GetComponent<AudioSource>().Play();
                        break;
                    case 6:
                        player.AntiPower();
                        break;
                    case 7: //Left blank for random no spawn
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            Destroy(gameObject);
        }

        if (other.tag == "Laser")
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
            Destroy(gameObject, 1.0f);
        }
    }

    private void Magnet()
    {
        transform.position = Vector3.Lerp(transform.position, Player.transform.position, _magnetSpeed * Time.deltaTime);
    }
}

