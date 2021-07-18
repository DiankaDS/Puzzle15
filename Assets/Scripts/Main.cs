using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Main MainSingleton;

    public event Action OnWin;

    [SerializeField] private GameObject prefab;
    [SerializeField] private Point[] points;
    [SerializeField] private AudioSource moveBlock;
    [SerializeField] private AudioSource notMoveBlock;
    [SerializeField] private Text movesUI;
    [SerializeField] private Text bestMovesUI;
    [SerializeField] private Text getBestMovesUI;

    private List<int> blockNumbers;
    private int[,] blocksArray;
    private Point nullBlock;
    private int moves;
    private int bestMoves;

    private static bool isGameActive;
    private bool isTraining;

    private void Awake()
    {
        InitializeVars();

        if (MainSingleton == null)
        {
            MainSingleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(StartNewGame());
    }

    public void MoveCube(CubeController cube)
    {
        Point positionInArray = cube.point;
        int num = cube.num;

        if (isGameActive && Mathf.Abs(positionInArray.i - nullBlock.i) + Mathf.Abs(positionInArray.j - nullBlock.j) < 2)
        {
            cube.transform.position = nullBlock.transform.position + cube.transform.localPosition + new Vector3(0, 0.2f, 0);
            cube.transform.parent = nullBlock.transform;

            blocksArray[nullBlock.i, nullBlock.j] = num;
            blocksArray[positionInArray.i, positionInArray.j] = 16;

            cube.SetPosition(nullBlock);
            nullBlock = positionInArray;
            moveBlock.Play();
            moves++;
            movesUI.text = $"Moves: {moves.ToString()}";

            if (IsWin())
            {
                OnWin?.Invoke();
                isGameActive = false;
                if (!isTraining && (bestMoves == 0 || moves < bestMoves)) {
                    StaticData.SetBestMoves(moves);
                    getBestMovesUI.text = "Best Moves!";
                }
            }
        }
        else
        {
            cube.transform.position = cube.transform.position + new Vector3(0, 0.2f, 0);
            notMoveBlock.Play();
        }
    }

    public IEnumerator StartNewGame()
    {
        SetBlockNumbers();

        int currentPosition = 15;

        for (int i = 3; i >= 0; i--)
        {
            for (int j = 3; j >= 0; j--)
            {
                int number = isTraining ? blockNumbers[currentPosition] : SetRandomNumber(blockNumbers);
                blocksArray[i,j] = number;

                if (number == 16) {
                    nullBlock = points[currentPosition];
                }
                else
                {
                    CubeController cube = Instantiate(prefab, points[currentPosition].transform)
                        .GetComponent<CubeController>();

                    cube.InitNumber(number);
                    cube.SetPosition(points[currentPosition]);

                    yield return new WaitForSeconds(0.2f);
               	}

               	currentPosition--;
            }
        }

        isGameActive = true;
    }

    private void InitializeVars()
    {
        blockNumbers = new List<int>();
        blocksArray = new int[4, 4];
        isGameActive = false;
        isTraining = StaticData.GetIsTrainingFlag();
        moves = 0;
        bestMoves = StaticData.GetBestMoves();
        movesUI.text = $"Moves: {moves.ToString()}";
        bestMovesUI.text = bestMoves != 0 ? $"Best Moves: {bestMoves.ToString()}" : "";
    }

    private void SetBlockNumbers()
    {
        for (int i = 1; i < 17; i++)
        {
            blockNumbers.Add(i);
        }
        blockNumbers[10] = 12;
        blockNumbers[11] = 15;
        blockNumbers[14] = 11;
    }

    private int SetRandomNumber(List<int> nums)
    {
        int index = UnityEngine.Random.Range(0, nums.Count);
        int number = nums[index];
        nums.RemoveAt(index);

        return number;
    }

    private bool IsWin()
    {
        int val = 1;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (blocksArray[i, j] != val++) {
                    return false;
                }
            }
        }
        return true;
    }
}
