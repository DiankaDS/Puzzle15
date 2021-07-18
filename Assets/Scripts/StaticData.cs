using UnityEngine;

public static class StaticData
{
    private static bool isTraining;
    private static int bestMoves = ReadBestMoves();

    private static int ReadBestMoves()
    {
        return 0;
    }
    private static void WriteBestMoves()
    {
        bestMoves = 0;
    }

    public static bool GetIsTrainingFlag()
    {
        return isTraining;
    }

    public static void SetIsTrainingFlag(bool value)
    {
        isTraining = value;
    }

    public static int GetBestMoves()
    {
        return bestMoves;
    }

    public static void SetBestMoves(int moves)
    {
        bestMoves = moves;
    }
}
