using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform target;
    public bool useLocal;

    void Update()
    {
        if (useLocal)
        {
            transform.localPosition = target.localPosition;
            transform.localRotation = target.localRotation;
        }
        else
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
