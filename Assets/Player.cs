using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] [Range(0, 10)] private float _speed;
    [SerializeField] private GameObject _projectileSpawnPoint;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _timertoShoot;
    [SerializeField] private float _timerReset;
    [SerializeField] private float _bulletSpeedY;
    void Awake()
    {
        transform.position = NewPosition(new Vector3(0, -2.251f, 0));
        getAllComponent();
    }
    void Update()
    {
        Controls(); shooting();
    }
    void getAllComponent()
    {
        _characterController = GetComponent<CharacterController>();
        _timerReset = resetTimer(2.5f);
    }
    void Controls()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 movement = new Vector3(horizontal, 0, 0).normalized;

        _characterController.Move(_speed * Time.deltaTime * movement);

        if (transform.position.x <= -8.865502f)
        {
            transform.position = NewPosition(new Vector3(8.865502f, transform.position.y, transform.position.z));

        }
        else if (transform.position.x >= 8.865502f)
        {
            transform.position = NewPosition(new Vector3(-8.865502f, transform.position.y, transform.position.z));
        }
    }
    void shooting()
    {
        if (_timertoShoot <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = Instantiate(_projectile, _projectileSpawnPoint.transform.position, Quaternion.identity);
                bullet.name = "BULLET";

                BoxCollider collider = bullet.GetComponent<BoxCollider>();
                Rigidbody rb = bullet.AddComponent<Rigidbody>();

                if (!collider.isTrigger)
                {
                    collider.isTrigger = true;
                }

                if (rb.useGravity)
                {
                    rb.useGravity = false;
                }
                rb.velocity = new Vector3(0, _bulletSpeedY, 0) + Vector3.up;
                Debug.Log(warning("Spawned " + bullet.name));
                _timertoShoot = _timerReset;
            }
        }
        else
        {
            _timertoShoot -= Time.deltaTime;
        }
    }
    float resetTimer(float resetTimer)
    {
        return resetTimer;
    }
    Vector3 NewPosition(Vector3 pos)
    {
        return pos;
    }
    string warning(string warn)
    {
        return warn;
    }
}
