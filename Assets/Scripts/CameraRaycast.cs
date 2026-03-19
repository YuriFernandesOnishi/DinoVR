using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private Camera _camera;
    public float maxDistance = 10f;
    private RaycastHit _hit;
    
    void Start()
    {
        _camera = GetComponent<Camera>();
    }


    void Update()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * maxDistance, Color.red);

        if (Physics.Raycast(ray, out _hit, maxDistance))
        {
            Debug.Log("Olhando para: " + _hit.collider.gameObject.name);
        }
    }
}