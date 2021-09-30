﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿# LiTools.Helpers.IO

[XmlDoc is available.](/docs/xmldocmd/LiTools.Helpers.IO.md)

## Functions

#### Directory

#### Files


## Version Information

No version is public on nuget. build from source if you want to use this.

| Version | Status |
| --- | --- |
| 0.0.2-beta1 |Dev version - Not released.|
| 0.0.1 | Latest stable release. Available on nuget |

## Version History and Information

### 0.0.2

#### New functions.

##### Get Files Inside Directory

```csharp
<param name="directory">Directory to scan.</param>
<param name="returnCompletPath">Return whit directory information or not.</param>
<returns>true if try work or false if catch happends. filelist as list.</returns>

public static Tuple<bool, List<string>> GetFilesInsideDirectory(string directory, bool returnCompletPath)
```
##### Rename folder

```csharp
<param name="from">current location.</param>
<param name="to">New location.</param>
<returns>true or false.</returns>

public static bool Rename(string from, string to)
```
##### File Move

```csharp
<summary>
Move file to new location.
</summary>
<param name="from">path to file.</param>
<param name="to">path to move the file info.</param>
<returns>true or false.</returns>

public static bool Move(string from, string to)
```

##### File Rename
Rename file.
```csharp
<param name="from">path to file.</param>
<param name="to">New name whit path.</param>
<returns>true or false.</returns>

public static bool Rename(string from, string to)
```

##### File GetFolderFromFilePath
Get Directory Path from file path.
```csharp
<param name="file">path to file.</param>
<returns>Path to directory.</returns>

public static string GetFolderFromFilePath(string file)
```

##### GetFileExtension
Get file extension.
```csharp
<param name="filename">Path to file.</param>
<returns>extension of file.</returns>

public static string GetFileExtension(string filename)
```

#### Obsolete and removed functions.

##### Directory Create
this is obsolete and will be removed. use Create instead.

### 0.0.1

