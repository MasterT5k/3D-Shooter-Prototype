using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera _mainCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0) && _mainCamera != null)
        {
            Vector3 screenMiddle = new Vector3(0.5f, 0.5f);
            Ray ray = _mainCamera.ScreenPointToRay(screenMiddle);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }
}
