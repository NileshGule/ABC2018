Param(
    [parameter(Mandatory=$false)][string]$resourceGroupName="ABC2018ResourceGroup",
    [parameter(Mandatory=$false)][string]$clusterName="ABC2018AKSCluster"
)

# Browse AKS dashboard
Write-Host "Browse AKS cluster $clusterName" -ForegroundColor Yellow
az aks browse `
--resource-group=$resourceGroupName `
--name=$clusterName