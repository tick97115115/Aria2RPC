# Aria2RPC

This is a library that provide self-contained Aria2 RPC service management.

For now it's only support RPC service process management. (start & shutdown)

download task management feature will be add in the future.

---

# How to create an Aria2 RPC service object

```csharp
using Aria2RPC.Services;

// you can use default config to create a service object by an empty string or null value.
var service = new Aria2RPCService("");
// or
var service = new Aria2RPCService(null);

// you can also submit a path to an aria2 profile with your own customized configuration.
var service = new Aria2RPCService("path/to/your/profile.conf");
```

---

# How to start & stop the service process

```csharp
// run a service object.
await service.RunAria2ServiceAsync();

// there are three ways to stop the service.
await service.SoftStopAsync();
// or
service.SoftStop();
// or
service.ForceShutdown();
```

No matter how you stop your service object, you should always do free resources by invoking the "close" method.

(class Aria2RPCService is derived from Process class)

```csharp
// for example
await service.SoftStopAsync();
service.close();
```
