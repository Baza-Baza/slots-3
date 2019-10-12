using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;

    public void ExitPanell() {

        Panel.gameObject.SetActive(false);

    }
}
