# .NET Core Docker Example
An example project for using Docker for local infrastructure in a .NET Core project

To get the infrastructure going:

```
cd scripts/
./recreate-infrastructure.sh
```

Then you can run the integration tests against the database:

```
cd ../ToDo.IntegrationTests/
cp .env.example .env
./with_env.sh dotnet test
```
