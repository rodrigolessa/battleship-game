# How to Run Locally?

To run the application locally, it is necessary to configure two projects and run them simultaneously, BattleshipGame.WebApi and BattleshipGame.Worker.

Compound/Multiples execution using:

[Rider IDE](https://www.jetbrains.com/help/rider/Run_Debug_Configuration.html#compound-configs)

For reading and recording data, the application uses two databases:

1. To build the Event Sourcing database, follow this [instructions](https://github.com/rodrigolessa/battleship-game)

1. To build the Read Model database, follow this [instructions](https://github.com/rodrigolessa/battleship-game)

Applications depend on setting environment variables to communicate with queues, database and cache systems. To set environment variables, follow this [instructions](./environment-variables.md).

## Installation

1. Clone the repository

```bash
git clone git@github.com:rodrigolessa/battleship-game.git

cd battleship-game
```

2. Restore solution's packages in interactive mode

```bash
dotnet restore --interactive
```
    
3. Point githooks to the correct folder in the project

```bash
git submodule update --init --recursive

git config core.hooksPath my-sonar-rules/.githooks
```

## Running with terminal

1. Clean before compilation

```bash
dotnet clean
```

2. Check your profile name at `launchSettings.json`

```json
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "BattleshipGame.Api": {
      "commandName": "Project",
      "environmentVariables": {
      }
    }
  }
}
```

3. Run the `WebApi` application, using the `--launch-profile` argument with the value equal to your launch profile name

```bash
dotnet run --project BattleshipGame.WebApi/BattleshipGame.WebApi.csproj --launch-profile BattleshipGame.WebApi
```

4. Open another terminal

5. Run the `Worker` application, using the `--launch-profile` argument with the value equal to your launch profile name

```bash
dotnet run --project src/BattleshipGame.Worker/BattleshipGame.Worker.csproj --launch-profile BattleshipGame.Worker
```

## Running with docker-compose

1. Create a _.env_ file based on the _sample.env_ template

2. Build application

```bash
dotnet build
```

3. Build the docker image

```bash
docker-compose -f .docker/docker-compose.yaml build
```

4. Up the container

```bash
docker-compose -f .docker/docker-compose.yaml up -d
```

## Running with minikube

1. Follow the [Running with docker-compose](#running-with-docker-compose) steps

2. Use the Docker daemon inside the Minikube instance

```bash
minikube start

eval $(minikube -p minikube docker-env)
```

3. Build the image

```bash
docker build -f .docker/Dockerfile.webapi -t my-game-api-img:latest-local .
```

4. Export and verify the environment variables

```bash
make prepare

env | sort
```

5. Replace token on k8s manifests

```bash
mkdir .kubernetes/local

cd .kubernetes/ && for file in *.yaml; do python3 ../replace_tokens.py < $file > local/$file; done && cd ..
```

6. Create namespace

```bash
kubectl create namespace battleship-api-local
```

7. Apply the application deployment

```bash
kubectl apply -f .kubernetes/local/
```

8. Check resources status and infos

```bash
kubectl describe -f .kubernetes/local/

kubectl get pods -n battleship-api-local

kubectl logs -n battleship-api-local `<pod_id>`
```

9. Check the service url

```bash
minikube service battleship-api-local -n battleship-api-local --url
```

10. Destroy all resources

```bash
kubectl delete -f .kubernetes/local/

kubectl delete namespace battleship-api-local
```