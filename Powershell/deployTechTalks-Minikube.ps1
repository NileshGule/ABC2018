
$minikubeStatus = minikube status

Write-Host $minikubeStatus -ForegroundColor Magenta

$IsStopped =$minikubeStatus.Contains("minikube: Stopped")

if($IsStopped)
{
    Write-Host "Starting local minikube cluster" -ForegroundColor Yellow
    minikube start
}

Write-Host "Starting deployment of TechTalks application and services" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Minikube

Write-Host "Creating namespace"  -ForegroundColor Yellow
kubectl apply --filename 00_ABCNamespace.yml
Write-Host "Namespace created successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks DB service" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Minikube/TechTalksDB
kubectl apply --recursive --filename . 

Write-Host "Tech talks DB service deployed successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks API service" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Minikube/TechTalksAPI
kubectl apply --recursive --filename . 

Write-Host "Tech talks API service deployed successfully" -ForegroundColor Cyan

Write-Host "Deploying Tech Talks web frontend" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Minikube/TechTalksWeb
kubectl apply --recursive --filename . 

Write-Host "Tech talks web frontend deployed successfully" -ForegroundColor Cyan

Write-Host "All the services related to Tech Talks application have been successfully deployed" -ForegroundColor Cyan

Set-Location ~/projects/ABC2018/Powershell