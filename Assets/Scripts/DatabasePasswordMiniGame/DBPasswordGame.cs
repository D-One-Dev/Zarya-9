using Sylphiette;
using TMPro;
using UnityEngine;
using Zenject;

namespace DatabasePasswordMiniGame
{
    public class DBPasswordGame : MonoBehaviour, IInteractable
    {
        [SerializeField] private TMP_Text tryCountText;
        [SerializeField] private TMP_Text passwordText;

        [SerializeField] private TMP_Text matchText;
        [SerializeField] private TMP_Text placeMatchText;

        [SerializeField] private Transform itemSpawnPoint;
        [SerializeField] private GameObject outputItem;
        [SerializeField] private string password;

        private bool _isActive;
        private string _enteredPassword;
        private int _tryCount = 5;

        private EventHandler _eventHandler;
        private DayCounter _dayCounter;

        [Inject]
        public void Construct(EventHandler eventHandler, DayCounter dayCounter)
        {
            _eventHandler = eventHandler;
            _dayCounter = dayCounter;
            _eventHandler.OnMinigameKeyPressed += CheckKey;
            _eventHandler.OnEnterLetter += EnterLetter;
        }

        private void CheckKey(string key)
        {
            if (_isActive)
            {
                switch (key)
                {
                    case "enter":
                        OnEnterClick();
                        break;
                    default:
                        Debug.LogWarning("Unknown key: " + key);
                        break;
                }
            }
        }

        private void EnterLetter(string letter)
        {
            passwordText.text += letter.ToLower();
            _enteredPassword += letter;
        }

        private void OnEnterClick()
        {
            if (_isActive)
            {
                _enteredPassword = _enteredPassword.ToLower();
                password = password.ToLower();

                if (_enteredPassword != password)
                {
                    _tryCount--;
                    tryCountText.text = "Осталось попыток: " + _tryCount;

                    matchText.text = "Совпадений букв: " + CountMatchingChars(_enteredPassword, password);
                    placeMatchText.text = "Совпадений букв по месту: " + CountPlaceMatchingChars(_enteredPassword, password);

                    passwordText.text = "Введенный пароль: ";
                    _enteredPassword = "";

                    if (_tryCount == 0)
                    {
                        //SoundController.instance.PlaySoundRandomPitch(gameLoose);
                        DeathController.instance.TriggerDeath("Из-за неудачного взлома вы навсегда потеряли доступ к базе данных НИК, потеряв любую надежду покинуть комплекс. Вы умерли от истощения");
                        Debug.Log("Loose");
                    }
                }
                else
                {
                    print("thats right");
                    _eventHandler.SetDayCounterTrigger("BD");

                    GameObject spawnedObject = Instantiate(outputItem, itemSpawnPoint.position, Quaternion.identity);

                    switch (_dayCounter.CurrentDay)
                    {
                        case 1:
                            spawnedObject.GetComponent<Item>().name = "Схема шестерни двери";
                            break;
                        case 4:
                            spawnedObject.GetComponent<Item>().name = "Схема бура для лунохода";
                            break;
                        case 7:
                            spawnedObject.GetComponent<Item>().name = "Схема панели для ракеты";
                            break;
                    }

                    spawnedObject.GetComponent<Rigidbody>().AddForce(spawnedObject.transform.forward * 1000);

                    if (_dayCounter.CurrentDay == 2)
                    {
                        SylphietteDialogueSystem.Instance.StartNextDialogue();
                    }
                }
            }
        }

        private int CountMatchingChars(string str1, string str2)
        {
            int count = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str2.IndexOf(str1[i]) >= 0)
                {
                    count++;
                }
            }
            return count;
        }

        private int CountPlaceMatchingChars(string str1, string str2)
        {
            int count = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                if (i < str2.Length && str1[i] == str2[i])
                {
                    count++;
                }
            }
            return count;
        }

        public void TurnOn()
        {
            Cursor.lockState = CursorLockMode.None;
            _isActive = true;
        }

        public void TurnOff()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _isActive = false;
        }
    }
}