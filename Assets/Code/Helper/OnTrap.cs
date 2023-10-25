using UnityEngine;

public class OnTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerController health = other.GetComponent<PlayerController>();
            health?.TakeDamage(25);
            other.GetComponent<CharacterController>().Move(other.transform.forward * -5);
        }
    }
}
