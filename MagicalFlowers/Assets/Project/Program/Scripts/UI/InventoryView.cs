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
        //�v���C���[�̃C���x���g��
        [SerializeField] private PlayerInventory playerInventory;
        private IReadOnlyList<ItemParameter> readOnlyInventory;
        
        //�C���x���g���̕\������
        [SerializeField] private RectTransform itemListArea;
        //�A�C�e�����g�p���郁�j���[
        [SerializeField] private RectTransform useMenu;

        bool useMenuFlag;
        //�A�C�e���p��UI�̃��X�g
        [SerializeField]List<ItemUIContent> itemUIContents;

        [SerializeField] Text pageText;

        EventSystem eventSystem;
        int selectedIndex = -1;
        int currentPage = 0;

        void Start()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            readOnlyInventory = playerInventory.ReadOnlyInventory;
            //���ʂ̏�����
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
                //�C���x���g�����J�����Ƃ��ɃA�C�e��������Ȃ��ԏ�ɃJ�[�\�������킹��
                else if(readOnlyInventory.Count > 0)
                {
                    itemUIContents[0].button.Select();
                }

            }
            // �I�����j���[���\������Ă��Ȃ��Ƃ��̓y�[�W�����L����
            if (playerInventory.IsOpen && !useMenuFlag)
            {
                pageText.text = (currentPage + 1).ToString();
                if (playerInventory.IsOpen)
                {
                    int maxpage = readOnlyInventory.Count / itemUIContents.Count;
                    if (currentPage > 0)
                    {
                        //�O�̃y�[�W��
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            currentPage--;
                            ChangeViewUpdate();
                        }
                    }

                    if (currentPage < maxpage)
                    {
                        //���̃y�[�W��
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

        //�y�[�W��ύX�����Ƃ��Ȃǂ̉�ʂ̍X�V
        private void ChangeViewUpdate()
        {
            int page = (currentPage * itemUIContents.Count);

            for (int i = 0; i < itemUIContents.Count; i++)
            {
                //�A�C�e��������Βl��ݒ�
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
            //�I�������A�C�e����Index�ۑ�
            selectedIndex = itemUIContents.FindIndex(n => ReferenceEquals(n.button.gameObject, eventSystem.currentSelectedGameObject));
            //�g�p���j���[�̕\���A���W����
            useMenu.gameObject.SetActive(true);
            useMenu.anchoredPosition = new Vector2(useMenu.anchoredPosition.x, select.anchoredPosition.y);
            useMenuFlag = true;
            //���j���[�̈�ԏ�̍��ڂ�I����Ԃ�
            useMenu.GetChild(0).GetComponent<Selectable>().Select();
        }

        public void CloseUseMenu()
        {
            //�g�p���j���[�̔�\��
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