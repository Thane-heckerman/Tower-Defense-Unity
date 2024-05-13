using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass 
{
    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPostion = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPostion.z = 0f;
        return mouseWorldPostion;
    }

    public static float GetRotation( Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degree = radians * Mathf.Rad2Deg;
        return degree;
    }

    public static Vector3 GetRandomDir()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)); ;
        return randomDir;
    } 
}
