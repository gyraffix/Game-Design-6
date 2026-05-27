using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graffiti : MonoBehaviour
{
    [SerializeField] private ParticleSystem graffiti;
    [SerializeField] private KeyCode graffitiKey;
    [SerializeField] private List<Color> colorList;
    [SerializeField] private Image colorIndicator;
    [SerializeField] private GameObject splatter;
    private Color selectedColor;
    private List<ParticleCollisionEvent> collisionEvents = new();

    private void Start()
    {
        selectedColor = colorList[0];
        ChangeColor(selectedColor);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(graffitiKey))
        {
            graffiti.Stop();
            
        }

        if(Input.GetKeyDown(graffitiKey))
        {
            graffiti.Play();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (colorList.IndexOf(selectedColor) == 0)
                selectedColor = colorList[colorList.Count - 1];
            else
            selectedColor = colorList[colorList.IndexOf(selectedColor)-1];
            
            ChangeColor(selectedColor);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (colorList.IndexOf(selectedColor) == colorList.Count-1)
                selectedColor = colorList[0];
            else
                selectedColor = colorList[colorList.IndexOf(selectedColor)+1];

            ChangeColor(selectedColor);
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        int collisionEventNumber = graffiti.GetCollisionEvents(other, collisionEvents);
        
        for(int i = 0; i < collisionEventNumber; i++)
        {
            Vector3 pos = collisionEvents[i].intersection;
            Vector3 normal = collisionEvents[i].normal;

            var go = Instantiate(splatter, pos, Quaternion.identity);

            go.transform.rotation = Quaternion.LookRotation(-normal);

        }

        
        Debug.Log("Hit");
    }

    void ChangeColor(Color selectedColor)
    {
        graffiti.startColor = selectedColor;
        colorIndicator.color = selectedColor;
    }
}
