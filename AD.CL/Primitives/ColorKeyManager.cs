using System.Collections.Concurrent;

namespace AD.CL.Primitives;

public static class ColorKeyManager
{
    private static readonly ConcurrentDictionary<uint, Primitive> _colorMap = new();

    // 从非黑色开始
    private static uint _currentKey = 0xFF000001;

    public static uint GenerateColorKey(Primitive primitive)
    {
        lock (_colorMap)
        {
            // 处理键溢出（0xFFFFFFFF 是 uint 最大值）
            if (_currentKey >= 0xFFFFFFFE || _currentKey == 0xFF000000)
                _currentKey = 0xFF000001;

            var colorKey = _currentKey++;
            _colorMap[colorKey] = primitive;
            return colorKey;
        }
    }

    public static bool TryGetPrimitive(uint colorKey, out Primitive? primitive) =>
        _colorMap.TryGetValue(colorKey, out primitive);

    public static void ReleaseColorKey(uint colorKey) =>
        _colorMap.TryRemove(colorKey, out _);
}
