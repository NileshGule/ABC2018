# ABC2018
This Repository contains the complete source code of the demo used for **[Global Azure Boot Camp 2018 at Singapore](http://singapore.azurebootcamp.net)**. The event was held on *`21 April 2018`*. The slidedeck used during the talk is available at [Speakerdeck](https://speakerdeck.com/nileshgule/modernize-application-development-with-highly-scalable-architecture-using-docker-and-azure). The codebase is useful for building a multi-container application with deployment to Kubernetes cluster.

The goal of the demo is to build an application for managing tech talks. There are multiple components which are added progressively to scale the application. All the components are deployed to **`Kubernetes`** cluster using **`Docker`** containers. The Kubernetes cluster is deployed to **`Azure`** using **`Azure Container Service (AKS)`**.

Initially it starts with a web front end named `TechTalksWeb`. This interacts with a backend API named `TechTalksAPI`. The persistence part is handled using `TechTalksDB`. A single node Kubernetes cluster is used to test the application locally using **`Minikube`**

![v1 application overview](/Images/V1-application-overview.png)

**RabbitMQ** provides the messaging capabilities. **Elastic Search** is used to Index data in denormalized form for reporting and visualization purposes. **Kibana** provides the UI to build dashboard using Elastic Search Index.
![Final application overview](/Images/Final-Application-Overview.png)

## Code Structure

***

The code is organized into following structure

- [Azure](/Azure)

Contains the Azure CLI commands and syntax for invoking different powershell commands.

- [Docker](/Docker)

Contains the docker compose file which describes the services. This is helpful in building and pushing Docker images with single docker-compose command.

- [Kubernetes](/Kubernetes)

Contains all the Kubernetes manifest files required for deploying services to the cluster. The manifest files contains required files for deploying application specific services like web front end, API nad database as well as infrastructure services like RabbitMQ, ELK cluster, OMS agents for `Container Monitoring Solution`.

- [Minikube](/Minikube)

Contains the Kubernetes manifest for Minikube related deployments. These menifest files are similar to the ones in Kubernetes directory. The main difference is that the Minikube version uses `NodePort` for exposing the services outside of the cluster. While in the AKS Kubernetes cluster, services are exposed using `LoadBalancer`. LoadBalancer privisions a public IP.

- [Powershell](/Powershell)

Contains all the Powershell scripts. These are helper scripts which are useful for deploying the application to Minikube or Kubernetes cluster. There are couple of scripts which are used for initializing the Kubernetes cluster and to tear down the resources after completing the tests.

- [TechTalksAPI](/TechTalksAPI)

Contains source code for the API module. API interacts with SQL server and the RabbitMQ server. The project type is .Net Core exe.

- [TechTalksDB](/TechTalksDB)

Contains the Initialization script used to bootstrap TechTalksDB with static data.

- [TechTalksELKProcessor](/TechTalksELKProcessor)

Contains source code for message consumer which pulls data from RabbitMQ and populates the Index in Elastic Search. The project type is .Net Core exe.

- [TechTalksModel](/TechTalksModel)

Contains source code for `Domain objects` and `Data Transfer Objects (DTO)` which are shared across multiple projects. The project type is .Net Core library.

- [TechTalksProcessor](/TechTalksProcessor)

Contains source code for RabbitMQ consumer which pulls data from RabbitMQ and inserts a record into database. The project type is .Net Core exe.

- [TechTalksWeb](/TechTalksWeb)

Contains source code for web front end of the TechTalks application built using ASP.Net Core MVC framework.