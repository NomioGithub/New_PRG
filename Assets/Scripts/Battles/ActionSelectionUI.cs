using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelectionUI : MonoBehaviour
{
    //�A�N�V����UI�̊Ǘ�
    //���������@or �ɂ���@�̂ǂ��炩��I�𒆂��c�����ĐF��ς���

    SelectableText[] selectableTexts;

    int selectedIndex;

    public int SelectedIndex { get => selectedIndex; }

    public void Init()
    {
        //�����̎q�v�f��<SelectableText>�R���|�[�l���g�������Ă�����̂��W�߂�
        selectableTexts = GetComponentsInChildren<SelectableText>();
    }

    public void HandleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
        }

        selectedIndex = Mathf.Clamp(selectedIndex, 0, selectableTexts.Length - 1);
        
        for(int i = 0; i< selectableTexts.Length; i ++)
        {
            if(selectedIndex == i)
            {
                selectableTexts[i].SetSelectedColor(true);
            }
            else 
            {
                selectableTexts[i].SetSelectedColor(false);
            }
        }
    }

    public void Open()
    {
        selectedIndex = 0;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
