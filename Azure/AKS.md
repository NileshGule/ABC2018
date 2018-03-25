# Commands related to Azure Container Service (AKS)

## Set Azure CLI in interactive mode

```bash

az interactive

```

## Set Subscription to one of the available ones

```bash

az account set --subscription "Visual Studio Enterprise"

az account set --subscription "Azure Pass"

az account set --subscription "Sandbox CACIB"

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

OMS Workspace : abc2018sgws

### Store OMS Secret key and ID

```bash

kubectl create secret generic omsagent-secret \
--from-literal=WSID=feb84307-b1ba-414e-baa8-ec8200ce9dee \
--from-literal=KEY=QtXDHX/3Dm9/Z05W+73Xhd/T87DDFg6ZOBmbzRm2p9fhdQbZJ2Bj6B7Etumxm25gAlFamMQjwPUdC4/cTDSJzQ==

```