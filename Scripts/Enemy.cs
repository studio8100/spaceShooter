using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    private Animator _anim;

    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();


        if(_player == null)
        {
            Debug.Log("Player is null");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.Log("Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move down 4
        transform.Translate(Vector3.down *_speed * Time.deltaTime);

        //off bottom screen, respwan at random position
        if(transform.position.y < -5f)
        {
            float randomX = Random.Range(-6.5f, 6.5f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit: " + other.transform.name);
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 1f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.6f);

        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(100);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 1f;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
        }

    }
}
