namespace AD.CL.AlignmentAlgorithm;

public static class AlignExtensions
{
    private const float Tolerance = 1e-6f;

    /// <summary>
    /// 对一组可对齐的对象执行对齐操作
    /// </summary>
    /// <typeparam name="T">实现 IAlignable 接口的类型</typeparam>
    /// <param name="items">要对齐的对象集合</param>
    /// <param name="type">对齐类型</param>
    /// <returns>一个布尔值，表示是否执行了对齐操作</returns>
    public static bool Align<T>(this IEnumerable<T> items, AlignType type) where T : IAlignable
    {
        var list = items.ToList();
        if (list.Count < 2)
        {
            Console.WriteLine("can align zero or two elements, fail silently");
            return false;
        }

        // 获取所有对象的当前状态
        var boxes = list.Select(item => item.GetWorldBoundingBox()).ToList();
        var originalTransforms = list.Select(item => item.GetWorldTransform()).ToList();

        // 计算合并边界框作为对齐参考
        var unionBox = MergeBoxes(boxes);

        // 检查是否已经对齐
        if (IsAlreadyAligned(boxes, type, unionBox))
        {
            return false;
        }

        // 计算并应用新的变换
        for (int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            var box = boxes[i];
            var originalTransform = originalTransforms[i];

            // 计算偏移量
            var (dx, dy) = CalculateOffset(box, type, unionBox);

            // 平移变换
            var newTransform = new Transform(
                originalTransform.A, originalTransform.B, originalTransform.C, originalTransform.D,
                originalTransform.Tx + dx, originalTransform.Ty + dy
            );

            // 应用变换
            item.SetWorldTransform(newTransform);
        }

        return true;
    }

    /// <summary>
    /// 合并多个边界框为一个
    /// </summary>
    private static Box MergeBoxes(IReadOnlyList<Box> boxes)
    {
        float minX = float.MaxValue, minY = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue;

        foreach (var box in boxes)
        {
            if (box.MinX < minX) minX = box.MinX;
            if (box.MinY < minY) minY = box.MinY;
            if (box.MaxX > maxX) maxX = box.MaxX;
            if (box.MaxY > maxY) maxY = box.MaxY;
        }

        return new Box(minX, minY, maxX, maxY);
    }

    /// <summary>
    /// 检查一组边界框是否已经按指定类型对齐
    /// </summary>
    private static bool IsAlreadyAligned(IReadOnlyList<Box> boxes, AlignType type, Box referenceBox)
    {
        float referenceValue = GetAlignmentValue(referenceBox, type);

        return boxes.All(box => Math.Abs(GetAlignmentValue(box, type) - referenceValue) < Tolerance);
    }

    /// <summary>
    /// 计算将一个边界框对齐到参考边界框所需的偏移量
    /// </summary>
    private static (float dx, float dy) CalculateOffset(Box box, AlignType type, Box referenceBox)
    {
        return type switch
        {
            AlignType.Left => (referenceBox.MinX - box.MinX, 0),
            AlignType.HCenter => (referenceBox.CenterX - box.CenterX, 0),
            AlignType.Right => (referenceBox.MaxX - box.MaxX, 0),
            AlignType.Top => (0, referenceBox.MaxY - box.MaxY),
            AlignType.VCenter => (0, referenceBox.CenterY - box.CenterY),
            AlignType.Bottom => (0, referenceBox.MinY - box.MinY),
            _ => throw new ArgumentException("无效的对齐类型", nameof(type)),
        };
    }

    /// <summary>
    /// 根据对齐类型获取边界框的相应值
    /// </summary>
    private static float GetAlignmentValue(Box box, AlignType type) => type switch
    {
        AlignType.Left => box.MinX,
        AlignType.HCenter => box.CenterX,
        AlignType.Right => box.MaxX,
        AlignType.Top => box.MaxY,
        AlignType.VCenter => box.CenterY,
        AlignType.Bottom => box.MinY,
        _ => throw new ArgumentException("无效的对齐类型", nameof(type)),
    };
}
