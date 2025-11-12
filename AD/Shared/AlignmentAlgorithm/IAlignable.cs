namespace AD.CL.AlignmentAlgorithm;

public interface IAlignable
{
    /// <summary>
    /// 获取世界坐标系下的轴对齐包围盒（AABB）
    /// </summary>
    Box GetWorldBoundingBox();

    /// <summary>
    /// 在世界坐标系下平移
    /// </summary>
    void Translate(float deltaX, float deltaY);
}

