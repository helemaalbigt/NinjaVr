using UnityEngine;

[System.Serializable]
public class AttackerStyle 
{
    public Color avatarColor;
    public Color arenaColor;
    public Color lightingColor;
}

[CreateAssetMenu(fileName = "Data", menuName = "NinjaVR/UserStyleList", order = 1)]
public class UserStyleList : ScriptableObject
{
    public AttackerStyle defaultStyle;
    public AttackerStyle p1;
    public AttackerStyle p2;

    public AttackerStyle GetStyle(AttackingPlayer player)
    {
        switch (player)
        {
            case AttackingPlayer.one:
                return p1;

            case AttackingPlayer.two:
                return p2;

            case AttackingPlayer.none:
            default:
                return defaultStyle;

        }
    }
}

public enum AttackingPlayer
{
    none,
    one,
    two
}
