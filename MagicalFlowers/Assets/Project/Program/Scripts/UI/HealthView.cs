using MagicalFlowers.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MagicalFlowers.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] PlayerActor playerActor;
        [SerializeField] RectTransform fill;
        [SerializeField] Text text;
        const float barMax = 560.0f;

        private void Start()
        {
            playerActor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();
        }

        private void Update()
        {
            //ÉoÅ[ÇÃïœçX
            if(playerActor.MaxHealth != 0)
                fill.sizeDelta = new Vector2(barMax * (1 - playerActor.Health / playerActor.MaxHealth), fill.sizeDelta.y);
            text.text = playerActor.Health.ToString().PadLeft(4) + " / " + playerActor.MaxHealth.ToString().PadLeft(4);
        }
    }
}