Param(
    [parameter(Mandatory=$false)]
    [string]$resourceGroupName="ABC2018ResourceGroup"
)

# Delete AKS cluster
Write-Host "Deleting resource group $resourceGroupName" -ForegroundColor Red
time az group delete --name=$resourceGroupName --yes