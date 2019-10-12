using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButtton : MonoBehaviour
{
    [SerializeField] GameObject Panel;

    public void ActivePanel() {


        Panel.gameObject.SetActive(true);


    }

}
