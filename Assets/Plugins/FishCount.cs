using UnityEngine;

public sealed class FishCount {

    private static FishCount instance = null;

    //fishID = 1
    private int counterTuna = 0;
    private int maxTuna = 30;

    //fishID = 2
    private int counterSockeye = 0;
    private int maxSockeye = 10;

    //fishID = 3
    private int counterShark = 0;
    private int maxShark = 5;

    public static FishCount Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FishCount();
            }
            return instance;
        }
    }

    public void increaseCounter(int fishID)
    {
        switch (fishID)
        {
            case 1:
                counterTuna++;
                break;
            case 2:
                counterSockeye++;
                break;
            case 3:
                counterShark++;
                break;
        }
    }

    public void decreaseCounter(int fishID)
    {
        switch (fishID)
        {
            case 1:
                counterTuna--;
                break;
            case 2:
                counterSockeye--;
                break;
            case 3:
                counterShark--;
                break;
        }
    }

    public bool canSpawn(int fishID)
    {
        switch (fishID)
        {
            case 1:
                return counterTuna < maxTuna ? true : false;
            case 2:
                return counterSockeye < maxSockeye ? true : false;
            case 3:
                return counterShark < maxShark ? true : false;
            default:
                return false;
        }

    }

}
