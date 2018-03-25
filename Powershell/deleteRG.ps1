Param(
    [parameter(Mandatory=$true)][string]$resourceGroupName
)

# Delete AKS cluster
Write-Host "Deleting resource group..." -ForegroundColor Yellow
az group delete --name=$resourceGroupName --yes --no-wait