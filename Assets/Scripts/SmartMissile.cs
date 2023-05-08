using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartMissile : MonoBehaviour
{

    [SerializeField]
    private GameObject _bombExplosionPrefab;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _movementSpeed = 3;

    public Player player;

    void Start()
    {
        player = GameObject.Find("Player_Ship").GetComponent<Player>();
        _target = player.transform;
    }

    
    void Update()
    {
        HomingBombBehavior();
    }

    public void HomingBombBehavior()
    {
        if (gameObject != null && player != null)
        {
            transform.Translate(Vector3.up * _movementSpeed * Time.deltaTime);
            transform.up = _target.position - transform.position;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            if (player != null)
            {
                player.Damage();
            }

            Instantiate(_bombExplosionPrefab, transform.position, Quaternion.identity);

            gameObject.GetComponent<AudioSource>().Play();

            GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 0.2f);

        }

        else if (other.tag == "Laser")
        {

            if (player != null)
            {
                player.AddScore(25);
            }

            Instantiate(_bombExplosionPrefab, transform.position, Quaternion.identity);

            gameObject.GetComponent<AudioSource>().Play();

            GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

            Destroy(other.gameObject);

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 0.2f);

        }

        else if (other.tag == "Laser_Beam")
        {

            if (player != null)
            {
                player.AddScore(25);
            }

            Instantiate(_bombExplosionPrefab, transform.position, Quaternion.identity);

            gameObject.GetComponent<AudioSource>().Play();

            GameObject.Find("Main Camera").GetComponent<CameraShake>().StartShake();

            Destroy(GetComponent<Collider2D>());

            Destroy(gameObject, 0.2f);

        }

    }

}
