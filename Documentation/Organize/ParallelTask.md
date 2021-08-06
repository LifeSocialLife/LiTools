# ParallelTask



## Howto

### ParallelTask.Start

Input values.

```csharp
public static void Start(Func<Task> action, CancellationToken cancellationToken, ILogger? logger, string taskname = "")

<param name="action">Function to run.</param>
<param name="cancellationToken">Token.</param>
<param name="logger">ILogger.</param>
<param name="taskname">Name of the task.</param>

```

Exempel whitout use of ILogger.

```csharp
public static void Start(Func<Task> action, CancellationToken cancellationToken, string taskname = "")
```

Exempel

```csharp
ParallelTask.Start(() => nameToRun(), _systemCancellationToken.Token, _logger, "taskname1");
```

Exempel whitout ILogger.

```csharp
ParallelTask.Start(() => nameToRun(), _systemCancellationToken.Token,"taskname1");
```

taskname can be null or empty.

Target function exempel.

```csharp
async Task nameToRun()
        {
            ...
        }
```

or

```csharp
async Task nameToRun(CancellationToken cancellationToken)
        {
            ...
        }
```

To start the last exempel. use this code.

```csharp
ParallelTask.Start(() => nameToRun(_systemCancellationToken.Token), _systemCancellationToken.Token,"taskname1");
```

### ParallelTask.StartLongRunning

```csharp
public static void StartLongRunning(Action action, CancellationToken cancellationToken, ILogger? logger, string taskname = "")

<param name="action">Function to run.</param>
<param name="cancellationToken">Token.</param>
<param name="logger">ILogger.</param>
<param name="taskname">Name of the task.</param>

```

Exempel whitout use of ILogger.

```csharp
public static void StartLongRunning(Action action, CancellationToken cancellationToken, string taskname = "")
```

How to use / start.

```csharp
ParallelTask.StartLongRunning(runThisLongtime, _systemCancellationToken.Token, _logger, "taskname1");
```

exemple action to run.

```csharp
void runThisLongtime()
{
    ...
}
```
