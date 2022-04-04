public enum ModiferType
{
    MULTIPLY,
    ADD,
    DIVIDE,
    SUBTRACT
}

public static class ModiferHelper
{
    public static float ApplyModifer(float oldVal, float modifer, ModiferType modiferType)
    {
        float val = 0;
        switch (modiferType)
        {
            case ModiferType.ADD: val = oldVal + modifer;
                break;
            case ModiferType.MULTIPLY: val = oldVal * modifer;
                break;
            case ModiferType.DIVIDE: val = oldVal / modifer;
                break;
            case ModiferType.SUBTRACT: val = oldVal - modifer;
                break;
        }
        return val;
    }


    public static float UndoModifer(float oldVal, float modifer, ModiferType modiferType)
    {
        float val = 0;
        switch (modiferType)
        {
            case ModiferType.ADD: val = oldVal - modifer;
                break;
            case ModiferType.MULTIPLY: val = oldVal / modifer;
                break;
            case ModiferType.DIVIDE: val = oldVal * modifer;
                break;
            case ModiferType.SUBTRACT: val = oldVal + modifer;
                break;
        }
        return val;
    }
}
