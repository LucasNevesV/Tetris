using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{

    public float repeatTime;

    bool blink = true;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        InvokeRepeating(nameof(Blink), 1f, repeatTime);
    }

    // Update is called once per frame
    void Blink()
    {
        text.enabled = !blink;
        blink = !blink;
    }
}
