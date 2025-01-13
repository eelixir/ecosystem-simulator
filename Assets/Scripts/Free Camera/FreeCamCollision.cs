using UnityEngine;

public class FreeCamCollision : MonoBehaviour
{
    public FreeCamController Movement;

    void OnCollisionEnter()
    {
        Movement.enabled = false;
    }

    void OnCollisionStay()
    {
        Movement.enabled = true;
    }
}
