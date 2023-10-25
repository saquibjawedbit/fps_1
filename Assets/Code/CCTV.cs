using UnityEngine;

public class CCTV : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        Milk.instance.SubCCTV();
        Destroy(this);
    }
}
