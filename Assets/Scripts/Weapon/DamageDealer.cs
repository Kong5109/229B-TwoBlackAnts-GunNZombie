using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private int damage;
    private bool isAlreadyDamage = false;
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isAlreadyDamage) { return; }
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            isAlreadyDamage = true;
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject, 3f);
    }
}
