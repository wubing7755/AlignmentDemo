namespace AD.CL.AlignmentAlgorithm;

/// <summary>
/// 一个二维仿射变换矩阵
/// </summary>
/// <remarks>
/// <para>
/// 3x3 矩阵：
/// 约定：列向量左乘
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
    /// 沿X轴方向的错切因子（影响Y坐标）
    /// </summary>
    public float B { get; set; }

    /// <summary>
    /// 沿Y轴方向的错切因子（影响X坐标）
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

    public static Transform Translate(float tx, float ty) => new Transform(1, 0, 0, 1, tx, ty);
}
