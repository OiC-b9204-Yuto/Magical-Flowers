using MagicalFlowers.Item;
using MagicalFlowers.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MagicalFlowers.UI 
{
    public class InventoryView : MonoBehaviour
    {
        //プレイヤーのインベントリ
        [SerializeField] private PlayerInventory playerInventory;
        private IReadOnlyList<ItemParameter> readOnlyInventory;
        
        //インベントリの表示部分
        [SerializeField] private RectTransform itemListArea;
        //アイテムを使用するメニュー
        [SerializeField] private RectTransform useMenu;

        bool useMenuFlag;
        //アイテム用のUIのリスト
        [SerializeField]List<ItemUIContent> itemUIContents;

        EventSystem eventSystem;

        int currentPage = 0;

        void Start()
        {
            playerInventory = FindObjectOfType<PlayerInventory  >();
            readOnlyInventory = playerInventory.ReadOnlyInventory;
            //共通の初期化
            itemUIContents.ForEach(n => n.button.onClick.AddListener(OpenUseMenu));
            ChangeViewUpdate();
            eventSystem = FindObjectOfType<EventSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                playerInventory.IsOpen = !playerInventory.IsOpen;
                playerInventory.IsOpen = !playerInventory.IsOpen;
                if (!playerInventory.IsOpen)
                {
                    useMenu.gameObject.SetActive(playerInventory.IsOpen);
                }
            }
        }

        //public void OpneInventoryView()
        //{
        //}

        //ページを変更したときなどの画面の更新
        private void ChangeViewUpdate()
        {
            for (int i = 0; i < itemUIContents.Count; i++)
            {
                //アイテムがあれば値を設定
                if (i < readOnlyInventory.Count)
                {
                    itemUIContents[i].button.gameObject.SetActive(true);
                    itemUIContents[i].text.text = readOnlyInventory[i]._data.Name;
                }
                else
                {
                    itemUIContents[i].text.text = "";
                    itemUIContents[i].button.gameObject.SetActive(false);
                }
            }
        }

        private void OpenUseMenu()
        {
            RectTransform select = eventSystem.currentSelectedGameObject.GetComponent<RectTransform>();
            if (!select)
            {
                return;
            }
            //使用メニューの表示、座標調整
            useMenu.gameObject.SetActive(true);
            useMenu.anchoredPosition = new Vector2(useMenu.anchoredPosition.x, select.anchoredPosition.y);
            useMenuFlag = true;
        }

        public void CloseUseMenu()
        {
            //使用メニューの非表示
            useMenu.gameObject.SetActive(false);
            useMenuFlag = false;
        }

        [Serializable]
        struct ItemUIContent
        {
            public Button button;
            public Text text;

            void TextClear()
            {
                text.text = "";
            }
        }

    }
}