using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI textcompound;
    public string[] line;
    public float TextSpeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {   
        textcompound.text = string.Empty;
        StartText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textcompound.text == line[index])
            {
                NextLine();
            }

            else 
            {
                StopAllCoroutines();
                textcompound.text = line[index];
            }
        }
    }

    void StartText()
    {
        index = 0;
        StartCoroutine(TypeLine()); 
    }

    IEnumerator TypeLine()
    {
        foreach (var c in line[index].ToCharArray())
        {
            textcompound.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }

    }

    void NextLine()
    {
        if(index < line.Length - 1) 
        {
            index++;
            textcompound.text = string.Empty;
            StartCoroutine (TypeLine());
        }

        else
        {
            gameObject.SetActive(false);
        }

        
    }
}
