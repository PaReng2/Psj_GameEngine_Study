using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoting : MonoBehaviour
{
    public GameObject projectilePrefab_1;
    public GameObject projectilePrefab_2;
    public Transform firePoint;
    public int nowWeapon;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
        nowWeapon = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            Debug.Log("น฿ป็");
        }
        ChageWeapon();
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        if ((nowWeapon == 0))
        {
        GameObject proj = Instantiate(projectilePrefab_1, firePoint.position, Quaternion.LookRotation(direction));

        }
        else if(nowWeapon == 1) 
        {
        GameObject proj = Instantiate(projectilePrefab_2, firePoint.position, Quaternion.LookRotation(direction));

        }
    }

    void ChageWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (nowWeapon == 0)
            {
                nowWeapon = 1;
            }
            else if (nowWeapon == 1)
            {
                nowWeapon = 0;
            }
        }
    }
}
