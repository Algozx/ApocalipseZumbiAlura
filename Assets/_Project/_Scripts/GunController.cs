using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField] private AudioClip shootSound;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject barrel;
    private Movement _movement;
    private bool _canShoot;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _canShoot = true;
    }

    void Update()
    {
        AutoShoot();
    }
    
    /// <summary>
    /// Call this function to verify if the right joystick is triggering and then shoot.
    /// </summary>
    private void AutoShoot()
    {
        if (_movement.isRotating)
        {
            if (_canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    /// <summary>
    /// Call this function to shoot with delay.
    /// </summary>
    private IEnumerator Shoot()
    {
        Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
        ControlaAudio.instancia.PlayOneShot(shootSound);

        _canShoot = false;
        yield return new WaitForSeconds(1f);
        _canShoot = true;
    }
}
