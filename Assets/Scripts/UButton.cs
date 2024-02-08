using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UButton : MonoBehaviour
{
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnSelected()
    {
        image.color = Color.blue;
    }
    public void OnDeselected()
    {
        image.color = Color.white;
    }
}
