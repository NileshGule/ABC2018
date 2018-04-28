# Kubernetes

This directory contains a set of Kubernetes manifest files required for deploying the TechTalks application to Kubernetes cluster. The names of the sub folders are self explanatory.

## Pre-requisite

Kubernetes cluster should be provisioned before running the scripts in this folder to create or update the resources. Container Monioring solution needs to be created before running the OMS manifest. Also the secret needs to be created in the Kubrnetes cluster. Refer to the [link](https://docs.microsoft.com/en-us/azure/aks/tutorial-kubernetes-monitor) for more details on setting up log monitoring solution.

## Note

Due to the resource requirements for different Docker images, it is advisable to deploy the components in following order

1. 00_ABCNamespace.yml
2. 01_StorageClass.yml
3. 02_PersistantVolumeClaim.yml
4. OMS
5. ElasticSearch
6. RabbitMQ
7. TechTalksDB
8. TechTalksAPI
9. TechTalksWeb
10. TechTalksProcessor
11. TechTalksELKProcessor

The order is necessary as ElasticSearch image tries to modify the heap size of the host machine. It has a resource requirement of `minimum 2 GB` of memory. Similarly `SQL Server 2017 Linux` image has a requirement of `minimum 2 GB RAM`.

Except for the OMS part, all other components / services can be deployed using [deploy tech talks](../Powershell/deployTechTalks.ps1) Powershell script which takes care of ordering.