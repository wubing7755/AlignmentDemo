namespace AD.CL.AlignmentAlgorithm;

public static class AlignExtensions
{
    /// <summary>
    /// 对齐算法
    /// </summary>
    /// <remarks>适用于实现 IAlignable接口 的元素</remarks>
    public static void Align<T>(IEnumerable<T> items, AlignType type)
        where T : IAlignable
    {
        var list = items.ToList();
        if (list.Count < 2)
        {
            Console.WriteLine("can align zero or two elements, fail silently");
            return;
        }

        // 1. 计算所有包围盒
        var boxes = list.Select(x => x.GetWorldBoundingBox()).ToList();

        // 2. 计算总包围盒（对齐参考线）
        var unionBox = MergeBoxes(boxes);

        // 3. 已对齐直接返回
        if (IsAlreadyAligned(boxes, type))
            return;

        foreach (var (item, box) in list.Zip(boxes))
        {
            float dx = 0, dy = 0;

            switch (type)
            {
                case AlignType.Left:
                    dx = unionBox.MinX - box.MinX;
                    break;
                case AlignType.HCenter:
                    dx = unionBox.CenterX - box.CenterX;
                    break;
                case AlignType.Right:
                    dx = unionBox.MaxX - box.MaxX;
                    break;

                case AlignType.Top:
                    dy = unionBox.MinY - box.MinY;
                    break;
                case AlignType.VCenter:
                    dy = unionBox.CenterY - box.CenterY;
                    break;
                case AlignType.Bottom:
                    dy = unionBox.MaxY - box.MaxY;
                    break;
            }

            if (MathF.Abs(dx) > 0.001f || MathF.Abs(dy) > 0.001f)
            {
                item.Translate(dx, dy);
            }
        }
    }

    // 合并包围盒
    private static Box MergeBoxes(IReadOnlyList<Box> boxes)
    {
        float minX = float.MaxValue, minY = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue;

        foreach (var b in boxes)
        {
            minX = Math.Min(minX, b.MinX);
            minY = Math.Min(minY, b.MinY);
            maxX = Math.Max(maxX, b.MaxX);
            maxY = Math.Max(maxY, b.MaxY);
        }

        return new Box(minX, minY, maxX, maxY);
    }

    // 是否已对齐（带容差）
    private static bool IsAlreadyAligned(IReadOnlyList<Box> boxes, AlignType type)
    {
        if (boxes.Count < 2) return true;

        float reference = GetAlignmentValue(boxes[0], type);
        const float tolerance = 0.5f; // 0.5 像素容差

        return boxes.All(b => MathF.Abs(GetAlignmentValue(b, type) - reference) <= tolerance);
    }

    private static float GetAlignmentValue(Box box, AlignType type) => type switch
    {
        AlignType.Left => box.MinX,
        AlignType.HCenter => box.CenterX,
        AlignType.Right => box.MaxX,
        AlignType.Top => box.MinY,
        AlignType.VCenter => box.CenterY,
        AlignType.Bottom => box.MaxY,
        _ => 0
    };
}
