﻿using System.Collections;
using UnityEngine;
using Zenject;

namespace Sylphiette
{
    public class SylphietteDialogueSystem : MonoBehaviour
    {
        [SerializeField] private TextViewer textViewer;

        public SylphietteDialogueBlock[] dialogueBlocks1;
        public SylphietteDialogueBlock[] dialogueBlocks2;
        public SylphietteDialogueBlock[] dialogueBlocks3;
        public SylphietteDialogueBlock[] dialogueBlocks4;
        public SylphietteDialogueBlock[] dialogueBlocks5;
        public SylphietteDialogueBlock[] dialogueBlocks6;
        public SylphietteDialogueBlock[] dialogueBlocks7;

        public static SylphietteDialogueSystem Instance;

        private int _currentDialogue;

        private DayCounter _dayCounter;

        [Inject]
        public void Construct(DayCounter dayCounter)
        {
            _dayCounter = dayCounter;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartCoroutine(StartWithDelay());
        }

        private IEnumerator StartWithDelay()
        {
            yield return new WaitForSeconds(3);
            ChooseDialogue();
        }

        public void StartNextDialogue()
        {
            _currentDialogue++;

            ChooseDialogue();
        }

        private void ChooseDialogue()
        {
            StopAllCoroutines();

            switch (_dayCounter.CurrentDay)
            {
                case 1:
                    StartCoroutine(ShowDialogue(dialogueBlocks1));
                    break;
                case 2:
                    StartCoroutine(ShowDialogue(dialogueBlocks2));
                    break;
                case 3:
                    StartCoroutine(ShowDialogue(dialogueBlocks3));
                    break;
                case 4:
                    StartCoroutine(ShowDialogue(dialogueBlocks4));
                    break;
                case 5:
                    StartCoroutine(ShowDialogue(dialogueBlocks5));
                    break;
                case 6:
                    StartCoroutine(ShowDialogue(dialogueBlocks6));
                    break;
                case 7:
                    StartCoroutine(ShowDialogue(dialogueBlocks7));
                    break;
            }
        }

        private IEnumerator ShowDialogue(SylphietteDialogueBlock[] dialogueBlocks)
        {
            for (int i = 0; i < dialogueBlocks[_currentDialogue].messages.Length; i++)
            {
                textViewer.Show(dialogueBlocks[_currentDialogue].messages[i]);

                while (!textViewer.isTextShown) yield return new WaitForSeconds(1f);

                yield return new WaitForSeconds(1.2f);
                textViewer.ClearText();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}