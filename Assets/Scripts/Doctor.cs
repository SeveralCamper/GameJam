using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Doctor : MonoBehaviour
{
    public bool follow = false;
    private NavMeshAgent agent;
    private Transform curTarget;
    private GameObject[] healTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;  // вращение в зависимости от вектора движения
        agent.updateUpAxis = false;  // расположение в зависимости от осей navSurface
        healTarget = GameObject.FindGameObjectsWithTag("Enemy");
        curTarget = transform;
    }

    // Update is called once per frame
    void Update()
    {
        healTarget = GameObject.FindGameObjectsWithTag("Enemy");

        bool lowHpEnemy = false;
        for (int i = 0; i < healTarget.Length; i++)
        {
            if (healTarget[i].GetComponent<EnemyController>().isDamaged())
            {
                curTarget = healTarget[i].transform;
                lowHpEnemy = true;
            }
        }

        if (agent.isActiveAndEnabled) agent.SetDestination(curTarget.position);

        if (lowHpEnemy) agent.enabled = true;
        else agent.enabled = false;

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 2.5f, 1 << 8);
        if (col.Length != 0)
        {
            while (col[0].GetComponent<EnemyController>().isDamaged()) col[0].GetComponent<EnemyController>().heal();
        }
    }
}
