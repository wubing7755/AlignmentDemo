namespace AD.CL.AlignmentAlgorithm;

/// <summary>
/// 轴对齐边界框
/// </summary>
public readonly struct Box : IEquatable<Box>
{
    /// <summary>
    /// 获取边界框的最小 X 坐标。
    /// </summary>
    public float MinX { get; }

    /// <summary>
    /// 获取边界框的最小 Y 坐标。
    /// </summary>
    public float MinY { get; }

    /// <summary>
    /// 获取边界框的最大 X 坐标。
    /// </summary>
    public float MaxX { get; }

    /// <summary>
    /// 获取边界框的最大 Y 坐标。
    /// </summary>
    public float MaxY { get; }

    /// <summary>
    /// 获取边界框的宽度。
    /// </summary>
    public float Width => MaxX - MinX;

    /// <summary>
    /// 获取边界框的高度。
    /// </summary>
    public float Height => MaxY - MinY;

    /// <summary>
    /// 获取边界框中心点的 X 坐标。
    /// </summary>
    public float CenterX => MinX + Width * 0.5f;

    /// <summary>
    /// 获取边界框中心点的 Y 坐标。
    /// </summary>
    public float CenterY => MinY + Height * 0.5f;

    /// <summary>
    /// 获取边界框的中心点。
    /// </summary>
    public Point Center => new Point(CenterX, CenterY);

    public Box(float minX, float minY, float maxX, float maxY)
    {
        if (minX > maxX) throw new ArgumentException("minX cannot be greater than maxX.");
        if (minY > maxY) throw new ArgumentException("minY cannot be greater than maxY.");

        MinX = minX; 
        MinY = minY;
        MaxX = maxX; 
        MaxY = maxY;
    }

    public static Box FromSize(float x, float y, float width, float height)
    {
        return new Box(x, y, x + width, y + height);
    }

    public bool Equals(Box other)
    {
        return MinX.Equals(other.MinX) && MinY.Equals(other.MinY) && MaxX.Equals(other.MaxX) && MaxY.Equals(other.MaxY);
    }

    public override bool Equals(object? obj)
    {
        return obj is Box other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MinX, MinY, MaxX, MaxY);
    }

    public static bool operator ==(Box left, Box right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Box left, Box right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return $"Box: Min=({MinX}, {MinY}), Max=({MaxX}, {MaxY}), Size=({Width}, {Height})";
    }
}
