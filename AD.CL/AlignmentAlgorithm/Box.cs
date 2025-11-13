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

    /// <summary>
    /// 初始化一个新的 <see cref="Box"/> 结构实例。
    /// </summary>
    /// <param name="minX">最小 X 坐标。</param>
    /// <param name="minY">最小 Y 坐标。</param>
    /// <param name="maxX">最大 X 坐标。</param>
    /// <param name="maxY">最大 Y 坐标。</param>
    public Box(float minX, float minY, float maxX, float maxY)
    {
        if (minX > maxX) throw new ArgumentException("minX cannot be greater than maxX.");
        if (minY > maxY) throw new ArgumentException("minY cannot be greater than maxY.");

        MinX = minX; 
        MinY = minY;
        MaxX = maxX; 
        MaxY = maxY;
    }

    /// <summary>
    /// 根据一个点和一个尺寸创建边界框
    /// </summary>
    /// <param name="x">左上角 X 坐标</param>
    /// <param name="y">左上角 Y 坐标</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    /// <returns>一个新的 <see cref="Box"/> 实例</returns>
    public static Box FromSize(float x, float y, float width, float height)
    {
        return new Box(x, y, x + width, y + height);
    }

    /// <summary>
    /// 判断两个边界框是否相等
    /// </summary>
    /// <param name="other">要比较的另一个边界框</param>
    /// <returns>如果相等则为 true；否则为 false</returns>
    public bool Equals(Box other)
    {
        return MinX.Equals(other.MinX) && MinY.Equals(other.MinY) && MaxX.Equals(other.MaxX) && MaxY.Equals(other.MaxY);
    }

    /// <summary>
    /// 判断当前对象是否与另一个对象相等
    /// </summary>
    /// <param name="obj">要比较的对象</param>
    /// <returns>如果相等则为 true；否则为 false</returns>
    public override bool Equals(object? obj)
    {
        return obj is Box other && Equals(other);
    }

    /// <summary>
    /// 返回此边界框的哈希代码
    /// </summary>
    /// <returns>哈希代码</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(MinX, MinY, MaxX, MaxY);
    }

    /// <summary>
    /// 判断两个边界框是否相等
    /// </summary>
    /// <param name="left">左边的边界框</param>
    /// <param name="right">右边的边界框</param>
    /// <returns>如果相等则为 true；否则为 false</returns>
    public static bool operator ==(Box left, Box right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// 判断两个边界框是否不相等
    /// </summary>
    /// <param name="left">左边的边界框</param>
    /// <param name="right">右边的边界框</param>
    /// <returns>如果不相等则为 true；否则为 false</returns>
    public static bool operator !=(Box left, Box right)
    {
        return !(left == right);
    }

    /// <summary>
    /// 返回表示当前边界框的字符串
    /// </summary>
    /// <returns>字符串表示形式</returns>
    public override string ToString()
    {
        return $"Box: Min=({MinX}, {MinY}), Max=({MaxX}, {MaxY}), Size=({Width}, {Height})";
    }
}
