using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public Animation gate;

    private void OnTriggerEnter(Collider other)
    {

        gate?.Play("gateOpen");
    }

    private void OnTriggerExit(Collider other)
    {
        if(gate != null)
        gate.Play("gateClose");
    }
}