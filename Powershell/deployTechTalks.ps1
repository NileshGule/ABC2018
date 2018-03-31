Param(
    [parameter(Mandatory=$false)][bool]$IsLocalCluster=$true
)

if($IsLocalCluster){

    $minikubeStatus = minikube status

    Write-Host $minikubeStatus -ForegroundColor Magenta

    $IsStopped =$minikubeStatus.Contains("minikube: Stopped")
    
    if($IsStopped)
    {
        Write-Host "Starting local minikube cluster" -ForegroundColor Yellow
        minikube start
    }
}

Write-Host "Starting deployment of TechTalks application and services" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes

Write-Host "Creating namespace"  -ForegroundColor Yellow
kubectl apply --filename 00_ABCNamespace.yml
Write-Host "Namespace created successfully" -ForegroundColor Cyan

Write-Host "Creating storage class" -ForegroundColor Yellow
kubectl apply --filename 01_StorageClass.yml
Write-Host "Storage class created successfully" -ForegroundColor Cyan

Write-Host "Creating Persistant Volume Claim" -ForegroundColor Yellow
kubectl apply --filename 02_PersistantVolumeClaim.yml
Write-Host "Persistant Volume Claim created successfully" -ForegroundColor Cyan

Write-Host "Deploying RabbitMQ service" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes/RabbitMQ
kubectl apply --recursive --filename . 

Write-Host "RabbitMQ service deployed successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks DB service" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes/TechTalksDB
kubectl apply --recursive --filename . 

Write-Host "Tech talks DB service deployed successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks API service" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes/TechTalksAPI
kubectl apply --recursive --filename . 

Write-Host "Tech talks API service deployed successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks web frontend" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes/TechTalksWeb
kubectl apply --recursive --filename . 

Write-Host "Tech talks web frontend deployed successfully" -ForegroundColor Cyan