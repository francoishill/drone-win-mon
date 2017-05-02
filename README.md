# win-eye

Drone CI build monitor for Windows

## Quick Start

Ensure you have a file at `%UserProfile%\.config\win-eye\.env`. This is an example with the required variables, fill in your own values.

```
DRONE_URL=https://drone.example.com
DRONE_TOKEN=token
DRONE_REPO_PATHS=owner1/repo1,owner1/repo2
```

## BYO plugin (bring your own plugin)

* Implement interfaces `IPluginCollector` and `IPlugin`
* Add your collector to `MainViewModel->m_PluginCollectors` variable.