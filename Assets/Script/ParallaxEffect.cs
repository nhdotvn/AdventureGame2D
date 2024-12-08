using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTarget;

    Vector2 StartingPosition;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - StartingPosition;

    float zDistanceFromTarget  => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    float StartingZ;
    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position; 
        StartingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPostion = StartingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPostion.x, newPostion.y, StartingZ);
    }
}
