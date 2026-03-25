using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    //[SerializeField] private Animator animator;
    [SerializeField] private BoxCollider HitBox;
    [SerializeField] private Rigidbody BodyRigidbody;
    [SerializeField] private Rigidbody velocityReference;

    private Collider[] allColliders;
    private Rigidbody[] allRb;

    void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        allRb = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider collider in allColliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }

        foreach (Rigidbody rigidbody in allRb)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        HitBox.enabled = !isRagdoll;

        if (isRagdoll == false) { return; }
        foreach (Rigidbody rigidbody in allRb)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.linearVelocity = velocityReference.linearVelocity;
            }
        }

        //BodyRigidbody.linearVelocity = velocityReference.linearVelocity;
        //animator.enabled = !isRagdoll;
    }
}
