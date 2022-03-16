using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceFuncoes
{
    /// <summary>
    /// Call this function to healing.
    /// </summary>
    void Heal(int healingQuantity);
    
    /// <summary>
    /// Call this function to receive damage.
    /// </summary>
    void TakeDamage(int damage);
    
    /// <summary>
    /// Call this function to kill.
    /// </summary>
    void Die();
}
