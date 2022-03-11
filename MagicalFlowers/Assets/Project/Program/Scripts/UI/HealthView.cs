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
        [SerializeField] float barMax = 560.0f;

        private void Start()
        {
            playerActor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();
            barMax = this.GetComponent<RectTransform>().rect.width;
        }

        private void Update()
        {
            //ÉoÅ[ÇÃïœçX
            fill.offsetMax = new Vector2(barMax * ((float)playerActor.Health / playerActor.MaxHealth), fill.offsetMax.y);
            text.text = playerActor.Health.ToString().PadLeft(4) + " / " + playerActor.MaxHealth.ToString().PadLeft(4);
        }
    }
}