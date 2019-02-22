using UnityEngine;

public static class NeatLib
{
    public static bool LinesIntersect(Vector2 l1s, Vector2 l1e, Vector2 l2s, Vector2 l2e)
    {
        return LinesIntersect(l1s, l1e, l2s, l2e, out var _);
    }

    public static bool LinesIntersect(Vector2 l1s, Vector2 l1e, Vector2 l2s, Vector2 l2e, out Vector2 intersection)
    {
        intersection = new Vector2();
        float Ua, Ub;

        Ua = ((l2e.x - l2s.x) * (l1s.y - l2s.y) - (l2e.y - l2s.y) * (l1s.x - l2s.x)) /
            ((l2e.y - l2s.y) * (l1e.x - l1s.x) - (l2e.x - l2s.x) * (l1e.y - l1s.y));

        Ub = ((l1e.x - l1s.x) * (l1s.y - l2s.y) - (l1e.y - l1s.y) * (l1s.x - l2s.x)) /
            ((l2e.y - l2s.y) * (l1e.x - l1s.x) - (l2e.x - l2s.x) * (l1e.y - l1s.y));

        if ((Ua >= 0.0f && Ua <= 1.0f) &&
            (Ub >= 0.0f && Ub <= 1.0f))
        {
            intersection.x = l1s.x + Ua * (l1e.x - l1s.x);
            intersection.y = l1s.y + Ua * (l1e.y - l1s.y);

            return true;
        }

        return false;
    }
}
