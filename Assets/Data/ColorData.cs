using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData")]
public class ColorData : ScriptableObject
{
    [Serializable]
    struct ColorDataDetails
    {
        public ColorDataDetails(Bubble.BubbleType type, Color color)
        {
            this.type = type;
            this.color = color;
        }

        public Bubble.BubbleType type;
        public Color color;
    }
    
    [SerializeField] private List<ColorDataDetails> colorsDetails = new List<ColorDataDetails>();
    
    public Color GetColor(Bubble.BubbleType type) => colorsDetails.Find(details => details.type == type).color;
}
