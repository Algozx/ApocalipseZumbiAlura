using UnityEngine;

public class BillBoard : MonoBehaviour
{
    void Update()
    {
        if (Camera.main == null) return;
        
        transform.LookAt(transform.position + Camera.main.transform.forward);
        
    }
}
