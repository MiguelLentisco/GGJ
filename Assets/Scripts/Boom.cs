using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
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
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
