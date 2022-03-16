using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour, InterfaceFuncoes
{
    public EventHandler OnBossTookDamage;
    public EventHandler OnBossDied;
    public int bossHealth;
    
    [SerializeField] private Movement movementController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private Status bossStatus;
    [SerializeField] private GameObject firstAid;
    
    private Transform _player;
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        agent.speed = bossStatus.speed;
    }

    private void Update()
    {
        agent.SetDestination(_player.position);
        animationController.Move(agent.velocity.magnitude);

        if (agent.hasPath)
        {
            bool isCloseToPlayer = agent.remainingDistance <= agent.stoppingDistance;

            if (isCloseToPlayer)
            {
                animationController.Attack(true);
                Vector3 dir = _player.position - transform.position;
                movementController.Rotate(dir);
            }
            else
            {
                animationController.Attack(false);
            }
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        _player.GetComponent<PlayerController>().TakeDamage(dano);
    }

    public void TakeDamage(int damage)
    {
        bossStatus.health -= damage;
        bossHealth = bossStatus.health;
        OnBossTookDamage?.Invoke(this, EventArgs.Empty);
        if (bossStatus.health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animationController.Died();
        movementController.Die();
        this.enabled = false;
        agent.enabled = false;
        OnBossDied?.Invoke(this, EventArgs.Empty);
        Instantiate<GameObject>(firstAid, transform.position, Quaternion.identity);
        Destroy(gameObject, 4);
    }
    
    public void Heal(int healingQuantity)
    {
    }
}
