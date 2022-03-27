using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class ScoreLabel : MonoBehaviour
    {
        private TextMeshProUGUI label;
        private void Awake()
        {
            label = GetComponent<TextMeshProUGUI>();
            GameManager.UpdateScoreEvent.AddListener(SetLabelText);
        }


        private void SetLabelText(int sum)
        {
            label.text = cFormat(sum);
        }
        
        public static string cFormat(int num)
        {
            
            if (num >= 10000000)
                return ((num) / 1000000.0).ToString("0.#") + "M";
            
            if (num >= 10000)
                return (num / 1000).ToString("0.#") + "K";

            return num.ToString("#,0");
        } 
        
        
    }
}