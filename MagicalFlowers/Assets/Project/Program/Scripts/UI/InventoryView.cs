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

        [SerializeField] Text pageText;

        EventSystem eventSystem;
        int selectedIndex = -1;
        int currentPage = 0;

        void Start()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
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
                itemListArea.gameObject.SetActive(playerInventory.IsOpen);
                if (!playerInventory.IsOpen)
                {
                    useMenu.gameObject.SetActive(playerInventory.IsOpen);
                    selectedIndex -= 1;
                }
                //インベントリを開いたときにアイテムがあるなら一番上にカーソルを合わせる
                else if(readOnlyInventory.Count > 0)
                {
                    itemUIContents[0].button.Select();
                }

            }
            // 選択メニューが表示されていないときはページ操作を有効に
            if (playerInventory.IsOpen && !useMenuFlag)
            {
                pageText.text = (currentPage + 1).ToString();
                if (playerInventory.IsOpen)
                {
                    int maxpage = readOnlyInventory.Count / itemUIContents.Count;
                    if (currentPage > 0)
                    {
                        //前のページに
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            currentPage--;
                            ChangeViewUpdate();
                        }
                    }

                    if (currentPage < maxpage)
                    {
                        //次のページに
                        if (Input.GetKeyDown(KeyCode.D))
                        {
                            currentPage++;
                            ChangeViewUpdate();
                        }
                    }
                }
            }
        }

        public void SelectedItemUse()
        {
            playerInventory.ItemUse(selectedIndex + currentPage * readOnlyInventory.Count);
            ChangeViewUpdate();
        }

        public void SelectedItemThrow()
        {
            playerInventory.ItemThrow(selectedIndex + currentPage * readOnlyInventory.Count);
            ChangeViewUpdate();
        }

        public void SelectedItemDrop()
        {
            playerInventory.ItemDrop(selectedIndex + currentPage * readOnlyInventory.Count);
            ChangeViewUpdate();
        }

        public void SelectedItemCncel()
        {
            itemUIContents[selectedIndex].button.Select();
            CloseUseMenu();
        }

        //ページを変更したときなどの画面の更新
        private void ChangeViewUpdate()
        {
            int page = (currentPage * itemUIContents.Count);

            for (int i = 0; i < itemUIContents.Count; i++)
            {
                //アイテムがあれば値を設定
                if (i + page < readOnlyInventory.Count)
                {
                    itemUIContents[i].button.gameObject.SetActive(true);
                    itemUIContents[i].text.text = readOnlyInventory[i + page]._data.Name;
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
            //選択したアイテムのIndex保存
            selectedIndex = itemUIContents.FindIndex(n => ReferenceEquals(n.button.gameObject, eventSystem.currentSelectedGameObject));
            //使用メニューの表示、座標調整
            useMenu.gameObject.SetActive(true);
            useMenu.anchoredPosition = new Vector2(useMenu.anchoredPosition.x, select.anchoredPosition.y);
            useMenuFlag = true;
            //メニューの一番上の項目を選択状態に
            useMenu.GetChild(0).GetComponent<Selectable>().Select();
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