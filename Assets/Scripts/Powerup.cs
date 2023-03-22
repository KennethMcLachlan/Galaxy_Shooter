using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;


    [SerializeField]
    //0 = Triple Shot | 1 = Speed | 2 = Shield | 3 = Ammo | 4 = Extra Life | 5 = Star Power | 6 = Anti-Power-up
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
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            Destroy(gameObject);
        }
    }
}

