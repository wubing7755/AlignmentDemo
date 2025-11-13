using AD.CL.AlignmentAlgorithm;

namespace AD.CL.Primitives;

public abstract class Primitive : IAlignable
{
    protected Primitive(string name)
    {
        Id = Guid.NewGuid();
        Props = new();

        Name = new StringPropValue("Name", name);
        AddProp(Name);
        ColorKey = new UintPropValue("ColorKey", ColorKeyManager.GenerateColorKey(this));
        AddProp(ColorKey);
    }

    public Guid Id { get; }

    public UintPropValue ColorKey { get; }

    public StringPropValue Name { get; }

    public List<PropValue> Props { get; }

    protected void AddProp(PropValue propValue)
    {
        Props.Add(propValue);
    }

    /// <heritdoc/>
    public abstract Box GetWorldBoundingBox();

    /// <heritdoc/>
    public virtual Transform GetWorldTransform()
    {
        var currentPos = GetCurrentPosition();
        return new Transform(1, 0, 0, 1, currentPos.X, currentPos.Y);
    }

    /// <heritdoc/>
    public virtual void SetWorldTransform(Transform transform)
    {
        var currentPos = GetCurrentPosition();
        // 计算需要平移的增量
        var deltaX = transform.Tx - currentPos.X;
        var deltaY = transform.Ty - currentPos.Y;
        Translate(deltaX, deltaY);
    }

    /// <summary>
    /// 获取图形的当前位置
    /// 子类必须实现此方法以提供其位置信息
    /// </summary>
    /// <returns>当前位置点</returns>
    protected abstract Point GetCurrentPosition();

    /// <summary>
    /// 根据给定的增量平移图形
    /// 子类必须实现此方法以更新其位置属性
    /// </summary>
    /// <param name="deltaX">X轴平移量。</param>
    /// <param name="deltaY">Y轴平移量。</param>
    public abstract void Translate(float deltaX, float deltaY);

}

public class Circle : Primitive
{
    public Circle() : base("Circle")
    {
        CenterX = new FloatPropValue("CenterX", 10.0f);
        CenterY = new FloatPropValue("CenterY", 10.0f);
        Radius = new FloatPropValue("Radius", 5.0f);
        Color = new UintPropValue("Color", 0xFFFFFFFF);

        AddProp(CenterX);
        AddProp(CenterY);
        AddProp(Radius);
        AddProp(Color);
    }

    public FloatPropValue CenterX { get; }
    public FloatPropValue CenterY { get; }
    public FloatPropValue Radius { get; }
    public UintPropValue Color { get; }

    public override Box GetWorldBoundingBox()
    {
        float left = CenterX.Value - Radius.Value;
        float top = CenterY.Value - Radius.Value;
        float right = CenterX.Value + Radius.Value;
        float bottom = CenterY.Value + Radius.Value;
        return new Box(left, top, right, bottom);
    }

    protected override Point GetCurrentPosition()
    {
        return new Point(CenterX.Value, CenterY.Value);
    }

    public override void Translate(float deltaX, float deltaY)
    {
        CenterX.Value += deltaX;
        CenterY.Value += deltaY;
    }
}

public class Rectangle : Primitive
{
    public Rectangle() : base("Rectangle")
    {
        PosX = new FloatPropValue("PosX", 10.0f);
        PosY = new FloatPropValue("PosY", 10.0f);
        Width = new FloatPropValue("Width", 10.0f);
        Height = new FloatPropValue("Height", 10.0f);
        Color = new UintPropValue("Color", 0xFFFFFFFF);

        AddProp(PosX);
        AddProp(PosY);
        AddProp(Width);
        AddProp(Height);
        AddProp(Color);
    }

    public FloatPropValue PosX { get; }
    public FloatPropValue PosY { get; }
    public FloatPropValue Width { get; }
    public FloatPropValue Height { get; }
    public UintPropValue Color { get; }

    public override Box GetWorldBoundingBox()
    {
        float left = PosX.Value;
        float top = PosY.Value;
        float right = PosX.Value + Width.Value;
        float bottom = PosY.Value + Height.Value;
        return new Box(left, top, right, bottom);
    }

    protected override Point GetCurrentPosition()
    {
        return new Point(PosX.Value, PosY.Value);
    }

    public override void Translate(float deltaX, float deltaY)
    {
        PosX.Value += deltaX;
        PosY.Value += deltaY;
    }
}

public class Triangle : Primitive
{
    public Triangle() : base("Triangle")
    {
        Vertex1X = new FloatPropValue("Vertex1X", 50.0f);
        Vertex1Y = new FloatPropValue("Vertex1Y", 50.0f);
        Vertex2X = new FloatPropValue("Vertex2X", 70.0f);
        Vertex2Y = new FloatPropValue("Vertex2Y", 50.0f);
        Vertex3X = new FloatPropValue("Vertex3X", 60.0f);
        Vertex3Y = new FloatPropValue("Vertex3Y", 60.0f);
        Color = new UintPropValue("Color", 0xFFFFFFFF);

        AddProp(Vertex1X);
        AddProp(Vertex1Y);
        AddProp(Vertex2X);
        AddProp(Vertex2Y);
        AddProp(Vertex3X);
        AddProp(Vertex3Y);
        AddProp(Color);
    }

    public FloatPropValue Vertex1X { get; }
    public FloatPropValue Vertex1Y { get; }
    public FloatPropValue Vertex2X { get; }
    public FloatPropValue Vertex2Y { get; }
    public FloatPropValue Vertex3X { get; }
    public FloatPropValue Vertex3Y { get; }
    public UintPropValue Color { get; }

    public override Box GetWorldBoundingBox()
    {
        float minX = Math.Min(Math.Min(Vertex1X.Value, Vertex2X.Value), Vertex3X.Value);
        float maxX = Math.Max(Math.Max(Vertex1X.Value, Vertex2X.Value), Vertex3X.Value);
        float minY = Math.Min(Math.Min(Vertex1Y.Value, Vertex2Y.Value), Vertex3Y.Value);
        float maxY = Math.Max(Math.Max(Vertex1Y.Value, Vertex2Y.Value), Vertex3Y.Value);
        return new Box(minX, minY, maxX, maxY);
    }

    protected override Point GetCurrentPosition()
    {
        // 使用边界框的左上角作为三角形的位置
        var bbox = GetWorldBoundingBox();
        return new Point(bbox.MinX, bbox.MinY);
    }

    public override void Translate(float deltaX, float deltaY)
    {
        Vertex1X.Value += deltaX; Vertex1Y.Value += deltaY;
        Vertex2X.Value += deltaX; Vertex2Y.Value += deltaY;
        Vertex3X.Value += deltaX; Vertex3Y.Value += deltaY;
    }
}
