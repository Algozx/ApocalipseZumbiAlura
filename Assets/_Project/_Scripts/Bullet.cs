using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rBullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int shotDamage;
    
    void FixedUpdate()
    {
        rBullet.MovePosition
            (rBullet.position + transform.forward * bulletSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Debug.Log("Colidiu");
                other.GetComponent<EnemyController>().TakeDamage(shotDamage);
                break;
            case "Boss":
                other.GetComponent<BossController>().TakeDamage(shotDamage);
                break;
        }
        Destroy(gameObject);
    }
}
