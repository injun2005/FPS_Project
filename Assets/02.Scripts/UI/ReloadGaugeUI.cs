using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGaugeUI : MonoBehaviour
{
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.fillMethod = Image.FillMethod.Radial360;
        _image.gameObject.SetActive(false);
    }
    public void ReloadGaugeNormal(float value)
    {
        _image.fillAmount = value;
    }
}
