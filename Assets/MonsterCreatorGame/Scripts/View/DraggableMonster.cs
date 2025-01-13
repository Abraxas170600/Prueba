using System;
using UnityEngine;

public class DraggableMonster : MonoBehaviour
{
    private Vector3 initialPosition;
    private Transform destroyTarget;

    public Action OnDestroyEvent;

    public GameObject deadParticle;

    private void Awake()
    {
        initialPosition = transform.position;

        GameObject targetObject = GameObject.FindWithTag("DestroyTarget");
        if (targetObject != null)
        {
            destroyTarget = targetObject.transform;
        }
        else
        {
            Debug.LogError("DestroyTarget no encontrado en la escena.");
        }
    }
    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }
    private void OnMouseUp()
    {
        if (destroyTarget != null && Vector2.Distance(transform.position, destroyTarget.position) < 1.0f)
        {
            OnDestroyEvent.Invoke();
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            transform.position = initialPosition;
        }
    }
}
