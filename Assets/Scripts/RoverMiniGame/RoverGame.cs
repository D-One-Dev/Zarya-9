using System.Collections;
using Sylphiette;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RoverGame : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector2Int roverStartPoint;
    [SerializeField] private Vector2Int itemPoint;
    [SerializeField] private MapTile[] tiles;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private int startEnergy;
    [SerializeField] private Image energySprite;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool canBreakRocks;

    [SerializeField] private AudioClip roverMove, rockBreak, gameWin, gameLoose, itemGet;

    private int _currentEnergy;
    private MapTile[,] _tileGrid;
    //private Controls _controls;
    private Vector2Int _roverPosition;
    private bool _isActive;
    private bool _isGameWon;
    private bool _hasItem;
    private EventHandler _eventHandler;
    private DayCounter _dayCounter;
    // private void Awake()
    // {
    //     _controls = new Controls();
    //     _controls.Gameplay.Up.performed += ctx => MoveUp();
    //     _controls.Gameplay.Down.performed += ctx => MoveDown();
    //     _controls.Gameplay.Left.performed += ctx => MoveLeft();
    //     _controls.Gameplay.Right.performed += ctx => MoveRight();
    // }
    // private void OnEnable()
    // {
    //     _controls.Enable();
    // }
    // private void OnDisable()
    // {
    //     _controls.Disable();
    // }

    [Inject]
    public void Construct(EventHandler eventHandler, DayCounter dayCounter)
    {
        _eventHandler = eventHandler;
        _eventHandler.OnMinigameKeyPressed += CheckKey;
        _dayCounter = dayCounter;
    }

    private void CheckKey(string key)
    {
        if (_isActive)
        {
            switch (key)
            {
                case "up":
                    MoveUp();
                    break;
                case "down":
                    MoveDown();
                    break;
                case "left":
                    MoveLeft();
                    break;
                case "right":
                    MoveRight();
                    break;
                default:
                    Debug.LogWarning("Unknown key: " + key);
                    break;
            }
        }
    }

    private void Start()
    {
        _currentEnergy = startEnergy;
        _roverPosition = roverStartPoint;
        _tileGrid = new MapTile[gridSize.x, gridSize.y];
        int i = 0;
        int j = 0;

        foreach (MapTile tile in tiles)
        {
            _tileGrid[i, j] = tile;
            if (i < gridSize.x - 1) i++;
            else
            {
                i = 0;
                j++;
            }
        }
    }

    private void MoveUp()
    {
        if (/*PlayerInteraction.instance.playerStatus == 1 && */_isActive && !_isGameWon)
        {
            if (_roverPosition.y > 0 && _tileGrid[_roverPosition.x, _roverPosition.y - 1].tileType != "Rock")
            {
                CheckItem(new Vector2Int(0, -1));
                if (CheckWin(new Vector2Int(0, -1)))
                {
                    Debug.Log("Win");
                }
                else
                {
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Ground";
                    _roverPosition.y--;
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    // SoundController.instance.PlaySoundRandomPitch(roverMove);
                }
            }
            else if (_roverPosition.y > 0 && _tileGrid[_roverPosition.x, _roverPosition.y - 1].tileType == "Rock")
            {
                if (canBreakRocks)
                {
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Ground";
                    _roverPosition.y--;
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    LooseEnergy();
                    // SoundController.instance.PlaySoundRandomPitch(rockBreak);
                }
            }
        }
    }

    private void MoveDown()
    {
        if (/*PlayerInteraction.instance.playerStatus == 1 && */_isActive && !_isGameWon)
        {
            if (_roverPosition.y < gridSize.y - 1 && _tileGrid[_roverPosition.x, _roverPosition.y + 1].tileType != "Rock")
            {
                CheckItem(new Vector2Int(0, 1));
                if (CheckWin(new Vector2Int(0, 1)))
                {
                    Debug.Log("Win");
                }
                else
                {
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Ground";
                    _roverPosition.y++;
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    // SoundController.instance.PlaySoundRandomPitch(roverMove);
                }
            }
            else if (_roverPosition.y < gridSize.y - 1 && _tileGrid[_roverPosition.x, _roverPosition.y + 1].tileType == "Rock")
            {
                if (canBreakRocks)
                {
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Ground";
                    _roverPosition.y++;
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    LooseEnergy();
                    // SoundController.instance.PlaySoundRandomPitch(rockBreak);
                }
            }
        }
    }

    private void MoveLeft()
    {
        if (/*PlayerInteraction.instance.playerStatus == 1 && */_isActive && !_isGameWon)
        {
            if (_roverPosition.x > 0 && _tileGrid[_roverPosition.x - 1, _roverPosition.y].tileType != "Rock")
            {
                CheckItem(new Vector2Int(-1, 0));
                if (CheckWin(new Vector2Int(-1, 0)))
                {
                    Debug.Log("Win");
                }
                else
                {
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Ground";
                    _roverPosition.x--;
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    // SoundController.instance.PlaySoundRandomPitch(roverMove);
                }
            }
            else if (_roverPosition.x > 0 && _tileGrid[_roverPosition.x - 1, _roverPosition.y].tileType == "Rock")
            {
                if (canBreakRocks)
                {
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Ground";
                    _roverPosition.x--;
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    LooseEnergy();
                    // SoundController.instance.PlaySoundRandomPitch(rockBreak);
                }
            }
        }
    }

    private void MoveRight()
    {
        if (/*PlayerInteraction.instance.playerStatus == 1 && */_isActive && !_isGameWon)
        {
            if (_roverPosition.x < gridSize.x - 1 && _tileGrid[_roverPosition.x + 1, _roverPosition.y].tileType != "Rock")
            {
                CheckItem(new Vector2Int(1, 0));
                if (CheckWin(new Vector2Int(1, 0)))
                {
                    Debug.Log("Win");
                }
                else
                {
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Ground";
                    _roverPosition.x++;
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    // SoundController.instance.PlaySoundRandomPitch(roverMove);
                }
            }
            else if (_roverPosition.x < gridSize.x - 1 && _tileGrid[_roverPosition.x + 1, _roverPosition.y].tileType == "Rock")
            {
                if (canBreakRocks)
                {
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Ground";
                    _roverPosition.x++;
                    _tileGrid[_roverPosition.x, _roverPosition.y].tileType = "Rover";
                    UpdateGrid();
                    LooseEnergy();
                    LooseEnergy();
                    // SoundController.instance.PlaySoundRandomPitch(rockBreak);
                }
            }
        }
    }

    private void UpdateGrid()
    {
        if (_roverPosition != roverStartPoint) _tileGrid[roverStartPoint.x, roverStartPoint.y].tileType = "Base";
        if (_roverPosition != itemPoint && _hasItem) _tileGrid[itemPoint.x, itemPoint.y].tileType = "Ground";
        foreach (MapTile tile in _tileGrid)
        {
            tile.UpdateTile();
        }
    }

    private void LooseEnergy()
    {
        if (_currentEnergy > 0) _currentEnergy--;
        energySprite.fillAmount = (float)_currentEnergy / startEnergy;
        if (_currentEnergy == 0)
        {
            // SoundController.instance.PlaySoundRandomPitch(gameLoose);
            DeathController.instance.TriggerDeath("Луноход не смог доставить необходимые материалы. Вы остались на луне и погибли от голода");
            Debug.Log("Loose");
        }
    }

    public void TurnOn()
    {
        _isActive = true;
        gameUI.SetActive(true);
    }

    public void TurnOff()
    {
        _isActive = false;
        gameUI.SetActive(false);
    }

    public void CheckItem(Vector2Int dir)
    {
        if (_roverPosition + dir == itemPoint)
        {
            _hasItem = true;
            // SoundController.instance.PlaySoundRandomPitch(itemGet);
        }
    }

    private bool CheckWin(Vector2Int dir)
    {
        if (_roverPosition + dir == roverStartPoint && _hasItem)
        {
            // SoundController.instance.PlaySoundRandomPitch(gameWin);
            _isGameWon = true;
            _animator.SetTrigger("Win");
            _eventHandler.SetDayCounterTrigger("Rover");
            // DayCounter.Instance.SetTrigger("Rover");
            Dispenser.Instance.SpawnOre();
            SylphietteDialogueSystem.Instance.StartNextDialogue();

            //if (DayCounter.Instance.currentDay == 1)
            if (_dayCounter.CurrentDay == 1)
            {
                StartCoroutine(ShowNextMessage());
            }

            return true;
        }
        return false;
    }

    private IEnumerator ShowNextMessage()
    {
        yield return new WaitForSeconds(9);
        SylphietteDialogueSystem.Instance.StartNextDialogue();
    }
}