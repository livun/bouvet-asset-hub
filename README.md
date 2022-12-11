# Bouvet Asset Hub
Datbac project/thesis


## Repository directory
## /docs
In the docs folder, you can find documentation of the system's planned architecture and features.
## /src
The source code of the system.


# Run Project
This application runs with `Docker Compose`. 

*Note:  The MSSQL Server `Docker Image` is not compatible with a MAC with Apple Silicon.*

1. Fork the repository
1. Download Docker Desktop. Available [here](https://docs.docker.com/get-docker/).
1. Run Docker Desktop
1. Open a terminal (e.g., Powershell)
1. Write `docker` to verify it's installed. Proceed if it's installed.
1. Copy this command into you terminal
```powershell
docker volume create assethubcontext-vol
```
7. Run this command in you terminal to verify that the volume is created. The volume name assethubcontext-vol should be listed
```powershell
docker volume ls
```
8. From the root folder of the repository **cd** into `/src` and run this command in the termial. (This folder should contain a docker-compose.yml file)
```powershell
docker compose up -d
```
9. Run this command in the termial to verify that all three containers are running. Can be identified  with the names:
- SqlServer
- bouvetassethubui
- bouvetassethubapi

```powershell
docker ps
```
10. Now the application is up and running on [localhost:3000](http://localhost:3000). (Might take some time the first time it runs)
10. Run this command in the terminal from  `/src`, to stop and remove containers and networks created by `docker compose up`.
```powershell
docker compose down
```

