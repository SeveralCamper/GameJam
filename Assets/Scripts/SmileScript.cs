using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileScript : MonoBehaviour
{
    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator move()
    {
        float moveStep = 10f;
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            if (flag) transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - moveStep);
            else transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + moveStep);
            flag = !flag;
        }
    }
}
