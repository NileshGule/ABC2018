Param(
    [parameter(Mandatory=$false)][string]$subscriptionName="Visual Studio Enterprise",
    [parameter(Mandatory=$false)][string]$resourceGroupName="ABC2018ResourceGroup",
    [parameter(Mandatory=$false)][string]$resourceGroupLocaltion="East US",
    [parameter(Mandatory=$false)][string]$clusterName="ABC2018AKSCluster"
)

# Set Azure subscription name
Write-Host "Setting Azure subscription to $subscriptionName"  -ForegroundColor Yellow
az account set --subscription=$subscriptionName

# Create resource group name
Write-Host "Creating resource group $resourceGroupName in region $resourceGroupLocaltion" -ForegroundColor Yellow
az group create `
--name=$resourceGroupName `
--location=$resourceGroupLocaltion `
--output=jsonc

# Create resource group name
Write-Host "Creating AKS cluster $clusterName with resource group $resourceGroupName in region $resourceGroupLocaltion" -ForegroundColor Yellow
az aks create `
--resource-group=$resourceGroupName `
--name=$clusterName `
--node-count=2 `
--kubernetes-version=1.8.1 `
--output=jsonc

# Get credentials for newly created cluster
Write-Host "Getting credentials for cluster $clusterName" -ForegroundColor Yellow
az aks get-credentials `
--resource-group=$resourceGroupName `
--name=$clusterName

Write-Host "Successfully created cluster $clusterName" -ForegroundColor Green