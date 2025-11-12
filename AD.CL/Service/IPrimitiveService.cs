using AD.CL.Primitives;

namespace AD.CL.Service;

public interface IPrimitiveService
{
    void Add(Primitive primitive);
    IEnumerable<Primitive> GetAll();
    void SetSelected(Primitive primitive);
    void SetSelectedRange(IEnumerable<Primitive> primitives);
    void AppendSelected(Primitive primitive);
    void ClearSelection();
    IEnumerable<Primitive> GetSelection();

    event Action<Primitive>? OnChange;
    event Action<IEnumerable<Primitive>>? OnSelectionChanged;
}
