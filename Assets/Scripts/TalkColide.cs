using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkColide : MonoBehaviour
{
    private Transform pos;
    public float range;

    float SpeM = 0.2f;
    float Spe = 0f;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        Time.timeScale = 1f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<NPCInteract>())
        {
            other.gameObject.GetComponent<NPCInteract>().stateSet = 2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Spe -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            pos.localScale = new Vector3(range, 0.1f, range);
            Spe = SpeM;
        }
        if (Spe <= 0)
        {
            pos.localScale = new Vector3(0f, 0.1f, 0f);
        }

    }
}
