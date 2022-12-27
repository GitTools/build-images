return new CakeHost()
    .UseContext<BuildContext>()
    .UseLifetime<BuildLifetime>()
    .UseTaskLifetime<BuildTaskLifetime>()
    .SetToolPath(Constants.ToolsDirectory)
    .Run(args);
