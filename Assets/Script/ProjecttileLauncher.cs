using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projecttilePrefab;

    public void FireProjectile()
    {
        GameObject projecttile = Instantiate(projecttilePrefab, launchPoint.position, projecttilePrefab.transform.rotation);
        Vector3 origScale = projecttile.transform.localScale;

        projecttile.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x > 0 ? 1 : -1
            , origScale.y
            , origScale.z
            );

    }

    
}
