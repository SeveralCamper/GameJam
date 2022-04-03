using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    [SerializeField] private bool follow = true;
    private int maxHp = 2;
    private int hp = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.Find("Player").transform;
        hp = maxHp;
    }

    private void Update()
    {
        if (agent.isActiveAndEnabled) agent.SetDestination(target.position);
        if (follow) agent.enabled = true;
        else agent.enabled = false;

        if (hp == 0)
        {
            gameObject.SetActive(false);
        }
        else if (hp < maxHp)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            GetComponent<SpriteRenderer>().color = color;
        }
        else if (hp == maxHp)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void damage()
    {
        hp--;
    }

    public bool isDamaged()
    {
        return hp < maxHp;
    }

    public void heal()
    {
        hp++;
    }

    public void setFollow(bool state)
    {
        follow = state;
    }
}
