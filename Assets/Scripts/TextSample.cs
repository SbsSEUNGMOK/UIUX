using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSample : MonoBehaviour
{
    public void OnValueChanged(string str)
    {
        Debug.Log($"{nameof(OnValueChanged)} : {str}");
    }
    public void OnEndEdit(string str)
    {
        Debug.Log($"{nameof(OnEndEdit)} : {str}");
    }

    public void OnSelected(string str)
    {
        Debug.Log($"{nameof(OnSelected)} : {str}");
    }
    public void OnDeselected(string str)
    {
        Debug.Log($"{nameof(OnDeselected)} : {str}");
    }
}
