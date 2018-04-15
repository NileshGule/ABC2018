# Commands related to Azure Container Service (AKS)

## Set Azure CLI in interactive mode

```bash

az interactive

```

## Set Subscription to one of the available ones

```bash

az account set --subscription "Visual Studio Enterprise"

az account set --subscription "Azure Pass"

```

## Enable AKS preview for subscription

```bash

az provider register -n Microsoft.ContainerService

```

## Monitor state of registration

```bash

az provider show -n Microsoft.ContainerService

```

## Variables for the azure CLI script

```bash

resourceGroupName="ABC2018ResourceGroup"
resourceGroupLocaltion="East US"
clusterName="ABC20182AKSCluster"

```

## Intsall kubectl CLI

```bash

az aks install-cli

```

## Create Resource Group

```bash

az group create \
--name $resourceGroupName \
--location $resourceGroupLocaltion \
--output jsonc

```

## Create AKS cluster

```bash

time \
az aks create \
--resource-group $resourceGroupName \
--name $clusterName \
--node-count 2 \
--kubernetes-version 1.8.1 \
--output jsonc

```

## Connect to AKS cluster

```bash

az aks get-credentials \
--resource-group $resourceGroupName \
--name $clusterName

```

## Verify connection to cluster

```bash

kubectl get nodes

```

## Get cluster versions

```bash

az aks get-upgrades \
--name $clusterName \
--resource-group $resourceGroupName \
--output jsonc

```

## Upgrade cluster to 1.8.1 version (if required)

```bash

az aks upgrade \
--name $clusterName \
--resource-group $resourceGroupName \
--kubernetes-version 1.8.1 \
--output jsonc

```

## Run the application

```bash

kubectl create -f coredemo.yml

```

## Monitor the progress of service

```bash

kubectl get service coremvc --watch

kubectl get service mssql-deployment --watch

```

## Browse AKS cluster dashboard

```bash

az aks browse \
--resource-group $resourceGroupName \
--name $clusterName

```

## Delete resource group

```bash

az group delete \
--name $resourceGroupName \
--yes \
--no-wait

```

Remove `--no-wait` parameter to wait for the call to complete

### Run powershell script to provision AKS cluster with all default parameters

```powershell

./initializeAKS.ps1

```

### Run powershell script to browse AKS cluster

```powershell

./browseAKS.ps1

```

### Run powershell script to teardown services

```powershell

./teardownTechTalks.ps1

```

### Run powershell script to delete resource group

```powershell

./deleteRG.ps1 -resourceGroupName ABC2018ResourceGroup

```

### Run deployment in `AKS mode`

```powershell

./deployTechTalks.ps1 -IsLocalCluster $False

```

### Run deployment in `minikube mode`

```powershell

./deployTechTalks.ps1 -IsLocalCluster $True

```

OMS Workspace : abc2018sgws

### Store OMS Secret key and ID

```bash

kubectl create secret generic omsagent-secret \
--from-literal=WSID=bff3ad60-541a-49ff-93f9-e52d2facbb41 \
--from-literal=KEY=u0eILhl3F9d999+NsUrU+SPUFWSiPhRKnx26KBArmfEpp2xMrCJrcdI+N9zTnlKFJD+7A1JiUmS9fW6BihfeCg==
```

### List all Kubernetes contexts

```bash

kubectl config get-contexts

```

### Set Kubernetes context to `minicube`

```bash

kubectl config use-context minicube

```

### Set Kubernetes context to `ABC2018AKSCluster`

```bash

kubectl config use-context ABC2018AKSCluster

```