Param(
    [parameter(Mandatory=$false)][string]$resourceGroupName="ABC2018ResourceGroup"
)

# Delete AKS cluster
Write-Host "Deleting resource group $ABC2018ResourceGroup" -ForegroundColor Yellow
az group delete --name=$resourceGroupName --yes