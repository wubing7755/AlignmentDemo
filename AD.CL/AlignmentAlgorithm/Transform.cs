namespace AD.CL.AlignmentAlgorithm;

/// <summary>
/// 一个二维仿射变换矩阵
/// </summary>
/// <remarks>
/// <para>
/// 3x3 矩阵：
/// <code>
/// | a  c  tx |
/// | b  d  ty |
/// | 0  0  1  |
/// </code>
/// </para>
/// <para>
/// 对于一个输入点 (x, y)，其变换后的坐标 (x', y') 计算公式为：
/// <code>
/// x' = a * x + c * y + tx
/// y' = b * x + d * y + ty
/// </code>
/// </para>
/// <para>
/// 应用方法：
/// <code>
/// 平移：Transform(1, 0, 0, 1, tx, ty)
/// 缩放：Transform(sx, 0, 0, sy, 0, 0)
/// 旋转：Transform(cos, sin, -sin, cos, 0, 0)
/// 水平斜切：Transform(1, 0, skewX, 1, 0, 0)
/// 垂直斜切：Transform(1, skewY, 0, 1, 0, 0)
/// </code>
/// </para>
/// </remarks>
public struct Transform
{
    /// <summary>
    /// X 轴缩放因子
    /// </summary>
    public float A { get; set; }

    /// <summary>
    /// Y方向对X的斜切
    /// </summary>
    public float B { get; set; }

    /// <summary>
    /// X方向对Y的斜切
    /// </summary>
    public float C { get; set; }

    /// <summary>
    /// Y 轴缩放因子
    /// </summary>
    public float D { get; set; }

    /// <summary>
    /// X轴平移
    /// </summary>
    public float Tx { get; set; }

    /// <summary>
    /// Y轴平移
    /// </summary>
    public float Ty { get; set; }

    public Transform(float a, float b, float c, float d, float tx, float ty)
        => (A, B, C, D, Tx, Ty) = (a, b, c, d, tx, ty);

    /// <summary>
    /// 单位矩阵
    /// </summary>
    public static readonly Transform Identity = new Transform(1, 0, 0, 1, 0, 0);

    public Transform Clone() => new Transform(A, B, C, D, Tx, Ty);

    public static Transform Translate(float tx, float ty) => new Transform(1, 0, 0, 1, tx, ty);

    public static Transform Scale(float sx, float sy) => new Transform(sx, 0, 0, sy, 0, 0);

    public static Transform Rotate(float degrees)
    {
        float rad = degrees * MathF.PI / 180;
        float c = MathF.Cos(rad);
        float s = MathF.Sin(rad);
        return new Transform(c, s, -s, c, 0, 0);
    }
    public Point ApplyTo(Point p) => new Point(
            p.X * A + p.Y * C + Tx,
            p.X * B + p.Y * D + Ty);

    public Point ApplyVector(Point v) => new Point(
            v.X * A + v.Y * C,
            v.X * B + v.Y * D);

    public static Transform operator *(Transform a, Transform b) => Multiply(a, b);

    public static Transform Multiply(Transform a, Transform b) => new Transform(
            a.A * b.A + a.C * b.B,
            a.B * b.A + a.D * b.B,
            a.A * b.C + a.C * b.D,
            a.B * b.C + a.D * b.D,
            a.A * b.Tx + a.C * b.Ty + a.Tx,
            a.B * b.Tx + a.D * b.Ty + a.Ty);
}
