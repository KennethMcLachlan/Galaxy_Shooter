using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    //0 = Triple Shot | 1 = Speed | 2 = Shield | 3 = Ammo | 4 = Extra Life
    private int powerupID;
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
               GameObject.Find("Powerup_Audio").GetComponent<AudioSource>().Play();
               
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldPowerupActive();
                        break;
                    case 3:
                        player.AmmoFillPowerupActive();
                        break;
                    case 4:
                        player.ExtraLifePowerupActive();
                        break;
                    case 5:
                        player.StarPowerupActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            
            Destroy(gameObject);
        }
    }
}

