# drone-win-mon

Drone CI build monitor for Windows

## Quick Start

Ensure you have a file at `%UserProfile%\.config\drone-win-mon\.env`. This is an example with the required variables, fill in your own values.

```
DRONE_URL=https://drone.example.com
DRONE_TOKEN=token
DRONE_REPO_PATHS=owner1/repo1,owner1/repo2
```

## Future Goals

* Consider exanding this basic UI to support plugins. So to have some generic IIndicator which is shown as Green/Red and the implementation(s) could be anything, including Drone Builds.
