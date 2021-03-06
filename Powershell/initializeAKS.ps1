Param(
    [parameter(Mandatory=$false)]
    # [string]$subscriptionName="Visual Studio Enterprise",
    [string]$subscriptionName="Microsoft Azure Sponsorship",
    [parameter(Mandatory=$false)]
    [string]$resourceGroupName="ABC2018ResourceGroup",
    [parameter(Mandatory=$false)]
    [string]$resourceGroupLocaltion="South East Asia",
    # [string]$resourceGroupLocaltion="Central US",
    [parameter(Mandatory=$false)]
    [string]$clusterName="ABC2018AKSCluster",
    [parameter(Mandatory=$false)]
    [int16]$workerNodeCount=3,
    [parameter(Mandatory=$false)]
    # [string]$kubernetesVersion="1.8.1"
    [string]$kubernetesVersion="1.9.2"
    
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

# Create AKS cluster
Write-Host "Creating AKS cluster $clusterName with resource group $resourceGroupName in region $resourceGroupLocaltion" -ForegroundColor Yellow
time az aks create `
--resource-group=$resourceGroupName `
--name=$clusterName `
--node-count=$workerNodeCount `
--disable-rbac `
--output=jsonc
# --kubernetes-version=$kubernetesVersion `

# Get credentials for newly created cluster
Write-Host "Getting credentials for cluster $clusterName" -ForegroundColor Yellow
az aks get-credentials `
--resource-group=$resourceGroupName `
--name=$clusterName

Write-Host "Successfully created cluster $clusterName with kubernetes version $kubernetesVersion and $workerNodeCount node(s)" -ForegroundColor Green