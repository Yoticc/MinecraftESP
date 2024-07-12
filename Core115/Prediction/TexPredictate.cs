namespace Core;
public unsafe record TexPredictate(params PredictQuad[] Quads)
{
    public bool IsSuitable(Quad* quads, int count)
    {
        if (Quads.Length > count)
            return false;

        for (var predicateIndex = 0; predicateIndex < Quads.Length; predicateIndex++)
        {
            var found = false;
            for (var quadIndex = 0; quadIndex < count; quadIndex++)
                if (Quads[predicateIndex].IsSuitable(quads + quadIndex))
                {
                    found = true;
                    break;  
                }

            if (!found)
                return false;
        }

        return true;
    }
}

public unsafe class MultiTexPredicate
{
    public MultiTexPredicate(params PredictQuad[][] predicates)
    {
        Predicates = new TexPredictate[predicates.Length];
        for (var i = 0; i < predicates.Length; i++)
            Predicates[i] = new(predicates[i]);
    }

    public TexPredictate[] Predicates;

    public bool IsSuitable(Quad* quads, int count)
    {
        foreach (var predicate in Predicates)
            if (predicate.IsSuitable(quads, count))
                return true;
        return false;
    }
}