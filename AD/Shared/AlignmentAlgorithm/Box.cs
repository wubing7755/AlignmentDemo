namespace AD.CL.AlignmentAlgorithm;

public readonly struct Box
{
    public float MinX { get; }
    public float MinY { get; }
    public float MaxX { get; }
    public float MaxY { get; }
    public float Width => MaxX - MinX;
    public float Height => MaxY - MinY;
    public float CenterX => MinX + Width * 0.5f;
    public float CenterY => MinY + Height * 0.5f;

    public Box(float minX, float minY, float maxX, float maxY)
    {
        MinX = minX; MinY = minY;
        MaxX = maxX; MaxY = maxY;
    }
}
