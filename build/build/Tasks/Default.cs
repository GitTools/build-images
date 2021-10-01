[TaskName(nameof(Default))]
[TaskDescription("Shows this output")]
public class Default : FrostingTask
{
    public override void Run(ICakeContext context)
    {
        var tasks = Assembly.GetEntryAssembly()?.FindAllDerivedTypes(typeof(IFrostingTask)).ToList();
        if (tasks == null) return;
        context.Information($"Available targets:{Environment.NewLine}");
        foreach (var task in tasks)
        {
            context.Information($"dotnet run/build.dll --target={task.GetTaskName()} # ({task.GetTaskDescription()})");
        }
    }
}
