using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] int speed = 10;
    [SerializeField] Vector3 rotation = new Vector3(10, 10, 10);

    void Update()
    {
        transform.Rotate(Time.deltaTime * rotation, Space.Self);        
    }
}