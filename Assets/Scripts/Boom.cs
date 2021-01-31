using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField]
    float time = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestruirParticula());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestruirParticula()
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
