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

    public Box GetWorldBoundingBox()
    {
        throw new NotImplementedException();
    }

    public void Translate(float deltaX, float deltaY)
    {
        throw new NotImplementedException();
    }

    protected void AddProp(PropValue propValue)
    {
        Props.Add(propValue);
    }
}

public class Circle : Primitive
{
    public Circle() : base("Circle")
    {
        CenterX = new FloatPropValue("CenterX", 0.0f);
        CenterY = new FloatPropValue("CenterY", 0.0f);
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
}

public class Rectangle : Primitive
{
    public Rectangle() : base("Rectangle")
    {
        PosX = new FloatPropValue("PosX", 0.0f);
        PosY = new FloatPropValue("PosY", 0.0f);
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
}

public class Triangle : Primitive
{
    public Triangle() : base("Triangle")
    {
        Vertex1X = new FloatPropValue("Vertex1X", 0.0f);
        Vertex1Y = new FloatPropValue("Vertex1Y", 0.0f);
        Vertex2X = new FloatPropValue("Vertex2X", 10.0f);
        Vertex2Y = new FloatPropValue("Vertex2Y", 0.0f);
        Vertex3X = new FloatPropValue("Vertex3X", 5.0f);
        Vertex3Y = new FloatPropValue("Vertex3Y", 10.0f);
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
}
