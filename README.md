# ABC2018
This Repository contains the complete source code of the demo used for **`Global Azure Boot Camp 2018 at Singapore`**. The event was held on *`21 April 2018`*. The slidedeck used during the talk is available at [Speakerdeck](https://speakerdeck.com/nileshgule/modernize-application-development-with-highly-scalable-architecture-using-docker-and-azure)

The end goal of the demo is to build an application for managing tech talks. There are multiple components which are added progressively to scale the application. All the components are deployed to **`Kubernetes`** cluster using **`Docker`** containers. The Kubernetes cluster is deployed to **`Azure`** using **`Azure Container Service (AKS)`**.

Initially it starts with a web front end named TechTalksWeb. This interacts with a backend API named TechTalksAPI. The persistence part is handled using TechTalksDB. A single node Kubernetes cluster is used to test the application locally using **`Minikube`**

![v1 application overview](/Images/V1-application-overview.png)

**RabbitMQ** provides the messaging capabilities. **Elastic Search** is used to Index data in denormalized form for reporting and visualization purposes. **Kibana** provides the UI to build dashboard using Elastic Search Index.
![Final application overview](/Images/Final-Application-Overview.png)

The code is organized into following structure

- Azure

Contains the Azure CLI commands and syntax for invoking different powershell commands.

- Docker

Contains the docker compose file which describes the services.

- Kubernetes

Contains all the Kubernetes menifest files required for deploying services to the cluster

- Minikube

Contains the Kuberntes menifest for Minikube related deployments

- Powershell

Contains all the Powershell scripts

- TechTalksAPI

Contains source code for the API module. API interacts with SQL server and the RabbitMQ server.

- TechTalksDB

Contains the Initialization script used to bootstrap TechTalksDB with static data.

- TechTalksELKProcessor

Contains source code for message consumer which pulls data from RabbitMQ and populate sthe Index in Elastic Search.

- TechTalksModel

Contains source code for Domain objects and Data Transfer Objects (DTO) which are shared across multiple projects.

- TechTalksProcessor

Contains source code for RabbitMQ consumer which pulls data from RabbitMQ and inserts a record into database.

- TechTalksWeb

Contains source code for web front end of the TechTalks application.