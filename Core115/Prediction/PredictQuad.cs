namespace Core;
public unsafe record struct PredictQuad(PredictVertex A, PredictVertex B, PredictVertex C, PredictVertex D)
{
    public bool IsSuitable(Quad* quad) => A.IsSuitable(quad->A) && B.IsSuitable(quad->B) && C.IsSuitable(quad->C) && D.IsSuitable(quad->D);

    public static implicit operator PredictQuad((PredictVertex A, PredictVertex B, PredictVertex C, PredictVertex D) args) => new(args.A, args.B, args.C, args.D);
}