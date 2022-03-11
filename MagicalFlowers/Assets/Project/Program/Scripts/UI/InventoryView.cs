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

        EventSystem eventSystem;

        int currentPage = 0;

        void Start()
        {
            playerInventory = FindObjectOfType<PlayerInventory  >();
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

        //�y�[�W��ύX�����Ƃ��Ȃǂ̉�ʂ̍X�V
        private void ChangeViewUpdate()
        {
            for (int i = 0; i < itemUIContents.Count; i++)
            {
                //�A�C�e��������Βl��ݒ�
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
            //�g�p���j���[�̕\���A���W����
            useMenu.gameObject.SetActive(true);
            useMenu.anchoredPosition = new Vector2(useMenu.anchoredPosition.x, select.anchoredPosition.y);
            useMenuFlag = true;
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