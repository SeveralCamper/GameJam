using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jumper : MonoBehaviour
{
    private Transform target;
    private EnemyController enemyController;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        enemyController = GetComponent<EnemyController>();
        StartCoroutine(jump());
    }

    private void Update()
    {
        
    }

    IEnumerator jump()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            transform.position = target.position;
            enemyController.setFollow(false);
            yield return new WaitForSeconds(1);
            enemyController.setFollow(true);
        }
    }
}
