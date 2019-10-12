using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Roll[] rolls;
    [SerializeField] private SlotInfo[] slots;
    [SerializeField] private LineInfo[] lines;

    private int weightSum;
    private bool isRolling;

    private void Start()
    {
        System.Array.ForEach(slots, s => weightSum += s.weight);
    }

    public void StartRolling()
    {
        if (isRolling)
            return;

        foreach(var roll in rolls)
        {
            roll.DisableHighlighting();
            roll.StartRolling();
        }

        StartCoroutine(StopRolling(2f));
    }

    private IEnumerator StopRolling(float delay)
    {
        isRolling = true;

        yield return new WaitForSeconds(delay);

        var result = new int[5][];

        for(int x = 0; x < 5; x++)
        {
            result[x] = new int[3];

            for(int y = 0; y < 3; y++)
            {
                var random = Random.Range(0, weightSum); // 8

                for(int w = slots[0].weight, i = 0; i < slots.Length; i++, w += slots[i].weight)
                {
                    if (w < random)
                        continue;

                    result[x][y] = i;
                    break;
                }
            }
        }

        for(int i = 0; i < rolls.Length; i++)
        {
            rolls[i].StopRolling(result[i]);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);

        int score = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            int currentIndex = -1;
            int repeatCount = 1;

            for (int x = 0; x < 5; x++)
            {
                int y = lines[i].indices[x];

                if (x == 0)
                {
                    currentIndex = result[x][y];
                }
                else if (result[x][y] == currentIndex)
                {
                    repeatCount++;
                }
                else
                {
                    break;
                }
            }

            if(repeatCount >= 3)
            {
                for (int j = 0; j < repeatCount; j++)
                {
                    rolls[j].Highlight(lines[i].indices[j], lines[i].color);
                }
            }

            if (repeatCount == 3)
            {
                //Debug.Log($"Line {i} wins {slots[currentIndex].price3}");
                score += slots[currentIndex].price3;
            }
            else if (repeatCount == 4)
            {
                //Debug.Log($"Line {i} wins {slots[currentIndex].price4}");
                score += slots[currentIndex].price4;
            }
            else if (repeatCount == 5)
            {
                //Debug.Log($"Line {i} wins {slots[currentIndex].price5}");
                score += slots[currentIndex].price5;
            }

            if(repeatCount >= 3)
                yield return new WaitForSeconds(0.7f);

            for (int j = 0; j < repeatCount; j++)
            {
                rolls[j].DisableHighlighting();
            }

            if (repeatCount >= 3)
                yield return new WaitForSeconds(0.1f);
        }

        Debug.Log($"Your win - {score}");
        isRolling = false;
    }
}
