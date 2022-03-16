using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void Attack(bool state)
    {
        animator.SetBool("Attack", state);
    }

    public void Move(float movementValue)
    {
        animator.SetFloat("Move", movementValue);
    }

    public void Died()
    {
        animator.SetTrigger("Die");
    }
}
