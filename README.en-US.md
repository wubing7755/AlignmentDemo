# Primitive Alignment Example Project

![Blazor WASM](https://img.shields.io/badge/Blazor-WebAssembly-blueviolet)
![.NET](https://img.shields.io/badge/.NET-6.0%2B-blue)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![EN](https://img.shields.io/badge/Language-English-blue)](README.en-US.md)
[![CN](https://img.shields.io/badge/语言-中文-red)](README.md)

## Interface Definition

```csharp
public interface IAlignable
{
    /// <summary>
    /// Gets the axis-aligned bounding box (AABB) in world coordinates
    /// </summary>
    Box GetWorldBoundingBox();

    /// <summary>
    /// Gets the world transformation matrix
    /// </summary>
    Transform GetWorldTransform();

    /// <summary>
    /// Sets the world transformation matrix
    /// </summary>
    void SetWorldTransform(Transform transform);
}
```

## Bounding Box Calculation

```html
<svg width="100" height="100">
  <symbol xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" content="&amp;lt;mxfile&amp;gt;&amp;lt;diagram id=&amp;quot;zgD00f8TT1Lvv8KWO0p6&amp;quot; name=&amp;quot;Page-1&amp;quot;&amp;gt;5VlNc5swEP01zKSHMgIhDMfY+eglM53JofFRNbLRBCOPLMe4v77CSAYhHNMEt27ig4ddISG997TsCgdOlsU9x6v0gSUkc3yQFA68cXw/QLH8Lx27ygE9VDkWnCaVC9SOR/qLVE5Pezc0IWvlq1yCsUzQlemcsTwnM2H4MOdsa942Z1liOFZ4QSzH4wxntvcHTURaeSMEav83QhepfrIHVMsS65uVY53ihG0bLnjrwAlnTFRXy2JCshI7E5e7I62HiXGSiz4dFBEvONuotal5iZ1eLGebPCHl/cCB421KBXlc4VnZupXsSl8qlpm0PHmZ4HW6v7c0DosrO9ozU5N9IVyQouFSM70nbEkE38lbVGsI1OwOshlV9rZBglpR2sB/FCjqFe2Lw8g1MvJCgdMNlH8aKJIn16W4pPUzY7NnExnZekczbR1FgySG9GwsGmvVemuuVfs4ybCgL6ZguwBQT/jOqJzJAWqoBaqgDkJgDrFmGz4jqldTYKcG8lsDCcwXRFgD7fk4LLsXRfC9FK3lXISKNuH/R5ln7o4Ajd5IGTox0HCUBRZlU4szGRzEnhzOnsmEZYxLf85y2TzGGV3k0pxJToj0j+eSr9Y9DYLLUENlGL9W3ZY0ScrHdEY1M+4NEL5gELf2FLTClxfa+tC8vid8hRbQTx8XaD8KTaABsoHueE94r+y5vkCPTgchwSnOF0fhaMDImZARgZUIfvV80ILd8WGCSJQEFmmyJfJ/wjA8y0s4dIPAwNePfbcDYgTdoAPlOHaHAFqP0UD66oHmUtayK5BX0y8XqvA5y/VbBg3DCYqQQQiM7cQIBjYV0QA02InR1QMuNA24+EQ0hKEZ4CHwLBqCDhpiFw1ARJ/0J8tkidQj8OD1qqqb5rQoAWvHHQAQIHMFYsM/3/+64hEA3s14MlQIAm5L8DrtbwreBhoOALOdsnxYmKO4DbOHXDvOnwloO2Wxga7TeTs0yIXz3VO52V2kzana+3vjpjCsnbYKKhrdpDXVI8rrulNp6D4VD/osoOpplMJH6aiScqdZjFfpteG6kBIjjFsZLGgR3bfEGIHIFBY8W4nh9cjI+unIM1RUi+qUjhoqqjV1Th150cULSZeUByG1hugrpPZA0D+fkHqclX0wIXUEJK2tSxFS+0gQtc76egsp9l4faEAhRZ8nhQhaCQTy/1oCoQ8am5XJZF9VqOKkMj5TfWKxEVlcdFXskatPBN/Fh12wn0/2PQ5GjqDXgfHxE73QBFSGow55I9QR/gao+DR+g328+ScYBn47p7QRjLoixGgABHvUzG9GUAUD84tYG8lQ/gA4mxrtUOt3HTBHf769pVl/paxegvWnXnj7Gw==&amp;lt;/diagram&amp;gt;&amp;lt;/mxfile&amp;gt;" viewBox="-0.5 -0.5 427 217" id="alignment">
    <defs></defs>
    <g>
      <g>
        <rect x="316" y="57" width="89" height="74" fill="#ffffff" stroke="#000000" stroke-dasharray="3 3" pointer-events="all"></rect>
      </g>
      <g>
        <path d="M 34 200 L 34 168.12" fill="none" stroke="#000000" stroke-miterlimit="10" pointer-events="stroke"></path>
        <path d="M 34 161.12 L 37.5 168.12 L 30.5 168.12 Z" fill="#000000" stroke="#000000" stroke-miterlimit="10" pointer-events="all"></path>
      </g>
      <g>
        <path d="M 26 197 L 57.88 197" fill="none" stroke="#000000" stroke-miterlimit="10" pointer-events="stroke"></path>
        <path d="M 64.88 197 L 57.88 200.5 L 57.88 193.5 Z" fill="#000000" stroke="#000000" stroke-miterlimit="10" pointer-events="all"></path>
      </g>
      <g>
        <rect x="63" y="203" width="16" height="12" fill="none" stroke="none" pointer-events="all"></rect>
      </g>
      <g>
        <g transform="translate(-0.5 -0.5)">
          <switch>
            <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility">
              <div xmlns="http://www.w3.org/1999/xhtml">
                <div>
                  <div>Y</div>
                </div>
              </div>
            </foreignObject><text x="71" y="213" fill="light-dark(#000000, #ffffff)" font-family="&amp;quot;Helvetica&amp;quot;" font-size="12px" text-anchor="middle">Y</text>
          </switch>
        </g>
      </g>
      <g>
        <rect x="0" y="145" width="19" height="11" fill="none" stroke="none" pointer-events="all"></rect>
      </g>
      <g>
        <g transform="translate(-0.5 -0.5)">
          <switch>
            <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility">
              <div xmlns="http://www.w3.org/1999/xhtml">
                <div>
                  <div>X</div>
                </div>
              </div>
            </foreignObject><text x="9" y="154" fill="light-dark(#000000, #ffffff)" font-family="&amp;quot;Helvetica&amp;quot;" font-size="12px" text-anchor="middle">X</text>
          </switch>
        </g>
      </g>
      <g>
        <path d="M 320.44 32.05 L 373.93 81.6 L 320.44 131.16 Z" fill="#d5e8d4" stroke="#82b366" stroke-miterlimit="10" transform="rotate(-120,347.18,81.6)" pointer-events="all"></path>
      </g>
      <g>
        <rect x="299" y="137" width="34" height="8" fill="none" stroke="none" pointer-events="all"></rect>
      </g>
      <g>
        <g transform="translate(-0.5 -0.5)">
          <switch>
            <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility">
              <div xmlns="http://www.w3.org/1999/xhtml">
                <div>
                  <div>(MinX, MinY)</div>
                </div>
              </div>
            </foreignObject><text x="316" y="143" fill="light-dark(#000000, #ffffff)" font-family="&amp;quot;Helvetica&amp;quot;" font-size="5px" text-anchor="middle">(MinX, MinY)</text>
          </switch>
        </g>
      </g>
      <g>
        <rect x="383" y="41" width="44" height="9.5" fill="none" stroke="none" pointer-events="all"></rect>
      </g>
      <g>
        <g transform="translate(-0.5 -0.5)">
          <switch>
            <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility">
              <div xmlns="http://www.w3.org/1999/xhtml">
                <div>
                  <div>(MaxX, MaxY)</div>
                </div>
              </div>
            </foreignObject><text x="405" y="47" fill="light-dark(#000000, #ffffff)" font-family="&amp;quot;Helvetica&amp;quot;" font-size="5px" text-anchor="middle">(MaxX, MaxY)</text>
          </switch>
        </g>
      </g>
      <g>
        <ellipse cx="316" cy="130.5" rx="1.5" ry="1.5" fill="#0050ef" stroke="#001dbc" pointer-events="all"></ellipse>
      </g>
      <g>
        <ellipse cx="405" cy="57" rx="1.5" ry="1.5" fill="#0050ef" stroke="#001dbc" pointer-events="all"></ellipse>
      </g>
      <g>
        <path d="M 360.5 131 L 360.5 57" fill="none" stroke="#000000" stroke-width="0.5" stroke-miterlimit="10" stroke-dasharray="1.5 1.5" pointer-events="stroke"></path>
      </g>
      <g>
        <path d="M 359 94 L 405 94" fill="none" stroke="#000000" stroke-width="0.5" stroke-miterlimit="10" stroke-dasharray="1.5 1.5" pointer-events="stroke"></path>
      </g>
      <g>
        <path d="M 316 94 L 362 94" fill="none" stroke="#000000" stroke-width="0.5" stroke-miterlimit="10" stroke-dasharray="1.5 1.5" pointer-events="stroke"></path>
      </g>
      <g>
        <ellipse cx="360.5" cy="94" rx="1.5" ry="1.5" fill="#0050ef" stroke="#001dbc" pointer-events="all"></ellipse>
      </g>
      <g>
        <rect x="359" y="98" width="49" height="8.52" fill="none" stroke="none" pointer-events="all"></rect>
      </g>
      <g>
        <g transform="translate(-0.5 -0.5)">
          <switch>
            <foreignObject pointer-events="none" width="100%" height="100%" requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility">
              <div xmlns="http://www.w3.org/1999/xhtml">
                <div>
                  <div>(CenterX, CenterY)</div>
                </div>
              </div>
            </foreignObject><text x="384" y="104" fill="light-dark(#000000, #ffffff)" font-family="&amp;quot;Helvetica&amp;quot;" font-size="5px" text-anchor="middle">(CenterX, CenterY)</text>
          </switch>
        </g>
      </g>
      <g>
        <ellipse cx="106.5" cy="28" rx="27.5" ry="27.5" fill="#d5e8d4" stroke="#82b366" pointer-events="all"></ellipse>
      </g>
      <g>
        <rect x="143" y="145" width="83" height="37" fill="#d5e8d4" stroke="#82b366" pointer-events="all"></rect>
      </g>
      <g>
        <rect x="79" y="0" width="326" height="182" fill="none" stroke="#666600" stroke-dasharray="3 3" pointer-events="all"></rect>
      </g>
    </g>
    <switch>
      <g requiredFeatures="http://www.w3.org/TR/SVG11/feature#Extensibility"></g><a transform="translate(0,-5)" xlink:href="https://www.drawio.com/doc/faq/svg-export-text-problems" target="_blank"><text text-anchor="middle" font-size="10px" x="50%" y="100%">Text is not SVG - cannot display</text></a>
    </switch>
  </symbol>
</svg>
```

## Alignment Algorithm

1. Retrieve world bounding boxes for all primitives
2. Calculate union of all bounding boxes to get combined bounds
3. Compute alignment positions based on combined bounds properties (MinX, MinY, MaxX, MaxY, CenterX, CenterY)

```csharp
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
        _ => throw new ArgumentException("Invalid alignment type", nameof(type)),
    };
}
```
