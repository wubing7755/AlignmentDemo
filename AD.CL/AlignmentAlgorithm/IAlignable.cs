namespace AD.CL.AlignmentAlgorithm;

public interface IAlignable
{
    /// <summary>
    /// 获取世界坐标系下的轴对齐包围盒（AABB）
    /// </summary>
    Box GetWorldBoundingBox();

    /// <summary>
    /// 获取图形的世界变换矩阵
    /// </summary>
    Transform GetWorldTransform();

    /// <summary>
    /// 设置图形的世界变换矩阵
    /// </summary>
    void SetWorldTransform(Transform transform);
}
