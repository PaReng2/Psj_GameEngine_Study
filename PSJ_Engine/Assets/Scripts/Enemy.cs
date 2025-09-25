using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int moveSpeed = 2;
    public int enemyHP = 5;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);

    }

    public void HitEnemy(int Damage)
    {
        enemyHP -= Damage;
        
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }

    }

}
