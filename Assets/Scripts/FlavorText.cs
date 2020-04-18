public static class FlavorText
{
    public static string GetFlavorForVikingAction(string action)
    {
        switch (action)
        {
            case "Attack":
                "Viking attacked or whatever";
            case "Flee":
                "Viking fled or whatever";
            default:
                "???";
        }
    }
}