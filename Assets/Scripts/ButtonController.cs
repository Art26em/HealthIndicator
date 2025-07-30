using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private int healPoints = 10;
    [SerializeField] private int damagePoints = 10;
    [SerializeField] private PlayerController player;
    
    public void Heal()
    {
        player.GetHeal(healPoints);    
    }

    public void Attack()
    {
        player.TakeDamage(damagePoints);
    }
    
}
