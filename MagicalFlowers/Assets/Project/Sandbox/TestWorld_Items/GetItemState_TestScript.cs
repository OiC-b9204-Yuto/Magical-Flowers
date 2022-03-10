using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Item
{
    public class GetItemState_TestScript : MonoBehaviour
    {
        [SerializeField] int maxobj;
        [SerializeField] GameObject[] gameObjects;

        ItemParameter[] itemParameter;

        private void Start()
        {
            for (int i = 0; i < maxobj; i++)
            {
                itemParameter[i] = gameObjects[i].GetComponent<ItemParameter>();
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {

            }
            if (Input.GetKeyDown(KeyCode.F2))
            {

            }
            if (Input.GetKeyDown(KeyCode.F3))
            {

            }
        }
    }
}
