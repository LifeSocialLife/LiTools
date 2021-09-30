# ParallelTask

Start a new task that shod run. 

You can start the task as “normal” or long-run, see documentation.



## Howto

### ParallelTask.Start

Input values.

```csharp
public static void Start(Func<Task> action, CancellationToken cancellationToken)

<param name="action">Function to run.</param>
<param name="cancellationToken">Token.</param>
```

Example

```csharp
ParallelTask.Start(() => nameToRun(), _systemCancellationToken.Token);
```

Function action example.
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

### ParallelTask.StartLongRunning

```csharp
public static void StartLongRunning(Action action, CancellationToken cancellationToken)

<param name="action">Function to run.</param>
<param name="cancellationToken">Token.</param>
```

How to use / start.

```csharp
ParallelTask.StartLongRunning(runThisLongtime, _systemCancellationToken.Token);
```

exemple action to run.

```csharp
void runThisLongtime()
{
    ...
}
```
