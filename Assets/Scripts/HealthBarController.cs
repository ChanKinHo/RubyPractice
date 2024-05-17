using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public static HealthBarController instance {  get; private set; }
    public Image mask;

    float orgizedSize;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        orgizedSize = mask.rectTransform.rect.width;
    }

    
    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, orgizedSize*value);

    }
}
