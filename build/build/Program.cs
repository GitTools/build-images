return new CakeHost()
    .UseContext<BuildContext>()
    .UseStartup<Startup>()
    .SetToolPath(Constants.ToolsDirectory)
    .Run(args);
