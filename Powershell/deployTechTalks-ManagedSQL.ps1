Param(
    [parameter(Mandatory=$false)]
    [bool]$ProvisionAKSCluster=$false
)


if($ProvisionAKSCluster)
{
    Write-Host "Provisioning AKS cluster with default parameters" -ForegroundColor Cyan
    & ((Split-Path $MyInvocation.InvocationName) + "\initializeAKS.ps1") 
}

Write-Host "Starting deployment of TechTalks application and services" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes-ManagedSQL

Write-Host "Creating namespace"  -ForegroundColor Yellow
kubectl apply --filename 00_ABCNamespace.yml
Write-Host "Namespace created successfully" -ForegroundColor Cyan

Write-Host "Deploying RabbitMQ service" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes-ManagedSQL/RabbitMQ
kubectl apply --recursive --filename . 

Write-Host "RabbitMQ service deployed successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks API service" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes-ManagedSQL/TechTalksAPI
kubectl apply --recursive --filename . 

Write-Host "Tech talks API service deployed successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks web frontend" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes-ManagedSQL/TechTalksWeb
kubectl apply --recursive --filename . 

Write-Host "Tech talks web frontend deployed successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks Processor" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes-ManagedSQL/TechTalksProcessor
kubectl apply --recursive --filename . 

Write-Host "Tech talks Processor deployed successfully" -ForegroundColor Cyan

Write-Host "All the services related to Tech Talks application have been successfully deployed" -ForegroundColor Cyan

Set-Location ~/projects/ABC2018/Powershell