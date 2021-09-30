# File.ReadBinaryFile method (1 of 2)

Read file as byte.

```csharp
public static byte[]? ReadBinaryFile(string filename)
```

| parameter | description |
| --- | --- |
| filename | filename to read. |

## Return Value

return as byte.

## See Also

* class [File](../File.md)
* namespace [LiTools.Helpers.IO](../../LiTools.Helpers.IO.md)

---

# File.ReadBinaryFile method (2 of 2)

Read file as byte. Start reading from input pos.

```csharp
public static Tuple<bool, byte[]> ReadBinaryFile(string filename, int from, int len)
```

| parameter | description |
| --- | --- |
| filename | Filename to read. |
| from | seek position to start read. |
| len | lengt to read from file. |

## Return Value

true/false &#x7C; byte from file if it was true.

## See Also

* class [File](../File.md)
* namespace [LiTools.Helpers.IO](../../LiTools.Helpers.IO.md)

<!-- DO NOT EDIT: generated by xmldocmd for LiTools.Helpers.IO.dll -->