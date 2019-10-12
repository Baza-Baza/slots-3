using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roll : MonoBehaviour
{
    [SerializeField] private RectTransform[] slots;
    [SerializeField] private SlotInfo[] signs;
    [SerializeField] private float speed = 150;
    [SerializeField] private Animator animator;

    private int[] values;

    private void Start()
    {
        foreach(var slot in slots)
        {
            var images = slot.GetComponentsInChildren<Image>(true);
            images[0].gameObject.SetActive(false);
            ApplySign(images[1]);
        }
    }

    public void StartRolling()
    {
        animator.SetTrigger("Roll");
    }

    public void StopRolling(params int[] values)
    {
        this.values = values;
        animator.SetTrigger("Stop");
    }

    private void Update()
    {
        foreach (var slot in slots)
        {
            slot.ForceUpdateRectTransforms();
        }
    }

    private void ApplySign(Image image, int target = -1)
    {
        image.sprite = signs[target < 0 ? Random.Range(0, signs.Length) : target].icon;
    }

    private void HandleRollStep(int index)
    {
        ApplySign(slots[index].GetComponentsInChildren<Image>(true)[1]);
    }

    private void ApplyConfigValue(int index)
    {
        ApplySign(slots[index + 1].GetComponentsInChildren<Image>(true)[1], values[index]);
    }

    public void Highlight(int index, Color color)
    {
        var image = slots[index + 1].GetComponentsInChildren<Image>(true)[0];
        image.gameObject.SetActive(true);
        image.color = color;
    }

    public void DisableHighlighting()
    {
        foreach(var slot in slots)
        {
            slot.GetComponentsInChildren<Image>(true)[0].gameObject.SetActive(false);
        }
    }
}
