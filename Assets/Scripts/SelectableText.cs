using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableText : MonoBehaviour
{
    // Text�̐F��ς���
    //�I�𒆂Ȃ物�F�F�����łȂ��Ȃ甒
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        SetSelectedColor(true);
    }

    //�@�I�𒆂Ȃ�F��ς���
    public void SetSelectedColor(bool selected)
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }

        if(selected)
        {
            text.color = Color.yellow;
        }
        else
        {
            text.color = Color.white;
        }
    }
}
