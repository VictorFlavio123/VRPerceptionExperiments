using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour {

    private Toggle m_Toggle;
    public Text m_Text;
    public BarOptionsController optionsController;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        //m_Text.text = "New Value : " + m_Toggle.isOn;
        //Debug.Log(m_Text);
        if(m_Toggle.isOn == true && transform.name == "ToggleEmo")
        {
            optionsController.gameController.isCollectivity = false;
            optionsController.gameController.isSocializationvsIsolation = false;
            optionsController.gameController.isEmotion = true;
            optionsController.isolation.isOn = false;
            optionsController.collectivity.isOn = false;
        }
        else if (m_Toggle.isOn == true && transform.name == "IsolationVsSocialization")
        {
            optionsController.gameController.isCollectivity = false;
            optionsController.gameController.isEmotion = false;
            optionsController.gameController.isSocializationvsIsolation = true;
            optionsController.emotion.isOn = false;
            optionsController.collectivity.isOn = false;
        }
        else if (m_Toggle.isOn == true && transform.name == "Collectivity")
        {
            optionsController.gameController.isEmotion = false;
            optionsController.gameController.isSocializationvsIsolation = false;
            optionsController.gameController.isCollectivity = true;
            optionsController.emotion.isOn = false;
            optionsController.isolation.isOn = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_Toggle == true)
        {
            m_Toggle.isOn = false;
        }
        else
        {
            m_Toggle.isOn = true;
        }
    }
}
